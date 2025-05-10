using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class RoomTypesForm : Form
    {
        private readonly RoomTypeService _roomTypeService;
        private List<RoomTypeModel> _roomTypes;

        public RoomTypesForm()
        {
            InitializeComponent();

            _roomTypeService = new RoomTypeService();

            this.Text = "Room Types";

            // Setup DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView
            dgvRoomTypes.AutoGenerateColumns = false;
            dgvRoomTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoomTypes.MultiSelect = false;
            dgvRoomTypes.ReadOnly = true;
            dgvRoomTypes.AllowUserToAddRows = false;
            dgvRoomTypes.AllowUserToDeleteRows = false;
            dgvRoomTypes.AllowUserToOrderColumns = true;
            dgvRoomTypes.AllowUserToResizeRows = false;

            // Styling
            dgvRoomTypes.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvRoomTypes.BorderStyle = BorderStyle.None;
            dgvRoomTypes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRoomTypes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRoomTypes.EnableHeadersVisualStyles = false;

            dgvRoomTypes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvRoomTypes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRoomTypes.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvRoomTypes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRoomTypes.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvRoomTypes.ColumnHeadersHeight = 40;

            dgvRoomTypes.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvRoomTypes.DefaultCellStyle.ForeColor = Color.White;
            dgvRoomTypes.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvRoomTypes.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvRoomTypes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvRoomTypes.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvRoomTypes.RowHeadersVisible = false;
            dgvRoomTypes.RowTemplate.Height = 35;

            // Define columns
            dgvRoomTypes.Columns.Clear();

            dgvRoomTypes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvRoomTypes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Name",
                Width = 200
            });

            dgvRoomTypes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaxOccupancy",
                DataPropertyName = "MaxOccupancy",
                HeaderText = "Max Occupancy",
                Width = 120
            });

            dgvRoomTypes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BasePrice",
                DataPropertyName = "BasePrice",
                HeaderText = "Price",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvRoomTypes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AvailableRooms",
                DataPropertyName = "AvailableRooms",
                HeaderText = "Available Rooms",
                Width = 120
            });

            dgvRoomTypes.Columns.Add(new DataGridViewTextBoxColumn
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
            dgvRoomTypes.Columns.Add(editColumn);

            var deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvRoomTypes.Columns.Add(deleteColumn);
        }

        private async void RoomTypesForm_Load(object sender, EventArgs e)
        {
            await LoadRoomTypes();
        }

        private async Task LoadRoomTypes()
        {
            try
            {
                // Show loading panel
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                // Load room type list
                _roomTypes = await _roomTypeService.GetAllRoomTypesAsync();

                // Bind data to DataGridView
                dgvRoomTypes.DataSource = null;
                dgvRoomTypes.DataSource = _roomTypes;

                // Show main panel
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                // Update record count label
                lblRecordCount.Text = $"Total room types: {_roomTypes.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room types: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void dgvRoomTypes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if click was on an action button
            if (e.RowIndex >= 0)
            {
                var selectedRoomType = _roomTypes[e.RowIndex];

                // Check which action column was clicked
                if (e.ColumnIndex == dgvRoomTypes.Columns["Edit"].Index)
                {
                    // Open edit form for the selected room type
                    EditRoomType(selectedRoomType.Id);
                }
                else if (e.ColumnIndex == dgvRoomTypes.Columns["Delete"].Index)
                {
                    // Confirm deletion
                    if (MessageBox.Show($"Are you sure you want to delete room type '{selectedRoomType.Name}'?",
                        "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteRoomType(selectedRoomType.Id);
                    }
                }
            }
        }

        private void EditRoomType(int roomTypeId)
        {
            // Get the selected room type
            var roomType = _roomTypes.Find(rt => rt.Id == roomTypeId);
            if (roomType == null) return;

            // Create and show edit dialog
            using (var form = new RoomTypeEditForm(roomType))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Reload room types after edit
                    LoadRoomTypes();
                }
            }
        }

        private async void DeleteRoomType(int roomTypeId)
        {
            try
            {
                // Delete room type
                await _roomTypeService.DeleteRoomTypeAsync(roomTypeId);

                // Reload room type list
                await LoadRoomTypes();

                MessageBox.Show("Room type deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting room type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddRoomType_Click(object sender, EventArgs e)
        {
            // Open dialog to add a new room type
            using (var form = new RoomTypeEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Reload room types after adding
                    LoadRoomTypes();
                }
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadRoomTypes();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterRoomTypes();
        }

        private void FilterRoomTypes()
        {
            if (_roomTypes == null) return;

            string searchText = txtSearch.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // If search is empty, show all room types
                dgvRoomTypes.DataSource = null;
                dgvRoomTypes.DataSource = _roomTypes;
                lblRecordCount.Text = $"Total room types: {_roomTypes.Count}";
                return;
            }

            // Filter room types by text
            var filteredRoomTypes = _roomTypes.FindAll(rt =>
                rt.Name.ToLower().Contains(searchText) ||
                rt.Description.ToLower().Contains(searchText));

            // Update DataGridView and label
            dgvRoomTypes.DataSource = null;
            dgvRoomTypes.DataSource = filteredRoomTypes;
            lblRecordCount.Text = $"Room types found: {filteredRoomTypes.Count} of {_roomTypes.Count}";
        }
    }

    // Helper form for editing room types
    public class RoomTypeEditForm : Form
    {
        private RoomTypeModel _roomType;
        private bool _isEditMode;
        private readonly RoomTypeService _roomTypeService;

        // Form controls
        private Label lblTitle;
        private Label lblName;
        private TextBox txtName;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblMaxOccupancy;
        private NumericUpDown numMaxOccupancy;
        private Label lblBasePrice;
        private NumericUpDown numBasePrice;
        private Button btnSave;
        private Button btnCancel;

        public RoomTypeEditForm(RoomTypeModel roomType = null)
        {
            _roomTypeService = new RoomTypeService();
            _isEditMode = roomType != null;
            _roomType = roomType ?? new RoomTypeModel
            {
                MaxOccupancy = 2,
                BasePrice = 100
            };

            InitializeComponent();
            InitializeFormControls();
        }

        private void InitializeComponent()
        {
            this.Text = _isEditMode ? "Edit Room Type" : "Add New Room Type";
            this.Size = new Size(500, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(32, 30, 45);
            this.ForeColor = Color.White;
        }

        private void InitializeFormControls()
        {
            // Title
            lblTitle = new Label
            {
                Text = _isEditMode ? "Edit Room Type" : "Add New Room Type",
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Name
            lblName = new Label
            {
                Text = "Name:",
                ForeColor = Color.White,
                Location = new Point(20, 70),
                AutoSize = true
            };

            txtName = new TextBox
            {
                Text = _roomType.Name,
                Location = new Point(150, 70),
                Width = 300
            };

            // Description
            lblDescription = new Label
            {
                Text = "Description:",
                ForeColor = Color.White,
                Location = new Point(20, 110),
                AutoSize = true
            };

            txtDescription = new TextBox
            {
                Text = _roomType.Description,
                Location = new Point(150, 110),
                Width = 300,
                Height = 100,
                Multiline = true
            };

            // Max Occupancy
            lblMaxOccupancy = new Label
            {
                Text = "Max Occupancy:",
                ForeColor = Color.White,
                Location = new Point(20, 230),
                AutoSize = true
            };

            numMaxOccupancy = new NumericUpDown
            {
                Value = _roomType.MaxOccupancy,
                Location = new Point(150, 230),
                Width = 100,
                Minimum = 1,
                Maximum = 10
            };

            // Base Price
            lblBasePrice = new Label
            {
                Text = "Base Price:",
                ForeColor = Color.White,
                Location = new Point(20, 270),
                AutoSize = true
            };

            numBasePrice = new NumericUpDown
            {
                Value = _roomType.BasePrice,
                Location = new Point(150, 270),
                Width = 100,
                Minimum = 0,
                Maximum = 10000,
                DecimalPlaces = 2
            };

            // Buttons
            btnSave = new Button
            {
                Text = _isEditMode ? "Save Changes" : "Add Room Type",
                BackColor = Color.FromArgb(0, 150, 136),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(260, 320),
                Width = 110,
                Height = 35,
                DialogResult = DialogResult.OK
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                BackColor = Color.FromArgb(192, 57, 43),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(380, 320),
                Width = 90,
                Height = 35,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            // Add controls to form
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtDescription);
            this.Controls.Add(lblMaxOccupancy);
            this.Controls.Add(numMaxOccupancy);
            this.Controls.Add(lblBasePrice);
            this.Controls.Add(numBasePrice);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            try
            {
                // Update room type object with values from controls
                _roomType.Name = txtName.Text;
                _roomType.Description = txtDescription.Text;
                _roomType.MaxOccupancy = (int)numMaxOccupancy.Value;
                _roomType.BasePrice = numBasePrice.Value;

                // Save room type
                if (_isEditMode)
                {
                    await _roomTypeService.UpdateRoomTypeAsync(_roomType);
                    MessageBox.Show("Room type updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await _roomTypeService.CreateRoomTypeAsync(_roomType);
                    MessageBox.Show("Room type added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving room type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }

        private bool ValidateForm()
        {
            // Check if all required fields are filled
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Room type name is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (numMaxOccupancy.Value < 1)
            {
                MessageBox.Show("Maximum occupancy must be at least 1!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numMaxOccupancy.Focus();
                return false;
            }

            if (numBasePrice.Value < 0)
            {
                MessageBox.Show("Base price cannot be negative!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numBasePrice.Focus();
                return false;
            }

            return true;
        }
    }
}