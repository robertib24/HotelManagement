using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class ServiceForm : Form
    {
        private readonly ServiceService _serviceService;
        private ServiceModel _service;
        private readonly int? _serviceId;
        private bool _isEditMode;

        // Common service categories
        private readonly string[] _serviceCategories = {
            "RoomService", "Spa", "Transportation", "Cleaning", "Restaurant",
            "Recreation", "Business", "Miscellaneous"
        };

        public ServiceForm(int? serviceId = null)
        {
            InitializeComponent();

            _serviceService = new ServiceService();
            _serviceId = serviceId;
            _isEditMode = serviceId.HasValue;

            // Set form title based on mode
            this.Text = _isEditMode ? "Edit Service" : "Add New Service";
            lblTitle.Text = _isEditMode ? "Edit Service" : "Add New Service";
            btnSave.Text = _isEditMode ? "Save Changes" : "Add Service";
        }

        private async void ServiceForm_Load(object sender, EventArgs e)
        {
            // Populate category dropdown
            cmbCategory.Items.AddRange(_serviceCategories);

            if (_isEditMode)
            {
                await LoadService();
            }
            else
            {
                // Initialize a new service
                _service = new ServiceModel
                {
                    IsAvailable = true,
                    Price = 0
                };

                // Set default values for controls
                txtName.Text = string.Empty;
                txtDescription.Text = string.Empty;
                numPrice.Value = 0;
                cmbCategory.SelectedIndex = 0;
                chkIsAvailable.Checked = true;
            }
        }

        private async Task LoadService()
        {
            try
            {
                // Show loading panel
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Load service for editing
                _service = await _serviceService.GetServiceByIdAsync(_serviceId.Value);

                // Populate controls with service data
                txtName.Text = _service.Name;
                txtDescription.Text = _service.Description;
                numPrice.Value = _service.Price;

                // Select the appropriate category
                int categoryIndex = Array.IndexOf(_serviceCategories, _service.Category);
                cmbCategory.SelectedIndex = categoryIndex >= 0 ? categoryIndex : 0;

                chkIsAvailable.Checked = _service.IsAvailable;

                // Show main panel
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading service: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            try
            {
                // Update service object with values from controls
                _service.Name = txtName.Text;
                _service.Description = txtDescription.Text;
                _service.Price = numPrice.Value;
                _service.Category = cmbCategory.SelectedItem.ToString();
                _service.IsAvailable = chkIsAvailable.Checked;

                // Save service
                if (_isEditMode)
                {
                    await _serviceService.UpdateServiceAsync(_service);
                    MessageBox.Show("Service updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await _serviceService.CreateServiceAsync(_service);
                    MessageBox.Show("Service added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Return to service list
                ((MainForm)ParentForm).OpenChildForm(new ServicesListForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving service: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            // Check if all required fields are filled
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Service name is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            if (numPrice.Value < 0)
            {
                MessageBox.Show("Price cannot be negative!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPrice.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Return to service list
            ((MainForm)ParentForm).OpenChildForm(new ServicesListForm());
        }
    }
}