using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Gat.Controls;
using EditHWPidListGUI.Models;
using System.Windows.Media;
using System.Windows.Input;
using EditHWPidListGUI.Extensions;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Point = System.Drawing.Point;
using Main.XmlDoc;


namespace EditHWPidListGUI.ViewModel
{
    public class ListViewItems // if you want runtime changes to be reflected in the UI
    {

        private string _fontWeight;
        private List<string> _bGcolor;

        public List<string> BGcolor
        {
            get { return _bGcolor; }
            set { _bGcolor = value; }
        }

        private List<string> _previewText;

        public List<string> PreviewText
        {
            get { return _previewText; }
            set { _previewText = value; }
        }

        public string FontWeight
        {
            get { return _fontWeight; }
            set { _fontWeight = value; }
        }

        //Constructor
        public ListViewItems(List<string> T, string FW, List<string> BGc, int LineNr)
        {
            _previewText = new List<string>();
            _bGcolor = new List<string>();
            PreviewText.Add((LineNr + 1).ToString());
            BGcolor.Add(BGc[0]);
            FontWeight = FW;
            for (int i = 0; i <= 16; i++)
            {
                if (i < T.Count)
                {
                    PreviewText.Add(T[i]);
                    BGcolor.Add(BGc[i]);
                }
                else
                {
                    PreviewText.Add("");
                    BGcolor.Add("White");
                }

            }

        }
    }


    public class ViewModel : INotifyPropertyChanged
    {
        ExEmEl CurrXml;
        private ExEmEl HwLogCrit;
        private XmlFile ConfigKeyDocumentDefinitions;
        string CurrDoc;
        string CurrEl;
        string CurrNode;

        private List<Dictionary<string, string>> RadioTableFromExcel;

        private int _selectedRadioIndex = 0;
        public int SelectedRadioIndex
        {
            get { return _selectedRadioIndex; }
            set
            {
                if (value >= KRCList.Count || value < 0)
                    return;
                _selectedRadioIndex = value;
                RaisePropertyChanged("SelectedRadioIndex");
                UpdateAllFields();
            }
        }

        private int _selectedTPIndex;

        public int SelectedTPIndex
        {
            get { return _selectedTPIndex; }
            set {
                _selectedTPIndex = value;
                clearGenStrOfLatCat();
                switch (value)
                {
                    case 1:
                        modifyGenStr("lat-category", "radio", true);
                        break;
                    case 2:
                        modifyGenStr("lat-category", "radio", true);
                        modifyGenStr("lat-refunit", "dus", true);
                        break;
                    case 3:
                        modifyGenStr("lat-category", "radio", true);
                        modifyGenStr("lat-refunit", "baseband", true); ;
                        break;
                    case 4:
                        modifyGenStr("lat-category", "baseband", true);
                        break;
                    case 5:
                        modifyGenStr("lat-category", "baseband", true);
                        modifyGenStr("lat-refunit", "dus", true);
                        break;
                    case 6:
                        modifyGenStr("lat-category", "duw_dul_dus", true);
                        break;
                    case 7:
                        modifyGenStr("lat-category", "dug", true);
                        break;
                    case 8:
                        modifyGenStr("lat-category", "tcu_siu", true);
                        break;
                }

                RaisePropertyChanged("SelectedTPIndex");
            }
        }

        private void clearGenStrOfLatCat()
        {
            GeneratedString = GeneratedString.Replace("\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"radio\"/>", "");
            GeneratedString = GeneratedString.Replace("\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"baseband\"/>", "");
            GeneratedString = GeneratedString.Replace("\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"duw_dul_dus\"/>", "");
            GeneratedString = GeneratedString.Replace("\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"dug\"/>", "");
            GeneratedString = GeneratedString.Replace("\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"tcu_siu\"/>", "");

            GeneratedString = GeneratedString.Replace("\n\t\t\t\t<Tag Name=\"lat-refunit\" Value=\"dus\"/>", "");
            GeneratedString = GeneratedString.Replace("\n\t\t\t\t<Tag Name=\"lat-refunit\" Value=\"baseband\"/>", "");
        }
        private void modifyGenStr(string Name, string Val, bool newItem)
        {
            if(!GeneratedString.Contains("<Tags>"))
                GeneratedString = GeneratedString.Replace("/>", ">");

            GeneratedString = GeneratedString.Replace("\r", "");

            GeneratedString = GeneratedString.Replace("\n\t\t</Product>", ""); //if exist

            string newStr = "\n\t\t\t\t<Tag Name=\"" + Name + "\" Value=\"" + Val + "\"/>";

            if (!GeneratedString.Contains("<Tags>"))
            {
                GeneratedString += "\n\t\t\t<Tags>";
            }
            else
            {
                GeneratedString = GeneratedString.Replace("\n\t\t\t</Tags>", "");
            }
                
            if (GeneratedString.Contains(newStr) || !newItem)
            {
                GeneratedString = GeneratedString.Replace(newStr, "");
            }
            else
            {
                GeneratedString += newStr;
            }

            GeneratedString += "\n\t\t\t</Tags>";
            GeneratedString += "\n\t\t</Product>";
        }

        private string genTagStr()
        {
            bool addTag = false;

            string TagStr = ">\n\t\t\t<Tags>";

            if (_prttTag)
            {
                addTag = true;
                TagStr += "\n\t\t\t\t<Tag Name=\"prtt-supported\" Value=\"Yes\"/>";
            }
            if (LatTag)
            {
                addTag = true;
                TagStr += "\n\t\t\t\t<Tag Name=\"lat-supported\" Value=\"Yes\"/>";
            }
            if (SelectedTPIndex > 0)
            {
                if(!(SelectedTPIndex==0))
                    addTag = true;

                switch (SelectedTPIndex)
                {
                    case 1:
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"radio\"/>";
                        break;
                    case 2:
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"radio\"/>";
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-refunit\" Value=\"dus\"/>";
                        break;
                    case 3:
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"radio\"/>";
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-refunit\" Value=\"baseband\"/>";
                        break;
                    case 4:
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"baseband\"/>";
                        break;
                    case 5:
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"baseband\"/>";
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-refunit\" Value=\"dus\"/>";
                        break;
                    case 6:
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"duw_dul_dus\"/>";
                        break;
                    case 7:
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"dug\"/>";
                        break;
                    case 8:
                        TagStr += "\n\t\t\t\t<Tag Name=\"lat-category\" Value=\"tcu_siu\"/>";
                        break;
                }
                
            }
            TagStr += "\n\t\t\t</Tags>";
            TagStr += "\n\t\t</Product>";
            if (!addTag)
                TagStr = "/> ";

            return TagStr;
        }
        private string _generatedString = "";

        public string GeneratedString
        {
            get { return _generatedString; }
            set
            {
                _generatedString = value;
                RaisePropertyChanged("GeneratedString");
            }
        }


        private string _testPower;


        public string TestPower
        {
            get { return _testPower; }
            set
            {
                _testPower = value;
                RaisePropertyChanged("TestPower");
                PreviewCurrXml();
            }
        }
        private string _freq;
        public string Freq
        {
            get { return _freq; }
            set
            {
                _freq = value;
                RaisePropertyChanged("Freq");
                PreviewCurrXml();
            }
        }
        private string _band;
        public string Band
        {
            get { return _band; }
            set
            {
                _band = value;
                RaisePropertyChanged("band");
                PreviewCurrXml();
            }
        }
        private string _rBB;
        public string RBB
        {
            get { return _rBB; }
            set
            {
                _rBB = value;
                RaisePropertyChanged("RBB");
                PreviewCurrXml();
            }
        }
        private string _software;
        public string Software
        {
            get { return _software; }
            set
            {
                _software = value;
                RaisePropertyChanged("Software");
                PreviewCurrXml();
            }
        }
        private string _prodName;
        public string ProdName
        {
            get { return _prodName; }
            set
            {
                _prodName = value;
                RaisePropertyChanged("ProdName");
                PreviewCurrXml();
            }
        }

        private bool _radioTestAllowed;

        public bool RadioTestAllowed
        {
            get { return _radioTestAllowed; }
            set
            {
                _radioTestAllowed = value;
                PreviewCurrXml();
            }
        }



        private string _markName;

        public string MarkName
        {
            get { return _markName; }
            set
            {
                _markName = value;
                RaisePropertyChanged("MarkName");
                PreviewCurrXml();
            }
        }



        private string _markNameColor;
        public string MarkNameColor
        {
            get { return _markNameColor; }
            set
            {
                _markNameColor = value;
                RaisePropertyChanged("MarkNameColor");
            }
        }


        public string _prodNameColor;
        public string ProdNameColor
        {
            get { return _prodNameColor; }
            set
            {
                _prodNameColor = value;
                RaisePropertyChanged("ProdNameColor");
            }
        }

        private string _bandColor;
        public string BandColor
        {
            get { return _bandColor; }
            set
            {
                _bandColor = value;
                RaisePropertyChanged("BandColor");
            }
        }

        private string _powerColor;
        public string PowerColor
        {
            get { return _powerColor; }
            set
            {
                _powerColor = value;
                RaisePropertyChanged("PowerColor");
            }
        }

        private string _freqColor;
        public string FreqColor
        {
            get { return _freqColor; }
            set
            {
                _freqColor = value;
                RaisePropertyChanged("FreqColor");
            }
        }

        private string _rBBcolor;
        public string RBBColor
        {
            get { return _rBBcolor; }
            set
            {
                _rBBcolor = value;
                RaisePropertyChanged("RBBColor");
            }
        }

        private string _softwareColor;
        public string SoftwareColor
        {
            get { return _softwareColor; }
            set
            {
                _softwareColor = value;
                RaisePropertyChanged("SoftwareColor");
            }
        }

        private bool _prttTag;

        public bool PrttTag
        {
            get { return _prttTag; }
            set {
                _prttTag = value;
                RaisePropertyChanged("PrttTag");
                modifyGenStr("prtt-supported","Yes", value);
               
            }
        }


        private bool _latTag;

        public bool LatTag
        {
            get { return _latTag; }
            set {
                _latTag = value;
                RaisePropertyChanged("LatTag");

                modifyGenStr("lat-supported", "Yes", value);
                if (!value) { }
                    SelectedTPIndex = 0;
            }
        }


        Dictionary<int, string> KRCDict;
        Dictionary<int, string> RUDict;

        private ObservableCollection<KeyValuePair<int, string>> _radioList;

        public ObservableCollection<KeyValuePair<int, string>> RadioList
        {
            get { return _radioList; }
            set
            {
                _radioList = value;
            }
        }

        private ObservableCollection<KeyValuePair<int, string>> _kRCList;

        public ObservableCollection<KeyValuePair<int, string>> KRCList
        {
            get { return _kRCList; }
            set
            {
                _kRCList = value;
            }
        }
        public string CurrentKRC
        {
            get
            {
                string s = "";
                if (KRCDict != null)
                    KRCDict.TryGetValue(_selectedRadioIndex, out s);
                //p(s);
                return s;
            }
            set { }

        }
        public string CurrentRU
        {
            get
            {
                string s = "";
                RUDict.TryGetValue(_selectedRadioIndex, out s);
                //p(s);
                return s;
            }
            set { }
        }

        ObservableCollection<ListViewItems> _rowsToDisp = new ObservableCollection<ListViewItems>();
        public ObservableCollection<ListViewItems> RowsToDisp
        {
            get
            {
                if (_rowsToDisp.Count <= 0)
                {
                    //_rowsToDisp.Add("1");
                    //_rowsToDisp.Add("2");
                    //_rowsToDisp.Add("3");
                    //_rowsToDisp.Add("4");
                }
                return _rowsToDisp;
            }
            set
            {
                _rowsToDisp = value;
            }
        }

        private ObservableCollection<KeyValuePair<int, string>> _tpList;
        public ObservableCollection<KeyValuePair<int, string>> TpList
        {
            get { return _tpList; }
            set
            {
                _tpList = value;
            }
        }

        public ICommand btnUseGenerated { get; set; }
        public ICommand btnInsertGenerated { get; set; }
        public ICommand btnSaveFile { get; set; }
        public ICommand btnFetchOriginal { get; set; }
        public ICommand btnRegenString { get; set; }
        public ICommand rbtnCurrXML { get; set; }
        public ICommand btAddProduct { get; set; }


        private bool canexecutemethod(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void executemethod(object parameter)
        {
            if (HwLogCrit == null)
            {
                HwLogCrit = new ExEmEl(PathToHWPidList, ExEmEl.NewDocument.No);
            }

            CurrXml = HwLogCrit;
            CurrDoc = "HWLogCriteria";
            CurrEl = "SearchGroup";
            CurrNode = "SearchKey"; //Where krc is found
            PreviewCurrXml();
        }


        private int CurrLineIndex = 0;
        private void ReplaceWithGenerated()
        {
            //p(GeneratedString);
            var GenStringArray = GeneratedString.Replace("\r", "").Split('\n');
            int countLines = GenStringArray.Length;//GeneratedString.Count(f => f == '\r');
            for (int i = 0; i < countLines; i++)
            {
                CurrXml.DocAsListOfStrings.Insert(CurrLineIndex + i, GenStringArray[i]);
            }
            //Remove line under node
            CurrXml.DocAsListOfStrings.RemoveAt(CurrLineIndex + countLines);
            //remove lines in node
            while (!CurrXml.DocAsListOfStrings[CurrLineIndex + countLines].Contains("<" + CurrEl) && CurrLineIndex + countLines <= CurrXml.endRow)
            {
                CurrXml.DocAsListOfStrings.RemoveAt(CurrLineIndex + countLines);
            }

            //CurrXml.DocAsListOfStrings.Insert(CurrLineIndex, GeneratedString);
            //CurrXml.DocAsListOfStrings.RemoveAt(CurrLineIndex + 1);
            //UpdateAllFields();
            PreviewXml();

            //InsertGen();
            //FetchOriginal(); //Show edited string again...
            //CurrXml.DocAsListOfStrings[]
        }
        private bool isInsertingLines = false;
        private void InsertGen()
        {
            var GenStringArray = GeneratedString.Replace("\r", "").Split('\n');
            int countLines = GenStringArray.Length;//GeneratedString.Count(f => f == '\r');

            CurrXml.DocAsListOfStrings.Insert(CurrLineIndex, GenStringArray[0]);
            for (int i = 1; i < countLines; i++) //Insert all lines under ie product (<rev...>)
            {
                CurrXml.DocAsListOfStrings.Insert(CurrLineIndex + i, GenStringArray[i]);
                isInsertingLines = true;
            }
            switch (CurrDoc)
            {
                case "HWPidList":

                    break;
                case "ConfigKeyDocumentDefinitions":

                    ConfigKeyDocumentDefinitions.endRow = ConfigKeyDocumentDefinitions.endRow + countLines;
                    CurrXml.endRow = CurrXml.endRow + countLines;

                    break;
            }
            // p(GeneratedString);
            //CurrXml.DocAsListOfStrings.Insert(CurrLineIndex, GeneratedString);
            //UpdateAllFields();
            PreviewXml();

            FetchOriginal(); //Show edited string again...
            //CurrXml.DocAsListOfStrings[]
        }

        private void FetchOriginal()
        {
            int i = CurrLineIndex;
            GeneratedString = CurrXml.DocAsListOfStrings[i];
            i++;

            while (!CurrXml.DocAsListOfStrings[i].Contains("<" + CurrEl))
            {
                GeneratedString += "\r\n" + CurrXml.DocAsListOfStrings[i];
                i++;
            }
            //p(GeneratedString);

        }
        private void RegenString()
        {
            GeneratedString = GenerateNewLine();
        }
        private bool _rowExist;

        public bool RowExist
        {
            get { return _rowExist; }
            set
            {
                _rowExist = value;
                RaisePropertyChanged("RowExist");
            }
        }


        public string PathToFile;
        private string _reposFolderPath;
        public string PathToHWPidList;
        public string PathToConfigKeyDocumentDefinitions;
        public string ReposFolderPath
        {
            get { return _reposFolderPath; }
            set
            {
                _reposFolderPath = value;
                PathToHWPidList = value + "\\HWLogCriteria.xml";//\\documents\\HWPidList.xml"; //DebugTest "HWPidList.xml";//Datapackets\HWPidList\documents
                PathToConfigKeyDocumentDefinitions = value + "HWLogCriteria.xml";
            }
        }

        private void SaveFiles()
        {
            if (HwLogCrit != null)
                HwLogCrit.SaveFile();

            if (ConfigKeyDocumentDefinitions != null)
                ConfigKeyDocumentDefinitions.SaveFile();
        }
        private void AddProduct()
        {
            var s = Interaction.InputBox("Enter SearchKey:", "hjkh", "K12345");
            if (s.Length == 0)
                return;
            var sgb = new SearchGroupBox
            {
                Location = new Point(200, 200),                
            };

            CurrNode = 
            
            if (sgb.Text.Length == 0)
                return;
            CurrLineIndex = CurrXml.FindCurrentNodeLineIndex(CurrEl, CurrNode, s.Replace(" ", ""));
            KRCList.Add(new KeyValuePair<int, string>(KRCDict.Count, s));

            RadioList.Add(new KeyValuePair<int, string>(KRCDict.Count, sgb.Text));

            KRCDict = KRCList.ToDictionary(x => x.Key, x => x.Value);
            RUDict = RadioList.ToDictionary(x => x.Key, x => x.Value);

            int i = 0;
            foreach (var r in RUDict)
            {
                if (r.Value == sgb.Text)
                {
                    SelectedRadioIndex = i;// r.Key;
                    break;
                }


                i++;
            }
        }

        private void insertTagsInHWPidList()
        {
            OpenDialogView openDialog = new OpenDialogView();
            OpenDialogViewModel vm = (OpenDialogViewModel)openDialog.DataContext;
            vm.Caption = "Select \".XLS...\"";
            vm.IsDirectoryChooser = false;
            vm.Show();

            PathToFile = vm.SelectedFilePath?.ToString();

            ReposFolderPath = Properties.Settings.Default.ReposPath;
            HwLogCrit = new XmlFile(PathToHWPidList);
            HwLogCrit.ReadXmlToRawStringList();
            //HwPidList.InsertTagsInHWPidlist(PathToFile);
            HwLogCrit.InsertTestplanTagsInHWPidlist(PathToFile);

            Environment.Exit(0);
        }
        private void ExportFromHWPidList()
        {
           
            ReposFolderPath = Properties.Settings.Default.ReposPath;
            HwPidList = new XmlFile(PathToHWPidList);

            HwPidList.ExportListOfNodesWithAttr("Product", "prtt-supported", "YES");

            Environment.Exit(0);
        }

        bool debugging = false;
        //Constructor
        public ViewModel()
        {
            debugging = true;
            //insertTagsInHWPidList();
            //ExportFromHWPidList();
            //return;

            //Relay button-commands
            btnUseGenerated = new RelayCommand(o => ReplaceWithGenerated());
            btnInsertGenerated = new RelayCommand(o => InsertGen());
            btnSaveFile = new RelayCommand(o => SaveFiles());
            btnFetchOriginal = new RelayCommand(o => FetchOriginal());
            btnRegenString = new RelayCommand(o => RegenString());
            btAddProduct = new RelayCommand(o => AddProduct());

            rbtnCurrXML = new RelayCommand(executemethod, canexecutemethod);//(o => executemethod(this));//



            //Get path to Input data PRTT.XLSX-file
            SelectExcelFile();//DebugTest//"Input data PRTT.XLSX";//

            //Properties.Settings.Default.Reset(); //Debug

            //Read stored default props to see if path is saved 
            if (Properties.Settings.Default.ReposPath == "")
            {
                //Get path to root repos-folder (to find HWPidlist.xml)
                ReposFolderPath = SelectRepFolder();//DebugTest "B:\\repos"; //
                //Spar mappväg för nästa gång programmet öppnas
                Properties.Settings.Default.ReposPath = ReposFolderPath;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }
            else
            {
                if (debugging)
                {
                    ReposFolderPath = Properties.Settings.Default.ReposPath;
                }
                else
                {
                    if (System.Windows.MessageBox.Show("Open Repos root path: " + Properties.Settings.Default.ReposPath, "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {

                        ReposFolderPath = Properties.Settings.Default.ReposPath;
                    }
                    else
                    {
                        //Get path to root repos-folder (to find HWPidlist.xml)
                        ReposFolderPath = SelectRepFolder();//DebugTest "B:\\repos"; //
                                                            //Spar mappväg för nästa gång programmet öppnas
                        Properties.Settings.Default.ReposPath = ReposFolderPath;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();
                    }
                }
                    
                
                
            }

            //CurrXml = new XmlFile(PathToConfigKeyDocumentDefinitions);
            //CurrXml.startRow = 712;
            //CurrXml.endRow = 984;
            //CurrXml.SortXML("ConfigKeyDocumentEntry");
            //return;

            TpList = new ObservableCollection<KeyValuePair<int, string>>();
            TpList.Add(new KeyValuePair<int, string>(0, string.Empty));
            TpList.Add(new KeyValuePair<int, string>(1, "Radio"));
            TpList.Add(new KeyValuePair<int, string>(2, "Radio_DUS"));
            TpList.Add(new KeyValuePair<int, string>(3, "Radio_Baseband"));
            TpList.Add(new KeyValuePair<int, string>(4, "Baseband"));
            TpList.Add(new KeyValuePair<int, string>(5, "Baseband_DUS"));
            TpList.Add(new KeyValuePair<int, string>(6, "DUW/DUL/DUS"));
            TpList.Add(new KeyValuePair<int, string>(7, "DUG"));
            TpList.Add(new KeyValuePair<int, string>(8, "TCU/SIU"));
            
            

            executemethod("HWPidList");
            
            CurrXml = HwPidList; //Set this 
            CurrDoc = "HWPidList";
            CurrEl = "Product";
            CurrNode = "Number";

            //initiate checkbox to true
            RadioTestAllowed = true;

            PreviewCurrXml();



        }





        private string SelectRepFolder()
        {
            System.Windows.MessageBox.Show("Select root Repos-folder in the tree-view window on next prompt", "", MessageBoxButton.OK, MessageBoxImage.Question);
            OpenDialogView openDialog = new OpenDialogView();
            OpenDialogViewModel vm = (OpenDialogViewModel)openDialog.DataContext;
            vm.Caption = "Select root Repos-folder in the tree-view window";
            vm.IsDirectoryChooser = true;
            vm.Show();

            string path = vm.SelectedFolder?.Path?.ToString();
            if (path == null)
            {
                MessageBox.Show("You didn't select the correct folder! \nRestart program to try again", "", MessageBoxButton.OK, MessageBoxImage.Question);
                //Kill program
                Environment.Exit(0);
                return null;
            }

            var CorrectFolder  = vm.SelectedFolder.Children.Any(childfolder => childfolder.Name == "HWLogTest");

            if (!CorrectFolder)
            {
                MessageBox.Show("You didn't select the correct folder! \nRestart program to try again", "!", MessageBoxButton.OK, MessageBoxImage.Question);
                //Kill program
                Environment.Exit(0);

                //SelectRepFolder();
                return null;
            }


            return path;
        }
        private void SelectExcelFile()
        {
            if (debugging)
                return;

            string path = "";
            if (MessageBox.Show("Do you want to import Excel-file: \"Input data PRTT.XLSX\"", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OpenDialogView openDialog = new OpenDialogView();
                OpenDialogViewModel vm = (OpenDialogViewModel)openDialog.DataContext;
                vm.Caption = "Select \"Input data PRTT.XLSX\"";
                vm.IsDirectoryChooser = false;
                vm.Show();

                path = vm.SelectedFilePath?.ToString();

                if (path == null || !path.ToUpper().Contains("XLSX"))
                {
                    MessageBox.Show("No file selected...\nRestart program to try again", "", MessageBoxButton.OK, MessageBoxImage.Question);
                    //SelectRepFolder();
                    return;
                }

                PathToFile = path;
                //Read Input data PRTT.XLSX and populate all inputs with first row
                PopulateWithFirstRow();
            }


        }

        private void PopulateWithFirstRow()
        {

            //Read all radio-information from the excel-sheet 
            RadioTableFromExcel = XlsmImport.ReadExcelFile(PathToFile);

            //Populate ddl:
            RadioList = new ObservableCollection<KeyValuePair<int, string>>();
            KRCList = new ObservableCollection<KeyValuePair<int, string>>();

            string CurVal;

            int i = 0;
            foreach (var r in RadioTableFromExcel)
            {
                CurVal = "";
                r.TryGetValue("Product", out CurVal);
                RadioList.Add(new KeyValuePair<int, string>(i, CurVal));

                r.TryGetValue("Product number", out CurVal);
                KRCList.Add(new KeyValuePair<int, string>(i, CurVal));

                i++;
            }

            //Find all KRx
            KRCDict = KRCList.ToDictionary(x => x.Key, x => x.Value);
            RUDict = RadioList.ToDictionary(x => x.Key, x => x.Value);

            UpdateAllFields();
        }

        private void UpdateAllFields()
        {
            string CurVal = "";

            //Populate txtboxes:
            CurVal = "";
            RadioTableFromExcel?[_selectedRadioIndex].TryGetValue("ProductName", out CurVal);
            ProdName = CurVal;

            CurVal = "";
            RadioTableFromExcel?[_selectedRadioIndex].TryGetValue("MarketName", out CurVal);
            MarkName = CurVal;

            RadioTableFromExcel?[_selectedRadioIndex].TryGetValue("Test output power [W]", out CurVal);
            TestPower = CurVal;

            CurVal = "";
            RadioTableFromExcel?[_selectedRadioIndex].TryGetValue("Test frequency [MHz]", out CurVal);
            Freq = CurVal;

            CurVal = "";
            RadioTableFromExcel?[_selectedRadioIndex].TryGetValue("TestBand", out CurVal);
            Band = CurVal;

            CurVal = "";
            RadioTableFromExcel?[_selectedRadioIndex].TryGetValue("RBB", out CurVal);
            RBB = CurVal;

            CurVal = "";
            RadioTableFromExcel?[_selectedRadioIndex].TryGetValue("Supported in software", out CurVal);
            Software = CurVal;

            PreviewCurrXml();
        }

        private void PreviewCurrXml()
        {
            if (CurrXml == null)
                return;

            if (CurrentKRC.Count() > 0)
            {
                CurrLineIndex = CurrXml.
            }
            else
            {
                RadioList = new ObservableCollection<KeyValuePair<int, string>>();
                KRCList = new ObservableCollection<KeyValuePair<int, string>>();

                var s = Interaction.InputBox("Product number:", "New product", "KRC 161 652/7");
                if (s.Length == 0)
                    return;

                var n = Interaction.InputBox("Product Name:", "New product", "Radio 2212 B4");
                if (n.Length == 0)
                    return;
                CurrLineIndex = CurrXml.FindCurrentNodeLineIndex(CurrEl, CurrNode, s.Replace(" ", ""));
                KRCList.Add(new KeyValuePair<int, string>(0, s));
                RadioList.Add(new KeyValuePair<int, string>(0, n));
                KRCDict = KRCList.ToDictionary(x => x.Key, x => x.Value);
                RUDict = RadioList.ToDictionary(x => x.Key, x => x.Value);

            }

            GeneratedString = GenerateNewLine();

            PreviewXml();
        }
        private void PreviewXml()
        {

            bool NL = false;
            if (CurrLineIndex < 2)
            {
                NL = true;
                string krc = CurrentKRC.Replace(" ", "");
                string CurrKrC = krc.Substring(0, 3) + " " + krc.Substring(3, 3) + " " + krc.Substring(6);

                CurrLineIndex = CurrXml.FindCurrentNodeLineInsertionPoint(CurrEl, CurrNode, CurrKrC);
                if (CurrDoc == "HWPidList")
                {
                    if (ProdName != null)
                        ProdNameColor = BGcExist;
                    if (MarkName != null)
                        MarkNameColor = BGcExist;
                    if (Band != null)
                        BandColor = BGcExist;
                    if (Freq != null)
                        FreqColor = BGcExist;
                    if (TestPower != null)
                        PowerColor = BGcExist;

                }
            }

            RowsToDisp.Clear();
            string FW;
            string BGc = "LightGray"; //Color.FromArgb(20, 192,192,192);//

            List<string> ListOfLineProps;
            List<string> ListOfPropColor;
            bool AfterCurrLine = false;
            int LastRow = 10;
            for (int i = -5; i <= LastRow; i++)
            {
                ListOfLineProps = new List<string>();
                ListOfPropColor = new List<string>();

                if (i == 0 && !AfterCurrLine) //Current line
                {
                    FW = "Normal";//"BOLD";
                    BGc = "LightGreen";
                    if (NL) //New line does not exist in hwpidlist 
                    {
                        BGc = "Yellow";
                        ListOfLineProps.Add(GeneratedString);
                        ListOfPropColor.Add("Yellow");
                        RowExist = true;
                        FW = "Normal";
                        BGc = "White";

                        i--; //Read next line again 
                        LastRow--;
                        NL = false;

                        AfterCurrLine = true;  //do not go here again! (i--)
                    }
                    else //Line exist:> show
                    {
                        RowExist = false;
                        List<string>[] Output = EvalCurrLine(CurrXml.DocAsListOfStrings[CurrLineIndex]); //Format the line
                        ListOfLineProps = Output[0];
                        ListOfPropColor = Output[1];
                        //while (NrOfLines > 1)
                        //{
                        //    RowsToDisp.Add(new ListViewItems(ListOfLineProps, FW, ListOfPropColor, CurrLineIndex + i));
                        //    ListOfLineProps = new List<string>();
                        //    ListOfPropColor = new List<string>();
                        //    ListOfLineProps.Add(CurrXml.DocAsListOfStrings[CurrLineIndex+i+1]);
                        //    ListOfPropColor.Add("Yellow");
                        //    NrOfLines--;
                        //}
                    }
                }
                else
                {
                    FW = "Normal";
                    BGc = "White";
                    ListOfLineProps.Add(CurrXml.DocAsListOfStrings[CurrLineIndex + i]);
                    ListOfPropColor.Add(BGc);
                }
                RowsToDisp.Add(new ListViewItems(ListOfLineProps, FW, ListOfPropColor, CurrLineIndex + i));


            }
        }

        string ColorByExistance(string Val, string CheckParam)
        {
            if (CheckParam == Val)
            {
                return "LightGreen";
            }
            return "Red";
        }
        string ReturnValOfAttr(string s, string[] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i].Contains(s))
                {
                    return A[i + 1];
                }

            }
            return "";
        }

        string BGcNotUsed = "White";
        string BGcExist = "LightGreen";
        string BGcMissing = "Salmon";
        private List<string>[] EvalCurrLine(string CurrExistingLine)
        {
            //<Product Number="KRC 161 495/2" Name="RRU4415B7" MarketName="Radio 4415 B7" RadioTestAllowed="YES" RequiresRadioTest="YES" RF_A="B7;40W" RF_B="B7;40W" RF_C="B7;40W" RF_D="B7;40W"/>

            List<string>[] Output = new List<string>[2];
            List<string> OutputText = new List<string>();
            List<string> OutputColor = new List<string>();




            string CurrStringBuilder;
            var ArrayOfCurr = CurrExistingLine.Split('\"');

            switch (CurrDoc)
            {
                case "HWPidList":

                    //add: <Product Number="KRC 161 495/2"
                    CurrStringBuilder = ArrayOfCurr[0] + "\"" + ArrayOfCurr[1] + "\"";// + ArrayOfCurr[2] + "\"" + ArrayOfCurr[3] + "\"";
                    OutputText.Add(CurrStringBuilder);
                    OutputColor.Add(BGcExist);



                    if (CurrExistingLine.Contains("Name=\"" + ProdName + "\""))
                    {
                        OutputText.Add(" Name=\"" + ProdName + "\"");
                        OutputColor.Add(BGcExist);
                        ProdNameColor = BGcExist;
                    }
                    else if (CurrExistingLine.Contains("Name"))
                    {
                        OutputText.Add(" Name=\"" + ReturnValOfAttr("Name", ArrayOfCurr) + "\"");
                        OutputColor.Add(BGcMissing);
                        ProdNameColor = BGcMissing;
                    }

                    if (CurrExistingLine.Contains("MarketName=\"" + MarkName + "\" "))
                    {
                        OutputText.Add(" MarketName=\"" + MarkName + "\"");
                        OutputColor.Add(BGcExist);
                        MarkNameColor = BGcExist;
                    }
                    else if (CurrExistingLine.Contains("MarketName"))
                    {
                        OutputText.Add(" MarketName=\"" + ReturnValOfAttr("MarketName", ArrayOfCurr) + "\"");
                        OutputColor.Add(BGcMissing);
                        MarkNameColor = BGcMissing;
                    }



                    if (RadioTestAllowed)
                    {
                        OutputText.Add(" RadioTestAllowed=\"YES\"");
                        OutputColor.Add(BGcExist);
                        OutputText.Add(" RequiresRadioTest=\"YES\"");
                        OutputColor.Add(BGcExist);
                    }

                    string TP = TestPower;
                    int NrOfTP = 0;
                    if (TP != null && TP.Contains("x"))
                    {
                        TP = TP.Split('x')[1];
                        NrOfTP = Int16.Parse(TestPower.Split('x')[0]);
                    }
                    if (CurrExistingLine.ToUpper().Contains("RF_A=\"" + Band + ";" + TP + "W;" + Freq + "MHZ\""))
                    {
                        OutputText.Add(" RF_A=\"" + Band + ";" + TP + "W;" + Freq + "MHZ\"");
                        OutputColor.Add(BGcExist);
                        BandColor = BGcExist;
                        PowerColor = BGcExist;
                        FreqColor = BGcExist;
                    }
                    else if (CurrExistingLine.Contains("RF_A"))
                    {
                        OutputText.Add(" RF_A=\"" + ReturnValOfAttr("RF_A", ArrayOfCurr) + "\"");
                        OutputColor.Add(BGcMissing);
                        if (!CurrExistingLine.Contains("RF_A=\"" + Band) || Band == null)
                        {
                            BandColor = BGcMissing;
                        }
                        if (!CurrExistingLine.Contains(";" + TP + "W"))
                        {
                            PowerColor = BGcMissing;
                        }
                        if (!CurrExistingLine.ToUpper().Contains(";" + Freq + "MHZ"))
                        {
                            FreqColor = BGcMissing;
                        }
                    }

                    char CurrBand = 'B';
                    for (int i = 1; i < NrOfTP; i++)
                    {
                        if (CurrExistingLine.ToUpper().Contains("RF_" + CurrBand + "=\"" + Band + ";" + TP + "W;" + Freq + "MHZ\""))
                        {
                            OutputText.Add(" RF_" + CurrBand + "=\"" + Band + ";" + TP + "W;" + Freq + "MHZ\"");
                            OutputColor.Add(BGcExist);
                        }
                        else if (CurrExistingLine.Contains("RF_" + CurrBand + ""))
                        {
                            OutputText.Add(" RF_" + CurrBand + "=\"" + ReturnValOfAttr("RF_" + CurrBand + "", ArrayOfCurr) + "\"");
                            OutputColor.Add(BGcMissing);
                        }
                        CurrBand++;
                    }



                    break;

                case "ConfigKeyDocumentDefinitions":
                    ProdNameColor = BGcNotUsed;
                    MarkNameColor = BGcMissing; //Used
                    BandColor = BGcMissing; //Used
                    FreqColor = BGcNotUsed;
                    PowerColor = BGcNotUsed;
                    RBBColor = BGcNotUsed;
                    SoftwareColor = BGcNotUsed;
                    //<ConfigKeyDocumentEntry Data="KRC 161 635/1" Band="B1" Label="Radio 4415 B1" ProductType="Rrus" ProductName="RRU4415" Description=""/>

                    CurrStringBuilder = ArrayOfCurr[0] + "\"" + ReturnValOfAttr("Data", ArrayOfCurr) + "\"";//add: <ConfigKeyDocumentEntry Data="KRC 161 635/1"
                    OutputText.Add(CurrStringBuilder);
                    OutputColor.Add(BGcExist);

                    if (CurrExistingLine.Contains("Band=\"" + ReturnValOfAttr("Band", ArrayOfCurr) + "\""))
                    {
                        OutputText.Add(" Band=\"" + ReturnValOfAttr("Band", ArrayOfCurr) + "\"");
                        OutputColor.Add(BGcExist);
                        BandColor = BGcExist;
                    }
                    else
                    {
                        OutputText.Add(" Band=\"" + ReturnValOfAttr("Band", ArrayOfCurr) + "\"");
                        OutputColor.Add(BGcMissing);
                        BandColor = BGcExist;
                        if (Band != null && Band != "")
                            BandColor = BGcMissing;
                    }

                    OutputText.Add(" Label=\"" + ReturnValOfAttr("Label", ArrayOfCurr) + "\"");
                    OutputColor.Add(BGcExist);

                    OutputText.Add(" ProductType=\"" + ReturnValOfAttr("ProductType", ArrayOfCurr) + "\"");
                    OutputColor.Add(BGcExist);

                    if (CurrExistingLine.Contains("ProductName=\"" + ReturnValOfAttr("ProductName", ArrayOfCurr) + "\"")) ;//RRU" + pn + "\""))
                    {
                        OutputText.Add(" ProductName=\"" + ReturnValOfAttr("ProductName", ArrayOfCurr) + "\""); //+ pn +
                        OutputColor.Add(BGcExist);
                    }

                    OutputText.Add(" Description=\"\"");
                    OutputColor.Add(BGcExist);

                    break;
            }
            if (CurrExistingLine.Contains("/>"))
            {
                OutputText.Add("/>");
            }
            else
            {
                OutputText.Add(">");
            }
            
            OutputColor.Add(BGcExist);

            //<Product Number="KRC 161 495/2" Name="RRU4415B7"  MarketName="Radio 4415 B7" kkk="j" RadioTestAllowed="YES" RequiresRadioTest="YES" RF_A="B7;40W" RF_B="B7;40W" RF_C="B7;40W" RF_D="B7;40W"/>
            //Array => kkk=4+5   6,7
            //Output => kkk=2    3
            for (int i = 2; i < ArrayOfCurr.Length - 1; i++) //Check if we missed something? start with Name...
            {
                bool Exists = false;
                if (ArrayOfCurr[i].Contains("="))
                {
                    int j;
                    for (j = 1; j < OutputText.Count; j++) //Skip /t<Product Number="KRC 161 495/2"
                    {
                        if (ArrayOfCurr[i].Contains(OutputText[j].Split('=')[0].Trim()))
                        {

                            Exists = true;
                            break;
                        }
                    }
                    if (!Exists)
                    {
                        OutputText.Insert(i / 2, ArrayOfCurr[i].Replace("/>", "") + "\"" + ArrayOfCurr[i + 1] + "\"");
                        OutputColor.Insert(i / 2, "Chocolate");
                    }
                }


            }

            Output[0] = OutputText;
            Output[1] = OutputColor;
            return Output;

        }
        private string GenerateNewLine()
        {
            string Output = "";
            //string Bx = "";
            //CurrentRU.Split(' ')[2].Substring(0,2)
            string krc = CurrentKRC.Replace(" ", "");

            switch (CurrDoc)
            {
                case "HWPidList":

                    ProdNameColor = BGcMissing;
                    MarkNameColor = BGcMissing;
                    BandColor = BGcMissing;
                    FreqColor = BGcMissing;
                    PowerColor = BGcMissing;
                    RBBColor = BGcNotUsed;
                    SoftwareColor = BGcNotUsed;

                    Output += "\t\t<Product Number=\"" + krc.Substring(0, 3) + " " + krc.Replace(" ", "").Substring(3, 3) + " " + krc.Substring(6) + "\"";

                    if (ProdName?.Length > 0)
                        Output += " Name=\"" + ProdName + "\"";

                    if (MarkName?.Length > 0)
                        Output += " MarketName=\"" + MarkName + "\"";

                    if (RadioTestAllowed)
                        Output += " RadioTestAllowed=\"YES\" RequiresRadioTest=\"YES\"";

                    string TP = TestPower;
                    int NrOfTP = 0;
                    if (TP?.Length > 0 && TP.Contains("x"))
                    {
                        TP = TP.Split('x')[1];
                        NrOfTP = Int16.Parse(TestPower.Split('x')[0]);
                    }


                    if (Band?.Length > 0 || TP?.Length > 0 || Freq?.Length > 0)
                    {
                        char CurrBand = 'A';
                        for (int i = 0; i < NrOfTP; i++)
                        {
                            Output += " RF_" + CurrBand + "=\"";
                            CurrBand++;

                            if (Band?.Length > 0)
                                Output += Band + ";";
                            if (TP?.Length > 0)
                                Output += TP + "W;";
                            if (Freq?.Length > 0)
                                Output += Freq + "MHZ";
                            Output += "\"";
                        }
                        Output = Output.Replace(";\"", "\"");
                    }

                    //Output += "/>";

                    Output += genTagStr();

                    break;

                case "ConfigKeyDocumentDefinitions":

                    ProdNameColor = BGcNotUsed;
                    MarkNameColor = BGcNotUsed;
                    BandColor = BGcExist;
                    FreqColor = BGcNotUsed;
                    PowerColor = BGcNotUsed;
                    RBBColor = BGcNotUsed;
                    SoftwareColor = BGcNotUsed;

                    //<ConfigKeyDocumentEntry Data="KRC 161 652/1" Band="B5" Label="Radio 2212 B5" ProductType="Rrus" ProductName="RRU2212" Description=""/>
                    Output += "\t\t\t\t<ConfigKeyDocumentEntry Data=\"" + krc.Substring(0, 3) + " " + krc.Replace(" ", "").Substring(3, 3) + " " + krc.Substring(6) + "\"";

                    Output += " Band=\"" + Band + "\"";

                    Output += " Label=\"" + CurrentRU + "\"";

                    Output += " ProductType=\"Rrus\"";

                    string pn = "";
                    //if (MarkName?.Length >= 7)
                    {
                        pn = CurrentRU.Split(' ')[1];
                    }


                    Output += " ProductName=\"RRU" + pn + "\"";

                    Output += " Description=\"\"";

                    Output += "/>";

                    break;

            }
            return Output;
        }


        //private double windowHeight;
        //public double WindowHeight
        //{
        //    get { return windowHeight; }
        //    set
        //    {

        //        windowHeight = value;

        //        //WindowHeight = windowHeight;

        //        RaisePropertyChanged("WindowHeight");

        //    }
        //}
        //private double windowWidth;
        //public double WindowWidth
        //{
        //    get { return windowWidth; }
        //    set
        //    {

        //        windowWidth = value;
        //        RaisePropertyChanged("WindowWidth");
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Kallas på från code-behind!
        public void Window_KeyDown(KeyEventArgs e)
        {
            // ... Test for F5 key.
            switch (e.Key)

            {
                //Behåll
                case Key.Right:
                case Key.Down:
                    SelectedRadioIndex++;
                    break;
                case Key.Left:
                case Key.Up:
                    SelectedRadioIndex--;
                    break;


            }

        }
        private void p(string s)
        {
            Debug.WriteLine(s);
        }
    }
}
