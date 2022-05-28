using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;


namespace HistoryNastBot
{
    struct WebLink
    {
        public string? Url { get; set; }
        public string? Name { get; set; }
        
    }
    internal class XmlHistoryList
    {
        public string? Region { get; set; }
        public List<WebLink> Links { get; set; }

        static string dirWork = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        /// <summary> Файл, который необходимо парсить </summary>
        string fileXML = dirWork + @"\XMLHistoryList.xml";

        public XmlHistoryList(string Name)
        {
            XmlDocument document = new XmlDocument(); 
            document.Load(fileXML);

            XmlElement? xRoot = document.DocumentElement;

            if (xRoot != null)
            {
                foreach (XmlElement xNode in xRoot)
                {
                    XmlNode? attr = xNode.Attributes.GetNamedItem("name");
                    if (attr != null && attr?.Value == Name)
                    {
                        Region = attr?.Value;

                        Links = new List<WebLink>();

                        foreach (XmlNode childenode in xNode.ChildNodes)
                        {
                            WebLink link = new WebLink();

                            foreach (XmlNode childenode2 in childenode.ChildNodes)
                            {
                                if (childenode2.Name == "url") 
                                    link.Url = childenode2.InnerText;

                                if (childenode2.Name == "text")
                                    link.Name = childenode2.InnerText;
                            }
                            Links.Add(link);
                        }
                    }
                    else continue;

                }
            }

        }


    }
}
