using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;
        private CustomerModel _customer;
        private readonly int? _customerId;
        private bool _isEditMode;

        public CustomerForm(int? customerId = null)
        {
            InitializeComponent();

            _customerService = new CustomerService();
            _customerId = customerId;
            _isEditMode = customerId.HasValue;

            this.Text = _isEditMode ? "Edit Customer" : "Add New Customer";
            lblTitle.Text = _isEditMode ? "Edit Customer" : "Add New Customer";
            btnSave.Text = _isEditMode ? "Save Changes" : "Add Customer";
        }

        private async void CustomerForm_Load(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                await LoadCustomer();
            }
            else
            {
                _customer = new CustomerModel
                {
                    RegistrationDate = DateTime.Now,
                    IsVIP = false
                };

                txtFirstName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtCountry.Text = "Romania";
                txtIdNumber.Text = string.Empty;
                dtpDateOfBirth.Value = DateTime.Now.AddYears(-30);
                chkIsVIP.Checked = false;
            }
        }

        private async Task LoadCustomer()
        {
            try
            {
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                _customer = await _customerService.GetCustomerByIdAsync(_customerId.Value);

                txtFirstName.Text = _customer.FirstName;
                txtLastName.Text = _customer.LastName;
                txtEmail.Text = _customer.Email;
                txtPhone.Text = _customer.Phone;
                txtAddress.Text = _customer.Address;
                txtCity.Text = _customer.City;
                txtCountry.Text = _customer.Country;
                txtIdNumber.Text = _customer.IdNumber;
                dtpDateOfBirth.Value = _customer.DateOfBirth;
                chkIsVIP.Checked = _customer.IsVIP;

                if (_customer.TotalStays > 0)
                {
                    lblTotalStays.Text = $"Total stays: {_customer.TotalStays}";
                    lblTotalSpent.Text = $"Total spent: {_customer.TotalSpent:C2}";
                    lblTotalStays.Visible = true;
                    lblTotalSpent.Visible = true;
                }
                else
                {
                    lblTotalStays.Visible = false;
                    lblTotalSpent.Visible = false;
                }

                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _customer.FirstName = txtFirstName.Text;
                _customer.LastName = txtLastName.Text;
                _customer.Email = txtEmail.Text;
                _customer.Phone = txtPhone.Text;
                _customer.Address = txtAddress.Text;
                _customer.City = txtCity.Text;
                _customer.Country = txtCountry.Text;
                _customer.IdNumber = txtIdNumber.Text;
                _customer.DateOfBirth = dtpDateOfBirth.Value;
                _customer.IsVIP = chkIsVIP.Checked;

                if (_isEditMode)
                {
                    await _customerService.UpdateCustomerAsync(_customer);
                    MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await _customerService.CreateCustomerAsync(_customer);
                    MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ((MainForm)ParentForm).OpenChildForm(new CustomersListForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First name is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last name is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email address is not valid!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIdNumber.Text))
            {
                MessageBox.Show("ID number is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdNumber.Focus();
                return false;
            }

            if (dtpDateOfBirth.Value > DateTime.Now.AddYears(-18))
            {
                MessageBox.Show("Customer must be at least 18 years old!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDateOfBirth.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((MainForm)ParentForm).OpenChildForm(new CustomersListForm());
        }

        private async void btnViewReservations_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                ((MainForm)ParentForm).OpenChildForm(new ReservationsListForm(_customer.Id));
            }
        }
    }
}