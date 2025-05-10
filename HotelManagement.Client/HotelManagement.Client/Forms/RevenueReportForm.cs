using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class RevenueReportForm : Form
    {
        private readonly InvoiceService _invoiceService;
        private readonly ReservationService _reservationService;
        private readonly HotelService _hotelService;
        private List<InvoiceModel> _invoices;
        private List<ReservationModel> _reservations;
        private List<HotelModel> _hotels;

        public RevenueReportForm()
        {
            InitializeComponent();

            _invoiceService = new InvoiceService();
            _reservationService = new ReservationService();
            _hotelService = new HotelService();

            this.Text = "Revenue Report";

            // Initialize date pickers to current month
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            dtpStartDate.Value = firstDayOfMonth;
            dtpEndDate.Value = lastDayOfMonth;
        }

        private async void RevenueReportForm_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                // Show loading panel
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Load all invoices and reservations
                _invoices = await _invoiceService.GetAllInvoicesAsync();
                _reservations = await _reservationService.GetAllReservationsAsync();
                _hotels = await _hotelService.GetAllHotelsAsync();

                // Generate the report
                GenerateReport();

                // Show main panel
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void GenerateReport()
        {
            // Clear existing controls
            pnlReport.Controls.Clear();

            // Set title
            lblReportTitle.Text = $"Revenue Report - {dtpStartDate.Value:yyyy-MM-dd} to {dtpEndDate.Value:yyyy-MM-dd}";

            // Filter invoices by date range
            var filteredInvoices = _invoices.Where(i =>
                i.IssueDate >= dtpStartDate.Value.Date &&
                i.IssueDate <= dtpEndDate.Value.Date).ToList();

            // Create a summary panel
            var summaryPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(40, 40, 60),
                Padding = new Padding(10)
            };

            // Calculate summary data
            decimal totalRevenue = filteredInvoices.Sum(i => i.Total);
            decimal paidRevenue = filteredInvoices.Where(i => i.IsPaid).Sum(i => i.Total);
            decimal unpaidRevenue = filteredInvoices.Where(i => !i.IsPaid).Sum(i => i.Total);
            int totalInvoices = filteredInvoices.Count;
            int paidInvoices = filteredInvoices.Count(i => i.IsPaid);
            int unpaidInvoices = filteredInvoices.Count(i => !i.IsPaid);

            // Add summary labels
            var lblTotalRevenue = new Label
            {
                Text = $"Total Revenue: {totalRevenue:C2}",
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblTotalRevenue);

            var lblPaidRevenue = new Label
            {
                Text = $"Paid Revenue: {paidRevenue:C2}",
                ForeColor = Color.LightGreen,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                Location = new Point(10, 40),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblPaidRevenue);

            var lblUnpaidRevenue = new Label
            {
                Text = $"Unpaid Revenue: {unpaidRevenue:C2}",
                ForeColor = Color.Tomato,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                Location = new Point(10, 70),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblUnpaidRevenue);

            var lblTotalInvoices = new Label
            {
                Text = $"Total Invoices: {totalInvoices}",
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Location = new Point(300, 40),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblTotalInvoices);

            var lblPaidInvoices = new Label
            {
                Text = $"Paid Invoices: {paidInvoices}",
                ForeColor = Color.LightGreen,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Location = new Point(300, 70),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblPaidInvoices);

            var lblUnpaidInvoices = new Label
            {
                Text = $"Unpaid Invoices: {unpaidInvoices}",
                ForeColor = Color.Tomato,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Location = new Point(450, 70),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblUnpaidInvoices);

            pnlReport.Controls.Add(summaryPanel);

            // Create a DataGridView for invoice details
            var dgvRevenueDetails = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.FromArgb(45, 45, 60),
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(51, 51, 76),
                    ForeColor = Color.White,
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                },
                ColumnHeadersHeight = 40,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(40, 40, 60),
                    ForeColor = Color.White,
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                    Padding = new Padding(10, 0, 0, 0),
                    SelectionBackColor = Color.FromArgb(0, 120, 215),
                    SelectionForeColor = Color.White
                },
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToOrderColumns = true,
                AllowUserToResizeRows = false
            };

            dgvRevenueDetails.RowTemplate.Height = 35;

            // Define columns
            dgvRevenueDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InvoiceNumber",
                HeaderText = "Invoice #",
                Width = 130
            });

            dgvRevenueDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                HeaderText = "Customer",
                Width = 180
            });

            dgvRevenueDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HotelName",
                HeaderText = "Hotel",
                Width = 150
            });

            dgvRevenueDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IssueDate",
                HeaderText = "Issue Date",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy"
                }
            });

            dgvRevenueDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SubTotal",
                HeaderText = "Subtotal",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvRevenueDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tax",
                HeaderText = "Tax",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvRevenueDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvRevenueDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 80
            });

            // Add data rows
            foreach (var invoice in filteredInvoices)
            {
                int rowIndex = dgvRevenueDetails.Rows.Add();
                var row = dgvRevenueDetails.Rows[rowIndex];

                row.Cells["InvoiceNumber"].Value = invoice.InvoiceNumber;
                row.Cells["CustomerName"].Value = invoice.CustomerName;
                row.Cells["HotelName"].Value = invoice.HotelName;
                row.Cells["IssueDate"].Value = invoice.IssueDate;
                row.Cells["SubTotal"].Value = invoice.SubTotal;
                row.Cells["Tax"].Value = invoice.Tax;
                row.Cells["Total"].Value = invoice.Total;
                row.Cells["Status"].Value = invoice.Status;

                // Set row color based on payment status
                if (invoice.IsPaid)
                {
                    row.DefaultCellStyle.ForeColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.ForeColor = Color.Tomato;
                }
            }

            // Add DataGridView to panel
            pnlReport.Controls.Add(dgvRevenueDetails);
        }

        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            // Validate date range
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Start date cannot be after end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await LoadData();
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            // Implement printing functionality
            MessageBox.Show("Printing functionality will be implemented in a future version.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            // Implement export functionality
            MessageBox.Show("Export functionality will be implemented in a future version.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}