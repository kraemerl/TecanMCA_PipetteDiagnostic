namespace TecanMCA_PipetteDiagnostic
{
    partial class ScanReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanReport));
            this.lblSampleDestination = new System.Windows.Forms.Label();
            this.lblSampleSource = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grdTransferOverview = new System.Windows.Forms.DataGridView();
            this.butContinue = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.lblTarget96Volume = new System.Windows.Forms.Label();
            this.lblTarget96Concentration = new System.Windows.Forms.Label();
            this.lblTargetConcentrationText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTarget384Volume = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patient_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sample_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.source_barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.control = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.source_volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.source_concentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destination_well = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destination_patient_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sample_needed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buffer_needed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferOverview)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSampleDestination
            // 
            this.lblSampleDestination.AutoSize = true;
            this.lblSampleDestination.Location = new System.Drawing.Point(119, 29);
            this.lblSampleDestination.Name = "lblSampleDestination";
            this.lblSampleDestination.Size = new System.Drawing.Size(25, 13);
            this.lblSampleDestination.TabIndex = 49;
            this.lblSampleDestination.Text = "???";
            // 
            // lblSampleSource
            // 
            this.lblSampleSource.AutoSize = true;
            this.lblSampleSource.Location = new System.Drawing.Point(119, 9);
            this.lblSampleSource.Name = "lblSampleSource";
            this.lblSampleSource.Size = new System.Drawing.Size(25, 13);
            this.lblSampleSource.TabIndex = 48;
            this.lblSampleSource.Text = "???";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Sample Destination:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Sample Sources:";
            // 
            // grdTransferOverview
            // 
            this.grdTransferOverview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTransferOverview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdTransferOverview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.source,
            this.patient_id,
            this.sample_id,
            this.source_barcode,
            this.category,
            this.control,
            this.source_volume,
            this.source_concentration,
            this.destination_well,
            this.destination_patient_id,
            this.sample_needed,
            this.buffer_needed});
            this.grdTransferOverview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdTransferOverview.Location = new System.Drawing.Point(12, 66);
            this.grdTransferOverview.Name = "grdTransferOverview";
            this.grdTransferOverview.ReadOnly = true;
            this.grdTransferOverview.Size = new System.Drawing.Size(942, 352);
            this.grdTransferOverview.TabIndex = 45;
            // 
            // butContinue
            // 
            this.butContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butContinue.Enabled = false;
            this.butContinue.Location = new System.Drawing.Point(798, 424);
            this.butContinue.Name = "butContinue";
            this.butContinue.Size = new System.Drawing.Size(75, 23);
            this.butContinue.TabIndex = 51;
            this.butContinue.Text = "Continue";
            this.butContinue.UseVisualStyleBackColor = true;
            this.butContinue.Click += new System.EventHandler(this.butContinue_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(879, 424);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 50;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // lblTarget96Volume
            // 
            this.lblTarget96Volume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTarget96Volume.AutoSize = true;
            this.lblTarget96Volume.Location = new System.Drawing.Point(927, 9);
            this.lblTarget96Volume.Name = "lblTarget96Volume";
            this.lblTarget96Volume.Size = new System.Drawing.Size(25, 13);
            this.lblTarget96Volume.TabIndex = 55;
            this.lblTarget96Volume.Text = "???";
            // 
            // lblTarget96Concentration
            // 
            this.lblTarget96Concentration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTarget96Concentration.AutoSize = true;
            this.lblTarget96Concentration.Location = new System.Drawing.Point(927, 26);
            this.lblTarget96Concentration.Name = "lblTarget96Concentration";
            this.lblTarget96Concentration.Size = new System.Drawing.Size(25, 13);
            this.lblTarget96Concentration.TabIndex = 54;
            this.lblTarget96Concentration.Text = "???";
            // 
            // lblTargetConcentrationText
            // 
            this.lblTargetConcentrationText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetConcentrationText.AutoSize = true;
            this.lblTargetConcentrationText.Location = new System.Drawing.Point(723, 26);
            this.lblTargetConcentrationText.Name = "lblTargetConcentrationText";
            this.lblTargetConcentrationText.Size = new System.Drawing.Size(177, 13);
            this.lblTargetConcentrationText.TabIndex = 53;
            this.lblTargetConcentrationText.Text = "96well Target Concentration (ng/µl):";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(723, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "96well Target Volume (µl):";
            // 
            // lblTarget384Volume
            // 
            this.lblTarget384Volume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTarget384Volume.AutoSize = true;
            this.lblTarget384Volume.Location = new System.Drawing.Point(927, 44);
            this.lblTarget384Volume.Name = "lblTarget384Volume";
            this.lblTarget384Volume.Size = new System.Drawing.Size(25, 13);
            this.lblTarget384Volume.TabIndex = 57;
            this.lblTarget384Volume.Text = "???";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(723, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "384well Target Volume (µl):";
            // 
            // source
            // 
            this.source.HeaderText = "source";
            this.source.Name = "source";
            this.source.ReadOnly = true;
            this.source.Width = 64;
            // 
            // patient_id
            // 
            this.patient_id.HeaderText = "patient_id";
            this.patient_id.Name = "patient_id";
            this.patient_id.ReadOnly = true;
            this.patient_id.Width = 78;
            // 
            // sample_id
            // 
            this.sample_id.HeaderText = "sample_id";
            this.sample_id.Name = "sample_id";
            this.sample_id.ReadOnly = true;
            this.sample_id.Width = 79;
            // 
            // source_barcode
            // 
            this.source_barcode.HeaderText = "source BC";
            this.source_barcode.Name = "source_barcode";
            this.source_barcode.ReadOnly = true;
            this.source_barcode.Width = 81;
            // 
            // category
            // 
            this.category.HeaderText = "category";
            this.category.Name = "category";
            this.category.ReadOnly = true;
            this.category.Width = 73;
            // 
            // control
            // 
            this.control.HeaderText = "control";
            this.control.Name = "control";
            this.control.ReadOnly = true;
            this.control.Width = 64;
            // 
            // source_volume
            // 
            this.source_volume.HeaderText = "volume (µl)";
            this.source_volume.Name = "source_volume";
            this.source_volume.ReadOnly = true;
            this.source_volume.Width = 83;
            // 
            // source_concentration
            // 
            this.source_concentration.HeaderText = "conc (ng/µl)";
            this.source_concentration.Name = "source_concentration";
            this.source_concentration.ReadOnly = true;
            this.source_concentration.Width = 90;
            // 
            // destination_well
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.destination_well.DefaultCellStyle = dataGridViewCellStyle1;
            this.destination_well.HeaderText = "well";
            this.destination_well.Name = "destination_well";
            this.destination_well.ReadOnly = true;
            this.destination_well.Width = 50;
            // 
            // destination_patient_id
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.destination_patient_id.DefaultCellStyle = dataGridViewCellStyle2;
            this.destination_patient_id.HeaderText = "patient_id";
            this.destination_patient_id.Name = "destination_patient_id";
            this.destination_patient_id.ReadOnly = true;
            this.destination_patient_id.Width = 78;
            // 
            // sample_needed
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sample_needed.DefaultCellStyle = dataGridViewCellStyle3;
            this.sample_needed.HeaderText = "sample (µl)";
            this.sample_needed.Name = "sample_needed";
            this.sample_needed.ReadOnly = true;
            this.sample_needed.Width = 82;
            // 
            // buffer_needed
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buffer_needed.DefaultCellStyle = dataGridViewCellStyle4;
            this.buffer_needed.HeaderText = "TE (µl)";
            this.buffer_needed.Name = "buffer_needed";
            this.buffer_needed.ReadOnly = true;
            this.buffer_needed.Width = 63;
            // 
            // ScanReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 459);
            this.Controls.Add(this.lblTarget384Volume);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTarget96Volume);
            this.Controls.Add(this.lblTarget96Concentration);
            this.Controls.Add(this.lblTargetConcentrationText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.butContinue);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.lblSampleDestination);
            this.Controls.Add(this.lblSampleSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdTransferOverview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScanReport";
            this.Text = "EVO MCA: ScanReport";
            this.Load += new System.EventHandler(this.ScanReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferOverview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSampleDestination;
        private System.Windows.Forms.Label lblSampleSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butContinue;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Label lblTarget96Volume;
        private System.Windows.Forms.Label lblTarget96Concentration;
        private System.Windows.Forms.Label lblTargetConcentrationText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTarget384Volume;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView grdTransferOverview;
        private System.Windows.Forms.DataGridViewTextBoxColumn source;
        private System.Windows.Forms.DataGridViewTextBoxColumn patient_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn sample_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn source_barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn control;
        private System.Windows.Forms.DataGridViewTextBoxColumn source_volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn source_concentration;
        private System.Windows.Forms.DataGridViewTextBoxColumn destination_well;
        private System.Windows.Forms.DataGridViewTextBoxColumn destination_patient_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn sample_needed;
        private System.Windows.Forms.DataGridViewTextBoxColumn buffer_needed;
    }
}