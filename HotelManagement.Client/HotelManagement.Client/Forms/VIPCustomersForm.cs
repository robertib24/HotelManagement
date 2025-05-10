using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class VIPCustomersForm : Form
    {
        private readonly CustomerService _customerService;
        private List<CustomerModel> _vipCustomers;

        public VIPCustomersForm()
        {
            InitializeComponent();

            _customerService = new CustomerService();

            this.Text = "VIP Customers";

            // Setup DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView
            dgvVIPCustomers.AutoGenerateColumns = false;
            dgvVIPCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVIPCustomers.MultiSelect = false;
            dgvVIPCustomers.ReadOnly = true;
            dgvVIPCustomers.AllowUserToAddRows = false;
            dgvVIPCustomers.AllowUserToDeleteRows = false;
            dgvVIPCustomers.AllowUserToOrderColumns = true;
            dgvVIPCustomers.AllowUserToResizeRows = false;

            // Styling
            dgvVIPCustomers.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvVIPCustomers.BorderStyle = BorderStyle.None;
            dgvVIPCustomers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVIPCustomers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvVIPCustomers.EnableHeadersVisualStyles = false;

            dgvVIPCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvVIPCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvVIPCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvVIPCustomers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvVIPCustomers.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvVIPCustomers.ColumnHeadersHeight = 40;

            dgvVIPCustomers.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvVIPCustomers.DefaultCellStyle.ForeColor = Color.White;
            dgvVIPCustomers.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvVIPCustomers.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvVIPCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvVIPCustomers.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvVIPCustomers.RowHeadersVisible = false;
            dgvVIPCustomers.RowTemplate.Height = 35;

            // Define columns
            dgvVIPCustomers.Columns.Clear();

            dgvVIPCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvVIPCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                DataPropertyName = "FullName",
                HeaderText = "Name",
                Width = 200
            });

            dgvVIPCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 180
            });

            dgvVIPCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Phone",
                DataPropertyName = "Phone",
                HeaderText = "Phone",
                Width = 120
            });

            dgvVIPCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalStays",
                DataPropertyName = "TotalStays",
                HeaderText = "Total Stays",
                Width = 100
            });

            dgvVIPCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalSpent",
                DataPropertyName = "TotalSpent",
                HeaderText = "Total Spent",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Add columns for action buttons
            var viewReservationsColumn = new DataGridViewButtonColumn
            {
                Name = "ViewReservations",
                HeaderText = "Reservations",
                Text = "View",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            dgvVIPCustomers.Columns.Add(viewReservationsColumn);

            var editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvVIPCustomers.Columns.Add(editColumn);

            var removeVIPColumn = new DataGridViewButtonColumn
            {
                Name = "RemoveVIP",
                HeaderText = "Remove VIP",
                Text = "Remove",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            dgvVIPCustomers.Columns.Add(removeVIPColumn);
        }

        private async void VIPCustomersForm_Load(object sender, EventArgs e)
        {
            await LoadVIPCustomers();
        }

        private async Task LoadVIPCustomers()
        {
            try
            {
                // Show loading panel
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Load VIP customer list
                _vipCustomers = await _customerService.GetVIPCustomersAsync();

                // Apply text filter if needed
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    FilterVIPCustomers();
                }
                else
                {
                    // Bind data to DataGridView
                    dgvVIPCustomers.DataSource = null;
                    dgvVIPCustomers.DataSource = _vipCustomers;
                }

                // Show main panel
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                // Update record count label
                lblRecordCount.Text = $"Total VIP customers: {_vipCustomers.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading VIP customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void dgvVIPCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if click was on an action button
            if (e.RowIndex >= 0)
            {
                var selectedCustomer = _vipCustomers[e.RowIndex];

                // Check which action column was clicked
                if (e.ColumnIndex == dgvVIPCustomers.Columns["ViewReservations"].Index)
                {
                    // Open reservations form for the selected customer
                    var reservationsForm = new ReservationsListForm(selectedCustomer.Id);
                    ((MainForm)ParentForm).OpenChildForm(reservationsForm);
                }
                else if (e.ColumnIndex == dgvVIPCustomers.Columns["Edit"].Index)
                {
                    // Open edit form for the selected customer
                    var customerForm = new CustomerForm(selectedCustomer.Id);
                    ((MainForm)ParentForm).OpenChildForm(customerForm);
                }
                else if (e.ColumnIndex == dgvVIPCustomers.Columns["RemoveVIP"].Index)
                {
                    // Confirm removing VIP status
                    if (MessageBox.Show($"Are you sure you want to remove VIP status from '{selectedCustomer.FullName}'?",
                        "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        RemoveVIPStatus(selectedCustomer);
                    }
                }
            }
        }

        private async void RemoveVIPStatus(CustomerModel customer)
        {
            try
            {
                // Update customer removing VIP status
                customer.IsVIP = false;
                await _customerService.UpdateCustomerAsync(customer);

                // Reload VIP customer list
                await LoadVIPCustomers();

                MessageBox.Show("VIP status removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing VIP status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadVIPCustomers();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterVIPCustomers();
        }

        private void FilterVIPCustomers()
        {
            if (_vipCustomers == null) return;

            string searchText = txtSearch.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // If search is empty, show all VIP customers
                dgvVIPCustomers.DataSource = null;
                dgvVIPCustomers.DataSource = _vipCustomers;
                lblRecordCount.Text = $"Total VIP customers: {_vipCustomers.Count}";
                return;
            }

            // Filter VIP customers by text
            var filteredCustomers = _vipCustomers.FindAll(c =>
                c.FullName.ToLower().Contains(searchText) ||
                c.Email.ToLower().Contains(searchText) ||
                c.Phone.ToLower().Contains(searchText) ||
                c.City.ToLower().Contains(searchText) ||
                c.Country.ToLower().Contains(searchText));

            // Update DataGridView and label
            dgvVIPCustomers.DataSource = null;
            dgvVIPCustomers.DataSource = filteredCustomers;
            lblRecordCount.Text = $"VIP customers found: {filteredCustomers.Count} of {_vipCustomers.Count}";
        }
    }
}