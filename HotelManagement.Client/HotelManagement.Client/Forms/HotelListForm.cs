using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class HotelsListForm : Form
    {
        private readonly HotelService _hotelService;
        private List<HotelModel> _hotels;

        public HotelsListForm()
        {
            InitializeComponent();

            _hotelService = new HotelService();

            this.Text = "Lista hoteluri";

            // Setări pentru DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Configurare DataGridView
            dgvHotels.AutoGenerateColumns = false;
            dgvHotels.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHotels.MultiSelect = false;
            dgvHotels.ReadOnly = true;
            dgvHotels.AllowUserToAddRows = false;
            dgvHotels.AllowUserToDeleteRows = false;
            dgvHotels.AllowUserToOrderColumns = true;
            dgvHotels.AllowUserToResizeRows = false;

            // Stilizare
            dgvHotels.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvHotels.BorderStyle = BorderStyle.None;
            dgvHotels.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHotels.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHotels.EnableHeadersVisualStyles = false;

            dgvHotels.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvHotels.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHotels.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvHotels.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvHotels.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvHotels.ColumnHeadersHeight = 40;

            dgvHotels.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvHotels.DefaultCellStyle.ForeColor = Color.White;
            dgvHotels.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvHotels.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvHotels.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvHotels.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvHotels.RowHeadersVisible = false;
            dgvHotels.RowTemplate.Height = 35;

            // Definire coloane
            dgvHotels.Columns.Clear();

            dgvHotels.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvHotels.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Nume",
                Width = 200
            });

            dgvHotels.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "City",
                DataPropertyName = "City",
                HeaderText = "Oraș",
                Width = 150
            });

            dgvHotels.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Country",
                DataPropertyName = "Country",
                HeaderText = "Țară",
                Width = 100
            });

            dgvHotels.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stars",
                DataPropertyName = "Stars",
                HeaderText = "Stele",
                Width = 60
            });

            dgvHotels.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalRooms",
                DataPropertyName = "TotalRooms",
                HeaderText = "Camere Total",
                Width = 100
            });

            dgvHotels.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AvailableRooms",
                DataPropertyName = "AvailableRooms",
                HeaderText = "Camere Disponibile",
                Width = 150
            });

            dgvHotels.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                DataPropertyName = "IsActive",
                HeaderText = "Activ",
                Width = 60
            });

            // Adaugă coloane pentru butoane/acțiuni
            var viewRoomsColumn = new DataGridViewButtonColumn
            {
                Name = "ViewRooms",
                HeaderText = "Camere",
                Text = "Vezi Camere",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            dgvHotels.Columns.Add(viewRoomsColumn);

            var editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "Editare",
                Text = "Editează",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvHotels.Columns.Add(editColumn);

            var deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Ștergere",
                Text = "Șterge",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvHotels.Columns.Add(deleteColumn);
        }

        private async void HotelsListForm_Load(object sender, EventArgs e)
        {
            await LoadHotels();
        }

        private async Task LoadHotels()
        {
            try
            {
                // Afișează panoul de încărcare
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Încarcă lista de hoteluri
                _hotels = await _hotelService.GetAllHotelsAsync();

                // Leagă datele la DataGridView
                dgvHotels.DataSource = null;
                dgvHotels.DataSource = _hotels;

                // Afișează panoul principal
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                // Actualizează eticheta cu numărul de înregistrări
                lblRecordCount.Text = $"Total hoteluri: {_hotels.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea hotelurilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void dgvHotels_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifică dacă s-a făcut clic pe un buton (coloană de acțiune)
            if (e.RowIndex >= 0)
            {
                var selectedHotel = _hotels[e.RowIndex];

                // Verifică ce coloană de acțiune a fost apăsată
                if (e.ColumnIndex == dgvHotels.Columns["ViewRooms"].Index)
                {
                    // Deschide formularul de camere pentru hotelul selectat
                    var roomsForm = new RoomsListForm(selectedHotel.Id, selectedHotel.Name);
                    ((MainForm)ParentForm).OpenChildForm(roomsForm);
                }
                else if (e.ColumnIndex == dgvHotels.Columns["Edit"].Index)
                {
                    // Deschide formularul de editare pentru hotelul selectat
                    var hotelForm = new HotelForm(selectedHotel.Id);
                    ((MainForm)ParentForm).OpenChildForm(hotelForm);
                }
                else if (e.ColumnIndex == dgvHotels.Columns["Delete"].Index)
                {
                    // Confirmă ștergerea
                    if (MessageBox.Show($"Sigur doriți să ștergeți hotelul '{selectedHotel.Name}'?",
                        "Confirmare ștergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteHotel(selectedHotel.Id);
                    }
                }
            }
        }

        private async void DeleteHotel(int hotelId)
        {
            try
            {
                // Șterge hotelul
                await _hotelService.DeleteHotelAsync(hotelId);

                // Reîncarcă lista de hoteluri
                await LoadHotels();

                MessageBox.Show("Hotel șters cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la ștergerea hotelului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddHotel_Click(object sender, EventArgs e)
        {
            // Deschide formularul pentru adăugarea unui hotel nou
            var hotelForm = new HotelForm();
            ((MainForm)ParentForm).OpenChildForm(hotelForm);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadHotels();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterHotels();
        }

        private void FilterHotels()
        {
            if (_hotels == null) return;

            string searchText = txtSearch.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Dacă căutarea este goală, afișează toate hotelurile
                dgvHotels.DataSource = _hotels;
                lblRecordCount.Text = $"Total hoteluri: {_hotels.Count}";
                return;
            }

            // Filtrează hotelurile după text
            var filteredHotels = _hotels.FindAll(h =>
                h.Name.ToLower().Contains(searchText) ||
                h.City.ToLower().Contains(searchText) ||
                h.Country.ToLower().Contains(searchText));

            // Actualizează DataGridView și eticheta
            dgvHotels.DataSource = null;
            dgvHotels.DataSource = filteredHotels;
            lblRecordCount.Text = $"Hoteluri găsite: {filteredHotels.Count} din {_hotels.Count}";
        }

        private void cbActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            FilterHotels();
        }
    }
}