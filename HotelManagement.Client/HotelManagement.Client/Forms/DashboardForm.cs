using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly HotelService _hotelService;
        private readonly ReservationService _reservationService;
        private readonly CustomerService _customerService;

        private int _totalRooms;
        private int _occupiedRooms;
        private int _availableRooms;
        private int _customersCount;
        private int _reservationsToday;
        private decimal _revenueToday;

        public DashboardForm()
        {
            InitializeComponent();

            _hotelService = new HotelService();
            _reservationService = new ReservationService();
            _customerService = new CustomerService();

            this.Text = "Dashboard";
        }

        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            await LoadDashboardData();
        }

        private async Task LoadDashboardData()
        {
            try
            {
                // Activează indicatorul de încărcare
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Încarcă datele pentru dashboard
                await Task.WhenAll(
                    LoadRoomsData(),
                    LoadCustomersData(),
                    LoadReservationsData()
                );

                // Actualizează controalele UI
                UpdateDashboardUI();

                // Dezactivează indicatorul de încărcare
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea datelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private async Task LoadRoomsData()
        {
            // Încarcă toate hotelurile pentru a obține numărul total de camere
            List<HotelModel> hotels = await _hotelService.GetAllHotelsAsync();

            _totalRooms = 0;
            _availableRooms = 0;

            foreach (var hotel in hotels)
            {
                _totalRooms += hotel.TotalRooms;
                _availableRooms += hotel.AvailableRooms;
            }

            _occupiedRooms = _totalRooms - _availableRooms;
        }

        private async Task LoadCustomersData()
        {
            // Încarcă toți clienții pentru a obține numărul total
            List<CustomerModel> customers = await _customerService.GetAllCustomersAsync();
            _customersCount = customers.Count;
        }

        private async Task LoadReservationsData()
        {
            // Încarcă check-in-urile pentru azi pentru a obține numărul de rezervări pentru ziua curentă
            List<ReservationModel> checkIns = await _reservationService.GetCheckInsForTodayAsync();
            _reservationsToday = checkIns.Count;

            // Calculează venitul pentru ziua curentă (suma prețurilor pentru check-in-urile din ziua curentă)
            _revenueToday = 0;
            foreach (var reservation in checkIns)
            {
                _revenueToday += reservation.TotalPrice;
            }
        }

        private void UpdateDashboardUI()
        {
            // Actualizează etichetele cu date
            lblTotalRooms.Text = _totalRooms.ToString();
            lblOccupiedRooms.Text = _occupiedRooms.ToString();
            lblAvailableRooms.Text = _availableRooms.ToString();
            lblCustomersCount.Text = _customersCount.ToString();
            lblReservationsToday.Text = _reservationsToday.ToString();
            lblRevenueToday.Text = $"{_revenueToday:N2} RON";

            // Actualizează ora și data curentă
            lblCurrentDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");

            // Actualizează procentul de ocupare și diagrama
            float occupancyRate = _totalRooms > 0 ? (float)_occupiedRooms / _totalRooms * 100 : 0;
            lblOccupancyRate.Text = $"{occupancyRate:N1}%";

            // Setează culori pentru diagrama de ocupare
            if (occupancyRate < 30)
                lblOccupancyRate.ForeColor = Color.DarkGreen;
            else if (occupancyRate < 70)
                lblOccupancyRate.ForeColor = Color.DarkOrange;
            else
                lblOccupancyRate.ForeColor = Color.DarkRed;

            // Actualizează graficul de ocupare (reprezentat printr-un ProgressBar)
            prgOccupancyRate.Value = (int)occupancyRate;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void btnCheckInsToday_Click(object sender, EventArgs e)
        {
            // Deschide formularul pentru check-in-urile de azi
            ((MainForm)ParentForm).OpenChildForm(new CheckInsForm());
        }

        private void btnCheckOutsToday_Click(object sender, EventArgs e)
        {
            // Deschide formularul pentru check-out-urile de azi
            ((MainForm)ParentForm).OpenChildForm(new CheckOutsForm());
        }

        private void btnNewReservation_Click(object sender, EventArgs e)
        {
            // Deschide formularul pentru adăugarea unei noi rezervări
            ((MainForm)ParentForm).OpenChildForm(new ReservationForm());
        }
    }
}