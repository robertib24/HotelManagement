namespace HotelManagement.Client.Forms
{
    partial class HotelForm
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
            this.panelRoomInfo = new System.Windows.Forms.Panel();
            this.btnViewRooms = new System.Windows.Forms.Button();
            this.lblAvailableRooms = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTotalRooms = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numStars = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.LoadingPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.panelRoomInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStars)).BeginInit();
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
            this.MainPanel.Controls.Add(this.panelRoomInfo);
            this.MainPanel.Controls.Add(this.btnCancel);
            this.MainPanel.Controls.Add(this.btnSave);
            this.MainPanel.Controls.Add(this.chkIsActive);
            this.MainPanel.Controls.Add(this.txtEmail);
            this.MainPanel.Controls.Add(this.label8);
            this.MainPanel.Controls.Add(this.txtPhone);
            this.MainPanel.Controls.Add(this.label7);
            this.MainPanel.Controls.Add(this.numStars);
            this.MainPanel.Controls.Add(this.label6);
            this.MainPanel.Controls.Add(this.txtCountry);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.txtCity);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.txtAddress);
            this.MainPanel.Controls.Add(this.label10);
            this.MainPanel.Controls.Add(this.txtName);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.lblTitle);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Padding = new System.Windows.Forms.Padding(40);
            this.MainPanel.Size = new System.Drawing.Size(800, 600);
            this.MainPanel.TabIndex = 1;
            // 
            // panelRoomInfo
            // 
            this.panelRoomInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRoomInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRoomInfo.Controls.Add(this.btnViewRooms);
            this.panelRoomInfo.Controls.Add(this.lblAvailableRooms);
            this.panelRoomInfo.Controls.Add(this.label11);
            this.panelRoomInfo.Controls.Add(this.lblTotalRooms);
            this.panelRoomInfo.Controls.Add(this.label9);
            this.panelRoomInfo.Controls.Add(this.label5);
            this.panelRoomInfo.Location = new System.Drawing.Point(503, 88);
            this.panelRoomInfo.Name = "panelRoomInfo";
            this.panelRoomInfo.Size = new System.Drawing.Size(254, 181);
            this.panelRoomInfo.TabIndex = 22;
            this.panelRoomInfo.Visible = false;
            // 
            // btnViewRooms
            // 
            this.btnViewRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(100)))));
            this.btnViewRooms.FlatAppearance.BorderSize = 0;
            this.btnViewRooms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewRooms.ForeColor = System.Drawing.Color.White;
            this.btnViewRooms.Location = new System.Drawing.Point(52, 133);
            this.btnViewRooms.Name = "btnViewRooms";
            this.btnViewRooms.Size = new System.Drawing.Size(141, 34);
            this.btnViewRooms.TabIndex = 5;
            this.btnViewRooms.Text = "Vezi camere";
            this.btnViewRooms.UseVisualStyleBackColor = false;
            this.btnViewRooms.Click += new System.EventHandler(this.btnViewRooms_Click);
            // 
            // lblAvailableRooms
            // 
            this.lblAvailableRooms.AutoSize = true;
            this.lblAvailableRooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableRooms.ForeColor = System.Drawing.Color.White;
            this.lblAvailableRooms.Location = new System.Drawing.Point(135, 87);
            this.lblAvailableRooms.Name = "lblAvailableRooms";
            this.lblAvailableRooms.Size = new System.Drawing.Size(19, 20);
            this.lblAvailableRooms.TabIndex = 4;
            this.lblAvailableRooms.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(14, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Camere disponibile:";
            // 
            // lblTotalRooms
            // 
            this.lblTotalRooms.AutoSize = true;
            this.lblTotalRooms.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRooms.ForeColor = System.Drawing.Color.White;
            this.lblTotalRooms.Location = new System.Drawing.Point(135, 54);
            this.lblTotalRooms.Name = "lblTotalRooms";
            this.lblTotalRooms.Size = new System.Drawing.Size(19, 20);
            this.lblTotalRooms.TabIndex = 2;
            this.lblTotalRooms.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(14, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Total camere:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(71, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Informații camere";
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
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.ForeColor = System.Drawing.Color.White;
            this.chkIsActive.Location = new System.Drawing.Point(157, 380);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(55, 17);
            this.chkIsActive.TabIndex = 19;
            this.chkIsActive.Text = "Activ";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(157, 345);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(280, 20);
            this.txtEmail.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(43, 348);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Email:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(157, 309);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(280, 20);
            this.txtPhone.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(43, 312);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Telefon:";
            // 
            // numStars
            // 
            this.numStars.Location = new System.Drawing.Point(157, 272);
            this.numStars.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numStars.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStars.Name = "numStars";
            this.numStars.Size = new System.Drawing.Size(58, 20);
            this.numStars.TabIndex = 14;
            this.numStars.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(43, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Stele:";
            // 
            // txtCountry
            // 
            this.txtCountry.Location = new System.Drawing.Point(157, 236);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(280, 20);
            this.txtCountry.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(43, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Țară:";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(157, 200);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(280, 20);
            this.txtCity.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(43, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Oraș:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(157, 164);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(280, 20);
            this.txtAddress.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(43, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Adresă:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(157, 128);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(280, 20);
            this.txtName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(43, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nume hotel:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(42, 49);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(148, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Adaugă hotel nou";
            // 
            // HotelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.LoadingPanel);
            this.Name = "HotelForm";
            this.Text = "Adaugă hotel";
            this.Load += new System.EventHandler(this.HotelForm_Load);
            this.LoadingPanel.ResumeLayout(false);
            this.LoadingPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.panelRoomInfo.ResumeLayout(false);
            this.panelRoomInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LoadingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numStars;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panelRoomInfo;
        private System.Windows.Forms.Button btnViewRooms;
        private System.Windows.Forms.Label lblAvailableRooms;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTotalRooms;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
    }//
}