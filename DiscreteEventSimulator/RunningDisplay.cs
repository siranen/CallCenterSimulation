using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines the Form for displaying the current running state of the simulation
    /// </summary>
    public partial class RunningDisplay : Form
    {
        //-------------------------------------------------------------------
        //- CONSTANTS                                                       -
        //-------------------------------------------------------------------
        private const int CALL_CIRCLE_SIZE = 40;
        private const int LINE_THICKNESS = 4;
        private const int CALL_CIRCLE_TEXT_ADJUST = 10;
        private const int TWO = 2;
        private const int QUEUE_START_X = 10;
        private const int CALL_SPACING = 5;
        private const int QUEUE_GAP = 10;
        private const int REP_BOX_START = 5;
        private const int DIFF_REP_TYPE_SPACE = 10;

        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private Simulator sim;
        private Dictionary<ProductType, Color> productColourMapping;
        private Dictionary<ProductType, TabPage> queuePages;
        private Dictionary<Panel, Graphics> panelGraphics;
        private Dictionary<Panel, Bitmap> panelBitmaps;
        private Dictionary<Panel, Graphics> panelBuffers;

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the RunningDisplay class bound to a given Simulator
        /// </summary>
        /// <param name="sim">The Simulator this display is bound to</param>
        /// <param name="windowX">The X-Coordinate that the window should start at</param>
        /// <param name="windowY">The Y-Cooridinate that the window should start at</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given Simulator is null</exception>
        public RunningDisplay(int windowX, int windowY, Simulator sim)
        {
            //Check that the given simulator is not null
            if (sim != null)
            {
                this.sim = sim;
                this.Location = new Point(windowX, windowY);

                InitializeComponent();

                //Create a random number generator
                Random rand = new Random();

                //Get all the predefined colours as an array
                Type t = typeof(Color);
                var properties = t.GetProperties();

                List<Color> colors = (from p in properties
                                      where p.PropertyType == t
                                      select (Color)p.GetValue(null, null)).ToList();

                //Remove white, black, Red, and DarkCyan
                colors.Remove(Color.White);
                colors.Remove(Color.Black);
                colors.Remove(Color.Red);
                colors.Remove(Color.DarkCyan);

                //Creat the dictionary of tab pages for the queues
                queuePages = new Dictionary<ProductType, TabPage>();

                //Create the colour to productType dictionary
                productColourMapping = new Dictionary<ProductType, Color>();

                //For every product type in the simulator
                foreach (ProductType pt in sim.ProductTypes)
                {
                    //Create a tab page for this product type
                    TabPage tp = CreateTabPageFor(pt);
                    //Add the TabPage to the queuePages dictionary
                    queuePages.Add(pt, tp);
                    //Add the TabPage to the TabControl
                    tcDisplay.TabPages.Add(tp);

                    //Genereate a random index into the colors array
                    int colorIndex = rand.Next(colors.Count);

                    //Add the color to the productColourMapping dictionary
                    productColourMapping.Add(pt, colors[colorIndex]);
                }

                //Create the dictionarys for the double buffering
                panelGraphics = new Dictionary<Panel, Graphics>();
                panelBitmaps = new Dictionary<Panel, Bitmap>();
                panelBuffers = new Dictionary<Panel, Graphics>();

                //Add the panel graphics to the dictionary
                panelGraphics.Add(pnlCallArrive, pnlCallArrive.CreateGraphics());
                panelGraphics.Add(pnlSwitch, pnlSwitch.CreateGraphics());
                panelGraphics.Add(pnlQueues, pnlQueues.CreateGraphics());
                panelGraphics.Add(pnlSalesReps, pnlSalesReps.CreateGraphics());

                //Create the Bitmaps
                Bitmap callArriveBMP = new Bitmap(pnlCallArrive.Width, pnlCallArrive.Height);
                Bitmap switchBMP = new Bitmap(pnlSwitch.Width, pnlSwitch.Height);
                Bitmap queuesBMP = new Bitmap(pnlQueues.Width, pnlQueues.Height);
                Bitmap repTypesBMP = new Bitmap(pnlSalesReps.Width, pnlSalesReps.Height);
                //Add the bitmaps to the dictionary
                panelBitmaps.Add(pnlCallArrive, callArriveBMP);
                panelBitmaps.Add(pnlSwitch, switchBMP);
                panelBitmaps.Add(pnlQueues, queuesBMP);
                panelBitmaps.Add(pnlSalesReps, repTypesBMP);

                //Create and add the buffer Graphics objects
                panelBuffers.Add(pnlCallArrive, Graphics.FromImage(callArriveBMP));
                panelBuffers.Add(pnlSwitch, Graphics.FromImage(switchBMP));
                panelBuffers.Add(pnlQueues, Graphics.FromImage(queuesBMP));
                panelBuffers.Add(pnlSalesReps, Graphics.FromImage(repTypesBMP));

                // Start listening for the Simulators EventOccured event
                sim.EventOccured += new Simulator.SimulationEventHandler(UpdateDisplay);
            }
            else // The given simulator is null
            {
                throw new ArgumentNullException("sim", "Attempted to pass null Simulator to RunningDisplay constructor");
            }
        }

        /// <summary>
        /// Creates and Returns a TabPage for displaying the Queue for the given ProductType
        /// </summary>
        /// <param name="pt">The ProductType this TabPage is for</param>
        /// <returns>A TabPage with DataGridView for displaying a queue</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the given ProductType is null</exception>
        private TabPage CreateTabPageFor(ProductType pt)
        {
            //Check that the given product type is not null
            if (pt != null)
            {
                //Create the new TabPage
                TabPage tp = new TabPage();

                //Set the TabPages text
                tp.Text = pt.TypeName;

                //Create a DataGridView for displaying the queue
                DataGridView dgvForPT = new DataGridView();
                //Set the DataGridViews propterties
                dgvForPT.Name = "DGV" + pt.TypeName;
                dgvForPT.AllowUserToAddRows = false;
                dgvForPT.AllowUserToDeleteRows = false;
                dgvForPT.ReadOnly = true;
                dgvForPT.Height = dgvCalendar.Height;
                dgvForPT.Width = dgvCalendar.Width;

                //Copy the columns from the Calendar DataGridView
                foreach (DataGridViewTextBoxColumn col in dgvCalendar.Columns)
                {
                    dgvForPT.Columns.Add(((DataGridViewTextBoxColumn)col.Clone()));
                }

                //Add the DataGridView to the TabPage
                tp.Controls.Add(dgvForPT);

                //Return the TabPage
                return tp;
            }
            else // The given ProductType is null
            {
                throw new ArgumentNullException("pt", "Attempted to pass null ProductType to RunningDisplay.CreateTabPagesFor");
            }
        }

        /// <summary>
        /// Updates the Display to represent the current state of the Simulator
        /// </summary>
        /// <param name="sender">The Simulator that triggered this event</param>
        /// <param name="e">The events SimulationEventArgs</param>
        void UpdateDisplay(object sender, Simulator.SimulationEventArgs e)
        {
            //Update the clock text label
            lblClock.Text = sim.Clock.ToLongTimeString();

            //Update the Calendar Tab
            UpdateCalendar();

            //Update the queues tabs
            UpdateQueues();

            //Updates the Visual Display tab
            UpdateGraphicalTab();

            //Do events just to make sure that the controls update
            Application.DoEvents();
                }

                /// <summary>
                /// Updates the Graphical display tab
                /// </summary>
                private void UpdateGraphicalTab()
                {
                    //Clear all buffers
            ClearAllBuffers();

            //Update all the panels
            UpdateCallArrivalPanel();
            UpdateSwitchPanel();
            UpdateQueuePanel();
            UpdateSalesRepresentativePanel();

            //Draw all the buffers to their panels
            DrawAllBuffers();
        }

        /// <summary>
        /// Draws all the buffers to their panel counterparts
        /// </summary>
        private void DrawAllBuffers()
        {
            //Loop foreach KeyValuePair in the panelGraphics dictionary
            foreach (KeyValuePair<Panel, Graphics> pnlGraphics in panelGraphics)
            {
                //Get the bitmap from the panelBitmaps dictionary using the Panel as the key
                Bitmap bmp = panelBitmaps[pnlGraphics.Key];

                //Draw the bitmap to the panel
                pnlGraphics.Value.DrawImage(bmp, 0, 0);
            }
        }

        /// <summary>
        /// Clears all the buffers
        /// </summary>
        private void ClearAllBuffers()
        {
            // Loop foreach buffer in the panelBuffers dictionary
            foreach (Graphics buffer in panelBuffers.Values)
            {
                //Clear the buffer
                buffer.Clear(Color.White);
            }
        }

        /// <summary>
        /// Updates the SalesRepresentatives panel
        /// </summary>
        private void UpdateSalesRepresentativePanel()
        {
            //Initialise the first boxes location
            int boxX = REP_BOX_START;
            int boxY = REP_BOX_START;

            //Make the box just large enough to fit the call circle
            int boxSize = (LINE_THICKNESS * 2) + CALL_CIRCLE_SIZE + (CALL_SPACING * TWO);

            //Loop foreach SalesRepType in the SalesForce
            foreach (KeyValuePair<SalesRepType, List<SalesRepresentative>> repType in sim.SalesManager.SalesForce)
            {
                //Loop foreach SalesRepresentative in the List
                foreach (SalesRepresentative sr in repType.Value)
                {
                    //If the Sales Representative currently has a phone call
                    if (sr.CurrentlyProcessing != null)
                    {
                        //Calculate the center point for the call
                        int centerX = boxX + boxSize / TWO;
                        int centerY = boxY + boxSize / TWO;

                        //Draw the call
                        DrawCall(sr.CurrentlyProcessing, new Point(centerX, centerY), panelBuffers[pnlSalesReps]);
                    }

                    //Calculate the line thickness of a single colour (The colour of the box is each colour of the productType that it handles
                    float colourLineThickness = (float)LINE_THICKNESS / repType.Key.Handles.Count;
                    float lineX = boxX;
                    float lineY = boxY;
                    float lineSize = boxSize;

                    //foreach ProductType that this SalesRepType can handle
                    foreach (ProductType pt in repType.Key.Handles)
                    {
                        // Create a pen for that salesRepTypes colour with a thickness of the calculated above
                        Pen p = new Pen(productColourMapping[pt], colourLineThickness);

                        // Use the buffer for the sales rep panel to draw a rectangle using the pen from above
                        panelBuffers[pnlSalesReps].DrawRectangle(p, lineX, lineY, lineSize, lineSize);

                        //Add to the line position the thickness of one colour
                        lineX += colourLineThickness;
                        lineY += colourLineThickness;
                        //Subtract the line thickness from the size
                        lineSize -= colourLineThickness * TWO;
                    }

                    // Increase the box y so that the next box is directly underneath the last
                    boxY += boxSize;
                }// end SalesRepresentative foreach
                // Shift the next RepTypes first box further down
                boxY += DIFF_REP_TYPE_SPACE;
            } //End SalesRepType foreach
        }

        /// <summary>
        /// Updates the Queues panel
        /// </summary>
        private void UpdateQueuePanel()
        {
            // The y value of the Queue is the same as the initial x value for a start
            int queueY = QUEUE_START_X;
            // Calculate the queue width and height
            int queueHeight = CALL_CIRCLE_SIZE + (CALL_SPACING * TWO) + (LINE_THICKNESS * TWO);
            int queueWidth = (CALL_CIRCLE_SIZE * sim.QueueManager.MaxQueueLength) + (CALL_SPACING * sim.QueueManager.MaxQueueLength) + (CALL_SPACING * TWO) + (LINE_THICKNESS * TWO);

            // Calculate the X value by starting at the right edge of the panel and moving backwards
            int queueX = pnlQueues.Width - QUEUE_START_X - queueWidth;

            // loop foreach queue in the QueueManager
            foreach (Queue q in sim.QueueManager.Queues)
            {
                // Calculate the Y value in the centre of the queue box
                int centerY = queueY + (queueHeight / TWO);
                // Calculate the X position of the first call circle
                int centerX = (queueX + queueWidth) - (LINE_THICKNESS + CALL_SPACING + (CALL_CIRCLE_SIZE / TWO));

                // loop foreach call in the Queue
                foreach (Call call in q.Calls)
                {
                    // Draw the call
                    DrawCall(call, new Point(centerX, centerY), panelBuffers[pnlQueues]);
                    // Move the X value backward for the next call
                    centerX -= CALL_SPACING + CALL_CIRCLE_SIZE;
                }

                // Retrieve the line colour of the queue
                Color lineColor = productColourMapping[q.TypeInQueue];

                //Create the pen
                Pen linePen = new Pen(lineColor, LINE_THICKNESS);

                //Draw the Queue rectangle using the buffer for this panel
                panelBuffers[pnlQueues].DrawRectangle(linePen, queueX, queueY, queueWidth, queueHeight);

                //Increase the queueY value to move the next queue box downward
                queueY += queueHeight + QUEUE_GAP;
            }
        }

        /// <summary>
        /// Updates the Switch panel
        /// </summary>
        private void UpdateSwitchPanel()
        {
            //Get the next SwitchCompletedEvent from the calendar
            SwitchCompletedEvent nextSCE = sim.Calendar.NextEventOfType(EEventType.SwitchCompleted) as SwitchCompletedEvent;
            //Refresh the panel
            pnlSwitch.Refresh();

            //If their was a scheduled SwitchCompletedEvent
            if (nextSCE != null)
            {
                //Draw the call
                int centerX = pnlSwitch.Width / TWO;
                int centerY = pnlSwitch.Height / TWO;

                DrawCall(nextSCE.Entity, new Point(centerX, centerY), panelBuffers[pnlSwitch]);
            }

            //Draw the SwitchRectangle
            Pen squarePen = new Pen(Color.Red, LINE_THICKNESS);
            panelBuffers[pnlSwitch].DrawRectangle(squarePen, 0, 0, pnlSwitch.Width, pnlSwitch.Height);
        }

        /// <summary>
        /// Updates the call arrival panel
        /// </summary>
        private void UpdateCallArrivalPanel()
        {
            //Get the next CallArriveEvent from the calendar
            CallArriveEvent nextCAE = sim.Calendar.NextEventOfType(EEventType.CallArrive) as CallArriveEvent;
            //Refresh the panel
            pnlCallArrive.Refresh();

            //Check that it is not null
            if (nextCAE != null)
            {
                //Draw the call
                int centerX = pnlCallArrive.Width / TWO;
                int centerY = pnlCallArrive.Height / TWO;

                DrawCall(nextCAE.Entity, new Point(centerX, centerY), panelBuffers[pnlCallArrive]);
            }
        }

        /// <summary>
        /// Draws a circle with the colour and entity number of the product type
        /// </summary>
        /// <param name="call">The Call that the circle is for</param>
        /// <param name="centerPoint">The center point of the calls circle</param>
        /// <param name="g">The graphics object to draw this circle to</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the given arguments are null</exception>
        private void DrawCall(Call call, Point centerPoint, Graphics g)
        {
            // Check that the given call is not null
            if (call != null)
            {
                // Check that the given centerPoint is not null
                if (centerPoint != null)
                {
                    //Check that the given Graphics object is not null
                    if (g != null)
                    {
                        //Calculate the X and Y location to draw at
                        int x = centerPoint.X - (CALL_CIRCLE_SIZE / TWO);
                        int y = centerPoint.Y - (CALL_CIRCLE_SIZE / TWO);

                        //Default background colour is white
                        Color backColor = Color.White;

                        //Choose the correct background colour
                        if (call.ProductType != null)
                        {
                            backColor = productColourMapping[call.ProductType];
                        }

                        //Get the color for the outer ring
                        Color ringColor = Color.DarkCyan;

                        //Create the brush for the background
                        Brush backBrush = new SolidBrush(backColor);
                        //Create the Pen for the ring
                        Pen ringPen = new Pen(ringColor, LINE_THICKNESS);

                        //Draw the call
                        g.FillEllipse(backBrush, x, y, CALL_CIRCLE_SIZE, CALL_CIRCLE_SIZE);
                        g.DrawEllipse(ringPen, x, y, CALL_CIRCLE_SIZE, CALL_CIRCLE_SIZE);
                        g.DrawString(call.CallId.ToString(), Font, Brushes.Black, x + CALL_CIRCLE_TEXT_ADJUST, y + CALL_CIRCLE_TEXT_ADJUST);
                    }
                    else // The given Graphics object is null
                    {
                        throw new ArgumentNullException("g", "Attempted to pass null Graphics object to RunningDisplay.DrawCall");
                    }
                }
                else // The given Point is null
                {
                    throw new ArgumentNullException("centerPoint", "Attempted to pass null Point to RunningDisplay.DrawCall");
                }
            }
            else // The given Call is null
            {
                throw new ArgumentNullException("call", "Attempted to pass null Call to RunningDisplay.DrawCall");
            }
        }

        /// <summary>
        /// Updates the Queue TabPages
        /// </summary>
        private void UpdateQueues()
        {
            // Loop foreach TabPage in the queuePages dictionaries
            foreach (KeyValuePair<ProductType, TabPage> queuePage in queuePages)
            {
                // Get the DataGridView from the TabPage
                DataGridView dgvForPT = (DataGridView)((queuePage.Value.Controls.Find("DGV" + queuePage.Key.TypeName, false))[0]);

                // Clear the DataGridView rows
                dgvForPT.Rows.Clear();

                //Draw the Queue
                sim.QueueManager.DrawQueueToDataGridView(queuePage.Key, dgvForPT);

            } // End queuePage foreach
        }

        /// <summary>
        /// Updates the Calendars tab
        /// </summary>
        private void UpdateCalendar()
        {
            //Clear the rows in the display
            dgvCalendar.Rows.Clear();

            //Get the calendar to draw itself to the datagridview
            sim.DrawCalendarToGataGridView(dgvCalendar);
        }

        /// <summary>
        /// Called when the form closes
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The events FormClosingEventArgs</param>
        private void RunningDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            sim.EventOccured -= new Simulator.SimulationEventHandler(UpdateDisplay);
        }
    }
}
