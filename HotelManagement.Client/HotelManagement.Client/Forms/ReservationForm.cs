using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class ReservationForm : Form
    {
        private readonly ReservationService _reservationService;
        private readonly CustomerService _customerService;
        private readonly HotelService _hotelService;
        private readonly RoomService _roomService;
        private readonly RoomTypeService _roomTypeService;

        private ReservationModel _reservation;
        private readonly int? _reservationId;
        private bool _isEditMode;

        private List<CustomerModel> _customers;
        private List<HotelModel> _hotels;
        private List<RoomTypeModel> _roomTypes;
        private List<RoomModel> _availableRooms;

        private int? _selectedCustomerId;
        private int? _selectedHotelId;
        private int? _selectedRoomTypeId;
        private int? _selectedRoomId;

        private DateTime _checkInDate;
        private DateTime _checkOutDate;

        public ReservationForm(int? reservationId = null)
        {
            InitializeComponent();

            _reservationService = new ReservationService();
            _customerService = new CustomerService();
            _hotelService = new HotelService();
            _roomService = new RoomService();
            _roomTypeService = new RoomTypeService();

            _reservationId = reservationId;
            _isEditMode = reservationId.HasValue;

            // Inițializează datele de check-in/check-out implicit
            _checkInDate = DateTime.Today;
            _checkOutDate = DateTime.Today.AddDays(1);

            // Setează titlul formularului în funcție de mod
            this.Text = _isEditMode ? "Editare rezervare" : "Adaugă rezervare nouă";
            lblTitle.Text = _isEditMode ? "Editare rezervare" : "Adaugă rezervare nouă";
            btnSave.Text = _isEditMode ? "Salvează modificările" : "Adaugă rezervare";
        }

        private async void ReservationForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Afișează panoul de încărcare
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Încarcă datele pentru dropdown-uri
                await LoadComboBoxData();

                // Setează data de check-in/check-out
                dtpCheckIn.Value = _checkInDate;
                dtpCheckOut.Value = _checkOutDate;

                if (_isEditMode)
                {
                    await LoadReservation();
                }
                else
                {
                    // Inițializează o rezervare nouă
                    _reservation = new ReservationModel
                    {
                        CheckInDate = _checkInDate,
                        CheckOutDate = _checkOutDate,
                        NumberOfGuests = 1,
                        ReservationStatus = "Confirmed"
                    };

                    // Activează grupul de status doar în modul de editare
                    grpStatus.Enabled = false;

                    // Actualizează numărul de nopți și prețul
                    UpdateNightsAndPrice();
                }

                // Afișează panoul principal
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

        private async Task LoadComboBoxData()
        {
            // Încarcă clienții, hotelurile și tipurile de camere pentru dropdown-uri
            _customers = await _customerService.GetAllCustomersAsync();
            _hotels = await _hotelService.GetAllHotelsAsync();
            _roomTypes = await _roomTypeService.GetAllRoomTypesAsync();

            // Populează dropdown pentru clienți
            cmbCustomer.DisplayMember = "FullName";
            cmbCustomer.ValueMember = "Id";
            cmbCustomer.DataSource = new List<CustomerModel>(_customers);

            // Populează dropdown pentru hoteluri
            cmbHotel.DisplayMember = "Name";
            cmbHotel.ValueMember = "Id";
            cmbHotel.DataSource = new List<HotelModel>(_hotels);

            // Populează dropdown pentru tipuri de camere
            cmbRoomType.DisplayMember = "Name";
            cmbRoomType.ValueMember = "Id";
            cmbRoomType.DataSource = new List<RoomTypeModel>(_roomTypes);

            // Inițializează dropdown pentru camere (va fi populat când se selectează hotel și tip cameră)
            cmbRoom.DisplayMember = "RoomNumber";
            cmbRoom.ValueMember = "Id";
        }

        private async Task LoadReservation()
        {
            try
            {
                // Încarcă rezervarea pentru editare
                _reservation = await _reservationService.GetReservationByIdAsync(_reservationId.Value);

                // Populează controalele cu datele rezervării
                _selectedCustomerId = _reservation.CustomerId;
                _selectedHotelId = _reservation.HotelId;
                _selectedRoomId = _reservation.RoomId;

                // Selectează clientul potrivit
                SelectComboBoxItem(cmbCustomer, _selectedCustomerId);

                // Selectează hotelul potrivit
                SelectComboBoxItem(cmbHotel, _selectedHotelId);

                // Încarcă camerele disponibile și selectează camera potrivită
                await LoadAvailableRooms();
                SelectComboBoxItem(cmbRoom, _selectedRoomId);

                // Setează data de check-in/check-out
                _checkInDate = _reservation.CheckInDate;
                _checkOutDate = _reservation.CheckOutDate;
                dtpCheckIn.Value = _checkInDate;
                dtpCheckOut.Value = _checkOutDate;

                // Setează numărul de persoane
                numGuests.Value = _reservation.NumberOfGuests;

                // Setează notele
                txtNotes.Text = _reservation.Notes;

                // Setează statusul rezervării
                switch (_reservation.ReservationStatus)
                {
                    case "Confirmed":
                        radConfirmed.Checked = true;
                        break;
                    case "CheckedIn":
                        radCheckedIn.Checked = true;
                        break;
                    case "Completed":
                        radCompleted.Checked = true;
                        break;
                    case "Cancelled":
                        radCancelled.Checked = true;
                        break;
                }

                // Actualizează informațiile despre nopți și preț
                UpdateNightsAndPrice();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea rezervării: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void SelectComboBoxItem(ComboBox comboBox, int? itemId)
        {
            if (itemId.HasValue && comboBox.Items.Count > 0)
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    dynamic item = comboBox.Items[i];
                    if (item.Id == itemId.Value)
                    {
                        comboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async Task LoadAvailableRooms()
        {
            if (!_selectedHotelId.HasValue) return;

            try
            {
                LoadingPanel.Visible = true;
                lblAvailableRooms.Text = "Se încarcă...";
                cmbRoom.DataSource = null;
                cmbRoom.Refresh();

                // Obține camerele disponibile
                _availableRooms = await _roomService.GetAvailableRoomsByHotelAsync(
                    _selectedHotelId.Value,
                    _selectedRoomTypeId,
                    _checkInDate,
                    _checkOutDate);

                // În modul editare, adaugă camera curentă
                if (_isEditMode && _selectedRoomId.HasValue)
                {
                    var currentRoom = _availableRooms.FirstOrDefault(r => r.Id == _selectedRoomId.Value);
                    if (currentRoom == null)
                    {
                        var allRooms = await _roomService.GetRoomsByHotelAsync(_selectedHotelId.Value);
                        currentRoom = allRooms?.FirstOrDefault(r => r.Id == _selectedRoomId.Value);
                        if (currentRoom != null)
                        {
                            _availableRooms.Insert(0, currentRoom);
                        }
                    }
                }

                // Actualizează UI
                cmbRoom.DataSource = _availableRooms;
                lblAvailableRooms.Text = $"Camere disponibile: {_availableRooms.Count}";

                if (_availableRooms.Count == 0)
                {
                    MessageBox.Show("Nu există camere disponibile conform criteriilor:\n" +
                                  $"• Hotel: {cmbHotel.SelectedItem}\n" +
                                  $"• Tip cameră: {cmbRoomType.SelectedItem}\n" +
                                  $"• Perioadă: {_checkInDate:d} - {_checkOutDate:d}",
                                  "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                LoadingPanel.Visible = false;
            }
        }

        private void UpdateNightsAndPrice()
        {
            // Calculează numărul de nopți
            int nights = Math.Max(1, (_checkOutDate - _checkInDate).Days);
            lblNights.Text = $"Nopți: {nights}";

            // Calculează prețul (dacă o cameră este selectată)
            decimal price = 0;
            if (cmbRoom.SelectedItem != null)
            {
                var selectedRoom = (RoomModel)cmbRoom.SelectedItem;
                price = selectedRoom.Price * nights;
                lblPrice.Text = $"Preț: {price:C2}";
                lblTotalPrice.Text = $"Total: {price:C2}";
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            try
            {
                // Obține valorile din controale
                _reservation.CustomerId = ((CustomerModel)cmbCustomer.SelectedItem).Id;
                _reservation.RoomId = ((RoomModel)cmbRoom.SelectedItem).Id;
                _reservation.CheckInDate = dtpCheckIn.Value;
                _reservation.CheckOutDate = dtpCheckOut.Value;
                _reservation.NumberOfGuests = (int)numGuests.Value;
                _reservation.Notes = txtNotes.Text;

                // Setează statusul rezervării
                if (radConfirmed.Checked)
                    _reservation.ReservationStatus = "Confirmed";
                else if (radCheckedIn.Checked)
                    _reservation.ReservationStatus = "CheckedIn";
                else if (radCompleted.Checked)
                    _reservation.ReservationStatus = "Completed";
                else if (radCancelled.Checked)
                    _reservation.ReservationStatus = "Cancelled";

                // Salvează rezervarea
                if (_isEditMode)
                {
                    await _reservationService.UpdateReservationAsync(_reservation);
                    MessageBox.Show("Rezervare actualizată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await _reservationService.CreateReservationAsync(_reservation);
                    MessageBox.Show("Rezervare adăugată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Revine la lista de rezervări
                ((MainForm)ParentForm).OpenChildForm(new ReservationsListForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la salvarea rezervării: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            // Verifică dacă toate câmpurile obligatorii sunt completate
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Trebuie să selectați un client!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCustomer.Focus();
                return false;
            }

            if (cmbHotel.SelectedIndex == -1)
            {
                MessageBox.Show("Trebuie să selectați un hotel!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbHotel.Focus();
                return false;
            }

            if (cmbRoom.SelectedIndex == -1)
            {
                MessageBox.Show("Trebuie să selectați o cameră!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRoom.Focus();
                return false;
            }

            // Verifică datele de check-in/check-out
            if (dtpCheckIn.Value.Date >= dtpCheckOut.Value.Date)
            {
                MessageBox.Show("Data de check-out trebuie să fie după data de check-in!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpCheckOut.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Revine la lista de rezervări
            ((MainForm)ParentForm).OpenChildForm(new ReservationsListForm());
        }

        private async void cmbHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHotel.SelectedItem != null)
            {
                _selectedHotelId = ((HotelModel)cmbHotel.SelectedItem).Id;
                await LoadAvailableRooms();
            }
        }

        private async void cmbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoomType.SelectedItem != null)
            {
                _selectedRoomTypeId = ((RoomTypeModel)cmbRoomType.SelectedItem).Id;
                await LoadAvailableRooms();
            }
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoom.SelectedItem != null)
            {
                _selectedRoomId = ((RoomModel)cmbRoom.SelectedItem).Id;
                UpdateNightsAndPrice();
            }
        }

        private async void dtpCheckIn_ValueChanged(object sender, EventArgs e)
        {
            _checkInDate = dtpCheckIn.Value.Date;

            // Asigură-te că data de check-out este după data de check-in
            if (_checkInDate >= dtpCheckOut.Value.Date)
            {
                dtpCheckOut.Value = _checkInDate.AddDays(1);
            }

            _checkOutDate = dtpCheckOut.Value.Date;
            UpdateNightsAndPrice();

            // Reîncarcă camerele disponibile pentru noile date
            await LoadAvailableRooms();
        }

        private async void dtpCheckOut_ValueChanged(object sender, EventArgs e)
        {
            _checkOutDate = dtpCheckOut.Value.Date;

            // Asigură-te că data de check-out este după data de check-in
            if (_checkOutDate <= dtpCheckIn.Value.Date)
            {
                _checkOutDate = dtpCheckIn.Value.Date.AddDays(1);
                dtpCheckOut.Value = _checkOutDate;
            }

            UpdateNightsAndPrice();

            // Reîncarcă camerele disponibile pentru noile date
            await LoadAvailableRooms();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            // Deschide formularul pentru adăugarea unui client nou
            ((MainForm)ParentForm).OpenChildForm(new CustomerForm());
        }
    }
}