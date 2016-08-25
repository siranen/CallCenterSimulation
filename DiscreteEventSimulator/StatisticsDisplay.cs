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
    /// Defines the Form for displaying statistics
    /// </summary>
    public partial class StatisticsDisplay : Form
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private StatisticsManager statsMan;

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the StatisticsDisplay bound to the given StatisticsManager
        /// </summary>
        /// <param name="statsMan">The StatisticsManager instance that this display is bound to</param>
        /// <param name="windowX">The X-Coordinate that the window should start at</param>
        /// <param name="windowY">The Y-Cooridinate that the window should start at</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given StatisticsManager is null</exception>
        public StatisticsDisplay(int windowX, int windowY, StatisticsManager statsMan)
        {
            // Check that the given StatisticsManager is not null
            if (statsMan != null)
            {
                InitializeComponent();
                this.statsMan = statsMan;
                this.Location = new Point(windowX, windowY);

                //Bind the StatisticsManager.StatisticsUpdated event to the UpdateDisplay method
                statsMan.StatisticsUpdated += new EventHandler(UpdateDisplay);
            }
            else // The given StatisticsManager is null
            {
                throw new ArgumentNullException("statsMan", "Attempted to pass null StatisticsManager to StatisticsDisplay constructor");
            }
        }

        /// <summary>
        /// Event Handler for the StatisticsManager.StatisticsUpdated event. 
        /// </summary>
        /// <param name="sender">The object that triggered this event</param>
        /// <param name="e">The arguments passed with the event</param>
        private void UpdateDisplay(object sender, EventArgs e)
        {
            //Create the string to be set on the display
            string elapsedDisplay = "";
            //Add the number of days to the string if the number of days is greater than 0
            elapsedDisplay += statsMan.TimeElapsed.Days > 0 ? (statsMan.TimeElapsed.Days.ToString() + " days ") : "";
            //Add the number of hours to the string if the number of hours is greater than 0
            elapsedDisplay += statsMan.TimeElapsed.Hours > 0 ? (statsMan.TimeElapsed.Hours.ToString() + " hours ") : "";
            //Add the number of minutes to the string if the number of minutes is greater than 0
            elapsedDisplay += statsMan.TimeElapsed.Minutes > 0 ? (statsMan.TimeElapsed.Minutes.ToString() + " minutes ") : "";
            //Add the number of seconds to the string if the number of seconds is greater than 0
            elapsedDisplay += statsMan.TimeElapsed.Seconds > 0 ? (statsMan.TimeElapsed.Seconds.ToString() + " seconds") : "";

            //Set the elapsed time label to the created string
            lblElapsedTime.Text = elapsedDisplay;

            //Update the DataGridView
            UpdateDataGridView();
        }

        /// <summary>
        /// Updates the DataGridView to represent the state of the StatisticsManager
        /// </summary>
        private void UpdateDataGridView()
        {
            //Clear all current Rows
            dgvStatistics.Rows.Clear();

            //Retrieve the statistics as a an array of object arrays
            object[][] statistics = statsMan.ToArray();

            //Loop foreach array in the outer array
            foreach (object[] row in statistics)
            {
                //Add the array as a row to the DataGridView
                dgvStatistics.Rows.Add(row);
            }
        }

        /// <summary>
        /// Triggered when the form is closed. Removes the EventHandler from the StatisticsManager
        /// </summary>
        private void StatisticsDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            statsMan.StatisticsUpdated -= UpdateDisplay;
        }

        private void dgvStatistics_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
