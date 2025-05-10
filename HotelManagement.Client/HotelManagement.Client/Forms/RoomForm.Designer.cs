namespace HotelManagement.Client.Forms
{
    partial class RoomForm
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
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.chkNeedsRepair = new System.Windows.Forms.CheckBox();
            this.chkIsClean = new System.Windows.Forms.CheckBox();
            this.chkIsOccupied = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numFloor = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbRoomType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbHotel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRoomNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.LoadingPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.grpStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFloor)).BeginInit();
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
            this.MainPanel.Controls.Add(this.grpStatus);
            this.MainPanel.Controls.Add(this.btnCancel);
            this.MainPanel.Controls.Add(this.btnSave);
            this.MainPanel.Controls.Add(this.lblPrice);
            this.MainPanel.Controls.Add(this.txtNotes);
            this.MainPanel.Controls.Add(this.label8);
            this.MainPanel.Controls.Add(this.numFloor);
            this.MainPanel.Controls.Add(this.label6);
            this.MainPanel.Controls.Add(this.cmbRoomType);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.cmbHotel);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.txtRoomNumber);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.lblTitle);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(40);
            this.MainPanel.Size = new System.Drawing.Size(800, 600);
            this.MainPanel.TabIndex = 1;
            // 
            // grpStatus
            // 
            this.grpStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStatus.Controls.Add(this.chkNeedsRepair);
            this.grpStatus.Controls.Add(this.chkIsClean);
            this.grpStatus.Controls.Add(this.chkIsOccupied);
            this.grpStatus.ForeColor = System.Drawing.Color.White;
            this.grpStatus.Location = new System.Drawing.Point(503, 100);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(254, 150);
            this.grpStatus.TabIndex = 22;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status cameră";
            // 
            // chkNeedsRepair
            // 
            this.chkNeedsRepair.AutoSize = true;
            this.chkNeedsRepair.Location = new System.Drawing.Point(27, 109);
            this.chkNeedsRepair.Name = "chkNeedsRepair";
            this.chkNeedsRepair.Size = new System.Drawing.Size(108, 17);
            this.chkNeedsRepair.TabIndex = 2;
            this.chkNeedsRepair.Text = "Necesită reparații";
            this.chkNeedsRepair.UseVisualStyleBackColor = true;
            // 
            // chkIsClean
            // 
            this.chkIsClean.AutoSize = true;
            this.chkIsClean.Location = new System.Drawing.Point(27, 71);
            this.chkIsClean.Name = "chkIsClean";
            this.chkIsClean.Size = new System.Drawing.Size(58, 17);
            this.chkIsClean.TabIndex = 1;
            this.chkIsClean.Text = "Curată";
            this.chkIsClean.UseVisualStyleBackColor = true;
            // 
            // chkIsOccupied
            // 
            this.chkIsOccupied.AutoSize = true;
            this.chkIsOccupied.Location = new System.Drawing.Point(27, 34);
            this.chkIsOccupied.Name = "chkIsOccupied";
            this.chkIsOccupied.Size = new System.Drawing.Size(68, 17);
            this.chkIsOccupied.TabIndex = 0;
            this.chkIsOccupied.Text = "Ocupată";
            this.chkIsOccupied.UseVisualStyleBackColor = true;
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
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblPrice.Location = new System.Drawing.Point(157, 267);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(78, 16);
            this.lblPrice.TabIndex = 19;
            this.lblPrice.Text = "Preț: 0 LEI";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(157, 296);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(280, 100);
            this.txtNotes.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(43, 299);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Note:";
            // 
            // numFloor
            // 
            this.numFloor.Location = new System.Drawing.Point(157, 233);
            this.numFloor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFloor.Name = "numFloor";
            this.numFloor.Size = new System.Drawing.Size(58, 20);
            this.numFloor.TabIndex = 14;
            this.numFloor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(43, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Etaj:";
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Location = new System.Drawing.Point(157, 197);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(280, 21);
            this.cmbRoomType.TabIndex = 8;
            this.cmbRoomType.SelectedIndexChanged += new System.EventHandler(this.cmbRoomType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(43, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tip cameră:";
            // 
            // cmbHotel
            // 
            this.cmbHotel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHotel.FormattingEnabled = true;
            this.cmbHotel.Location = new System.Drawing.Point(157, 161);
            this.cmbHotel.Name = "cmbHotel";
            this.cmbHotel.Size = new System.Drawing.Size(280, 21);
            this.cmbHotel.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(43, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hotel:";
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.Location = new System.Drawing.Point(157, 128);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.Size = new System.Drawing.Size(280, 20);
            this.txtRoomNumber.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(43, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Număr cameră:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(42, 49);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(179, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Adaugă cameră nouă";
            // 
            // RoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.LoadingPanel);
            this.Name = "RoomForm";
            this.Text = "Adaugă cameră";
            this.Load += new System.EventHandler(this.RoomForm_Load);
            this.LoadingPanel.ResumeLayout(false);
            this.LoadingPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFloor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LoadingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtRoomNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbHotel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbRoomType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numFloor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.CheckBox chkNeedsRepair;
        private System.Windows.Forms.CheckBox chkIsClean;
        private System.Windows.Forms.CheckBox chkIsOccupied;
    }
}