namespace DiscreteEventSimulator
{
    partial class RunningDisplay
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
            this.tabPageCalendar = new System.Windows.Forms.TabPage();
            this.dgvCalendar = new System.Windows.Forms.DataGridView();
            this.colEntity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeginWait = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcDisplay = new System.Windows.Forms.TabControl();
            this.tabGraphical = new System.Windows.Forms.TabPage();
            this.lblBeingServiced = new System.Windows.Forms.Label();
            this.lblQueues = new System.Windows.Forms.Label();
            this.lblNextSwitch = new System.Windows.Forms.Label();
            this.pnlCallArrive = new System.Windows.Forms.Panel();
            this.lblCallArrive = new System.Windows.Forms.Label();
            this.pnlSalesReps = new System.Windows.Forms.Panel();
            this.pnlQueues = new System.Windows.Forms.Panel();
            this.pnlSwitch = new System.Windows.Forms.Panel();
            this.lblArrow1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSystemTime = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.tabPageCalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).BeginInit();
            this.tcDisplay.SuspendLayout();
            this.tabGraphical.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageCalendar
            // 
            this.tabPageCalendar.Controls.Add(this.dgvCalendar);
            this.tabPageCalendar.Location = new System.Drawing.Point(4, 27);
            this.tabPageCalendar.Name = "tabPageCalendar";
            this.tabPageCalendar.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCalendar.Size = new System.Drawing.Size(868, 508);
            this.tabPageCalendar.TabIndex = 0;
            this.tabPageCalendar.Text = "Calendar";
            this.tabPageCalendar.UseVisualStyleBackColor = true;
            // 
            // dgvCalendar
            // 
            this.dgvCalendar.AllowUserToAddRows = false;
            this.dgvCalendar.AllowUserToDeleteRows = false;
            this.dgvCalendar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalendar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEntity,
            this.colEvent,
            this.colEventTime,
            this.colProductType,
            this.colStartTime,
            this.colBeginWait});
            this.dgvCalendar.Location = new System.Drawing.Point(3, 3);
            this.dgvCalendar.Name = "dgvCalendar";
            this.dgvCalendar.ReadOnly = true;
            this.dgvCalendar.Size = new System.Drawing.Size(865, 502);
            this.dgvCalendar.TabIndex = 1;
            // 
            // colEntity
            // 
            this.colEntity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colEntity.HeaderText = "Entity";
            this.colEntity.Name = "colEntity";
            this.colEntity.ReadOnly = true;
            this.colEntity.Width = 75;
            // 
            // colEvent
            // 
            this.colEvent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEvent.HeaderText = "Event";
            this.colEvent.Name = "colEvent";
            this.colEvent.ReadOnly = true;
            // 
            // colEventTime
            // 
            this.colEventTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colEventTime.HeaderText = "Event Time";
            this.colEventTime.Name = "colEventTime";
            this.colEventTime.ReadOnly = true;
            this.colEventTime.Width = 105;
            // 
            // colProductType
            // 
            this.colProductType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colProductType.HeaderText = "Product Type";
            this.colProductType.Name = "colProductType";
            this.colProductType.ReadOnly = true;
            this.colProductType.Width = 122;
            // 
            // colStartTime
            // 
            this.colStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colStartTime.HeaderText = "Start Time";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.ReadOnly = true;
            this.colStartTime.Width = 98;
            // 
            // colBeginWait
            // 
            this.colBeginWait.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colBeginWait.HeaderText = "Begin Wait";
            this.colBeginWait.Name = "colBeginWait";
            this.colBeginWait.ReadOnly = true;
            this.colBeginWait.Width = 105;
            // 
            // tcDisplay
            // 
            this.tcDisplay.Controls.Add(this.tabGraphical);
            this.tcDisplay.Controls.Add(this.tabPageCalendar);
            this.tcDisplay.Location = new System.Drawing.Point(1, 45);
            this.tcDisplay.Name = "tcDisplay";
            this.tcDisplay.SelectedIndex = 0;
            this.tcDisplay.Size = new System.Drawing.Size(876, 539);
            this.tcDisplay.TabIndex = 0;
            // 
            // tabGraphical
            // 
            this.tabGraphical.Controls.Add(this.lblBeingServiced);
            this.tabGraphical.Controls.Add(this.lblQueues);
            this.tabGraphical.Controls.Add(this.lblNextSwitch);
            this.tabGraphical.Controls.Add(this.pnlCallArrive);
            this.tabGraphical.Controls.Add(this.lblCallArrive);
            this.tabGraphical.Controls.Add(this.pnlSalesReps);
            this.tabGraphical.Controls.Add(this.pnlQueues);
            this.tabGraphical.Controls.Add(this.pnlSwitch);
            this.tabGraphical.Controls.Add(this.lblArrow1);
            this.tabGraphical.Controls.Add(this.label1);
            this.tabGraphical.Location = new System.Drawing.Point(4, 27);
            this.tabGraphical.Name = "tabGraphical";
            this.tabGraphical.Padding = new System.Windows.Forms.Padding(3);
            this.tabGraphical.Size = new System.Drawing.Size(868, 508);
            this.tabGraphical.TabIndex = 1;
            this.tabGraphical.Text = "Visual Display";
            this.tabGraphical.UseVisualStyleBackColor = true;
            // 
            // lblBeingServiced
            // 
            this.lblBeingServiced.AutoSize = true;
            this.lblBeingServiced.Location = new System.Drawing.Point(746, 479);
            this.lblBeingServiced.Name = "lblBeingServiced";
            this.lblBeingServiced.Size = new System.Drawing.Size(122, 18);
            this.lblBeingServiced.TabIndex = 7;
            this.lblBeingServiced.Text = "Being Serviced";
            // 
            // lblQueues
            // 
            this.lblQueues.AutoSize = true;
            this.lblQueues.Location = new System.Drawing.Point(398, 479);
            this.lblQueues.Name = "lblQueues";
            this.lblQueues.Size = new System.Drawing.Size(146, 18);
            this.lblQueues.TabIndex = 6;
            this.lblQueues.Text = "Waiting In Queues";
            // 
            // lblNextSwitch
            // 
            this.lblNextSwitch.AutoSize = true;
            this.lblNextSwitch.Location = new System.Drawing.Point(153, 283);
            this.lblNextSwitch.Name = "lblNextSwitch";
            this.lblNextSwitch.Size = new System.Drawing.Size(66, 36);
            this.lblNextSwitch.TabIndex = 5;
            this.lblNextSwitch.Text = "Switch \r\nProcess";
            this.lblNextSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCallArrive
            // 
            this.pnlCallArrive.Location = new System.Drawing.Point(6, 191);
            this.pnlCallArrive.Name = "pnlCallArrive";
            this.pnlCallArrive.Size = new System.Drawing.Size(94, 89);
            this.pnlCallArrive.TabIndex = 2;
            // 
            // lblCallArrive
            // 
            this.lblCallArrive.AutoSize = true;
            this.lblCallArrive.Location = new System.Drawing.Point(13, 283);
            this.lblCallArrive.Name = "lblCallArrive";
            this.lblCallArrive.Size = new System.Drawing.Size(72, 54);
            this.lblCallArrive.TabIndex = 4;
            this.lblCallArrive.Text = "Next \r\nArriving \r\nCall";
            this.lblCallArrive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSalesReps
            // 
            this.pnlSalesReps.Location = new System.Drawing.Point(775, 17);
            this.pnlSalesReps.Name = "pnlSalesReps";
            this.pnlSalesReps.Size = new System.Drawing.Size(83, 459);
            this.pnlSalesReps.TabIndex = 3;
            // 
            // pnlQueues
            // 
            this.pnlQueues.AutoScroll = true;
            this.pnlQueues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQueues.Location = new System.Drawing.Point(266, 18);
            this.pnlQueues.Name = "pnlQueues";
            this.pnlQueues.Size = new System.Drawing.Size(482, 458);
            this.pnlQueues.TabIndex = 2;
            // 
            // pnlSwitch
            // 
            this.pnlSwitch.Location = new System.Drawing.Point(137, 191);
            this.pnlSwitch.Name = "pnlSwitch";
            this.pnlSwitch.Size = new System.Drawing.Size(94, 89);
            this.pnlSwitch.TabIndex = 1;
            // 
            // lblArrow1
            // 
            this.lblArrow1.AutoSize = true;
            this.lblArrow1.Font = new System.Drawing.Font("Lucida Sans", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArrow1.Location = new System.Drawing.Point(92, 204);
            this.lblArrow1.Name = "lblArrow1";
            this.lblArrow1.Size = new System.Drawing.Size(59, 45);
            this.lblArrow1.TabIndex = 8;
            this.lblArrow1.Text = "→";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 45);
            this.label1.TabIndex = 9;
            this.label1.Text = "→";
            // 
            // lblSystemTime
            // 
            this.lblSystemTime.AutoSize = true;
            this.lblSystemTime.Location = new System.Drawing.Point(2, 9);
            this.lblSystemTime.Name = "lblSystemTime";
            this.lblSystemTime.Size = new System.Drawing.Size(107, 18);
            this.lblSystemTime.TabIndex = 1;
            this.lblSystemTime.Text = "Current Time:";
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Location = new System.Drawing.Point(115, 9);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(0, 18);
            this.lblClock.TabIndex = 2;
            // 
            // RunningDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 580);
            this.Controls.Add(this.lblClock);
            this.Controls.Add(this.lblSystemTime);
            this.Controls.Add(this.tcDisplay);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RunningDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Current Simulation State";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RunningDisplay_FormClosing);
            this.tabPageCalendar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).EndInit();
            this.tcDisplay.ResumeLayout(false);
            this.tabGraphical.ResumeLayout(false);
            this.tabGraphical.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageCalendar;
        private System.Windows.Forms.DataGridView dgvCalendar;
        private System.Windows.Forms.TabControl tcDisplay;
        private System.Windows.Forms.Label lblSystemTime;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeginWait;
        private System.Windows.Forms.TabPage tabGraphical;
        private System.Windows.Forms.Label lblBeingServiced;
        private System.Windows.Forms.Label lblQueues;
        private System.Windows.Forms.Label lblNextSwitch;
        private System.Windows.Forms.Panel pnlCallArrive;
        private System.Windows.Forms.Label lblCallArrive;
        private System.Windows.Forms.Panel pnlSalesReps;
        private System.Windows.Forms.Panel pnlQueues;
        private System.Windows.Forms.Panel pnlSwitch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblArrow1;
    }
}