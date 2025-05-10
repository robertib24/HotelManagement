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

            // DPI settings for better scaling
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;

            _hotelService = new HotelService();
            _reservationService = new ReservationService();
            _customerService = new CustomerService();

            this.Text = "Dashboard";
        }

        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            // Suspend layout to prevent flickering
            this.SuspendLayout();

            // Initially hide the main panel
            MainPanel.Visible = false;
            LoadingPanel.Visible = true;

            await LoadDashboardData();

            // Resume layout
            this.ResumeLayout(false);
        }

        private async Task LoadDashboardData()
        {
            try
            {
                // Load data for dashboard
                await Task.WhenAll(
                    LoadRoomsData(),
                    LoadCustomersData(),
                    LoadReservationsData()
                );

                // Update UI controls
                UpdateDashboardUI();

                // Show main panel
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
            // Load hotel data
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
            // Load customer data
            List<CustomerModel> customers = await _customerService.GetAllCustomersAsync();
            _customersCount = customers.Count;
        }

        private async Task LoadReservationsData()
        {
            // Load reservation data
            List<ReservationModel> checkIns = await _reservationService.GetCheckInsForTodayAsync();
            _reservationsToday = checkIns.Count;

            // Calculate revenue
            _revenueToday = 0;
            foreach (var reservation in checkIns)
            {
                _revenueToday += reservation.TotalPrice;
            }
        }

        private void UpdateDashboardUI()
        {
            // Update labels with data
            lblTotalRooms.Text = _totalRooms.ToString();
            lblOccupiedRooms.Text = _occupiedRooms.ToString();
            lblAvailableRooms.Text = _availableRooms.ToString();
            lblCustomersCount.Text = _customersCount.ToString();
            lblReservationsToday.Text = _reservationsToday.ToString();
            lblRevenueToday.Text = $"{_revenueToday:N2} RON";

            // Update current date/time
            lblCurrentDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");

            // Update occupancy rate and progress bar
            float occupancyRate = _totalRooms > 0 ? (float)_occupiedRooms / _totalRooms * 100 : 0;
            lblOccupancyRate.Text = $"{occupancyRate:N1}%";

            // Ensure occupancy rate doesn't exceed 100 for progress bar
            prgOccupancyRate.Value = Math.Min((int)occupancyRate, 100);

            // Set colors based on occupancy rate
            if (occupancyRate < 30)
                lblOccupancyRate.ForeColor = Color.DarkGreen;
            else if (occupancyRate < 70)
                lblOccupancyRate.ForeColor = Color.DarkOrange;
            else
                lblOccupancyRate.ForeColor = Color.DarkRed;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void btnNewReservation_Click(object sender, EventArgs e)
        {
            // Open reservation form
            ((MainForm)ParentForm).OpenChildForm(new ReservationForm());
        }

        private void btnCheckInsToday_Click(object sender, EventArgs e)
        {
            // Open check-in form
            ((MainForm)ParentForm).OpenChildForm(new CheckInsForm());
        }

        private void btnCheckOutsToday_Click(object sender, EventArgs e)
        {
            // Open check-out form
            ((MainForm)ParentForm).OpenChildForm(new CheckOutsForm());
        }
    }
}