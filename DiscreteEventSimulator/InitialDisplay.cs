using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace DiscreteEventSimulator
{
    public partial class InitialDisplay : Form
    {
        //-------------------------------------------------------------------
        //- CONSTANTS                                                       -
        //-------------------------------------------------------------------
        private const int MIN_ROLL = 2;
        private const int MAX_ROLL = 12;
        private const int MEAN_ROLL = 7;
        private const double MAX_PROBABILITY = 1;
        private const int PRODUCT_TYPE_GROUPBOX_HEIGHT = 100;
        private const int PRODCUCT_TYPE_GROUPBOX_WIDTH = 560;
        private const int SPACING_BETWEEN_GROUPBOXES = 20;
        private const int GROUPBOX_X = 5;
        private const int GROUPBOX_INITIAL_Y = 5;
        private const int PRODUCT_TYPE_LABEL_X = 5;
        private const int PROCESS_DELAY_LABEL_Y = 20;
        private const int PROCESS_DELAY_DECIMAL_PLACES = 2;
        private const int PROCESS_DELAY_NUD_X = 220;
        private const int PROCESS_DELAY_NUD_MAX = 10;
        private const int PRODUCT_PROBABILITY_LABEL_Y = 60;
        private const int PRODUCT_PROBABILITY_TRACKBAR_WIDTH = 250;
        private const int PRODUCT_PROBABILITY_MAX = 100;
        private const int PROBABILITY_TO_PERCENT_MULTIPLIER = 100;
        private const double PROCESS_DELAY_NUD_MIN = 0.01;
        private const double PROCESS_DELAY_NUD_INC = 0.01;
        private const int LABEL_SPACING = 10;
        private const int REMOVE_BUTTON_X = 530;
        private const int REMOVE_BUTTON_Y = 10;
        private const int REMOVE_BUTTON_HEIGHT = 30;
        private const int REMOVE_BUTTON_WIDTH = 30;

        
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private Core gov;
        private ToolTip toolTips;
        private bool updateFromGovernor;

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the InitialDisplay Form class
        /// </summary>
        public InitialDisplay()
        {
            InitializeComponent();

            //Create the govenor
            gov = new Core();

            //Create the ToolTip
            toolTips = new ToolTip();
            toolTips.ToolTipIcon = ToolTipIcon.Info;
            
            //Bind the events for changing the display when the govenors details are changed
            gov.ProductTypeChanged += new EventHandler(RefreshProductTypeDisplay);
            gov.ProductTypeChanged += new EventHandler(UpdateRepTypeProductLists);
            gov.RepTypesChanged += new EventHandler(UpdateRepTypeList);
            gov.MainSettingsChanged += new EventHandler(UpdateMainSettingsControls);
            
            //Bind the events for checking that the simualation parameters are valid
            gov.ProductTypeChanged += new EventHandler(SimulationValidCheck);
            gov.RepTypesChanged += new EventHandler(SimulationValidCheck);
            gov.MainSettingsChanged += new EventHandler(SimulationValidCheck);

            //Bind events for updating the MainSettings NuD labels
            nudCallArrival.ValueChanged += new EventHandler(nudCallArrival_ValueChanged);
            nudSwitchDelay.ValueChanged += new EventHandler(nudSwitchDelay_ValueChanged);

            //Set the tooltip values
            toolTips.SetToolTip(btnAddProductType, "Adds a new ProductType with the entered name");
            toolTips.SetToolTip(txtTypeName, "The name of the new ProductType, cannot be empty");
            toolTips.SetToolTip(btnAddRepType, "Adds a new Sales Representative Type with the name from the TextBox above");
            toolTips.SetToolTip(txtRepTypeName, "The type name of the new Sales Representative type, cannot be empty");
            toolTips.SetToolTip(btnAddHandle, "Makes the Product Type selected in the right hand list processible \nby the Sales Representative type selected in the right hand list");
            toolTips.SetToolTip(listRepTypes, "The list of Sales Representative types in the system, click one to see its details");
            toolTips.SetToolTip(listHandles, "This list shows the Product Types that the currently selected Sales Representative Type handles");
            toolTips.SetToolTip(listProductTypes, "This list shows the Product Types in the system that are \nnot assigned to the selected Sales Representative type");
            toolTips.SetToolTip(nudRepNumber, "Increase or decrease the number of Sales Representatives of the selected type");
            toolTips.SetToolTip(cboxSingleQueueLength, "Sets whether or not the maximum queue length applies to all queues combined, or to each individual queue");
            toolTips.SetToolTip(lblPercentRemainingValue, "This must be 0% before the simulation can run");

            // clear the update from govenor flag
            updateFromGovernor = false;

            // if the default simulation file exists, load it
            if (File.Exists(DESConstants.APP_DIR + DESConstants.DEFAULT_SIM_SETTINGS))
            {
                gov.LoadSettingsFromFile(DESConstants.APP_DIR + DESConstants.DEFAULT_SIM_SETTINGS);
            }
        }

        /// <summary>
        /// Updates the Main Settings controls when the govenors settings change
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The events EventArgs</param>
        private void UpdateMainSettingsControls(object sender, EventArgs e)
        {
            // Set the updateFromGovernor flag
            updateFromGovernor = true;

            //Update all the controls
            nudCallArrival.Value = (decimal)gov.CallArrivalMultiplier;
            nudSwitchDelay.Value = (decimal)gov.SwitchDelayMultiplier;

            nudBeginTimeHour.Value = (decimal)gov.BeginTime.Hour;
            nudBeginTimeMinute.Value = (decimal)gov.BeginTime.Minute;
            nudBeginTimeSecond.Value = (decimal)gov.BeginTime.Second;

            nudDurationDays.Value = (decimal)gov.Duration.Days;
            nudDurationHours.Value = (decimal)gov.Duration.Hours;
            nudDurationMinutes.Value = (decimal)gov.Duration.Minutes;
            nudDurationSeconds.Value = (decimal)gov.Duration.Seconds;

            nudExcessiveWaitHour.Value = (decimal)gov.ExcessiveWait.Hours;
            nudExcessiveWaitMinute.Value = (decimal)gov.ExcessiveWait.Minutes;
            nudExcessiveWaitSecond.Value = (decimal)gov.ExcessiveWait.Seconds;

            nudMaxQueueLength.Value = (decimal)gov.MaxQueueLength;
            cboxSingleQueueLength.Checked = gov.SingleQueueLength;

            // clear the update from govenor flag
            updateFromGovernor = false;
        }

        /// <summary>
        /// Checks if the simulation is valid and either enables or disables the RunSim button
        /// </summary>
        /// <param name="sender">The object that triggered this eventHandler</param>
        /// <param name="e">The EventArgs of the event</param>
        private void SimulationValidCheck(object sender, EventArgs e)
        {
            //Check if the simulation parameters are valid
            if (gov.IsSimulationValid())
            {
                //If yes enable runSim button
                btnRunSim.Enabled = true;
            }
            else
            {
                //If no disable runSim button
                btnRunSim.Enabled = false;
            }
        }

        /// <summary>
        /// Updates the ListBox of RepTypes on the Representatives Tab
        /// </summary>
        /// <param name="sender">The object that triggered this eventHandler</param>
        /// <param name="e">The EventArgs of the event</param>
        private void UpdateRepTypeList(object sender, EventArgs e)
        {
            //Rebind the datasource for the listbox
            listRepTypes.DisplayMember = "TypeName";
            listRepTypes.DataSource = gov.RepNums.Keys.ToList();
        }

        /// <summary>
        /// Updates the ProductTypes Tab
        /// </summary>
        /// <param name="sender">The object that triggered this eventHandler</param>
        /// <param name="e">The EventArgs of the event</param>
        private void RefreshProductTypeDisplay(object sender, EventArgs e)
        {
            //Clear all the controls on the panel
            pnlProductTypeDisplay.Controls.Clear();

            //Create the initial Y value
            int y = GROUPBOX_INITIAL_Y;

            //Loop foreach product type currently in the Governors list
            foreach (ProductType pt in gov.ProductTypes)
            {
                //Create the groupbox for holding the controls for this product type
                GroupBox gb = CreatePTGroupBox(y, pt);

                //Create the Label ProcessDelay Label
                Label lblProcessDelay = CreatePTProcessDelayLabel();
                
                //Create the ProcessDelay Numeric UpDown
                NumericUpDown nudProcessDelay = CreatePTProcessDelayNuD(pt);
                
                //Create the Label for showing the process delay range
                Label lblProcessRange = CreatePTProcessDelayRangeLabel(nudProcessDelay);
                
                //Create the Probability label
                Label lblProbability = CreatePTProbabilityLabel();
                
                //Create the pobability trackbar
                TrackBar tbProbability = CreatePTProbabilityTrackBar(pt);
                
                //Create the probability percent lbl
                Label lblProbabilityPercent = CreatePTProbabilityPercentLabel(tbProbability);
                
                //Create the button for removing this product type
                Button btnRemoveProductType = CreatePTRemoveButton();

                //Add all the controls to the group box
                gb.Controls.Add(lblProcessDelay);
                gb.Controls.Add(nudProcessDelay);
                gb.Controls.Add(lblProcessRange);
                gb.Controls.Add(lblProbability);
                gb.Controls.Add(tbProbability);
                gb.Controls.Add(lblProbabilityPercent);
                gb.Controls.Add(btnRemoveProductType);

                //Bind the events for the various control events
                nudProcessDelay.ValueChanged += new EventHandler(ProductTypeProcessDelayChanged);
                btnRemoveProductType.Click += new EventHandler(RemoveProductType);
                tbProbability.ValueChanged += new EventHandler(ProductTypeProbabilityValueChanged);

                //Update the multiplier label to set its initial value
                UpdateMultiplierLabel(pt.ProcessingDelayMultiplier, lblProcessRange);
                
                //Add the groupbox to the panel
                pnlProductTypeDisplay.Controls.Add(gb);

                //Increase the Y value for the next group box
                y += PRODUCT_TYPE_GROUPBOX_HEIGHT + SPACING_BETWEEN_GROUPBOXES;
            }//End foreach

            // Update the percent remaining label
            lblPercentRemainingValue.Text = (MAX_PROBABILITY - gov.TotalProbability()).ToString("p2");
        }

        /// <summary>
        /// Creates the X button for ProductType groupboxes for removing the product type
        /// </summary>
        /// <returns>A button with an X in it</returns>
        private Button CreatePTRemoveButton()
        {
            //Create the button
            Button btnRemoveProductType = new Button();
            //Set its properties to the appropriate values
            btnRemoveProductType.Name = "btnRemoveProductType";
            btnRemoveProductType.Text = "X";
            btnRemoveProductType.Location = new Point(REMOVE_BUTTON_X, REMOVE_BUTTON_Y);
            btnRemoveProductType.Size = new System.Drawing.Size(REMOVE_BUTTON_WIDTH, REMOVE_BUTTON_HEIGHT);
            btnRemoveProductType.TextAlign = ContentAlignment.MiddleCenter;
            toolTips.SetToolTip(btnRemoveProductType, "Click to remove this Product Type");
            //Return the button
            return btnRemoveProductType;
        }

        /// <summary>
        /// Creates the Label for ProductType GroupBoxes that displays the percentage value for the trackbar
        /// </summary>
        /// <param name="tbProbability">The trackbar this label displays the value for</param>
        /// <returns>A Label</returns>
        private Label CreatePTProbabilityPercentLabel(TrackBar tbProbability)
        {
            // Check that the given trackbar is not null
            if (tbProbability != null)
            {
                //Create the Label
                Label lblProbabilityPercent = new Label();
                //Set the labels properties
                lblProbabilityPercent.Name = "lblProbabilityPercent";
                lblProbabilityPercent.Location = new Point(tbProbability.Right + LABEL_SPACING, PRODUCT_PROBABILITY_LABEL_Y);
                //Set the text using the trackbars value
                lblProbabilityPercent.Text = ((double)tbProbability.Value / PROBABILITY_TO_PERCENT_MULTIPLIER).ToString("p2");
                //Return the label
                return lblProbabilityPercent;
            }
            else // The given trackbar was null
            {
                throw new ArgumentNullException("tbProbability", "Attempted to pass null TrackBar to InitialDisplay.CreatePTProbabilityPercentLabel");
            }
        }

        /// <summary>
        /// Creates the TrackBar for adjusting a ProductTypes probability
        /// </summary>
        /// <param name="pt">The ProductType this trackbar is for</param>
        /// <returns>A TrackBar</returns>
        private TrackBar CreatePTProbabilityTrackBar(ProductType pt)
        {
            // Check that the given ProductType is not null
            if (pt != null)
            {
                //Create the TrackBar
                TrackBar tbProbability = new TrackBar();
                //Set the TrackBars properties
                tbProbability.Name = "tbProbability";
                tbProbability.Minimum = 0;
                tbProbability.Maximum = PRODUCT_PROBABILITY_MAX;
                tbProbability.TickFrequency = 1;
                tbProbability.Width = PRODUCT_PROBABILITY_TRACKBAR_WIDTH;
                //Set the TrackBars value using the ProductTypes probability
                tbProbability.Value = (int)(pt.ProductTypeProbability * PROBABILITY_TO_PERCENT_MULTIPLIER);
                tbProbability.Location = new Point(PROCESS_DELAY_NUD_X, PRODUCT_PROBABILITY_LABEL_Y);

                toolTips.SetToolTip(tbProbability, "Sets the probability of a Call being this Product Type");
                //Return the TrackBar
                return tbProbability;
            }
            else // The given ProductType is null
            {
                throw new ArgumentNullException("pt", "Attempted to pass null ProductType to InitialDisplay.CreatePTProbabilityTrackBar");
            }
        }

        /// <summary>
        /// Creates the Label for the ProductTypes GroupBox for the probability
        /// </summary>
        /// <returns>A Label</returns>
        private Label CreatePTProbabilityLabel()
        {
            //Create the Label
            Label lblProbability = new Label();
            //Set the Labels properties
            lblProbability.Name = "lblProbability";
            lblProbability.Text = "Probability:";
            lblProbability.Location = new Point(PRODUCT_TYPE_LABEL_X, PRODUCT_PROBABILITY_LABEL_Y);
            //Return the Label
            return lblProbability;
        }

        /// <summary>
        /// Creates the Label for the ProductTypes GroupBox that displays the process delay as a range
        /// </summary>
        /// <param name="nudProcessDelay">The numeric up down this label displays beside</param>
        /// <returns>A Label</returns>
        private Label CreatePTProcessDelayRangeLabel(NumericUpDown nudProcessDelay)
        {
            // Check that the given NumericUpDown is not null
            if (nudProcessDelay != null)
            {
                //Create the Label
                Label lblProcessRange = new Label();
                //Set the Labels properties
                lblProcessRange.Name = "lblProcessRange";
                //Set the labels location to be next to the numeric up down
                lblProcessRange.Location = new Point(nudProcessDelay.Right + LABEL_SPACING, PROCESS_DELAY_LABEL_Y);
                lblProcessRange.AutoSize = true;
                lblProcessRange.TextAlign = ContentAlignment.MiddleCenter;
                //Return the label
                return lblProcessRange;
            }
            else // The given NumericUpDown is null
            {
                throw new ArgumentNullException("nudProcessDelay", "Attempted to pass null NumericUpDown to InitialDisplay.CreatePTProcessDelayRangeLabel");
            }
        }

        /// <summary>
        /// Creates the NumericUpDown for changing a ProductTypes processDelayMultiplier
        /// </summary>
        /// <param name="pt">The ProductType this NumericUpDown modifies</param>
        /// <returns>A NumericUpDown</returns>
        private NumericUpDown CreatePTProcessDelayNuD(ProductType pt)
        {
            // Check that the given ProductType is not null
            if (pt != null)
            {
                //Create the NumericUpDown
                NumericUpDown nudProcessDelay = new NumericUpDown();
                //Set the NumericUpDowns properties
                nudProcessDelay.Name = "nudProcessDelay";
                nudProcessDelay.Minimum = (decimal)PROCESS_DELAY_NUD_MIN;
                nudProcessDelay.DecimalPlaces = PROCESS_DELAY_DECIMAL_PLACES;
                nudProcessDelay.Maximum = PROCESS_DELAY_NUD_MAX;
                nudProcessDelay.Location = new Point(PROCESS_DELAY_NUD_X, PROCESS_DELAY_LABEL_Y);
                //Set the NumericUpDowns initial value to be the ProductTypes process delay multiplier
                nudProcessDelay.Value = (decimal)pt.ProcessingDelayMultiplier;
                nudProcessDelay.Increment = (decimal)PROCESS_DELAY_NUD_INC;
                toolTips.SetToolTip(nudProcessDelay, "Sets the time multiplier for calculating how long this ProductType takes to process");
                //Return the NumericUpDown
                return nudProcessDelay;
            }
            else // The given ProductType is null
            {
                throw new ArgumentNullException("pt", "Attempted to pass null ProductType to InitialDisplay.CreatePTProcessDelayNuD");
            }
        }

        /// <summary>
        /// Creates the Label for a ProductTypes GroupBox for the Process Delay
        /// </summary>
        /// <returns>A Label</returns>
        private Label CreatePTProcessDelayLabel()
        {
            //Create the Label
            Label lblProcessDelay = new Label();
            //Set the Labels Properties
            lblProcessDelay.Name = "lblProcessDelay";
            lblProcessDelay.Text = "Process Delay Multiplier:";
            lblProcessDelay.Size = lblProcessDelay.PreferredSize;
            lblProcessDelay.AutoSize = true;
            lblProcessDelay.Location = new Point(PRODUCT_TYPE_LABEL_X, PROCESS_DELAY_LABEL_Y);
            //Return the Label
            return lblProcessDelay;
        }

        /// <summary>
        /// Create the GroupBox for a given ProductType for the ProductTypes TabPage
        /// </summary>
        /// <param name="y">The y location of the GroupBox</param>
        /// <param name="pt">The ProductType this GroupBox is for</param>
        /// <returns>A GroupBox</returns>
        private GroupBox CreatePTGroupBox(int y, ProductType pt)
        {
            //Check that the given ProductType is not null
            if (pt != null)
            {
                //Create the GroupBox
                GroupBox gb = new GroupBox();
                //Set the GroupBoxes properties
                gb.Name = "gb" + pt.TypeName;
                gb.Text = pt.TypeName;
                gb.Location = new Point(GROUPBOX_X, y);
                gb.Size = new Size(PRODCUCT_TYPE_GROUPBOX_WIDTH, PRODUCT_TYPE_GROUPBOX_HEIGHT);
                //Return the GroupBox
                return gb;
            }
            else // The given ProductType is null
            {
                throw new ArgumentNullException("pt", "Attempted to pass null ProductType to InitialDisplay.CreatePTGroupBox");
            }
        }

        /// <summary>
        /// Removes the ProductType that the trigger button is for
        /// </summary>
        /// <param name="sender">The Button on a GroupBox</param>
        /// <param name="e">The events EventArgs</param>
        private void RemoveProductType(object sender, EventArgs e)
        {
            //Cast the sender as a button
            Button btn = sender as Button;

            //If the above cast worked
            if (btn != null)
            {
                //get the buttons parent which is a groupbox
                GroupBox parent = btn.Parent as GroupBox;

                //Check that cast above worked
                if (parent != null)
                {
                    //Get the ProductType using the GroupBoxes text
                    ProductType pt = gov.ProductTypes.FirstOrDefault(p => p.TypeName == parent.Text);

                    //Remove the ProductType from the Core
                    gov.RemoveProductType(pt); 
                }
                else // The Button is not contained within a GroupBox
                {
                    throw new ArgumentOutOfRangeException("sender", "RemoveProductType called using sender that is not contained within a GroupBox");
                }
            }
            else // The case failed
            {
                throw new ArgumentOutOfRangeException("sender", "Attempted to call RemoveProductType with a sender that was not a Button");
            }
        }

        /// <summary>
        /// Called when the TrackBar for a ProductTypes probability changes
        /// </summary>
        /// <param name="sender">The TrackBar that triggered this event</param>
        /// <param name="e">The events EventArgs</param>
        private void ProductTypeProbabilityValueChanged(object sender, EventArgs e)
        {
            //Cast the sender as a TrackBar
            TrackBar tb = sender as TrackBar;

            //If the above cast worked
            if (tb != null)
            {
                //Retrieve the parent groupbox
                GroupBox parent = (GroupBox)tb.Parent;

                //Check that the parent cast worked
                if (parent != null)
                {
                    //Get the sum of all probabilities of ProductTypes
                    double total = gov.TotalProbability();

                    //Calculate the new probability value
                    double newProbability = (double)tb.Value / PROBABILITY_TO_PERCENT_MULTIPLIER;

                    //Retrieve the ProductType that the parent GroupBox is for
                    ProductType pt = gov.ProductTypes.FirstOrDefault(p => p.TypeName == parent.Text);

                    double difference = newProbability - pt.ProductTypeProbability;

                    //If the total probability is not 1 (100%) and the new probability is greater than the old value OR the newProbability is less than the old value
                    if (total + difference <= 1)
                    {
                        //Retrieve the percent label for this groupBox
                        Label lblPercent = parent.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblProbabilityPercent");
                        //Update the label
                        lblPercent.Text = newProbability.ToString("p2");
                        //Update the settings for this ProductType
                        gov.SetProductTypeSettings(parent.Text, newProbability, pt.ProcessingDelayMultiplier);
                    }
                    else // There is no more of the 100% probability to distribute
                    {
                        //Set the TrackBars value to the ProductTypes probability
                        tb.Value = (int)(pt.ProductTypeProbability * PROBABILITY_TO_PERCENT_MULTIPLIER);
                    }

                    // Update the percent remaining label
                    lblPercentRemainingValue.Text = (MAX_PROBABILITY - gov.TotalProbability()).ToString("p2");
                    
                }
                else // Casting Parent to a GroupBox failed
                {
                    throw new ArgumentOutOfRangeException("sender", "ProductTypeProbabilityChanged called using sender that is not contained within a GroupBox");
                }
            }
            else // Casting sender to a TrackBar failed
            {
                throw new ArgumentOutOfRangeException("sender", "ProductTypeProbabilityChanged called using sender that is not a TrackBar");
            }
        }

        /// <summary>
        /// Updates the ProductTypes ProcessDelay value
        /// </summary>
        /// <param name="sender">The NumericUpDown that triggered this event</param>
        /// <param name="e">The events EventArgs</param>
        void ProductTypeProcessDelayChanged(object sender, EventArgs e)
        {
            //Cast sender as a NumericUpDown
            NumericUpDown nud = sender as NumericUpDown;

            //Check if the above cast was successfull
            if (nud != null)
            {
                //Retrieve the parent groupbox
                GroupBox parent = nud.Parent as GroupBox;

                //Check that the parent cast was successfull
                if (parent != null)
                {
                    //Get the ProductType that this GroupBox is for
                    ProductType pt = gov.ProductTypes.FirstOrDefault(p => p.TypeName == parent.Text);

                    //Get the range label
                    Label rangeLabel = parent.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblProcessRange");
                    //Update the range label
                    UpdateMultiplierLabel((double)nud.Value, rangeLabel);

                    //Update the ProductTypes settings
                    gov.SetProductTypeSettings(parent.Text, pt.ProductTypeProbability, (double)nud.Value);
                }
                else //The parent cast failed
                {
                    throw new ArgumentOutOfRangeException("sender", "ProductTypeProcessDelayChanged called using sender that is not contained within a GroupBox");
                }
            }
            else // Casting Sender failed
            {
                throw new ArgumentOutOfRangeException("sender", "ProductTypeProcessDelayChanged called using sender that is not a NumericUpDown");
            }
        }

        /// <summary>
        /// Called when the NumericUpDown for the CallArrivalMultiplier's value changes
        /// </summary>
        /// <param name="sender">The NumericUpDown whose value changed</param>
        /// <param name="e">The events EventArgs</param>
        private void nudCallArrival_ValueChanged(object sender, EventArgs e)
        {
            //Update the Label for the CallArriveMultiplier using the NumericUpDowns value
            UpdateMultiplierLabel((double)nudCallArrival.Value, lblCallArrivalRange);
        }

        /// <summary>
        /// Called when the NumericUpDown for the SwitchDelayMultiplier's value changes
        /// </summary>
        /// <param name="sender">The NumericUpDown whose value changed</param>
        /// <param name="e">The events EventArgs</param>
        private void nudSwitchDelay_ValueChanged(object sender, EventArgs e)
        {
            UpdateMultiplierLabel((double)nudSwitchDelay.Value, lblSwitchDelayRange);
        }

        /// <summary>
        /// Updates the given label with the calculated timespan values
        /// </summary>
        /// <param name="value">The multiplier value</param>
        /// <param name="label">The Label whose text will be set</param>
        private void UpdateMultiplierLabel(double value, Label label)
        {
            //Calculate the minimum value
            double minVal = MIN_ROLL * value;
            //Calculate the Mean value
            double meanVal = MEAN_ROLL * value;
            //Calculat the max value
            double maxVal = MAX_ROLL * value;

            //Convert the calculated values to TimeSpans
            TimeSpan minAsTS = TimeSpan.FromMinutes(minVal);
            TimeSpan meanAsTS = TimeSpan.FromMinutes(meanVal);
            TimeSpan maxAsTS = TimeSpan.FromMinutes(maxVal);

            //Convert the timespans to strings
            string minAsString = TimeSpanToString(minAsTS);
            string meanAsString = TimeSpanToString(meanAsTS);
            string maxAsString = TimeSpanToString(maxAsTS);

            //Set the Label text using the strings above
            label.Text = minAsString + " - " + maxAsString + "\r\n("+meanAsString+")";
        }

        /// <summary>
        /// Returns a string representation of the given TimeSpan in a nice format
        /// </summary>
        /// <param name="ts">The TimeSpan</param>
        /// <returns>A nicely formatted version of the TimeSpan</returns>
        private string TimeSpanToString(TimeSpan ts)
        {
            //Create the string to be returned
            string retString = "";
            //Add the number of days to the string if the number of days is greater than 0
            retString += ts.Days > 0 ? (ts.Days.ToString() + " days ") : "";
            //Add the number of hours to the string if the number of hours is greater than 0
            retString += ts.Hours > 0 ? (ts.Hours.ToString() + " h ") : "";
            //Add the number of minutes to the string if the number of minutes is greater than 0
            retString += ts.Minutes > 0 ? (ts.Minutes.ToString() + " m ") : "";
            //Add the number of seconds to the string if the number of seconds is greater than 0
            retString += ts.Seconds > 0 ? (ts.Seconds.ToString() + " s") : "";

            return retString;
        }

        /// <summary>
        /// Called when the AddProductType button is clicked
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The events EventArgs</param>
        private void btnAddProductType_Click(object sender, EventArgs e)
        {
            // If the Length of the textBox is at least one and is not just spaces
            if (txtTypeName.Text.Length > 0 && (txtTypeName.Text.Count(c => (char.IsNumber(c) || char.IsLetter(c))) > 0))
            {
                //Add the ProductType
                gov.AddNewProductType(txtTypeName.Text);
                //Clear the TextBox
                txtTypeName.Clear();
            }
            else // The entered name was not valid
            {
                MessageBox.Show("Please enter a valid type name before adding");
            }
        }

        /// <summary>
        /// Called when the AddRepType button is clicked
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The events EventArgs</param>
        private void btnAddRepType_Click(object sender, EventArgs e)
        {
            // If the length of the TextBox is at least one is not just spaces
            if (txtRepTypeName.Text.Length > 0 && (txtRepTypeName.Text.Count(c => (char.IsNumber(c) || char.IsLetter(c))) > 0))
            {
                // Add the RepType
                gov.AddNewRepType(txtRepTypeName.Text);
                //Clear the TextBox
                txtRepTypeName.Clear();
            }
            else // The entered name was not valid
            {
                MessageBox.Show("Please enter a type name before adding");
            }
        }

        /// <summary>
        /// Called when a Key is pressed down while the RepTypes list is in focus
        /// </summary>
        /// <param name="sender">The ListBox that triggered this event</param>
        /// <param name="e">The events KeyEventArgss</param>
        private void listRepTypes_KeyDown(object sender, KeyEventArgs e)
        {
            //Check if the pressed key was Delete or Backspace
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                //If it was then get the RepresentativeType that was selected in the list
                SalesRepType repType = (SalesRepType)listRepTypes.SelectedItem;

                //Remove this RepType from the Core
                gov.RemoveRepType(repType);
            }
            //Else do nothing
        }

        /// <summary>
        /// Updates the ListBoxes for ProductTypes on the Representatives TabPage
        /// </summary>
        /// <param name="sender">The object that triggered this event</param>
        /// <param name="e">The events EventArgs</param>
        private void UpdateRepTypeProductLists(object sender, EventArgs e)
        {
            //Clear both listBoxes
            listHandles.Items.Clear();
            listProductTypes.Items.Clear();

            //Get the currently selected SalesRepType in the salesRep listbox
            SalesRepType srt = (SalesRepType)listRepTypes.SelectedItem;

            //If there is a selected SalesRepType
            if (srt != null)
            {
                //Set the Handles listboxes display member
                listHandles.DisplayMember = "TypeName";
                //Loop foreach ProductType in the SalesRepTypes handles list
                foreach (ProductType pt in srt.Handles)
                {
                    //Add the ProductType to the handles listbox
                    listHandles.Items.Add(pt);
                }

                //Set the productType listboxes display member
                listProductTypes.DisplayMember = "TypeName";

                //Foreach ProductType in the list of ProductTypes that is not in the SalesRepTypes handles list
                foreach (ProductType pt in gov.ProductTypes.Where(pt => srt.Handles.Contains(pt) == false).ToList())
                {
                    //Add the ProductType to the ProductType Listbox
                    listProductTypes.Items.Add(pt);
                }

                //Set the NumericUpDowns value
                nudRepNumber.Value = gov.RepNums[srt]; 
            }
        }

        /// <summary>
        /// Called when a key is pressed while the Handles list box is in focus
        /// </summary>
        /// <param name="sender">The ListBox that triggered this event</param>
        /// <param name="e">The events KeyEventArgs</param>
        private void listHandles_KeyDown(object sender, KeyEventArgs e)
        {
            //If the key that was pressed was either the Delete key or the Backspace key
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                //Get the RepType that is currently selected in the RepTypes listbox
                SalesRepType repType = (SalesRepType)listRepTypes.SelectedItem;
                //Get the ProductType that is currently selected in the Hangles ListBox
                ProductType toRemove = (ProductType)listHandles.SelectedItem;

                //Remove the ProductType from the RepTypes handle List
                gov.RemoveRepTypeHandle(repType, toRemove);

                //Set the reptypes listbox selected item to be the previously selected item
                listRepTypes.SelectedItem = repType;
            }
        }

        /// <summary>
        /// Called when the AddHandle button is clicked
        /// </summary>
        /// <param name="sender">The Button that triggered this event</param>
        /// <param name="e">The events EventArgs</param>
        private void btnAddHandle_Click(object sender, EventArgs e)
        {
            //Try and Get the currently selected RepType
            SalesRepType repType = listRepTypes.SelectedItem as SalesRepType;
            
            //If a SalesRepType was returned from above
            if (repType != null)
            {
                //Try and Get the currently selected ProductType
                ProductType toHandle = listProductTypes.SelectedItem as ProductType;

                //If a ProductType was returned from above
                if (toHandle != null)
                {
                    //Add the productType to this reptypes handles list
                    gov.AddRepTypeHandle(repType, toHandle);

                    //Set the reptypes listbox selected item to be the previously selected item
                    listProductTypes.SelectedItem = repType; 
                }
            }            
        }

        /// <summary>
        /// Updates the values in the Core when any of the main controls changes
        /// </summary>
        /// <param name="sender">The Control that triggered this event</param>
        /// <param name="e">The events EventArgs</param>
        private void UpdateGovernor(object sender, EventArgs e)
        {
            // only update the govenor if the updateFromGovernor flag is not set
            if (!updateFromGovernor)
            {
                //Update the callArrival multiplier
                gov.CallArrivalMultiplier = (double)nudCallArrival.Value;
                //Update the SwitchDelayMultiplier
                gov.SwitchDelayMultiplier = (double)nudSwitchDelay.Value;
                //Update the begin time
                gov.BeginTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, (int)nudBeginTimeHour.Value, (int)nudBeginTimeMinute.Value, (int)nudBeginTimeSecond.Value);
                //Update the duration
                gov.Duration = new TimeSpan((int)nudDurationDays.Value, (int)nudDurationHours.Value, (int)nudDurationMinutes.Value, (int)nudDurationSeconds.Value);
                //Update the Excessive wait value
                gov.ExcessiveWait = new TimeSpan((int)nudExcessiveWaitHour.Value, (int)nudExcessiveWaitMinute.Value, (int)nudExcessiveWaitSecond.Value);
                //Update the max queue length
                gov.MaxQueueLength = (int)nudMaxQueueLength.Value;
                //Update the SingleQueueLength value
                gov.SingleQueueLength = cboxSingleQueueLength.Checked; 
            }
        }

        /// <summary>
        /// Called when the RunSim button is clicked
        /// </summary>
        /// <param name="sender">The Button that triggered this event</param>
        /// <param name="e">The events EventArgs</param>
        private void btnRunSim_Click(object sender, EventArgs e)
        {
            //Hide this form
            this.Hide();
            //Have the govenor execute a simulation run
            gov.ExecuteSimulation();
            //Show this form
            this.Show();
        }
        
        /// <summary>
        /// Called when the Representative number numericupdown's value changes
        /// </summary>
        /// <param name="sender">The NumericUpDown that triggered this event</param>
        /// <param name="e">The events EventArgs</param>
        private void nudRepNumber_ValueChanged(object sender, EventArgs e)
        {
            //Get the currently selected SalesRepType
            SalesRepType selected = listRepTypes.SelectedItem as SalesRepType;

            //If there was a selected repType
            if (selected != null)
            {
                //If the repNums dictionary contains this reptype as a key
                if (gov.RepNums.ContainsKey(selected))
                {
                    //Set the new number of representatives
                    gov.RepNums[selected] = (int)nudRepNumber.Value;
                }
            }
        }

        /// <summary>
        /// Resets the Core when the New menu option is clicked
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The events EventArgs</param>
        private void newSimMenuItem_Click(object sender, EventArgs e)
        {
            //Reset the govenor
            gov.Reset();
        }

        /// <summary>
        /// Loads a chosen .sim file
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The events EventArgs</param>
        private void openSimMenuItem_Click(object sender, EventArgs e)
        {
            // Get the path for the executable
            string saveDir = DESConstants.APP_DIR + DESConstants.SIMULATIONS_SAVE_DIRECTORY;

            //Check that the savesDir exists
            if (Directory.Exists(saveDir))
            {
                // Set the open file dialogs initial path
                openSimFile.InitialDirectory = saveDir;
            }
            

            // Show the open file dialog and store the result
            DialogResult dr = openSimFile.ShowDialog();

            // If the result was a OK then attempt to load the file
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Get the file name from the open file dialog
                string file = openSimFile.FileName;
               
                //Try to load the settings
                try
                {
                    gov.LoadSettingsFromFile(file);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to load file");
                }                
            }
        }

        /// <summary>
        /// Saves the current simulation settings to a .sim file
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The events EventArgs</param>
        private void saveSimMenuItem_Click(object sender, EventArgs e)
        {
            // Get the path for saving sims
            string saveDir = DESConstants.APP_DIR + DESConstants.SIMULATIONS_SAVE_DIRECTORY;

            // Check that the savesDir exists
            if (Directory.Exists(saveDir))
            {
                // Set the save file dialogs initial path
                saveSimFile.InitialDirectory = saveDir;
            }

            // Show the save file dialog and store the result
            DialogResult dr = saveSimFile.ShowDialog();

            // If the dialog result is OK
            if (dr == DialogResult.OK)
            {
                // Get the filepath from the save file dialog
                string file = saveSimFile.FileName;
                // Get the govenor to save its settings
                gov.SaveSettingsToFile(file);
            }
        }


        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The events EventArgs</param>
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
