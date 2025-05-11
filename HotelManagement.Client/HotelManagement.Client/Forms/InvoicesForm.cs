using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class InvoicesForm : Form
    {
        private readonly InvoiceService _invoiceService;
        private List<InvoiceModel> _invoices;
        private readonly int? _invoiceId;

        public InvoicesForm(int? invoiceId = null)
        {
            InitializeComponent();

            _invoiceService = new InvoiceService();
            _invoiceId = invoiceId;

            this.Text = "Invoices";

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvInvoices.AutoGenerateColumns = false;
            dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInvoices.MultiSelect = false;
            dgvInvoices.ReadOnly = true;
            dgvInvoices.AllowUserToAddRows = false;
            dgvInvoices.AllowUserToDeleteRows = false;
            dgvInvoices.AllowUserToOrderColumns = true;
            dgvInvoices.AllowUserToResizeRows = false;

            dgvInvoices.BackgroundColor = Color.FromArgb(45, 45, 60);
            dgvInvoices.BorderStyle = BorderStyle.None;
            dgvInvoices.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInvoices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvInvoices.EnableHeadersVisualStyles = false;

            dgvInvoices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvInvoices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInvoices.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvInvoices.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvInvoices.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvInvoices.ColumnHeadersHeight = 40;

            dgvInvoices.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvInvoices.DefaultCellStyle.ForeColor = Color.White;
            dgvInvoices.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            dgvInvoices.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvInvoices.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvInvoices.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvInvoices.RowHeadersVisible = false;
            dgvInvoices.RowTemplate.Height = 35;

            dgvInvoices.Columns.Clear();

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InvoiceNumber",
                DataPropertyName = "InvoiceNumber",
                HeaderText = "Invoice #",
                Width = 130
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                DataPropertyName = "CustomerName",
                HeaderText = "Customer",
                Width = 180
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IssueDate",
                DataPropertyName = "IssueDate",
                HeaderText = "Issue Date",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy"
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DueDate",
                DataPropertyName = "DueDate",
                HeaderText = "Due Date",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy"
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                DataPropertyName = "Total",
                HeaderText = "Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 80
            });

            var viewColumn = new DataGridViewButtonColumn
            {
                Name = "View",
                HeaderText = "View",
                Text = "View",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvInvoices.Columns.Add(viewColumn);

            var payColumn = new DataGridViewButtonColumn
            {
                Name = "Pay",
                HeaderText = "Pay",
                Text = "Pay",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvInvoices.Columns.Add(payColumn);

            var printColumn = new DataGridViewButtonColumn
            {
                Name = "Print",
                HeaderText = "Print",
                Text = "Print",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvInvoices.Columns.Add(printColumn);
        }

        private async void InvoicesForm_Load(object sender, EventArgs e)
        {
            await LoadInvoices();

            if (_invoiceId.HasValue)
            {
                SelectInvoice(_invoiceId.Value);
            }
        }

        private void SelectInvoice(int invoiceId)
        {
            for (int i = 0; i < dgvInvoices.Rows.Count; i++)
            {
                var invoice = dgvInvoices.Rows[i].DataBoundItem as InvoiceModel;
                if (invoice != null && invoice.Id == invoiceId)
                {
                    dgvInvoices.ClearSelection();
                    dgvInvoices.Rows[i].Selected = true;
                    dgvInvoices.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }

        private async Task LoadInvoices()
        {
            try
            {
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                _invoices = new List<InvoiceModel>();

                if (radAllInvoices.Checked)
                {
                    _invoices = await _invoiceService.GetAllInvoicesAsync();
                }
                else if (radUnpaidInvoices.Checked)
                {
                    _invoices = await _invoiceService.GetUnpaidInvoicesAsync();
                }
                else if (radPaidInvoices.Checked)
                {
                    _invoices = await _invoiceService.GetPaidInvoicesAsync();
                }

                dgvInvoices.DataSource = null;
                dgvInvoices.DataSource = _invoices;

                LoadingPanel.Visible = false;
                MainPanel.Visible = true;

                lblRecordCount.Text = $"Total invoices: {_invoices.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void dgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedInvoice = _invoices[e.RowIndex];

                if (e.ColumnIndex == dgvInvoices.Columns["View"].Index)
                {
                    ViewInvoiceDetails(selectedInvoice);
                }
                else if (e.ColumnIndex == dgvInvoices.Columns["Pay"].Index)
                {
                    if (!selectedInvoice.IsPaid)
                    {
                        PayInvoice(selectedInvoice);
                    }
                    else
                    {
                        MessageBox.Show("This invoice is already paid.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (e.ColumnIndex == dgvInvoices.Columns["Print"].Index)
                {
                    PrintInvoice(selectedInvoice);
                }
            }
        }

        private void ViewInvoiceDetails(InvoiceModel invoice)
        {
            string message = $"Invoice: {invoice.InvoiceNumber}\n" +
                            $"Customer: {invoice.CustomerName}\n" +
                            $"Room: {invoice.RoomInfo}\n" +
                            $"Hotel: {invoice.HotelName}\n" +
                            $"Check-In: {invoice.CheckInDate:dd.MM.yyyy}\n" +
                            $"Check-Out: {invoice.CheckOutDate:dd.MM.yyyy}\n" +
                            $"Issue Date: {invoice.IssueDate:dd.MM.yyyy}\n" +
                            $"Due Date: {invoice.DueDate:dd.MM.yyyy}\n" +
                            $"Subtotal: {invoice.SubTotal:C2}\n" +
                            $"Tax: {invoice.Tax:C2}\n" +
                            $"Total: {invoice.Total:C2}\n" +
                            $"Status: {invoice.Status}";

            MessageBox.Show(message, "Invoice Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void PayInvoice(InvoiceModel invoice)
        {
            string[] paymentMethods = { "Cash", "Credit Card", "Bank Transfer" };
            string selectedMethod = ShowPaymentMethodDialog(paymentMethods);

            if (selectedMethod != null)
            {
                try
                {
                    await _invoiceService.MarkAsPaidAsync(invoice.Id, selectedMethod);

                    await LoadInvoices();

                    MessageBox.Show("Invoice marked as paid successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error marking invoice as paid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string ShowPaymentMethodDialog(string[] methods)
        {
            Form form = new Form();
            form.Text = "Select Payment Method";
            form.ClientSize = new Size(300, 200);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            Label label = new Label();
            label.Text = "Select payment method:";
            label.AutoSize = true;
            label.Location = new Point(20, 20);
            form.Controls.Add(label);

            ListBox listBox = new ListBox();
            listBox.Items.AddRange(methods);
            listBox.Location = new Point(20, 50);
            listBox.Size = new Size(260, 95);
            listBox.SelectedIndex = 0;
            form.Controls.Add(listBox);

            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(105, 160);
            form.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(195, 160);
            form.Controls.Add(cancelButton);

            form.AcceptButton = okButton;
            form.CancelButton = cancelButton;

            if (form.ShowDialog() == DialogResult.OK)
            {
                return listBox.SelectedItem.ToString();
            }
            else
            {
                return null;
            }
        }

        private void PrintInvoice(InvoiceModel invoice)
        {
            MessageBox.Show("Invoice printing functionality will be implemented in a future version.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadInvoices();
        }

        private async void radAllInvoices_CheckedChanged(object sender, EventArgs e)
        {
            if (radAllInvoices.Checked)
            {
                await LoadInvoices();
            }
        }

        private async void radUnpaidInvoices_CheckedChanged(object sender, EventArgs e)
        {
            if (radUnpaidInvoices.Checked)
            {
                await LoadInvoices();
            }
        }

        private async void radPaidInvoices_CheckedChanged(object sender, EventArgs e)
        {
            if (radPaidInvoices.Checked)
            {
                await LoadInvoices();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterInvoices();
        }

        private void FilterInvoices()
        {
            if (_invoices == null) return;

            string searchText = txtSearch.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                dgvInvoices.DataSource = _invoices;
                lblRecordCount.Text = $"Total invoices: {_invoices.Count}";
                return;
            }

            var filteredInvoices = _invoices.FindAll(i =>
                i.InvoiceNumber.ToLower().Contains(searchText) ||
                i.CustomerName.ToLower().Contains(searchText) ||
                i.HotelName.ToLower().Contains(searchText));

            dgvInvoices.DataSource = null;
            dgvInvoices.DataSource = filteredInvoices;
            lblRecordCount.Text = $"Invoices found: {filteredInvoices.Count} of {_invoices.Count}";
        }
    }
}