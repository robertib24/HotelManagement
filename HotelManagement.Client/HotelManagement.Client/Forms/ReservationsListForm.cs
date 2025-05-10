using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class ReservationsListForm : Form
    {
        private readonly ReservationService _reservationService;
        private readonly InvoiceService _invoiceService;
        private List<ReservationModel> _reservations;
        private readonly int? _customerId;
        private readonly int? _hotelId;

        public ReservationsListForm(int? customerId = null, int? hotelId = null)
        {
            InitializeComponent();

            _reservationService = new ReservationService();
            _invoiceService = new InvoiceService();
            _customerId = customerId;
            _hotelId = hotelId;

            // Set form title based on parameters
            if (_customerId.HasValue)
            {
                this.Text = "Customer Reservations";
                lblTitle.Text = "Customer Reservations";
            }
            else if (_hotelId.HasValue)
            {
                this.Text = "Hotel Reservations";
                lblTitle.Text = "Hotel Reservations";
            }
            else
            {
                this.Text = "All Reservations";
                lblTitle.Text = "All Reservations";
            }

            // Setup DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView
            dgvReservations.AutoGenerateColumns = false;
            dgvReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservations.MultiSelect = false;
            dgvReservations.ReadOnly = true;
            dgvReservations.AllowUserToAddRows = false;
            dgvReservations.AllowUserToDeleteRows = false;
            dgvReservations.AllowUserToOrderColumns = true;
            dgvReservations.AllowUserToResizeRows = false;

            // Styling
            dgvReservations.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvReservations.BorderStyle = BorderStyle.None;
            dgvReservations.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReservations.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvReservations.EnableHeadersVisualStyles = false;

            dgvReservations.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvReservations.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReservations.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvReservations.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvReservations.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvReservations.ColumnHeadersHeight = 40;

            dgvReservations.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvReservations.DefaultCellStyle.ForeColor = Color.White;
            dgvReservations.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvReservations.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvReservations.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvReservations.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvReservations.RowHeadersVisible = false;
            dgvReservations.RowTemplate.Height = 35;

            // Define columns
            dgvReservations.Columns.Clear();

            dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            // Only show customer column if not filtering by customer
            if (!_customerId.HasValue)
            {
                dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "CustomerName",
                    DataPropertyName = "CustomerName",
                    HeaderText = "Customer",
                    Width = 150
                });
            }

            // Only show hotel column if not filtering by hotel
            if (!_hotelId.HasValue)
            {
                dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "HotelName",
                    DataPropertyName = "HotelName",
                    HeaderText = "Hotel",
                    Width = 150
                });
            }

            dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RoomNumber",
                DataPropertyName = "RoomNumber",
                HeaderText = "Room",
                Width = 80
            });

            dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CheckInDate",
                DataPropertyName = "CheckInDate",
                HeaderText = "Check-In",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy"
                }
            });

            dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CheckOutDate",
                DataPropertyName = "CheckOutDate",
                HeaderText = "Check-Out",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy"
                }
            });

            dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumberOfGuests",
                DataPropertyName = "NumberOfGuests",
                HeaderText = "Guests",
                Width = 60
            });

            dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalPrice",
                DataPropertyName = "TotalPrice",
                HeaderText = "Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvReservations.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StatusDisplay",
                DataPropertyName = "StatusDisplay",
                HeaderText = "Status",
                Width = 100
            });

            dgvReservations.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsPaid",
                DataPropertyName = "IsPaid",
                HeaderText = "Paid",
                Width = 50
            });

            // Add columns for action buttons
            var editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 60,
                FlatStyle = FlatStyle.Flat
            };
            dgvReservations.Columns.Add(editColumn);

            var checkInColumn = new DataGridViewButtonColumn
            {
                Name = "CheckIn",
                HeaderText = "Check-In",
                Text = "Check-In",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvReservations.Columns.Add(checkInColumn);

            var checkOutColumn = new DataGridViewButtonColumn
            {
                Name = "CheckOut",
                HeaderText = "Check-Out",
                Text = "Check-Out",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvReservations.Columns.Add(checkOutColumn);

            var cancelColumn = new DataGridViewButtonColumn
            {
                Name = "Cancel",
                HeaderText = "Cancel",
                Text = "Cancel",
                UseColumnTextForButtonValue = true,
                Width = 70,
                FlatStyle = FlatStyle.Flat
            };
            dgvReservations.Columns.Add(cancelColumn);

            var invoiceColumn = new DataGridViewButtonColumn
            {
                Name = "Invoice",
                HeaderText = "Invoice",
                Text = "Invoice",
                UseColumnTextForButtonValue = true,
                Width = 70,
                FlatStyle = FlatStyle.Flat
            };
            dgvReservations.Columns.Add(invoiceColumn);
        }

        private async void ReservationsListForm_Load(object sender, EventArgs e)
        {
            await LoadReservations();
        }

        private async Task LoadReservations()
        {
            try
            {
                // Show loading panel
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Load reservation list based on filters
                if (_customerId.HasValue)
                {
                    _reservations = await _reservationService.GetReservationsByCustomerAsync(_customerId.Value);
                }
                else if (_hotelId.HasValue)
                {
                    _reservations = await _reservationService.GetReservationsByHotelAsync(_hotelId.Value);
                }
                else
                {
                    // Load all reservations
                    _reservations = await _reservationService.GetAllReservationsAsync();
                }

                // Apply status filter if needed
                if (cmbStatus.SelectedIndex > 0)
                {
                    FilterReservationsByStatus();
                }
                else
                {
                    // Bind data to DataGridView
                    dgvReservations.DataSource = null;
                    dgvReservations.DataSource = _reservations;
                }

                // Show main panel
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                // Update record count label
                lblRecordCount.Text = $"Total reservations: {_reservations.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reservations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void FilterReservationsByStatus()
        {
            if (_reservations == null) return;

            // Get selected status
            string selectedStatus = cmbStatus.SelectedItem.ToString();

            // Define status mapping
            var statusMapping = new Dictionary<string, string>
            {
                { "All", "" },
                { "Confirmed", "Confirmed" },
                { "Checked-In", "CheckedIn" },
                { "Completed", "Completed" },
                { "Cancelled", "Cancelled" }
            };

            // If "All" is selected, show all reservations
            if (selectedStatus == "All")
            {
                dgvReservations.DataSource = null;
                dgvReservations.DataSource = _reservations;
                lblRecordCount.Text = $"Total reservations: {_reservations.Count}";
                return;
            }

            // Filter reservations by status
            var filteredReservations = _reservations.FindAll(r => r.ReservationStatus == statusMapping[selectedStatus]);

            // Update DataGridView and label
            dgvReservations.DataSource = null;
            dgvReservations.DataSource = filteredReservations;
            lblRecordCount.Text = $"Reservations found: {filteredReservations.Count} of {_reservations.Count}";
        }

        private void dgvReservations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if click was on an action button
            if (e.RowIndex >= 0)
            {
                var selectedReservation = _reservations[e.RowIndex];

                // Check which action column was clicked
                if (e.ColumnIndex == dgvReservations.Columns["Edit"].Index)
                {
                    // Open edit form for the selected reservation
                    var reservationForm = new ReservationForm(selectedReservation.Id);
                    ((MainForm)ParentForm).OpenChildForm(reservationForm);
                }
                else if (e.ColumnIndex == dgvReservations.Columns["CheckIn"].Index)
                {
                    // Check-in the reservation
                    if (selectedReservation.ReservationStatus == "Confirmed")
                    {
                        PerformCheckIn(selectedReservation.Id);
                    }
                    else
                    {
                        MessageBox.Show("Check-in can only be performed on confirmed reservations.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (e.ColumnIndex == dgvReservations.Columns["CheckOut"].Index)
                {
                    // Check-out the reservation
                    if (selectedReservation.ReservationStatus == "CheckedIn")
                    {
                        PerformCheckOut(selectedReservation.Id);
                    }
                    else
                    {
                        MessageBox.Show("Check-out can only be performed on checked-in reservations.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (e.ColumnIndex == dgvReservations.Columns["Cancel"].Index)
                {
                    // Cancel the reservation
                    if (selectedReservation.ReservationStatus == "Confirmed")
                    {
                        CancelReservation(selectedReservation.Id);
                    }
                    else
                    {
                        MessageBox.Show("Only confirmed reservations can be cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (e.ColumnIndex == dgvReservations.Columns["Invoice"].Index)
                {
                    // Generate or view invoice for the reservation
                    GenerateOrViewInvoice(selectedReservation.Id);
                }
            }
        }

        private async void PerformCheckIn(int reservationId)
        {
            try
            {
                // Confirm check-in
                if (MessageBox.Show("Are you sure you want to check in this reservation?", "Confirm Check-In", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Perform check-in
                    await _reservationService.CheckInAsync(reservationId);

                    // Reload reservations
                    await LoadReservations();

                    MessageBox.Show("Check-in completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing check-in: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void PerformCheckOut(int reservationId)
        {
            try
            {
                // Confirm check-out
                if (MessageBox.Show("Are you sure you want to check out this reservation?", "Confirm Check-Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Perform check-out
                    await _reservationService.CheckOutAsync(reservationId);

                    // Reload reservations
                    await LoadReservations();

                    // Ask if user wants to create an invoice
                    if (MessageBox.Show("Check-out completed successfully! Would you like to generate an invoice?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        GenerateOrViewInvoice(reservationId);
                    }
                    else
                    {
                        MessageBox.Show("Check-out completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing check-out: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CancelReservation(int reservationId)
        {
            try
            {
                // Confirm cancellation
                if (MessageBox.Show("Are you sure you want to cancel this reservation?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Cancel the reservation
                    await _reservationService.CancelReservationAsync(reservationId);

                    // Reload reservations
                    await LoadReservations();

                    MessageBox.Show("Reservation cancelled successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cancelling reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GenerateOrViewInvoice(int reservationId)
        {
            try
            {
                // Check if invoice already exists
                var invoice = await _invoiceService.GetInvoiceByReservationAsync(reservationId);

                if (invoice == null)
                {
                    // Generate new invoice
                    invoice = await _invoiceService.GenerateInvoiceAsync(reservationId);
                    MessageBox.Show($"Invoice generated successfully! Invoice number: {invoice.InvoiceNumber}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Open invoice form
                ((MainForm)ParentForm).OpenChildForm(new InvoicesForm(invoice.Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error with invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddReservation_Click(object sender, EventArgs e)
        {
            // Open form to add a new reservation
            var reservationForm = new ReservationForm();
            ((MainForm)ParentForm).OpenChildForm(reservationForm);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadReservations();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterReservations();
        }

        private void FilterReservations()
        {
            if (_reservations == null) return;

            string searchText = txtSearch.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // If search is empty, apply only status filter
                if (cmbStatus.SelectedIndex > 0)
                {
                    FilterReservationsByStatus();
                }
                else
                {
                    // Or show all reservations if no status filter
                    dgvReservations.DataSource = null;
                    dgvReservations.DataSource = _reservations;
                    lblRecordCount.Text = $"Total reservations: {_reservations.Count}";
                }
                return;
            }

            // Start with all reservations
            var filteredReservations = new List<ReservationModel>(_reservations);

            // Apply status filter if needed
            if (cmbStatus.SelectedIndex > 0)
            {
                string selectedStatus = cmbStatus.SelectedItem.ToString();
                var statusMapping = new Dictionary<string, string>
                {
                    { "All", "" },
                    { "Confirmed", "Confirmed" },
                    { "Checked-In", "CheckedIn" },
                    { "Completed", "Completed" },
                    { "Cancelled", "Cancelled" }
                };

                if (selectedStatus != "All")
                {
                    filteredReservations = filteredReservations.FindAll(r => r.ReservationStatus == statusMapping[selectedStatus]);
                }
            }

            // Apply text filter
            filteredReservations = filteredReservations.FindAll(r =>
                r.CustomerName.ToLower().Contains(searchText) ||
                r.HotelName.ToLower().Contains(searchText) ||
                r.RoomNumber.ToLower().Contains(searchText) ||
                r.Notes != null && r.Notes.ToLower().Contains(searchText));

            // Update DataGridView and label
            dgvReservations.DataSource = null;
            dgvReservations.DataSource = filteredReservations;
            lblRecordCount.Text = $"Reservations found: {filteredReservations.Count} of {_reservations.Count}";
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterReservations();
        }
    }
}