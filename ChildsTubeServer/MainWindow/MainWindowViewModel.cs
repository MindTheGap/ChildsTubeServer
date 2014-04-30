using ChildsTubeServer.DB;
using ChildsTubeServer.Helpers;
using ChildsTubeServer.LogManagerNS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ChildsTubeServer.ViewModel
{
    public class MainWindowViewModel
    {
        #region DB

        public static mainDBEntities MainDBEntities = new mainDBEntities();

        #endregion DB

        #region Data Members

        public LogManager LogManager { get; set; }

        #endregion Data Members

        public XElement ConfigurationXMLObject { get; set; }

        public MainWindowViewModel()
        {
            try
            {
                this.ConfigurationXMLObject = XElement.Load(Definitions.ConfigurationFileName);
                if (this.ConfigurationXMLObject == null)
                {
                    Helper.PrintErrorMessage("Configuration file couldn't be loaded!");
                }
                else
                {
                    InitObject();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception.Message);
            }
        }

        private void InitObject()
        {
            // Open App.Config of executable
            var configurationXMLObject = this.ConfigurationXMLObject;
            XElement logManagerChunk = configurationXMLObject.Element("LogManager");
            if (logManagerChunk != null)
            {
                this.LogManager = new LogManager();
                this.LogManager.Create(logManagerChunk);
            }
        }
    }
}
