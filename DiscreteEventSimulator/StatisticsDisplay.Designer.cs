namespace DiscreteEventSimulator
{
    partial class StatisticsDisplay
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
            this.lblElapsed = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.dgvStatistics = new System.Windows.Forms.DataGridView();
            this.colStat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeObserved = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).BeginInit();
            this.SuspendLayout();
            // 
            // lblElapsed
            // 
            this.lblElapsed.AutoSize = true;
            this.lblElapsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblElapsed.Location = new System.Drawing.Point(13, 9);
            this.lblElapsed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(137, 24);
            this.lblElapsed.TabIndex = 0;
            this.lblElapsed.Text = "Elapsed Time: ";
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.AutoSize = true;
            this.lblElapsedTime.Location = new System.Drawing.Point(131, 9);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(0, 20);
            this.lblElapsedTime.TabIndex = 1;
            // 
            // dgvStatistics
            // 
            this.dgvStatistics.AllowUserToAddRows = false;
            this.dgvStatistics.AllowUserToDeleteRows = false;
            this.dgvStatistics.AllowUserToOrderColumns = true;
            this.dgvStatistics.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStatistics.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatistics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStat,
            this.colStatValue,
            this.colTimeObserved});
            this.dgvStatistics.Cursor = System.Windows.Forms.Cursors.Help;
            this.dgvStatistics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvStatistics.Location = new System.Drawing.Point(0, 53);
            this.dgvStatistics.Name = "dgvStatistics";
            this.dgvStatistics.ReadOnly = true;
            this.dgvStatistics.Size = new System.Drawing.Size(800, 400);
            this.dgvStatistics.TabIndex = 2;
            this.dgvStatistics.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatistics_CellContentClick);
            // 
            // colStat
            // 
            this.colStat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStat.HeaderText = "Statistic";
            this.colStat.Name = "colStat";
            this.colStat.ReadOnly = true;
            // 
            // colStatValue
            // 
            this.colStatValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colStatValue.HeaderText = "Value";
            this.colStatValue.Name = "colStatValue";
            this.colStatValue.ReadOnly = true;
            this.colStatValue.Width = 75;
            // 
            // colTimeObserved
            // 
            this.colTimeObserved.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTimeObserved.HeaderText = "Time Observed";
            this.colTimeObserved.Name = "colTimeObserved";
            this.colTimeObserved.ReadOnly = true;
            this.colTimeObserved.Width = 140;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(346, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Elapsed Time: ";
            // 
            // StatisticsDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 453);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvStatistics);
            this.Controls.Add(this.lblElapsedTime);
            this.Controls.Add(this.lblElapsed);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StatisticsDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Current Statistics";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StatisticsDisplay_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblElapsed;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.DataGridView dgvStatistics;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeObserved;
        private System.Windows.Forms.Label label1;
    }
}