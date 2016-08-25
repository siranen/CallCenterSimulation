using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// This class defines constants that could be used throught the application
    /// </summary>
    public static class DESConstants
    {
        //-------------------------------------------------------------------
        //- CONSTANTS                                                       -
        //-------------------------------------------------------------------
        public static string APP_DIR = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

        public const string SIMULATIONS_SAVE_DIRECTORY = @"\simulations";
        public const string DATA_FILE_DIRECTORY = @"\data";
        public const string DEFAULT_SIM_SETTINGS = SIMULATIONS_SAVE_DIRECTORY + @"\default.sim";
        public const string SAVE_FILE_TEMPLATE = DATA_FILE_DIRECTORY + @"\template.xml";        
    }
}
