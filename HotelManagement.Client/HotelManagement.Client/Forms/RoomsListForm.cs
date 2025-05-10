using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class RoomsListForm : Form
    {
        private readonly RoomService _roomService;
        private readonly RoomTypeService _roomTypeService;
        private readonly int? _hotelId;
        private readonly string _hotelName;
        private List<RoomModel> _rooms;
        private List<RoomTypeModel> _roomTypes;

        public RoomsListForm(int? hotelId = null, string hotelName = null)
        {
            InitializeComponent();

            _roomService = new RoomService();
            _roomTypeService = new RoomTypeService();
            _hotelId = hotelId;
            _hotelName = hotelName;

            this.Text = _hotelId.HasValue ? $"Camere - {_hotelName}" : "Lista camere";
            lblTitle.Text = _hotelId.HasValue ? $"Camere pentru hotelul: {_hotelName}" : "Toate camerele";

            // Setări pentru DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Configurare DataGridView
            dgvRooms.AutoGenerateColumns = false;
            dgvRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRooms.MultiSelect = false;
            dgvRooms.ReadOnly = true;
            dgvRooms.AllowUserToAddRows = false;
            dgvRooms.AllowUserToDeleteRows = false;
            dgvRooms.AllowUserToOrderColumns = true;
            dgvRooms.AllowUserToResizeRows = false;

            // Stilizare
            dgvRooms.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvRooms.BorderStyle = BorderStyle.None;
            dgvRooms.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRooms.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRooms.EnableHeadersVisualStyles = false;

            dgvRooms.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvRooms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRooms.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvRooms.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRooms.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvRooms.ColumnHeadersHeight = 40;

            dgvRooms.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvRooms.DefaultCellStyle.ForeColor = Color.White;
            dgvRooms.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvRooms.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvRooms.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvRooms.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvRooms.RowHeadersVisible = false;
            dgvRooms.RowTemplate.Height = 35;

            // Definire coloane
            dgvRooms.Columns.Clear();

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RoomNumber",
                DataPropertyName = "RoomNumber",
                HeaderText = "Număr cameră",
                Width = 120
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HotelName",
                DataPropertyName = "HotelName",
                HeaderText = "Hotel",
                Width = 180
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RoomTypeName",
                DataPropertyName = "RoomTypeName",
                HeaderText = "Tip cameră",
                Width = 120
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Floor",
                DataPropertyName = "Floor",
                HeaderText = "Etaj",
                Width = 60
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Price",
                DataPropertyName = "Price",
                HeaderText = "Preț",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 100
            });

            // Adaugă coloane pentru butoane/acțiuni
            var editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "Editare",
                Text = "Editează",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvRooms.Columns.Add(editColumn);

            var deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Ștergere",
                Text = "Șterge",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvRooms.Columns.Add(deleteColumn);
        }

        private async void RoomsListForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Încarcă datele pentru dropdown-uri
                await LoadComboBoxData();

                // Încarcă camerele
                await LoadRooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea datelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadComboBoxData()
        {
            // Încarcă tipurile de camere pentru filtrare
            _roomTypes = await _roomTypeService.GetAllRoomTypesAsync();

            // Populează dropdown pentru tipuri de camere
            cmbRoomType.Items.Clear();
            cmbRoomType.Items.Add("Toate tipurile");

            foreach (var roomType in _roomTypes)
            {
                cmbRoomType.Items.Add(roomType.Name);
            }

            cmbRoomType.SelectedIndex = 0;

            // Populează dropdown pentru status
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Toate statusurile");
            cmbStatus.Items.Add("Disponibilă");
            cmbStatus.Items.Add("Ocupată");
            cmbStatus.Items.Add("Necesită curățenie");
            cmbStatus.Items.Add("Necesită reparații");

            cmbStatus.SelectedIndex = 0;
        }

        private async Task LoadRooms()
        {
            try
            {
                // Afișează panoul de încărcare
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Încarcă lista de camere
                if (_hotelId.HasValue)
                {
                    // Încarcă doar camerele pentru hotelul specificat
                    _rooms = await _roomService.GetRoomsByHotelAsync(_hotelId.Value);
                }
                else
                {
                    // Încarcă toate camerele
                    _rooms = await _roomService.GetAllRoomsAsync();
                }

                // Aplică filtrele
                ApplyFilters();

                // Afișează panoul principal
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea camerelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void ApplyFilters()
        {
            if (_rooms == null) return;

            // Începe cu toate camerele disponibile
            var filteredRooms = new List<RoomModel>(_rooms);

            // Aplică filtrul pentru căutare text
            string searchText = txtSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredRooms = filteredRooms.FindAll(r =>
                    r.RoomNumber.ToLower().Contains(searchText) ||
                    r.HotelName.ToLower().Contains(searchText) ||
                    r.RoomTypeName.ToLower().Contains(searchText));
            }

            // Aplică filtrul pentru tipul de cameră
            if (cmbRoomType.SelectedIndex > 0)
            {
                string selectedType = cmbRoomType.SelectedItem.ToString();
                filteredRooms = filteredRooms.FindAll(r => r.RoomTypeName == selectedType);
            }

            // Aplică filtrul pentru status
            if (cmbStatus.SelectedIndex > 0)
            {
                string selectedStatus = cmbStatus.SelectedItem.ToString();
                filteredRooms = filteredRooms.FindAll(r => r.Status == selectedStatus);
            }

            // Actualizează DataGridView și eticheta
            dgvRooms.DataSource = null;
            dgvRooms.DataSource = filteredRooms;
            lblRecordCount.Text = $"Camere găsite: {filteredRooms.Count}" + (_rooms.Count != filteredRooms.Count ? $" din {_rooms.Count}" : "");
        }

        private void dgvRooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifică dacă s-a făcut clic pe un buton (coloană de acțiune)
            if (e.RowIndex >= 0)
            {
                var rooms = dgvRooms.DataSource as List<RoomModel>;
                if (rooms == null || e.RowIndex >= rooms.Count) return;

                var selectedRoom = rooms[e.RowIndex];

                // Verifică ce coloană de acțiune a fost apăsată
                if (e.ColumnIndex == dgvRooms.Columns["Edit"].Index)
                {
                    // Deschide formularul de editare pentru camera selectată
                    var roomForm = new RoomForm(selectedRoom.Id);
                    ((MainForm)ParentForm).OpenChildForm(roomForm);
                }
                else if (e.ColumnIndex == dgvRooms.Columns["Delete"].Index)
                {
                    // Confirmă ștergerea
                    if (MessageBox.Show($"Sigur doriți să ștergeți camera '{selectedRoom.RoomNumber}'?",
                        "Confirmare ștergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteRoom(selectedRoom.Id);
                    }
                }
            }
        }

        private async void DeleteRoom(int roomId)
        {
            try
            {
                // Șterge camera
                await _roomService.DeleteRoomAsync(roomId);

                // Reîncarcă lista de camere
                await LoadRooms();

                MessageBox.Show("Cameră ștearsă cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la ștergerea camerei: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            // Deschide formularul pentru adăugarea unei camere noi
            var roomForm = new RoomForm(null, _hotelId);
            ((MainForm)ParentForm).OpenChildForm(roomForm);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadRooms();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Revine la lista de hoteluri
            ((MainForm)ParentForm).OpenChildForm(new HotelsListForm());
        }
    }
}