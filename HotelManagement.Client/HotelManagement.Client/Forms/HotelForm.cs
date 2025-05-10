using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class HotelForm : Form
    {
        private readonly HotelService _hotelService;
        private HotelModel _hotel;
        private readonly int? _hotelId;
        private bool _isEditMode;

        public HotelForm(int? hotelId = null)
        {
            InitializeComponent();

            _hotelService = new HotelService();
            _hotelId = hotelId;
            _isEditMode = hotelId.HasValue;

            // Setează titlul formularului în funcție de mod
            this.Text = _isEditMode ? "Editare hotel" : "Adaugă hotel nou";
            lblTitle.Text = _isEditMode ? "Editare hotel" : "Adaugă hotel nou";
            btnSave.Text = _isEditMode ? "Salvează modificările" : "Adaugă hotel";
        }

        private async void HotelForm_Load(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                await LoadHotel();
            }
            else
            {
                // Inițializează un hotel nou
                _hotel = new HotelModel
                {
                    IsActive = true,
                    Stars = 3
                };

                // Setează valorile implicite pentru controale
                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtCountry.Text = "România";
                numStars.Value = 3;
                txtPhone.Text = string.Empty;
                txtEmail.Text = string.Empty;
                chkIsActive.Checked = true;
            }
        }

        private async Task LoadHotel()
        {
            try
            {
                // Afișează panoul de încărcare
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Încarcă hotelul pentru editare
                _hotel = await _hotelService.GetHotelByIdAsync(_hotelId.Value);

                // Populează controalele cu datele hotelului
                txtName.Text = _hotel.Name;
                txtAddress.Text = _hotel.Address;
                txtCity.Text = _hotel.City;
                txtCountry.Text = _hotel.Country;
                numStars.Value = _hotel.Stars;
                txtPhone.Text = _hotel.ContactPhone;
                txtEmail.Text = _hotel.Email;
                chkIsActive.Checked = _hotel.IsActive;

                lblTotalRooms.Text = $"{_hotel.TotalRooms}";
                lblAvailableRooms.Text = $"{_hotel.AvailableRooms}";

                // Afișează panoul principal
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                // Afișează panoul cu informații despre camere doar în modul de editare
                panelRoomInfo.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea hotelului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Actualizează obiectul hotel cu valorile din controale
                _hotel.Name = txtName.Text;
                _hotel.Address = txtAddress.Text;
                _hotel.City = txtCity.Text;
                _hotel.Country = txtCountry.Text;
                _hotel.Stars = (int)numStars.Value;
                _hotel.ContactPhone = txtPhone.Text;
                _hotel.Email = txtEmail.Text;
                _hotel.IsActive = chkIsActive.Checked;

                // Salvează hotelul
                if (_isEditMode)
                {
                    await _hotelService.UpdateHotelAsync(_hotel);
                    MessageBox.Show("Hotel actualizat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await _hotelService.CreateHotelAsync(_hotel);
                    MessageBox.Show("Hotel adăugat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Revine la lista de hoteluri
                ((MainForm)ParentForm).OpenChildForm(new HotelsListForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la salvarea hotelului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            // Verifică dacă toate câmpurile obligatorii sunt completate
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Numele hotelului este obligatoriu!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                MessageBox.Show("Orașul este obligatoriu!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCity.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCountry.Text))
            {
                MessageBox.Show("Țara este obligatorie!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCountry.Focus();
                return false;
            }

            // Verifică validitatea adresei de email (dacă este furnizată)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Adresa de email nu este validă!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
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
            // Revine la lista de hoteluri
            ((MainForm)ParentForm).OpenChildForm(new HotelsListForm());
        }

        private void btnViewRooms_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                // Deschide formularul de camere pentru hotelul curent
                var roomsForm = new RoomsListForm(_hotel.Id, _hotel.Name);
                ((MainForm)ParentForm).OpenChildForm(roomsForm);
            }
        }
    }
}