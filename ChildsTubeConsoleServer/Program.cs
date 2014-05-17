using System.Data.SqlClient;
using System.Runtime.InteropServices;
using ChildsTubeConsoleServer.ConsoleManagerNS;
using ChildsTubeConsoleServer.Helpers;
using ChildsTubeConsoleServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;

namespace ChildsTubeConsoleServer
{
    class Program
    {
        static public MainWindowViewModel ViewModel { get; set; }

        public static XElement ConfigurationXMLObject { get; set; }

        static void Main(string[] args)
        {
            ConfigurationXMLObject = XElement.Load(Definitions.ConfigurationFileName);
            if (ConfigurationXMLObject == null)
            {
                Helper.PrintErrorMessage("Configuration file couldn't be loaded!");
                return;
            }

            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

            ViewModel = new MainWindowViewModel(ConfigurationXMLObject);

            ConsoleManager.InitObject(ConfigurationXMLObject);

            Console.WriteLine("Listening... type 'help' for further usage.");

            ConsoleManager.ReadCommands(); // blocking command
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
          // Put your own handler here
          switch (ctrlType)
          {
            case CtrlTypes.CTRL_C_EVENT:
            case CtrlTypes.CTRL_CLOSE_EVENT:
            case CtrlTypes.CTRL_LOGOFF_EVENT:
            case CtrlTypes.CTRL_SHUTDOWN_EVENT:
              ConsoleManager.SaveToDbHandler();
              Console.WriteLine("Wrote to DB successfully!");
              Console.ReadLine();
              break;
          }
          return true;
        }

      

        #region unmanaged
        // Declare the SetConsoleCtrlHandler function
        // as external and receiving a delegate.

        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine
        // for SetConsoleCtrlHandler.
        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        // An enumerated type for the control messages
        // sent to the handler routine.
        public enum CtrlTypes
        {
          CTRL_C_EVENT = 0,
          CTRL_BREAK_EVENT,
          CTRL_CLOSE_EVENT,
          CTRL_LOGOFF_EVENT = 5,
          CTRL_SHUTDOWN_EVENT
        }

        #endregion
 
    }
}
