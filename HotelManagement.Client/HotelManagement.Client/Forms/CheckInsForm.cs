using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class CheckInsForm : Form
    {
        private readonly ReservationService _reservationService;
        private List<ReservationModel> _checkIns;

        public CheckInsForm()
        {
            InitializeComponent();

            _reservationService = new ReservationService();

            this.Text = "Check-In Astăzi";

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvCheckIns.AutoGenerateColumns = false;
            dgvCheckIns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCheckIns.MultiSelect = false;
            dgvCheckIns.ReadOnly = true;
            dgvCheckIns.AllowUserToAddRows = false;
            dgvCheckIns.AllowUserToDeleteRows = false;
            dgvCheckIns.AllowUserToOrderColumns = true;
            dgvCheckIns.AllowUserToResizeRows = false;

            dgvCheckIns.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvCheckIns.BorderStyle = BorderStyle.None;
            dgvCheckIns.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCheckIns.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCheckIns.EnableHeadersVisualStyles = false;

            dgvCheckIns.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvCheckIns.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCheckIns.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvCheckIns.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCheckIns.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCheckIns.ColumnHeadersHeight = 40;

            dgvCheckIns.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvCheckIns.DefaultCellStyle.ForeColor = Color.White;
            dgvCheckIns.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvCheckIns.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCheckIns.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvCheckIns.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvCheckIns.RowHeadersVisible = false;
            dgvCheckIns.RowTemplate.Height = 35;

            dgvCheckIns.Columns.Clear();

            dgvCheckIns.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvCheckIns.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                DataPropertyName = "CustomerName",
                HeaderText = "Client",
                Width = 180
            });

            dgvCheckIns.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HotelName",
                DataPropertyName = "HotelName",
                HeaderText = "Hotel",
                Width = 150
            });

            dgvCheckIns.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RoomNumber",
                DataPropertyName = "RoomNumber",
                HeaderText = "Cameră",
                Width = 80
            });

            dgvCheckIns.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CheckInDate",
                DataPropertyName = "CheckInDate",
                HeaderText = "Data Check-In",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy HH:mm"
                }
            });

            dgvCheckIns.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CheckOutDate",
                DataPropertyName = "CheckOutDate",
                HeaderText = "Data Check-Out",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy"
                }
            });

            dgvCheckIns.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumberOfGuests",
                DataPropertyName = "NumberOfGuests",
                HeaderText = "Persoane",
                Width = 80
            });

            dgvCheckIns.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StatusDisplay",
                DataPropertyName = "StatusDisplay",
                HeaderText = "Status",
                Width = 100
            });

            var checkInColumn = new DataGridViewButtonColumn
            {
                Name = "CheckIn",
                HeaderText = "Check-In",
                Text = "Check-In",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            dgvCheckIns.Columns.Add(checkInColumn);

            var editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "Editare",
                Text = "Editează",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvCheckIns.Columns.Add(editColumn);
        }

        private async void CheckInsForm_Load(object sender, EventArgs e)
        {
            await LoadCheckIns();
        }

        private async Task LoadCheckIns()
        {
            try
            {
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                _checkIns = await _reservationService.GetCheckInsForTodayAsync();

                dgvCheckIns.DataSource = null;
                dgvCheckIns.DataSource = _checkIns;

                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                lblRecordCount.Text = $"Total check-in-uri pentru astăzi: {_checkIns.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea check-in-urilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private async void dgvCheckIns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedReservation = _checkIns[e.RowIndex];

                if (e.ColumnIndex == dgvCheckIns.Columns["CheckIn"].Index)
                {
                    if (MessageBox.Show($"Sigur doriți să efectuați check-in pentru rezervarea {selectedReservation.Id}?",
                        "Confirmare Check-In", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await PerformCheckIn(selectedReservation.Id);
                    }
                }
                else if (e.ColumnIndex == dgvCheckIns.Columns["Edit"].Index)
                {
                    var reservationForm = new ReservationForm(selectedReservation.Id);
                    ((MainForm)ParentForm).OpenChildForm(reservationForm);
                }
            }
        }

        private async Task PerformCheckIn(int reservationId)
        {
            try
            {
                await _reservationService.CheckInAsync(reservationId);

                await LoadCheckIns();

                MessageBox.Show("Check-in efectuat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la efectuarea check-in-ului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadCheckIns();
        }

        private void btnNewReservation_Click(object sender, EventArgs e)
        {
            var reservationForm = new ReservationForm();
            ((MainForm)ParentForm).OpenChildForm(reservationForm);
        }
    }
}