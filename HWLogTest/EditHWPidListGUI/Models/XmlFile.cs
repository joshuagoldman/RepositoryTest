using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace EditHWPidListGUI.Models
{
    public class XmlFile
    {
        //private static string PathToXML = "HWPidList.xml";
        //private static string PathToXLSX = "Input data PRTT.XLSX";

        private XmlDocument _doc = new XmlDocument();
        private XmlNodeList _selectedNodeList;

        public List<string> DocAsListOfStrings = new List<string>();

        private string _pathToXml;


        //Constructor
        public XmlFile(string PathToXml)
        {
            _pathToXml = PathToXml;
        }

        public void ReadXmlToRawStringList()
        {
            DocAsListOfStrings = File.ReadAllLines(_pathToXml).ToList();
        }
        public int startRow = 0;
        public int endRow = 100000;
        public int FindCurrentNodeLineIndex(string NodeType, string AttributeType, string Attribute)
        {
            for (int i = startRow; i < DocAsListOfStrings.Count && i < endRow; i++)
            {
                //p(_docAsListOfStrings[i]);
                if (DocAsListOfStrings[i].Contains("<" + NodeType) && DocAsListOfStrings[i].Replace(" ", "").Contains(AttributeType + "=\"" + Attribute))
                {
                    return i;
                }
            }
            return -1;
        }
        public int FindCurrentNodeLineInsertionPoint(string NodeType, string AttributeType, string Attribute)
        {
            for (int i = startRow; i < DocAsListOfStrings.Count && i < endRow; i++)
            {
                //p(_docAsListOfStrings[i]);
                if (DocAsListOfStrings[i].Contains("<" + NodeType))
                {
                    var A = DocAsListOfStrings[i].Split('\"');
                    for (int c = 0; c < A.Length; c++)
                    {
                        if (A[c].Contains(AttributeType))
                        {
                            if (A[c + 1].CompareTo(Attribute) > 0)
                                return i;
                            c = A.Length;
                        }
                    }
                }
            }
            return endRow;
        }


        public void ReadXmlNodes(string SelectNodes)
        {
            // load XML from file
            _doc.Load(_pathToXml);

            // get a list of nodes 
            _selectedNodeList = _doc.SelectNodes(SelectNodes);// "/Document/HWPidList/Product");

        }

        
        public void SortXML(string NodeToOrderBy)
        {
            //LINQ-Lambda is not applicable since it cannot handle the comments...
            ////Read all elements in xml-file
            //XElement root = XElement.Load(PathToXML);
            ////Select and sort the products by Number-attribute
            //var orderedProducts = root.Elements("HWPidList").Elements("Product")
            //                      .OrderBy(xtab => xtab.Attribute("Number").ToString())
            //                      .ToArray();



            //filestream (to allso read the comments)
            var originalXmlFile = File.ReadAllLines(_pathToXml).ToList();

            int FirstProdLine;
            int LastProdLine;
            //Find first noden
            for (FirstProdLine = startRow; !originalXmlFile[FirstProdLine].Contains("<" + NodeToOrderBy); FirstProdLine++) { } //45 i originalet för att skippa exemplet \t<
            //Find last product
            for (LastProdLine = originalXmlFile.Count() - 1; !originalXmlFile[LastProdLine].Contains("<" + NodeToOrderBy); LastProdLine--) { }
            //LastProdLine = 664; //skippa produktgrupperna i slutet...!
            if (LastProdLine > endRow)
                LastProdLine = endRow;

            int NrOfKLines = 1;
            int NrOfProdLines = 1;

            for (var j = FirstProdLine + 1; j < LastProdLine; j++)
            {


                NrOfKLines = 1;
                NrOfProdLines = 1;
                var k = j - 1;

                //Go down to next product (if current is not a product)
                while (!originalXmlFile[j].Contains("<" + NodeToOrderBy) && j < LastProdLine) { j++; }

                //Calc nr of lines for current product
                while (!originalXmlFile[j + NrOfProdLines].Contains("<" + NodeToOrderBy) && j + NrOfProdLines < LastProdLine) { NrOfProdLines++; }

                //Go up to previous product
                while (!originalXmlFile[k].Contains("<" + NodeToOrderBy) && k > FirstProdLine)
                {
                    k--;
                    NrOfKLines++;
                }

                //Extract first attribute ("Number")
                var CurProdNr = originalXmlFile[j].Split('\"')[1];
                var KProdNr = originalXmlFile[k].Split('\"')[1];

                //Default moveto-position is current position
                int moveTo = j;

                //do the matching to find where row j is to be inserted
                while ((CurProdNr.CompareTo(KProdNr) < 0 || !originalXmlFile[k].Contains("<" + NodeToOrderBy)) && k >= FirstProdLine) //Compare Number strings
                {
                    //If line "k" is a product, do the match and if true: update moveTo-line nr. 
                    if (originalXmlFile[k].Contains("<" + NodeToOrderBy) && CurProdNr.CompareTo(KProdNr) < 0)
                        moveTo = k;


                    k--;

                    //retrive "Number" for line k if its a product-line
                    if (originalXmlFile[k].Contains("<" + NodeToOrderBy))
                        KProdNr = originalXmlFile[k].Split('\"')[1];

                }
                //Revert one line down since last check didn't match
                k++;

                //if moveTo-line is set other then current => move 
                if (j != moveTo)
                {
                    int n = NrOfProdLines;
                    while (n > 0)
                    {
                        originalXmlFile.Insert(moveTo, originalXmlFile[j + NrOfProdLines - 1]);
                        originalXmlFile.RemoveAt(j + NrOfProdLines);
                        n--;
                    }



                }

            }

            //Write to file
            using (TextWriter tw = new StreamWriter(_pathToXml))
            {
                foreach (String s in originalXmlFile)
                {
                    //if(s.Trim().Length>0) //remove empty lines
                    tw.WriteLine(s);
                }

            }


        }

        public void InsertTagsInHWPidlist(string PathToXLSX)
        {
            List<Dictionary<string, string>> RadioTableFromExcel = XlsmImport.ReadExcelFile(PathToXLSX, "Tags");

            //FindCurrentNodeLineIndex
            for (int i = 0; i < RadioTableFromExcel.Count; i++)
            {
                string currKrc = "";
                RadioTableFromExcel[i].TryGetValue("PRTT", out currKrc);
                string LAT = "";
                RadioTableFromExcel[i].TryGetValue("LAT", out LAT);

                for (int j = 0; j < DocAsListOfStrings.Count; j++)
                {
                    string currLine = DocAsListOfStrings[j];
                    if (currLine.Length > 0)
                        currLine= currLine.Replace(" ", "");
                    
                    if (currLine.Contains("Number=\"" + currKrc + "\""))
                    {
                        //p("Före:");
                        //p(DocAsListOfStrings[j - 1]);
                        //p(DocAsListOfStrings[j]);
                        //p(DocAsListOfStrings[j + 1]);
                        //< Tags >
                        //    < Tag Name = "prtt-supported" Value = "yes" />
                        //   < Tag Name = "lat-supported" Value = "yes" />
                        //</ Tags >
                        DocAsListOfStrings[j] = DocAsListOfStrings[j].Replace("/>", ">");
                        string TagStr = "\t\t\t<Tags>";
                        DocAsListOfStrings.Insert(j + 1, TagStr);
                        j++;

                        TagStr = "\t\t\t\t<Tag Name=\"prtt-supported\" Value=\"Yes\" />";
                        DocAsListOfStrings.Insert(j + 1, TagStr);
                        j++;

                        if (LAT != "")
                        {
                            TagStr = "\t\t\t\t<Tag Name=\"lat-supported\" Value=\"Yes\" />";
                            DocAsListOfStrings.Insert(j + 1, TagStr);
                            j++;
                        }
                            
                        TagStr = "\t\t\t</Tags>";
                        DocAsListOfStrings.Insert(j + 1, TagStr);
                        if (!DocAsListOfStrings[j + 2].Contains("<Rev"))
                        {
                            j++;
                            DocAsListOfStrings.Insert(j + 1, "\t\t</Product>");
                        }
                        

                        //p("Efter:");
                        //p(DocAsListOfStrings[j - 1]);
                        //p(DocAsListOfStrings[j]);
                        //p(DocAsListOfStrings[j + 1]);
                        //p(DocAsListOfStrings[j + 2]);
                        //p(DocAsListOfStrings[j + 3]);
                        //p(DocAsListOfStrings[j + 4]);
                        //p(DocAsListOfStrings[j + 5]);

                    }
                }
            }
            SaveFile();
        }
        public void InsertTestplanTagsInHWPidlist(string PathToXLSX)
        {
            List<Dictionary<string, string>> RadioTableFromExcel = XlsmImport.ReadExcelFile(PathToXLSX);

            //FindCurrentNodeLineIndex
            for (int i = 0; i < RadioTableFromExcel.Count; i++)
            {
                string currKrc = "";
                RadioTableFromExcel[i].TryGetValue("KRC", out currKrc);
                string CurrTP = "";
                RadioTableFromExcel[i].TryGetValue("TP", out CurrTP);

                currKrc = currKrc.Replace(" ","");
                CurrTP=CurrTP.Replace(" ", "");
                for (int j = 0; j < DocAsListOfStrings.Count; j++)
                {
                    string currLine = DocAsListOfStrings[j];
                    if (currLine.Length > 0)
                        currLine = currLine.Replace(" ", "");

                    if (currLine.Contains("Number=\"" + currKrc + "\""))
                    {
                        p("Före:");
                        for (int l = -5; l < 10; l++)
                            p(DocAsListOfStrings[j + l]);
                        //< Tags >
                        //    < Tag Name = "prtt-supported" Value = "yes" />
                        //   < Tag Name = "lat-supported" Value = "yes" />
                        //</ Tags >
                        bool NewTag = false;
                        string TagStr = "";
                        if (!DocAsListOfStrings[j + 1].Contains("<Tags"))
                        {
                            NewTag = true;
                            DocAsListOfStrings[j] = DocAsListOfStrings[j].Replace("/>", ">");

                            TagStr = "\t\t\t<Tags>";
                            DocAsListOfStrings.Insert(j + 1, TagStr);
                            
                        }
                        j++;
                        switch (CurrTP)
                        {
                            case "20":
                                TagStr = "\t\t\t\t<Tag Name=\"lat-category\" Value=\"duw-dul-dus\"/> <!-- Valid for testplan 20/-->";
                                break;
                            case "21":
                                TagStr = "\t\t\t\t<Tag Name=\"lat-category\" Value=\"dug\"/> <!--Valid for testplan 21/-->";
                                break;
                            case "22":
                                TagStr = "\t\t\t\t<Tag Name=\"lat-category\" Value=\"radio\"/> <!--Valid for testplan 22/-->";
                                break;
                            case "23":
                                TagStr = "\t\t\t\t<Tag Name=\"lat-category\" Value=\"radio\"/> <!--Valid for testplan 23/-->";
                                DocAsListOfStrings.Insert(j + 1, TagStr);
                                j++;
                                TagStr = "\t\t\t\t<Tag Name=\"lat-refunit\" Value=\"dus\"/> <!--Valid for testplan 23/-->";
                                break;
                            case "25":
                                TagStr = "\t\t\t\t<Tag Name=\"lat - category\" Value=\"tcu-siu\"/> <!--Valid for testplan 25/-->";
                                break;
                            case "26":
                                TagStr = "\t\t\t\t<Tag Name=\"lat-category\" Value=\"baseband\"/> <!--Valid for testplan 26/-->";
                                break;
                            case "27":
                                TagStr = "\t\t\t\t<Tag Name=\"lat-category\" Value=\"radio\"/> <!--Valid for testplan 27/-->";
                                DocAsListOfStrings.Insert(j + 1, TagStr);
                                j++;
                                TagStr = "t\t\t\t<Tag Name=\"lat-refunit\" Value=\"baseband\"/> <!--Valid for testplan 27/-->";
                                break;
                        }
                        
                        DocAsListOfStrings.Insert(j + 1, TagStr);
                        j++;

                        if (NewTag)
                        {
                            TagStr = "\t\t\t</Tags>";
                            DocAsListOfStrings.Insert(j + 1, TagStr);
                            j++;
                            if (!DocAsListOfStrings[j + 1].Contains("<Rev"))
                            {
                                
                                DocAsListOfStrings.Insert(j + 1, "\t\t</Product>");
                            }
                        }




                        p("Efter:");
                        for(int l=-5;l<10;l++)
                            p(DocAsListOfStrings[j +l]);
                        
                        
                        
                    }
                }
            }
            SaveFile();
        }

        private void EditXML(string PathToXML, string NewString)
        {

            // get a list of nodes 
            XmlNodeList aNodes = this._doc.SelectNodes("/Document/HWPidList/Product");

            // loop through all selected nodes
            foreach (XmlNode CurrentNode in aNodes)
            {
                // grab the attribute
                XmlAttribute CurrAttribute = CurrentNode.Attributes["Number"];

                // check if the attribute exists...
                if (CurrAttribute != null)
                {
                    string currentValue = CurrAttribute.Value;

                    if (currentValue == "KDU 137 944/1")
                    {
                        XmlNode NewNode = CurrentNode.Clone();
                        NewNode.Attributes["Number"].Value = NewString;

                        CurrentNode.ParentNode.InsertAfter(NewNode, CurrentNode); //Insert under CurrentNode

                        //CurrentNode.ParentNode.AppendChild(NewNode); //Insert at bottom. 

                    }

                    //New test;:
                    //XmlAttribute NewAttr = doc.CreateAttribute("test");

                    //NewAttr.Value = "Testar";

                    //CurrentNode.Attributes.Append(NewAttr); //Insert as last node...
                    break;

                }
            }

            // save the XmlDocument back to disk
            _doc.Save(PathToXML); //Note that it's only saved in debug-folder copy of this file!!!
        }

        public void ExportListOfNodesWithAttr(string Node, string Attrib, string val)
        {
            List<string> strLst = new List<string>();

            _doc.Load(_pathToXml);
            // get a list of nodes 
            XmlNodeList aNodes = this._doc.SelectNodes("/Document/HWPidList/" + Node);

            // loop through all selected nodes
            foreach (XmlNode CurrentNode in aNodes)
            {
                // grab the attribute
                XmlAttribute CurrAttribute = CurrentNode.Attributes[Attrib];
                
                foreach(var childNode in CurrentNode.ChildNodes)
                {
                    if(childNode.GetType()== typeof(XmlElement))
                    {
                        XmlNode a = (XmlNode)childNode;
                        string s = "";
                        foreach(XmlNode x in a.ChildNodes)
                        {
                            if (x.GetType() == typeof(XmlElement))
                            {
                                if (x.Attributes["Name"].Value == Attrib)
                                {
                                    if (x.Attributes["Value"].Value.ToUpper() == "YES")
                                        s += ";" + Attrib;
                                }
                                if (x.Attributes["Name"].Value == "lat-supported")
                                {
                                    if (x.Attributes["Value"].Value.ToUpper() == "YES")
                                        s += ";;lat-supported";

                                }

                            }

                        }
                        if(s!="")
                            strLst.Add(CurrentNode.Attributes["Number"].Value + s);
                    }
                }

            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (TextWriter tw = new StreamWriter(path + "\\HWPidListExport.txt"))
            {
                foreach (String s in strLst)
                    tw.WriteLine(s);
            }

        }

        public void SaveFile()
        {

            using (TextWriter tw = new StreamWriter(_pathToXml))
            {
                foreach (String s in DocAsListOfStrings)
                    tw.WriteLine(s);
            }
            MessageBox.Show($"Done saving to: {_pathToXml}\r\n Remember to update \"TestFileVersion.txt\" before Push!", "", MessageBoxButton.OK, MessageBoxImage.Question);
        }


        private void p(string s)
        {
            Debug.WriteLine(s);
        }
    }

}


//// Open the text file using a stream reader.
//using (StreamReader sr = new StreamReader(PathToXML))
//{
//    // Read the stream to a string, and write the string to the console.
//    String line = sr.ReadToEnd();
//    Console.WriteLine(line);
//}

//var xDoc = Sort(XDocument.Load(PathToXML));

//xDoc.Save(PathToXML);
////var xDoc = XDocument.Load(PathToXML);
//var xDoc = XDocument.Load(PathToXML);
////var accounts = xDoc.Root.Elements("summary").Elements("account");

//var accounts = xDoc.Root.Elements("HWPidList").Elements("Product");

//foreach (XElement elem in accounts)
//{
//    //sb.Append(elem.ToString());
//}





