using System;
using System.Drawing;
using System.Windows.Forms;
using HotelManagement.Client.Forms;
using HotelManagement.Client.Services;

namespace HotelManagement.Client
{
    public partial class MainForm : Form
    {
        private Form activeForm = null;

        public MainForm()
        {
            InitializeComponent();
            CustomizeDesign();
        }

        private void CustomizeDesign()
        {
            // Ascunde submenurile la inițializare
            panelHotelsSubmenu.Visible = false;
            panelRoomsSubmenu.Visible = false;
            panelCustomersSubmenu.Visible = false;
            panelReservationsSubmenu.Visible = false;
            panelServicesSubmenu.Visible = false;
            panelReportsSubmenu.Visible = false;
        }

        private void HideSubMenu()
        {
            if (panelHotelsSubmenu.Visible) panelHotelsSubmenu.Visible = false;
            if (panelRoomsSubmenu.Visible) panelRoomsSubmenu.Visible = false;
            if (panelCustomersSubmenu.Visible) panelCustomersSubmenu.Visible = false;
            if (panelReservationsSubmenu.Visible) panelReservationsSubmenu.Visible = false;
            if (panelServicesSubmenu.Visible) panelServicesSubmenu.Visible = false;
            if (panelReportsSubmenu.Visible) panelReportsSubmenu.Visible = false;
        }

        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        #region Menu Button Click Events
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DashboardForm());
            HideSubMenu();
        }

        private void btnHotels_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelHotelsSubmenu);
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelRoomsSubmenu);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelCustomersSubmenu);
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelReservationsSubmenu);
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelServicesSubmenu);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelReportsSubmenu);
        }
        #endregion

        #region Submenu Button Click Events
        private void btnHotelsList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HotelsListForm());
            HideSubMenu();
        }

        private void btnAddHotel_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HotelForm());
            HideSubMenu();
        }

        private void btnRoomsList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RoomsListForm());
            HideSubMenu();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RoomForm());
            HideSubMenu();
        }

        private void btnRoomTypes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RoomTypesForm());
            HideSubMenu();
        }

        private void btnCustomersList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CustomersListForm());
            HideSubMenu();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CustomerForm());
            HideSubMenu();
        }

        private void btnVIPCustomers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new VIPCustomersForm());
            HideSubMenu();
        }

        private void btnReservationsList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ReservationsListForm());
            HideSubMenu();
        }

        private void btnAddReservation_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ReservationForm());
            HideSubMenu();
        }

        private void btnCheckInToday_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CheckInsForm());
            HideSubMenu();
        }

        private void btnCheckOutToday_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CheckOutsForm());
            HideSubMenu();
        }

        private void btnServicesList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ServicesListForm());
            HideSubMenu();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ServiceForm());
            HideSubMenu();
        }

        private void btnInvoices_Click(object sender, EventArgs e)
        {
            OpenChildForm(new InvoicesForm());
            HideSubMenu();
        }

        private void btnOccupancyReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(new OccupancyReportForm());
            HideSubMenu();
        }

        private void btnRevenueReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RevenueReportForm());
            HideSubMenu();
        }
        #endregion

        public void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblCurrentFormTitle.Text = childForm.Text;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Verifică conectivitatea la API
            try
            {
                ApiHelper.InitializeClient();
                OpenChildForm(new DashboardForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la conectarea la API: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // Import necesare pentru a face posibil mutarea formularului
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
    }
}