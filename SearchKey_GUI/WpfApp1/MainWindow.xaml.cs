using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SearchKey_GUI.Methods;
using Xceed.Wpf.Toolkit;
using System.ComponentModel;



namespace SearchKey_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public InitializeClasses ClassInit { get; set; }

        public MainWindow Main { get; set; }

        public ExpressionWindow ExWindow { get; set; }

        public ProductsWindow ProdWindow { get; set; }

        public InfoTextWindow InfoTextWin { get; set; }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public MainWindow()
        {
            InitializeComponent();

            ExWindow = new ExpressionWindow();

            ProdWindow = new ProductsWindow();

            InfoTextWin = new InfoTextWindow();

            ClassInit = new InitializeClasses
            {
                Main = this,
                ExWindow = ExWindow,
                ProdWindow = ProdWindow,
                InfoTextWin= InfoTextWin
            };

            ClassInit.PerformInitiliazation();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Generate_Click(object sender, RoutedEventArgs e)
        {
            ClassInit.ChoicAct.NotifyMandatoryFields = ChoiceActions.TurnEmptyToRed.No;
            ClassInit.ChoicAct.WindowText2ControlInfoClass();
            ClassInit.ChoicAct.Write32App();
        }

        public void Save_Clicked(object sender, RoutedEventArgs e)
        {
            ClassInit.ChoicAct.SaveFile = ChoiceActions.SaveXmlFile.Yes;
            ClassInit.ChoicAct.SaveFileActions();
        }

        private void Expression_Clicked(object sender, RoutedEventArgs e)
        {
            ExWindow.Show();
            ExWindow.DataContext = ClassInit.ChoicAct.ControlInfo;
        }

        private void Products_Clicked(object sender, RoutedEventArgs e)
        {
            ProdWindow.Visibility = Visibility.Visible;
            ProdWindow.DataContext = ClassInit.ChoicAct.ControlInfo;
        }

        private void Infotext_Clicked(object sender, RoutedEventArgs e)
        {
            InfoTextWin.Show();
            InfoTextWin.DataContext = ClassInit.ChoicAct.ControlInfo;
        }

        private void Get_Clicked(object sender, RoutedEventArgs e)
        {
            ClassInit.ChoicAct.WindowText2ControlInfoClass();
            ClassInit.ChoicAct.CheckTreeExistence();
        }
    }
}
