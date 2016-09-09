namespace TecanMCA_PipetteDiagnostic
{
    partial class InitializeForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitializeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cbEppiRacksToScan = new System.Windows.Forms.ComboBox();
            this.chkMicronicRack = new System.Windows.Forms.CheckBox();
            this.txtMicronicRackBarcode = new System.Windows.Forms.TextBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt96wellPlateBarcode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt384wellPlateBarcode2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt384wellPlateBarcode1 = new System.Windows.Forms.TextBox();
            this.butOK = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblNeedVolume = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt96wellTargetConcentration = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt384wellTargetVolume = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt96wellTargetVolume = new System.Windows.Forms.TextBox();
            this.gbMCAWash = new System.Windows.Forms.GroupBox();
            this.cbDitiPosition = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkWashStationPrimed = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbMCAWash.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Eppi Racks to scan:";
            // 
            // cbEppiRacksToScan
            // 
            this.cbEppiRacksToScan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEppiRacksToScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEppiRacksToScan.FormattingEnabled = true;
            this.cbEppiRacksToScan.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cbEppiRacksToScan.Location = new System.Drawing.Point(152, 24);
            this.cbEppiRacksToScan.Name = "cbEppiRacksToScan";
            this.cbEppiRacksToScan.Size = new System.Drawing.Size(57, 21);
            this.cbEppiRacksToScan.TabIndex = 1;
            // 
            // chkMicronicRack
            // 
            this.chkMicronicRack.AutoSize = true;
            this.chkMicronicRack.Checked = true;
            this.chkMicronicRack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMicronicRack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMicronicRack.Location = new System.Drawing.Point(6, 59);
            this.chkMicronicRack.Name = "chkMicronicRack";
            this.chkMicronicRack.Size = new System.Drawing.Size(140, 17);
            this.chkMicronicRack.TabIndex = 3;
            this.chkMicronicRack.Text = "Micronic Rack barcode:";
            this.chkMicronicRack.UseVisualStyleBackColor = true;
            this.chkMicronicRack.CheckedChanged += new System.EventHandler(this.chkMicronicRack_CheckedChanged);
            // 
            // txtMicronicRackBarcode
            // 
            this.txtMicronicRackBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMicronicRackBarcode.Location = new System.Drawing.Point(152, 57);
            this.txtMicronicRackBarcode.Name = "txtMicronicRackBarcode";
            this.txtMicronicRackBarcode.Size = new System.Drawing.Size(156, 20);
            this.txtMicronicRackBarcode.TabIndex = 4;
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(249, 451);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 5;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "96well Plate barcode:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbEppiRacksToScan);
            this.groupBox1.Controls.Add(this.txtMicronicRackBarcode);
            this.groupBox1.Controls.Add(this.chkMicronicRack);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 88);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sample Sources";
            // 
            // txt96wellPlateBarcode
            // 
            this.txt96wellPlateBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt96wellPlateBarcode.Location = new System.Drawing.Point(152, 26);
            this.txt96wellPlateBarcode.Name = "txt96wellPlateBarcode";
            this.txt96wellPlateBarcode.Size = new System.Drawing.Size(156, 20);
            this.txt96wellPlateBarcode.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt384wellPlateBarcode2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txt384wellPlateBarcode1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txt96wellPlateBarcode);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 118);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plates to create";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "384well Plate barcode 2:";
            // 
            // txt384wellPlateBarcode2
            // 
            this.txt384wellPlateBarcode2.Enabled = false;
            this.txt384wellPlateBarcode2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt384wellPlateBarcode2.Location = new System.Drawing.Point(152, 87);
            this.txt384wellPlateBarcode2.Name = "txt384wellPlateBarcode2";
            this.txt384wellPlateBarcode2.Size = new System.Drawing.Size(156, 20);
            this.txt384wellPlateBarcode2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "384well Plate barcode 1:";
            // 
            // txt384wellPlateBarcode1
            // 
            this.txt384wellPlateBarcode1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt384wellPlateBarcode1.Location = new System.Drawing.Point(152, 61);
            this.txt384wellPlateBarcode1.Name = "txt384wellPlateBarcode1";
            this.txt384wellPlateBarcode1.Size = new System.Drawing.Size(156, 20);
            this.txt384wellPlateBarcode1.TabIndex = 7;
            this.txt384wellPlateBarcode1.TextChanged += new System.EventHandler(this.txt384wellPlateBarcode1_TextChanged);
            // 
            // butOK
            // 
            this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOK.Location = new System.Drawing.Point(168, 451);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 9;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblNeedVolume);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txt96wellTargetConcentration);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txt384wellTargetVolume);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txt96wellTargetVolume);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 230);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(314, 118);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Volumes and concentration";
            // 
            // lblNeedVolume
            // 
            this.lblNeedVolume.AutoSize = true;
            this.lblNeedVolume.ForeColor = System.Drawing.Color.Red;
            this.lblNeedVolume.Location = new System.Drawing.Point(233, 25);
            this.lblNeedVolume.Name = "lblNeedVolume";
            this.lblNeedVolume.Size = new System.Drawing.Size(75, 13);
            this.lblNeedVolume.TabIndex = 13;
            this.lblNeedVolume.Text = "not enough!";
            this.lblNeedVolume.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(172, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "96well target concentration (ng/µl):";
            // 
            // txt96wellTargetConcentration
            // 
            this.txt96wellTargetConcentration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt96wellTargetConcentration.Location = new System.Drawing.Point(184, 48);
            this.txt96wellTargetConcentration.MaxLength = 3;
            this.txt96wellTargetConcentration.Name = "txt96wellTargetConcentration";
            this.txt96wellTargetConcentration.Size = new System.Drawing.Size(43, 20);
            this.txt96wellTargetConcentration.TabIndex = 11;
            this.txt96wellTargetConcentration.Text = "5";
            this.txt96wellTargetConcentration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt96wellTargetConcentration.TextChanged += new System.EventHandler(this.txt96wellTargetConcentration_TextChanged);
            this.txt96wellTargetConcentration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt96wellTargetConcentration_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "384well target volume (µl):";
            // 
            // txt384wellTargetVolume
            // 
            this.txt384wellTargetVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt384wellTargetVolume.Location = new System.Drawing.Point(184, 84);
            this.txt384wellTargetVolume.MaxLength = 3;
            this.txt384wellTargetVolume.Name = "txt384wellTargetVolume";
            this.txt384wellTargetVolume.Size = new System.Drawing.Size(43, 20);
            this.txt384wellTargetVolume.TabIndex = 9;
            this.txt384wellTargetVolume.Text = "5";
            this.txt384wellTargetVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt384wellTargetVolume.TextChanged += new System.EventHandler(this.txt384wellTargetVolume_TextChanged);
            this.txt384wellTargetVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt384wellTargetVolume_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "96well target volume (µl):";
            // 
            // txt96wellTargetVolume
            // 
            this.txt96wellTargetVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt96wellTargetVolume.Location = new System.Drawing.Point(184, 22);
            this.txt96wellTargetVolume.MaxLength = 3;
            this.txt96wellTargetVolume.Name = "txt96wellTargetVolume";
            this.txt96wellTargetVolume.Size = new System.Drawing.Size(43, 20);
            this.txt96wellTargetVolume.TabIndex = 7;
            this.txt96wellTargetVolume.Text = "45";
            this.txt96wellTargetVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt96wellTargetVolume.TextChanged += new System.EventHandler(this.txt96wellTargetVolume_TextChanged);
            this.txt96wellTargetVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt96wellTargetVolume_KeyPress);
            // 
            // gbMCAWash
            // 
            this.gbMCAWash.Controls.Add(this.cbDitiPosition);
            this.gbMCAWash.Controls.Add(this.label9);
            this.gbMCAWash.Controls.Add(this.chkWashStationPrimed);
            this.gbMCAWash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMCAWash.Location = new System.Drawing.Point(10, 354);
            this.gbMCAWash.Name = "gbMCAWash";
            this.gbMCAWash.Size = new System.Drawing.Size(314, 92);
            this.gbMCAWash.TabIndex = 11;
            this.gbMCAWash.TabStop = false;
            this.gbMCAWash.Text = "MCA Wash";
            // 
            // cbDitiPosition
            // 
            this.cbDitiPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDitiPosition.FormattingEnabled = true;
            this.cbDitiPosition.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbDitiPosition.Location = new System.Drawing.Point(81, 53);
            this.cbDitiPosition.Name = "cbDitiPosition";
            this.cbDitiPosition.Size = new System.Drawing.Size(49, 21);
            this.cbDitiPosition.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "DiTI position:";
            // 
            // chkWashStationPrimed
            // 
            this.chkWashStationPrimed.AutoSize = true;
            this.chkWashStationPrimed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWashStationPrimed.Location = new System.Drawing.Point(6, 23);
            this.chkWashStationPrimed.Name = "chkWashStationPrimed";
            this.chkWashStationPrimed.Size = new System.Drawing.Size(119, 17);
            this.chkWashStationPrimed.TabIndex = 0;
            this.chkWashStationPrimed.Text = "wash-station primed";
            this.chkWashStationPrimed.UseVisualStyleBackColor = true;
            this.chkWashStationPrimed.CheckedChanged += new System.EventHandler(this.chkWashStationPrimed_CheckedChanged);
            // 
            // InitializeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 486);
            this.Controls.Add(this.gbMCAWash);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InitializeForm";
            this.Text = "EVO MCA: Pipette Diagnostic";
            this.Load += new System.EventHandler(this.InitializeForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbMCAWash.ResumeLayout(false);
            this.gbMCAWash.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbEppiRacksToScan;
        private System.Windows.Forms.CheckBox chkMicronicRack;
        private System.Windows.Forms.TextBox txtMicronicRackBarcode;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt96wellPlateBarcode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt384wellPlateBarcode2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt384wellPlateBarcode1;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt96wellTargetConcentration;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt384wellTargetVolume;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt96wellTargetVolume;
        private System.Windows.Forms.Label lblNeedVolume;
        private System.Windows.Forms.GroupBox gbMCAWash;
        private System.Windows.Forms.CheckBox chkWashStationPrimed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbDitiPosition;
    }
}

