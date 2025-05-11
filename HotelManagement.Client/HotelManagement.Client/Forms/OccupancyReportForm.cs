using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Client.Models;
using HotelManagement.Client.Services;

namespace HotelManagement.Client.Forms
{
    public partial class OccupancyReportForm : Form
    {
        private readonly HotelService _hotelService;
        private readonly RoomService _roomService;
        private List<HotelModel> _hotels;
        private Dictionary<int, List<RoomModel>> _hotelRooms;

        public OccupancyReportForm()
        {
            InitializeComponent();

            _hotelService = new HotelService();
            _roomService = new RoomService();
            _hotelRooms = new Dictionary<int, List<RoomModel>>();

            this.Text = "Occupancy Report";

            dtpReportDate.Value = DateTime.Now;
        }

        private async void OccupancyReportForm_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                LoadingPanel.Visible = true;
                MainPanel.Visible = false;

                _hotels = await _hotelService.GetAllHotelsAsync();

                foreach (var hotel in _hotels)
                {
                    var rooms = await _roomService.GetRoomsByHotelAsync(hotel.Id);
                    _hotelRooms[hotel.Id] = rooms;
                }

                GenerateReport();

                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadingPanel.Visible = false;
                MainPanel.Visible = true;
            }
        }

        private void GenerateReport()
        {
            pnlReport.Controls.Clear();

            lblReportTitle.Text = $"Occupancy Report - {dtpReportDate.Value:MMMM yyyy}";

            var summaryPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(40, 40, 60),
                Padding = new Padding(10)
            };

            int totalRooms = 0;
            int occupiedRooms = 0;
            int availableRooms = 0;

            foreach (var kvp in _hotelRooms)
            {
                totalRooms += kvp.Value.Count;
                occupiedRooms += kvp.Value.Count(r => r.IsOccupied);
                availableRooms += kvp.Value.Count(r => !r.IsOccupied && !r.NeedsRepair);
            }

            double occupancyRate = totalRooms > 0 ? (double)occupiedRooms / totalRooms * 100 : 0;

            var lblTotalRooms = new Label
            {
                Text = $"Total Rooms: {totalRooms}",
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblTotalRooms);

            var lblOccupiedRooms = new Label
            {
                Text = $"Occupied Rooms: {occupiedRooms}",
                ForeColor = Color.Tomato,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                Location = new Point(200, 10),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblOccupiedRooms);

            var lblAvailableRooms = new Label
            {
                Text = $"Available Rooms: {availableRooms}",
                ForeColor = Color.LightGreen,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                Location = new Point(400, 10),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblAvailableRooms);

            var lblOccupancyRate = new Label
            {
                Text = $"Overall Occupancy Rate: {occupancyRate:F1}%",
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                Location = new Point(10, 40),
                AutoSize = true
            };
            summaryPanel.Controls.Add(lblOccupancyRate);

            pnlReport.Controls.Add(summaryPanel);

            var hotelContainer = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            int yPos = 10;
            foreach (var hotel in _hotels)
            {
                var hotelPanel = new Panel
                {
                    Location = new Point(10, yPos),
                    Width = hotelContainer.Width - 40,
                    Height = 180,
                    BackColor = Color.FromArgb(50, 50, 70),
                    Padding = new Padding(10),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
                };

                var hotelRooms = _hotelRooms[hotel.Id];
                int hotelTotalRooms = hotelRooms.Count;
                int hotelOccupiedRooms = hotelRooms.Count(r => r.IsOccupied);
                int hotelNeedsRepairRooms = hotelRooms.Count(r => r.NeedsRepair);
                int hotelNeedsCleaningRooms = hotelRooms.Count(r => !r.IsClean);
                int hotelAvailableRooms = hotelRooms.Count(r => !r.IsOccupied && !r.NeedsRepair && r.IsClean);
                double hotelOccupancyRate = hotelTotalRooms > 0 ? (double)hotelOccupiedRooms / hotelTotalRooms * 100 : 0;

                var lblHotelName = new Label
                {
                    Text = hotel.Name,
                    ForeColor = Color.White,
                    Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };
                hotelPanel.Controls.Add(lblHotelName);

                var lblHotelAddress = new Label
                {
                    Text = $"{hotel.City}, {hotel.Country}",
                    ForeColor = Color.LightGray,
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular),
                    Location = new Point(10, 35),
                    AutoSize = true
                };
                hotelPanel.Controls.Add(lblHotelAddress);

                var lblHotelStats = new Label
                {
                    Text = $"Total Rooms: {hotelTotalRooms}   |   Occupied: {hotelOccupiedRooms}   |   Available: {hotelAvailableRooms}   |   Needs Repair: {hotelNeedsRepairRooms}   |   Needs Cleaning: {hotelNeedsCleaningRooms}",
                    ForeColor = Color.LightGray,
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular),
                    Location = new Point(10, 60),
                    AutoSize = true
                };
                hotelPanel.Controls.Add(lblHotelStats);

                var lblHotelOccupancyRate = new Label
                {
                    Text = $"Occupancy Rate: {hotelOccupancyRate:F1}%",
                    ForeColor = Color.White,
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                    Location = new Point(10, 90),
                    AutoSize = true
                };
                hotelPanel.Controls.Add(lblHotelOccupancyRate);

                var prgHotelOccupancyRate = new ProgressBar
                {
                    Location = new Point(10, 120),
                    Width = hotelPanel.Width - 40,
                    Height = 20,
                    Value = (int)hotelOccupancyRate,
                    Minimum = 0,
                    Maximum = 100,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right
                };
                hotelPanel.Controls.Add(prgHotelOccupancyRate);

                hotelContainer.Controls.Add(hotelPanel);
                yPos += 200;
            }

            pnlReport.Controls.Add(hotelContainer);
        }

        private async void dtpReportDate_ValueChanged(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Printing functionality will be implemented in a future version.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export functionality will be implemented in a future version.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}