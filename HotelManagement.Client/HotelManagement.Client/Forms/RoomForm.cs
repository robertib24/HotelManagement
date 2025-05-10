using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class RoomForm : Form
    {
        private readonly RoomService _roomService;
        private readonly HotelService _hotelService;
        private readonly RoomTypeService _roomTypeService;
        private RoomModel _room;
        private readonly int? _roomId;
        private readonly int? _preselectedHotelId;
        private bool _isEditMode;
        private List<HotelModel> _hotels;
        private List<RoomTypeModel> _roomTypes;

        public RoomForm(int? roomId = null, int? hotelId = null)
        {
            InitializeComponent();

            _roomService = new RoomService();
            _hotelService = new HotelService();
            _roomTypeService = new RoomTypeService();
            _roomId = roomId;
            _preselectedHotelId = hotelId;
            _isEditMode = roomId.HasValue;

            // Setează titlul formularului în funcție de mod
            this.Text = _isEditMode ? "Editare cameră" : "Adaugă cameră nouă";
            lblTitle.Text = _isEditMode ? "Editare cameră" : "Adaugă cameră nouă";
            btnSave.Text = _isEditMode ? "Salvează modificările" : "Adaugă cameră";
        }

        private async void RoomForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Încarcă datele pentru dropdown-uri
                await LoadComboBoxData();

                if (_isEditMode)
                {
                    await LoadRoom();
                }
                else
                {
                    // Inițializează o cameră nouă
                    _room = new RoomModel
                    {
                        IsClean = true,
                        IsOccupied = false,
                        NeedsRepair = false,
                        Floor = 1
                    };

                    // Presetează hotelul dacă a fost specificat
                    if (_preselectedHotelId.HasValue)
                    {
                        for (int i = 0; i < cmbHotel.Items.Count; i++)
                        {
                            var hotel = (HotelModel)cmbHotel.Items[i];
                            if (hotel.Id == _preselectedHotelId.Value)
                            {
                                cmbHotel.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    // Activează grupul de status doar în modul de editare
                    grpStatus.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea datelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadComboBoxData()
        {
            // Afișează panoul de încărcare
            LoadingPanel.Visible = true;
            MainPanel.Visible = false;

            // Încarcă hotelurile și tipurile de camere pentru dropdown-uri
            _hotels = await _hotelService.GetAllHotelsAsync();
            _roomTypes = await _roomTypeService.GetAllRoomTypesAsync();

            // Populează dropdown pentru hoteluri
            cmbHotel.DisplayMember = "Name";
            cmbHotel.ValueMember = "Id";
            cmbHotel.DataSource = new List<HotelModel>(_hotels);

            // Populează dropdown pentru tipuri de camere
            cmbRoomType.DisplayMember = "Name";
            cmbRoomType.ValueMember = "Id";
            cmbRoomType.DataSource = new List<RoomTypeModel>(_roomTypes);

            // Afișează panoul principal
            LoadingPanel.Visible = false;
            MainPanel.Visible = true;
        }

        private async Task LoadRoom()
        {
            try
            {
                // Afișează panoul de încărcare
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Încarcă camera pentru editare
                _room = await _roomService.GetRoomByIdAsync(_roomId.Value);

                // Populează controalele cu datele camerei
                txtRoomNumber.Text = _room.RoomNumber;

                // Selectează hotelul potrivit
                for (int i = 0; i < cmbHotel.Items.Count; i++)
                {
                    var hotel = (HotelModel)cmbHotel.Items[i];
                    if (hotel.Id == _room.HotelId)
                    {
                        cmbHotel.SelectedIndex = i;
                        break;
                    }
                }

                // Selectează tipul de cameră potrivit
                for (int i = 0; i < cmbRoomType.Items.Count; i++)
                {
                    var roomType = (RoomTypeModel)cmbRoomType.Items[i];
                    if (roomType.Id == _room.RoomTypeId)
                    {
                        cmbRoomType.SelectedIndex = i;
                        break;
                    }
                }

                numFloor.Value = _room.Floor;
                txtNotes.Text = _room.Notes;

                // Setează controalele de status
                chkIsClean.Checked = _room.IsClean;
                chkIsOccupied.Checked = _room.IsOccupied;
                chkNeedsRepair.Checked = _room.NeedsRepair;

                // Afișează panoul principal
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea camerei: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
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
                // Actualizează obiectul cameră cu valorile din controale
                _room.RoomNumber = txtRoomNumber.Text;
                _room.HotelId = (int)((HotelModel)cmbHotel.SelectedItem).Id;
                _room.RoomTypeId = (int)((RoomTypeModel)cmbRoomType.SelectedItem).Id;
                _room.Floor = (int)numFloor.Value;
                _room.Notes = txtNotes.Text;

                if (_isEditMode)
                {
                    // Actualizează statusul doar în modul de editare
                    _room.IsClean = chkIsClean.Checked;
                    _room.IsOccupied = chkIsOccupied.Checked;
                    _room.NeedsRepair = chkNeedsRepair.Checked;

                    // Salvează camera
                    await _roomService.UpdateRoomAsync(_room);
                    MessageBox.Show("Cameră actualizată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Pentru cameră nouă, întotdeauna setează statusul inițial (curată, neocupată, nu necesită reparații)
                    _room.IsClean = true;
                    _room.IsOccupied = false;
                    _room.NeedsRepair = false;

                    // Adaugă camera
                    await _roomService.CreateRoomAsync(_room);
                    MessageBox.Show("Cameră adăugată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Revine la lista de camere
                // FIX: Înlocuirea operatorului ?? cu logică condițională explicită
                int? hotelId;
                if (_preselectedHotelId.HasValue)
                {
                    hotelId = _preselectedHotelId.Value;
                }
                else if (_isEditMode)
                {
                    hotelId = _room.HotelId;
                }
                else
                {
                    hotelId = null;
                }

                // FIX: Înlocuirea condiției ternare cu verificare explicită
                string hotelName = null;
                if (hotelId.HasValue)
                {
                    HotelModel selectedHotel = (HotelModel)cmbHotel.SelectedItem;
                    if (selectedHotel != null)
                    {
                        hotelName = selectedHotel.Name;
                    }
                }

                ((MainForm)ParentForm).OpenChildForm(new RoomsListForm(hotelId, hotelName));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la salvarea camerei: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            // Verifică dacă toate câmpurile obligatorii sunt completate
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
            {
                MessageBox.Show("Numărul camerei este obligatoriu!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoomNumber.Focus();
                return false;
            }

            if (cmbHotel.SelectedIndex == -1)
            {
                MessageBox.Show("Trebuie să selectați un hotel!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbHotel.Focus();
                return false;
            }

            if (cmbRoomType.SelectedIndex == -1)
            {
                MessageBox.Show("Trebuie să selectați un tip de cameră!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRoomType.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Revine la lista de camere
            // FIX: Înlocuirea operatorului ?? cu logică condițională explicită
            int? hotelId;
            if (_preselectedHotelId.HasValue)
            {
                hotelId = _preselectedHotelId.Value;
            }
            else if (_isEditMode)
            {
                hotelId = _room.HotelId;
            }
            else
            {
                hotelId = null;
            }

            // FIX: Înlocuirea condiției ternare cu verificare explicită
            string hotelName = null;
            if (hotelId.HasValue && _isEditMode && _room != null)
            {
                hotelName = _room.HotelName;
            }

            ((MainForm)ParentForm).OpenChildForm(new RoomsListForm(hotelId, hotelName));
        }

        private void cmbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoomType.SelectedItem != null)
            {
                var roomType = (RoomTypeModel)cmbRoomType.SelectedItem;
                lblPrice.Text = $"Preț: {roomType.BasePrice:C2}";
            }
        }
    }
}