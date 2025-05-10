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

            // Setări pentru DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Configurare DataGridView
            dgvCheckOuts.AutoGenerateColumns = false;
            dgvCheckOuts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCheckOuts.MultiSelect = false;
            dgvCheckOuts.ReadOnly = true;
            dgvCheckOuts.AllowUserToAddRows = false;
            dgvCheckOuts.AllowUserToDeleteRows = false;
            dgvCheckOuts.AllowUserToOrderColumns = true;
            dgvCheckOuts.AllowUserToResizeRows = false;

            // Stilizare
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

            // Definire coloane
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

            // Adaugă coloană pentru butonul de check-out
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

            // Adaugă coloană pentru butonul de facturare
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
                // Afișează panoul de încărcare
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Încarcă lista de check-out-uri pentru astăzi
                _checkOuts = await _reservationService.GetCheckOutsForTodayAsync();

                // Leagă datele la DataGridView
                dgvCheckOuts.DataSource = null;
                dgvCheckOuts.DataSource = _checkOuts;

                // Afișează panoul principal
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                // Actualizează eticheta cu numărul de înregistrări
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
            // Verifică dacă s-a făcut clic pe un buton (coloană de acțiune)
            if (e.RowIndex >= 0)
            {
                var selectedReservation = _checkOuts[e.RowIndex];

                // Verifică ce coloană de acțiune a fost apăsată
                if (e.ColumnIndex == dgvCheckOuts.Columns["CheckOut"].Index)
                {
                    // Confirmă check-out-ul
                    if (MessageBox.Show($"Sigur doriți să efectuați check-out pentru rezervarea {selectedReservation.Id}?",
                        "Confirmare Check-Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await PerformCheckOut(selectedReservation.Id);
                    }
                }
                else if (e.ColumnIndex == dgvCheckOuts.Columns["Invoice"].Index)
                {
                    // Generează factura pentru rezervarea selectată
                    await GenerateInvoice(selectedReservation.Id);
                }
            }
        }

        private async Task PerformCheckOut(int reservationId)
        {
            try
            {
                // Efectuează check-out pentru rezervare
                await _reservationService.CheckOutAsync(reservationId);

                // Reîncarcă lista de check-out-uri
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
                // Deschide formularul pentru facturi cu rezervarea selectată
                InvoiceService invoiceService = new InvoiceService();
                var invoice = await invoiceService.GenerateInvoiceAsync(reservationId);

                MessageBox.Show($"Factură generată cu succes! Număr factură: {invoice.InvoiceNumber}", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Deschide formularul pentru facturi
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