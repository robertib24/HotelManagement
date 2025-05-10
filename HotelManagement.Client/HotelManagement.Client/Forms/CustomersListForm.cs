using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class CustomersListForm : Form
    {
        private readonly CustomerService _customerService;
        private List<CustomerModel> _customers;

        public CustomersListForm()
        {
            InitializeComponent();

            _customerService = new CustomerService();

            this.Text = "Customers List";

            // Setup DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AllowUserToOrderColumns = true;
            dgvCustomers.AllowUserToResizeRows = false;

            // Styling
            dgvCustomers.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvCustomers.BorderStyle = BorderStyle.None;
            dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCustomers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCustomers.EnableHeadersVisualStyles = false;

            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvCustomers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCustomers.ColumnHeadersHeight = 40;

            dgvCustomers.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvCustomers.DefaultCellStyle.ForeColor = Color.White;
            dgvCustomers.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvCustomers.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvCustomers.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.RowTemplate.Height = 35;

            // Define columns
            dgvCustomers.Columns.Clear();

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                DataPropertyName = "FullName",
                HeaderText = "Name",
                Width = 200
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 180
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Phone",
                DataPropertyName = "Phone",
                HeaderText = "Phone",
                Width = 120
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "City",
                DataPropertyName = "City",
                HeaderText = "City",
                Width = 120
            });

            dgvCustomers.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsVIP",
                DataPropertyName = "IsVIP",
                HeaderText = "VIP",
                Width = 50
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalStays",
                DataPropertyName = "TotalStays",
                HeaderText = "Stays",
                Width = 60
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalSpent",
                DataPropertyName = "TotalSpent",
                HeaderText = "Total Spent",
                Width = 100,
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
            dgvCustomers.Columns.Add(viewReservationsColumn);

            var editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvCustomers.Columns.Add(editColumn);

            var deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvCustomers.Columns.Add(deleteColumn);
        }

        private async void CustomersListForm_Load(object sender, EventArgs e)
        {
            await LoadCustomers();
        }

        private async Task LoadCustomers()
        {
            try
            {
                // Show loading panel
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Load customer list
                _customers = await _customerService.GetAllCustomersAsync();

                // Bind data to DataGridView
                dgvCustomers.DataSource = null;
                dgvCustomers.DataSource = _customers;

                // Show main panel
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                // Update record count label
                lblRecordCount.Text = $"Total customers: {_customers.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if click was on an action button
            if (e.RowIndex >= 0)
            {
                var selectedCustomer = _customers[e.RowIndex];

                // Check which action column was clicked
                if (e.ColumnIndex == dgvCustomers.Columns["ViewReservations"].Index)
                {
                    // Open reservations form for the selected customer
                    var reservationsForm = new ReservationsListForm(selectedCustomer.Id);
                    ((MainForm)ParentForm).OpenChildForm(reservationsForm);
                }
                else if (e.ColumnIndex == dgvCustomers.Columns["Edit"].Index)
                {
                    // Open edit form for the selected customer
                    var customerForm = new CustomerForm(selectedCustomer.Id);
                    ((MainForm)ParentForm).OpenChildForm(customerForm);
                }
                else if (e.ColumnIndex == dgvCustomers.Columns["Delete"].Index)
                {
                    // Confirm deletion
                    if (MessageBox.Show($"Are you sure you want to delete customer '{selectedCustomer.FullName}'?",
                        "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteCustomer(selectedCustomer.Id);
                    }
                }
            }
        }

        private async void DeleteCustomer(int customerId)
        {
            try
            {
                // Delete customer
                await _customerService.DeleteCustomerAsync(customerId);

                // Reload customer list
                await LoadCustomers();

                MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            // Open form to add a new customer
            var customerForm = new CustomerForm();
            ((MainForm)ParentForm).OpenChildForm(customerForm);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadCustomers();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterCustomers();
        }

        private void FilterCustomers()
        {
            if (_customers == null) return;

            string searchText = txtSearch.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // If search is empty, show all customers
                dgvCustomers.DataSource = _customers;
                lblRecordCount.Text = $"Total customers: {_customers.Count}";
                return;
            }

            // Filter customers by text
            var filteredCustomers = _customers.FindAll(c =>
                c.FullName.ToLower().Contains(searchText) ||
                c.Email.ToLower().Contains(searchText) ||
                c.Phone.ToLower().Contains(searchText) ||
                c.City.ToLower().Contains(searchText) ||
                c.Country.ToLower().Contains(searchText));

            // Update DataGridView and label
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = filteredCustomers;
            lblRecordCount.Text = $"Customers found: {filteredCustomers.Count} of {_customers.Count}";
        }

        private void chkShowVIPOnly_CheckedChanged(object sender, EventArgs e)
        {
            FilterCustomers();
        }
    }
}