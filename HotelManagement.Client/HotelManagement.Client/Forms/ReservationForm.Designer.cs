namespace HotelManagement.Client.Forms
{
    partial class ReservationForm
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
            this.lblAvailableRooms = new System.Windows.Forms.Label();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.radCancelled = new System.Windows.Forms.RadioButton();
            this.radCompleted = new System.Windows.Forms.RadioButton();
            this.radCheckedIn = new System.Windows.Forms.RadioButton();
            this.radConfirmed = new System.Windows.Forms.RadioButton();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblNights = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numGuests = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbRoom = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRoomType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbHotel = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.LoadingPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.grpStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGuests)).BeginInit();
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
            this.MainPanel.Controls.Add(this.lblAvailableRooms);
            this.MainPanel.Controls.Add(this.btnAddCustomer);
            this.MainPanel.Controls.Add(this.grpStatus);
            this.MainPanel.Controls.Add(this.lblTotalPrice);
            this.MainPanel.Controls.Add(this.lblPrice);
            this.MainPanel.Controls.Add(this.lblNights);
            this.MainPanel.Controls.Add(this.btnCancel);
            this.MainPanel.Controls.Add(this.btnSave);
            this.MainPanel.Controls.Add(this.txtNotes);
            this.MainPanel.Controls.Add(this.label8);
            this.MainPanel.Controls.Add(this.numGuests);
            this.MainPanel.Controls.Add(this.label7);
            this.MainPanel.Controls.Add(this.dtpCheckOut);
            this.MainPanel.Controls.Add(this.label6);
            this.MainPanel.Controls.Add(this.dtpCheckIn);
            this.MainPanel.Controls.Add(this.label5);
            this.MainPanel.Controls.Add(this.cmbRoom);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.cmbRoomType);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.cmbHotel);
            this.MainPanel.Controls.Add(this.label10);
            this.MainPanel.Controls.Add(this.cmbCustomer);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.lblTitle);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(40);
            this.MainPanel.Size = new System.Drawing.Size(800, 600);
            this.MainPanel.TabIndex = 1;
            // 
            // lblAvailableRooms
            // 
            this.lblAvailableRooms.AutoSize = true;
            this.lblAvailableRooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableRooms.ForeColor = System.Drawing.Color.White;
            this.lblAvailableRooms.Location = new System.Drawing.Point(157, 264);
            this.lblAvailableRooms.Name = "lblAvailableRooms";
            this.lblAvailableRooms.Size = new System.Drawing.Size(127, 15);
            this.lblAvailableRooms.TabIndex = 30;
            this.lblAvailableRooms.Text = "Camere disponibile: 0";
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnAddCustomer.FlatAppearance.BorderSize = 0;
            this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCustomer.ForeColor = System.Drawing.Color.White;
            this.btnAddCustomer.Location = new System.Drawing.Point(455, 125);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(30, 25);
            this.btnAddCustomer.TabIndex = 29;
            this.btnAddCustomer.Text = "+";
            this.btnAddCustomer.UseVisualStyleBackColor = false;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.radCancelled);
            this.grpStatus.Controls.Add(this.radCompleted);
            this.grpStatus.Controls.Add(this.radCheckedIn);
            this.grpStatus.Controls.Add(this.radConfirmed);
            this.grpStatus.ForeColor = System.Drawing.Color.White;
            this.grpStatus.Location = new System.Drawing.Point(491, 306);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(266, 154);
            this.grpStatus.TabIndex = 28;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status rezervare";
            // 
            // radCancelled
            // 
            this.radCancelled.AutoSize = true;
            this.radCancelled.Location = new System.Drawing.Point(22, 121);
            this.radCancelled.Name = "radCancelled";
            this.radCancelled.Size = new System.Drawing.Size(66, 17);
            this.radCancelled.TabIndex = 3;
            this.radCancelled.Text = "Anulată";
            this.radCancelled.UseVisualStyleBackColor = true;
            // 
            // radCompleted
            // 
            this.radCompleted.AutoSize = true;
            this.radCompleted.Location = new System.Drawing.Point(22, 89);
            this.radCompleted.Name = "radCompleted";
            this.radCompleted.Size = new System.Drawing.Size(78, 17);
            this.radCompleted.TabIndex = 2;
            this.radCompleted.Text = "Finalizată";
            this.radCompleted.UseVisualStyleBackColor = true;
            // 
            // radCheckedIn
            // 
            this.radCheckedIn.AutoSize = true;
            this.radCheckedIn.Location = new System.Drawing.Point(22, 57);
            this.radCheckedIn.Name = "radCheckedIn";
            this.radCheckedIn.Size = new System.Drawing.Size(124, 17);
            this.radCheckedIn.TabIndex = 1;
            this.radCheckedIn.Text = "Check-In efectuat";
            this.radCheckedIn.UseVisualStyleBackColor = true;
            // 
            // radConfirmed
            // 
            this.radConfirmed.AutoSize = true;
            this.radConfirmed.Checked = true;
            this.radConfirmed.Location = new System.Drawing.Point(22, 25);
            this.radConfirmed.Name = "radConfirmed";
            this.radConfirmed.Size = new System.Drawing.Size(88, 17);
            this.radConfirmed.TabIndex = 0;
            this.radConfirmed.TabStop = true;
            this.radConfirmed.Text = "Confirmată";
            this.radConfirmed.UseVisualStyleBackColor = true;
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.AutoSize = true;
            this.lblTotalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblTotalPrice.Location = new System.Drawing.Point(490, 264);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(119, 20);
            this.lblTotalPrice.TabIndex = 27;
            this.lblTotalPrice.Text = "Total: 0.00 LEI";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.White;
            this.lblPrice.Location = new System.Drawing.Point(584, 236);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(80, 16);
            this.lblPrice.TabIndex = 26;
            this.lblPrice.Text = "Preț: 0.00 LEI";
            // 
            // lblNights
            // 
            this.lblNights.AutoSize = true;
            this.lblNights.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNights.ForeColor = System.Drawing.Color.White;
            this.lblNights.Location = new System.Drawing.Point(491, 236);
            this.lblNights.Name = "lblNights";
            this.lblNights.Size = new System.Drawing.Size(53, 16);
            this.lblNights.TabIndex = 25;
            this.lblNights.Text = "Nopți: 1";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(517, 515);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 35);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Anulează";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(647, 515);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 35);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Salvează";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(157, 306);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(328, 154);
            this.txtNotes.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(43, 309);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Note:";
            // 
            // numGuests
            // 
            this.numGuests.Location = new System.Drawing.Point(691, 194);
            this.numGuests.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numGuests.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGuests.Name = "numGuests";
            this.numGuests.Size = new System.Drawing.Size(47, 20);
            this.numGuests.TabIndex = 17;
            this.numGuests.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(557, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Număr persoane cazate:";
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckOut.Location = new System.Drawing.Point(691, 161);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(100, 20);
            this.dtpCheckOut.TabIndex = 15;
            this.dtpCheckOut.ValueChanged += new System.EventHandler(this.dtpCheckOut_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(557, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Data check-out:";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckIn.Location = new System.Drawing.Point(691, 126);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(100, 20);
            this.dtpCheckIn.TabIndex = 13;
            this.dtpCheckIn.ValueChanged += new System.EventHandler(this.dtpCheckIn_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(557, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Data check-in:";
            // 
            // cmbRoom
            // 
            this.cmbRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoom.FormattingEnabled = true;
            this.cmbRoom.Location = new System.Drawing.Point(157, 233);
            this.cmbRoom.Name = "cmbRoom";
            this.cmbRoom.Size = new System.Drawing.Size(280, 21);
            this.cmbRoom.TabIndex = 11;
            this.cmbRoom.SelectedIndexChanged += new System.EventHandler(this.cmbRoom_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(43, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Cameră:";
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Location = new System.Drawing.Point(157, 197);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(280, 21);
            this.cmbRoomType.TabIndex = 9;
            this.cmbRoomType.SelectedIndexChanged += new System.EventHandler(this.cmbRoomType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(43, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tip cameră:";
            // 
            // cmbHotel
            // 
            this.cmbHotel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHotel.FormattingEnabled = true;
            this.cmbHotel.Location = new System.Drawing.Point(157, 161);
            this.cmbHotel.Name = "cmbHotel";
            this.cmbHotel.Size = new System.Drawing.Size(280, 21);
            this.cmbHotel.TabIndex = 7;
            this.cmbHotel.SelectedIndexChanged += new System.EventHandler(this.cmbHotel_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(43, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Hotel:";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(157, 125);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(280, 21);
            this.cmbCustomer.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(43, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Client:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(42, 49);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(197, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Adaugă rezervare nouă";
            // 
            // ReservationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.LoadingPanel);
            this.Name = "ReservationForm";
            this.Text = "Adaugă rezervare";
            this.Load += new System.EventHandler(this.ReservationForm_Load);
            this.LoadingPanel.ResumeLayout(false);
            this.LoadingPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGuests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LoadingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbHotel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbRoomType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbRoom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numGuests;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblNights;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblTotalPrice;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.RadioButton radCancelled;
        private System.Windows.Forms.RadioButton radCompleted;
        private System.Windows.Forms.RadioButton radCheckedIn;
        private System.Windows.Forms.RadioButton radConfirmed;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.Label lblAvailableRooms;
    }
}