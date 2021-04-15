using PredmetniZadatak2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PredmetniZadatak2.Controlers
{
    public class XMLParser
    {
        private List<PointEntity> pointEntities = new List<PointEntity>();
        //private List<LineEntity> lineEntities = new List<LineEntity>();
        //private string fileName;

        //public string FileName
        //{
        //    get { return fileName; }
        //    set { fileName = value; }
        //}


        public static void LoadSubstations(List<PointEntity> entities, double pointX, double pointY, string filename)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            XmlNodeList nodeList;
            nodeList = xmlDocument.DocumentElement.SelectNodes("/NetworkModel/Substations/SubstationEntity");

            foreach (XmlNode node in nodeList)
            {

            }
        }

        public static void LoadSwitches()
        {

        }

        public static void LoadNodes()
        {

        }
    }
}
