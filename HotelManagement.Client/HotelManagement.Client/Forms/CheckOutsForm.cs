using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class CheckOutsForm : Form
    {
        private readonly ReservationService _reservationService;
        private List<ReservationModel> _checkOuts;

        public CheckOutsForm()
        {
            InitializeComponent();

            _reservationService = new ReservationService();

            this.Text = "Check-Out Astăzi";

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvCheckOuts.AutoGenerateColumns = false;
            dgvCheckOuts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCheckOuts.MultiSelect = false;
            dgvCheckOuts.ReadOnly = true;
            dgvCheckOuts.AllowUserToAddRows = false;
            dgvCheckOuts.AllowUserToDeleteRows = false;
            dgvCheckOuts.AllowUserToOrderColumns = true;
            dgvCheckOuts.AllowUserToResizeRows = false;

            dgvCheckOuts.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvCheckOuts.BorderStyle = BorderStyle.None;
            dgvCheckOuts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCheckOuts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCheckOuts.EnableHeadersVisualStyles = false;

            dgvCheckOuts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvCheckOuts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCheckOuts.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvCheckOuts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCheckOuts.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCheckOuts.ColumnHeadersHeight = 40;

            dgvCheckOuts.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvCheckOuts.DefaultCellStyle.ForeColor = Color.White;
            dgvCheckOuts.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvCheckOuts.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCheckOuts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvCheckOuts.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvCheckOuts.RowHeadersVisible = false;
            dgvCheckOuts.RowTemplate.Height = 35;

            dgvCheckOuts.Columns.Clear();

            dgvCheckOuts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvCheckOuts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                DataPropertyName = "CustomerName",
                HeaderText = "Client",
                Width = 180
            });

            dgvCheckOuts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HotelName",
                DataPropertyName = "HotelName",
                HeaderText = "Hotel",
                Width = 150
            });

            dgvCheckOuts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RoomNumber",
                DataPropertyName = "RoomNumber",
                HeaderText = "Cameră",
                Width = 80
            });

            dgvCheckOuts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CheckInDate",
                DataPropertyName = "CheckInDate",
                HeaderText = "Data Check-In",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy"
                }
            });

            dgvCheckOuts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CheckOutDate",
                DataPropertyName = "CheckOutDate",
                HeaderText = "Data Check-Out",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy HH:mm"
                }
            });

            dgvCheckOuts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumberOfGuests",
                DataPropertyName = "NumberOfGuests",
                HeaderText = "Persoane",
                Width = 80
            });

            dgvCheckOuts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StatusDisplay",
                DataPropertyName = "StatusDisplay",
                HeaderText = "Status",
                Width = 100
            });

            var checkOutColumn = new DataGridViewButtonColumn
            {
                Name = "CheckOut",
                HeaderText = "Check-Out",
                Text = "Check-Out",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            dgvCheckOuts.Columns.Add(checkOutColumn);

            var invoiceColumn = new DataGridViewButtonColumn
            {
                Name = "Invoice",
                HeaderText = "Factură",
                Text = "Factură",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            dgvCheckOuts.Columns.Add(invoiceColumn);
        }

        private async void CheckOutsForm_Load(object sender, EventArgs e)
        {
            await LoadCheckOuts();
        }

        private async Task LoadCheckOuts()
        {
            try
            {
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                _checkOuts = await _reservationService.GetCheckOutsForTodayAsync();

                dgvCheckOuts.DataSource = null;
                dgvCheckOuts.DataSource = _checkOuts;

                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                lblRecordCount.Text = $"Total check-out-uri pentru astăzi: {_checkOuts.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea check-out-urilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private async void dgvCheckOuts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedReservation = _checkOuts[e.RowIndex];

                if (e.ColumnIndex == dgvCheckOuts.Columns["CheckOut"].Index)
                {
                    if (MessageBox.Show($"Sigur doriți să efectuați check-out pentru rezervarea {selectedReservation.Id}?",
                        "Confirmare Check-Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await PerformCheckOut(selectedReservation.Id);
                    }
                }
                else if (e.ColumnIndex == dgvCheckOuts.Columns["Invoice"].Index)
                {
                    await GenerateInvoice(selectedReservation.Id);
                }
            }
        }

        private async Task PerformCheckOut(int reservationId)
        {
            try
            {
                await _reservationService.CheckOutAsync(reservationId);

                await LoadCheckOuts();

                MessageBox.Show("Check-out efectuat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la efectuarea check-out-ului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task GenerateInvoice(int reservationId)
        {
            try
            {
                InvoiceService invoiceService = new InvoiceService();
                var invoice = await invoiceService.GenerateInvoiceAsync(reservationId);

                MessageBox.Show($"Factură generată cu succes! Număr factură: {invoice.InvoiceNumber}", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ((MainForm)ParentForm).OpenChildForm(new InvoicesForm(invoice.Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la generarea facturii: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadCheckOuts();
        }
    }
}