namespace HotelManagement.Client.Forms
{
    partial class DashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnCheckOutsToday = new System.Windows.Forms.Button();
            this.btnCheckInsToday = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.lblReservationsToday = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRevenueToday = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCustomersCount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.prgOccupancyRate = new System.Windows.Forms.ProgressBar();
            this.lblOccupancyRate = new System.Windows.Forms.Label();
            this.lblAvailableRooms = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblOccupiedRooms = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalRooms = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNewReservation = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblCurrentDateTime = new System.Windows.Forms.Label();
            this.LoadingPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadingPanel
            // 
            this.LoadingPanel.Controls.Add(this.label1);
            this.LoadingPanel.Controls.Add(this.progressBar1);
            this.LoadingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadingPanel.Location = new System.Drawing.Point(0, 0);
            this.LoadingPanel.Name = "LoadingPanel";
            this.LoadingPanel.Size = new System.Drawing.Size(800, 600);
            this.LoadingPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(339, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Se încarcă date...";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(250, 300);
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.panel5);
            this.MainPanel.Controls.Add(this.panel4);
            this.MainPanel.Controls.Add(this.panel3);
            this.MainPanel.Controls.Add(this.panel2);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(20);
            this.MainPanel.Size = new System.Drawing.Size(800, 600);
            this.MainPanel.TabIndex = 1;
            this.MainPanel.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.panel5.Controls.Add(this.btnCheckOutsToday);
            this.panel5.Controls.Add(this.btnCheckInsToday);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.lblReservationsToday);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Location = new System.Drawing.Point(430, 233);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(350, 160);
            this.panel5.TabIndex = 4;
            // 
            // btnCheckOutsToday
            // 
            this.btnCheckOutsToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnCheckOutsToday.FlatAppearance.BorderSize = 0;
            this.btnCheckOutsToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckOutsToday.ForeColor = System.Drawing.Color.White;
            this.btnCheckOutsToday.Location = new System.Drawing.Point(184, 109);
            this.btnCheckOutsToday.Name = "btnCheckOutsToday";
            this.btnCheckOutsToday.Size = new System.Drawing.Size(150, 35);
            this.btnCheckOutsToday.TabIndex = 8;
            this.btnCheckOutsToday.Text = "Check-Out astăzi";
            this.btnCheckOutsToday.UseVisualStyleBackColor = false;
            this.btnCheckOutsToday.Click += new System.EventHandler(this.btnCheckOutsToday_Click);
            // 
            // btnCheckInsToday
            // 
            this.btnCheckInsToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnCheckInsToday.FlatAppearance.BorderSize = 0;
            this.btnCheckInsToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckInsToday.ForeColor = System.Drawing.Color.White;
            this.btnCheckInsToday.Location = new System.Drawing.Point(15, 109);
            this.btnCheckInsToday.Name = "btnCheckInsToday";
            this.btnCheckInsToday.Size = new System.Drawing.Size(150, 35);
            this.btnCheckInsToday.TabIndex = 7;
            this.btnCheckInsToday.Text = "Check-In astăzi";
            this.btnCheckInsToday.UseVisualStyleBackColor = false;
            this.btnCheckInsToday.Click += new System.EventHandler(this.btnCheckInsToday_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(15, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 20);
            this.label11.TabIndex = 6;
            this.label11.Text = "Rezervări azi (total)";
            // 
            // lblReservationsToday
            // 
            this.lblReservationsToday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReservationsToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservationsToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.lblReservationsToday.Location = new System.Drawing.Point(15, 46);
            this.lblReservationsToday.Name = "lblReservationsToday";
            this.lblReservationsToday.Size = new System.Drawing.Size(322, 46);
            this.lblReservationsToday.TabIndex = 5;
            this.lblReservationsToday.Text = "0";
            this.lblReservationsToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 13);
            this.label13.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.lblRevenueToday);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(430, 97);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(350, 130);
            this.panel4.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(15, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "Venit astăzi (lei)";
            // 
            // lblRevenueToday
            // 
            this.lblRevenueToday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRevenueToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRevenueToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblRevenueToday.Location = new System.Drawing.Point(15, 46);
            this.lblRevenueToday.Name = "lblRevenueToday";
            this.lblRevenueToday.Size = new System.Drawing.Size(322, 46);
            this.lblRevenueToday.TabIndex = 5;
            this.lblRevenueToday.Text = "0";
            this.lblRevenueToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.lblCustomersCount);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Location = new System.Drawing.Point(20, 330);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(350, 130);
            this.panel3.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(15, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 6;
            this.label8.Text = "Total clienți";
            // 
            // lblCustomersCount
            // 
            this.lblCustomersCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustomersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomersCount.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblCustomersCount.Location = new System.Drawing.Point(15, 46);
            this.lblCustomersCount.Name = "lblCustomersCount";
            this.lblCustomersCount.Size = new System.Drawing.Size(322, 46);
            this.lblCustomersCount.TabIndex = 5;
            this.lblCustomersCount.Text = "0";
            this.lblCustomersCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.panel2.Controls.Add(this.prgOccupancyRate);
            this.panel2.Controls.Add(this.lblOccupancyRate);
            this.panel2.Controls.Add(this.lblAvailableRooms);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lblOccupiedRooms);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblTotalRooms);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(20, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 227);
            this.panel2.TabIndex = 1;
            // 
            // prgOccupancyRate
            // 
            this.prgOccupancyRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgOccupancyRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgOccupancyRate.ForeColor = System.Drawing.Color.DarkOrange;
            this.prgOccupancyRate.Location = new System.Drawing.Point(20, 189);
            this.prgOccupancyRate.Name = "prgOccupancyRate";
            this.prgOccupancyRate.Size = new System.Drawing.Size(310, 23);
            this.prgOccupancyRate.TabIndex = 11;
            // 
            // lblOccupancyRate
            // 
            this.lblOccupancyRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOccupancyRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccupancyRate.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblOccupancyRate.Location = new System.Drawing.Point(16, 151);
            this.lblOccupancyRate.Name = "lblOccupancyRate";
            this.lblOccupancyRate.Size = new System.Drawing.Size(321, 23);
            this.lblOccupancyRate.TabIndex = 10;
            this.lblOccupancyRate.Text = "0%";
            this.lblOccupancyRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvailableRooms
            // 
            this.lblAvailableRooms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAvailableRooms.AutoSize = true;
            this.lblAvailableRooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableRooms.ForeColor = System.Drawing.Color.LightGreen;
            this.lblAvailableRooms.Location = new System.Drawing.Point(272, 115);
            this.lblAvailableRooms.Name = "lblAvailableRooms";
            this.lblAvailableRooms.Size = new System.Drawing.Size(17, 18);
            this.lblAvailableRooms.TabIndex = 9;
            this.lblAvailableRooms.Text = "0";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.LightGray;
            this.label6.Location = new System.Drawing.Point(158, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Disponibile: ";
            // 
            // lblOccupiedRooms
            // 
            this.lblOccupiedRooms.AutoSize = true;
            this.lblOccupiedRooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccupiedRooms.ForeColor = System.Drawing.Color.Tomato;
            this.lblOccupiedRooms.Location = new System.Drawing.Point(116, 115);
            this.lblOccupiedRooms.Name = "lblOccupiedRooms";
            this.lblOccupiedRooms.Size = new System.Drawing.Size(17, 18);
            this.lblOccupiedRooms.TabIndex = 7;
            this.lblOccupiedRooms.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(20, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ocupate: ";
            // 
            // lblTotalRooms
            // 
            this.lblTotalRooms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalRooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRooms.ForeColor = System.Drawing.Color.White;
            this.lblTotalRooms.Location = new System.Drawing.Point(15, 46);
            this.lblTotalRooms.Name = "lblTotalRooms";
            this.lblTotalRooms.Size = new System.Drawing.Size(322, 46);
            this.lblTotalRooms.TabIndex = 5;
            this.lblTotalRooms.Text = "0";
            this.lblTotalRooms.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Total camere";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panel1.Controls.Add(this.btnNewReservation);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.lblCurrentDateTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 50);
            this.panel1.TabIndex = 0;
            // 
            // btnNewReservation
            // 
            this.btnNewReservation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewReservation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnNewReservation.FlatAppearance.BorderSize = 0;
            this.btnNewReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewReservation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewReservation.ForeColor = System.Drawing.Color.White;
            this.btnNewReservation.Location = new System.Drawing.Point(568, 9);
            this.btnNewReservation.Name = "btnNewReservation";
            this.btnNewReservation.Size = new System.Drawing.Size(140, 35);
            this.btnNewReservation.TabIndex = 2;
            this.btnNewReservation.Text = "Rezervare nouă";
            this.btnNewReservation.UseVisualStyleBackColor = false;
            this.btnNewReservation.Click += new System.EventHandler(this.btnNewReservation_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(100)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(714, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(35, 35);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "⟳";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblCurrentDateTime
            // 
            this.lblCurrentDateTime.AutoSize = true;
            this.lblCurrentDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentDateTime.ForeColor = System.Drawing.Color.White;
            this.lblCurrentDateTime.Location = new System.Drawing.Point(3, 16);
            this.lblCurrentDateTime.Name = "lblCurrentDateTime";
            this.lblCurrentDateTime.Size = new System.Drawing.Size(189, 20);
            this.lblCurrentDateTime.TabIndex = 0;
            this.lblCurrentDateTime.Text = "Luni, 01 Ianuarie 2000 12:00:00";
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.LoadingPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DashboardForm";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.LoadingPanel.ResumeLayout(false);
            this.LoadingPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LoadingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCurrentDateTime;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnNewReservation;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTotalRooms;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOccupiedRooms;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAvailableRooms;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblOccupancyRate;
        private System.Windows.Forms.ProgressBar prgOccupancyRate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCustomersCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRevenueToday;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblReservationsToday;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCheckOutsToday;
        private System.Windows.Forms.Button btnCheckInsToday;
    }
}