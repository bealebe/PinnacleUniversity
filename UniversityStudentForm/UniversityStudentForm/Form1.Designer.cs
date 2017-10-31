namespace PinnacleUniversity
{
    partial class frmUniversityStudents
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
            this.dgvStudentsOverview = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAutoDrop = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentsOverview)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStudentsOverview
            // 
            this.dgvStudentsOverview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStudentsOverview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudentsOverview.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvStudentsOverview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudentsOverview.Location = new System.Drawing.Point(12, 12);
            this.dgvStudentsOverview.MultiSelect = false;
            this.dgvStudentsOverview.Name = "dgvStudentsOverview";
            this.dgvStudentsOverview.ReadOnly = true;
            this.dgvStudentsOverview.RowHeadersVisible = false;
            this.dgvStudentsOverview.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvStudentsOverview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudentsOverview.Size = new System.Drawing.Size(1221, 497);
            this.dgvStudentsOverview.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1157, 519);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAutoDrop
            // 
            this.btnAutoDrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoDrop.Location = new System.Drawing.Point(1041, 519);
            this.btnAutoDrop.Name = "btnAutoDrop";
            this.btnAutoDrop.Size = new System.Drawing.Size(110, 23);
            this.btnAutoDrop.TabIndex = 4;
            this.btnAutoDrop.Text = "Initiate Auto Drop";
            this.btnAutoDrop.UseVisualStyleBackColor = true;
            this.btnAutoDrop.Click += new System.EventHandler(this.btnAutoDrop_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(12, 515);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(264, 34);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 5;
            this.progressBar.UseWaitCursor = true;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Location = new System.Drawing.Point(282, 527);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(54, 13);
            this.lblProgress.TabIndex = 6;
            this.lblProgress.Text = "Loading...";
            // 
            // frmUniversityStudents
            // 
            this.AcceptButton = this.btnAutoDrop;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 554);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnAutoDrop);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvStudentsOverview);
            this.Name = "frmUniversityStudents";
            this.ShowIcon = false;
            this.Text = "University Students";
            this.Load += new System.EventHandler(this.frmUniversityStudents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentsOverview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStudentsOverview;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAutoDrop;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
    }
}

