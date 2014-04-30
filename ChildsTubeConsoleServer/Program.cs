using ChildsTubeConsoleServer.Helpers;
using ChildsTubeConsoleServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChildsTubeConsoleServer
{
    class Program
    {
        static public MainWindowViewModel ViewModel { get; set; }

        public static XElement ConfigurationXMLObject { get; set; }

        private static string UsageString { get; set; }

        static void Main(string[] args)
        {
            ConfigurationXMLObject = XElement.Load(Definitions.ConfigurationFileName);
            if (ConfigurationXMLObject == null)
            {
                Helper.PrintErrorMessage("Configuration file couldn't be loaded!");
                return;
            }

            ViewModel = new MainWindowViewModel(ConfigurationXMLObject);

            InitObject(ConfigurationXMLObject);

            while (true)
            {
                string strCommand = Console.ReadLine();
                HandleCommand(strCommand);
            }
        }

        static void InitObject(XElement ConfigurationXElement)
        {
            var consoleChunk = ConfigurationXElement.Element("Console");
            if (consoleChunk != null)
            {
                string strUsage;
                Helper.ReadValue(consoleChunk, "Usage", out strUsage);
                UsageString = strUsage;
            }
        }

        static void HandleCommand(string strCommand)
        {
            if (CompareLowerStrings(strCommand, "help","usage","?"))
            {
                Console.WriteLine(UsageString);
            }
        }

        public static bool CompareLowerStrings(string string1, params string[] arrStrings)
        {
            if (string1 == null || arrStrings == null)
                return false;

            foreach (var item in arrStrings)
            {
                if (string1.ToLower().CompareTo(item) == 0)
                    return true;
            }

            return false;
        }
    }
}
