using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class ServicesListForm : Form
    {
        private readonly ServiceService _serviceService;
        private List<ServiceModel> _services;

        public ServicesListForm()
        {
            InitializeComponent();

            _serviceService = new ServiceService();

            this.Text = "Services";

            // Setup DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView
            dgvServices.AutoGenerateColumns = false;
            dgvServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServices.MultiSelect = false;
            dgvServices.ReadOnly = true;
            dgvServices.AllowUserToAddRows = false;
            dgvServices.AllowUserToDeleteRows = false;
            dgvServices.AllowUserToOrderColumns = true;
            dgvServices.AllowUserToResizeRows = false;

            // Styling
            dgvServices.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvServices.BorderStyle = BorderStyle.None;
            dgvServices.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvServices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvServices.EnableHeadersVisualStyles = false;

            dgvServices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvServices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvServices.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvServices.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvServices.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvServices.ColumnHeadersHeight = 40;

            dgvServices.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvServices.DefaultCellStyle.ForeColor = Color.White;
            dgvServices.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvServices.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvServices.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvServices.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvServices.RowHeadersVisible = false;
            dgvServices.RowTemplate.Height = 35;

            // Define columns
            dgvServices.Columns.Clear();

            dgvServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Name",
                Width = 200
            });

            dgvServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Category",
                DataPropertyName = "Category",
                HeaderText = "Category",
                Width = 150
            });

            dgvServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Price",
                DataPropertyName = "Price",
                HeaderText = "Price",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvServices.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsAvailable",
                DataPropertyName = "IsAvailable",
                HeaderText = "Available",
                Width = 80
            });

            dgvServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                DataPropertyName = "Description",
                HeaderText = "Description",
                Width = 250
            });

            // Add columns for action buttons
            var editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvServices.Columns.Add(editColumn);

            var deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvServices.Columns.Add(deleteColumn);
        }

        private async void ServicesListForm_Load(object sender, EventArgs e)
        {
            await LoadServices();
        }

        private async Task LoadServices()
        {
            try
            {
                // Show loading panel
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Load service list
                _services = await _serviceService.GetAllServicesAsync();

                // Apply filter for category if selected
                if (cmbCategory.SelectedIndex > 0)
                {
                    FilterServicesByCategory();
                }
                else
                {
                    // Bind data to DataGridView
                    dgvServices.DataSource = null;
                    dgvServices.DataSource = _services;
                }

                // Show main panel
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                // Update record count label
                lblRecordCount.Text = $"Total services: {_services.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading services: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void FilterServicesByCategory()
        {
            if (_services == null) return;

            // Get selected category
            string selectedCategory = cmbCategory.SelectedItem.ToString();

            // If "All Categories" is selected, show all services
            if (selectedCategory == "All Categories")
            {
                dgvServices.DataSource = null;
                dgvServices.DataSource = _services;
                lblRecordCount.Text = $"Total services: {_services.Count}";
                return;
            }

            // Filter services by category
            var filteredServices = _services.FindAll(s => s.Category == selectedCategory);

            // Update DataGridView and label
            dgvServices.DataSource = null;
            dgvServices.DataSource = filteredServices;
            lblRecordCount.Text = $"Services found: {filteredServices.Count} of {_services.Count}";
        }

        private void dgvServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if click was on an action button
            if (e.RowIndex >= 0)
            {
                var selectedService = _services[e.RowIndex];

                // Check which action column was clicked
                if (e.ColumnIndex == dgvServices.Columns["Edit"].Index)
                {
                    // Open edit form for the selected service
                    var serviceForm = new ServiceForm(selectedService.Id);
                    ((MainForm)ParentForm).OpenChildForm(serviceForm);
                }
                else if (e.ColumnIndex == dgvServices.Columns["Delete"].Index)
                {
                    // Confirm deletion
                    if (MessageBox.Show($"Are you sure you want to delete service '{selectedService.Name}'?",
                        "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteService(selectedService.Id);
                    }
                }
            }
        }

        private async void DeleteService(int serviceId)
        {
            try
            {
                // Delete service
                await _serviceService.DeleteServiceAsync(serviceId);

                // Reload service list
                await LoadServices();

                MessageBox.Show("Service deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting service: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            // Open form to add a new service
            var serviceForm = new ServiceForm();
            ((MainForm)ParentForm).OpenChildForm(serviceForm);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadServices();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterServices();
        }

        private void FilterServices()
        {
            if (_services == null) return;

            string searchText = txtSearch.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // If search is empty, apply only category filter
                if (cmbCategory.SelectedIndex > 0)
                {
                    FilterServicesByCategory();
                }
                else
                {
                    // Or show all services if no category filter
                    dgvServices.DataSource = null;
                    dgvServices.DataSource = _services;
                    lblRecordCount.Text = $"Total services: {_services.Count}";
                }
                return;
            }

            // Start with all services
            var filteredServices = new List<ServiceModel>(_services);

            // Apply category filter if needed
            if (cmbCategory.SelectedIndex > 0)
            {
                string selectedCategory = cmbCategory.SelectedItem.ToString();
                if (selectedCategory != "All Categories")
                {
                    filteredServices = filteredServices.FindAll(s => s.Category == selectedCategory);
                }
            }

            // Apply text filter
            filteredServices = filteredServices.FindAll(s =>
                s.Name.ToLower().Contains(searchText) ||
                s.Description.ToLower().Contains(searchText) ||
                s.Category.ToLower().Contains(searchText));

            // Update DataGridView and label
            dgvServices.DataSource = null;
            dgvServices.DataSource = filteredServices;
            lblRecordCount.Text = $"Services found: {filteredServices.Count} of {_services.Count}";
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterServices();
        }

        private void chkShowAvailableOnly_CheckedChanged(object sender, EventArgs e)
        {
            FilterServices();
        }

        private async void LoadCategories()
        {
            try
            {
                // Load all services to get unique categories
                var allServices = await _serviceService.GetAllServicesAsync();

                // Extract unique categories
                var categories = new HashSet<string>();
                foreach (var service in allServices)
                {
                    if (!string.IsNullOrEmpty(service.Category))
                    {
                        categories.Add(service.Category);
                    }
                }

                // Populate category dropdown
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("All Categories");
                foreach (var category in categories)
                {
                    cmbCategory.Items.Add(category);
                }

                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading service categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}