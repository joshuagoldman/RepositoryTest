using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Methods;
using System.ComponentModel;



namespace WpfApp1
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

        public void Generate_Click(object sender, RoutedEventArgs e)
        {
            ClassInit.ChoicAct.ChangeToRedNotification();
            ClassInit.ChoicAct.RedNotificationPopUpMessage();
        }

        public void Save_Clicked(object sender, RoutedEventArgs e)
        {
            ClassInit.ChoicAct.SaveFileOrNot();
        }

        private void Expression_Clicked(object sender, RoutedEventArgs e)
        {
            ExWindow.Show();
            ExWindow.DataContext = ClassInit.ChoicAct.ControlInfo;
        }

        private void Products_Clicked(object sender, RoutedEventArgs e)
        {
            ProdWindow.Show();
            ProdWindow.DataContext = ClassInit.ChoicAct.ControlInfo;
        }

        private void InfoText_Clicked(object sender, RoutedEventArgs e)
        {
            InfoTextWin.Show();
            InfoTextWin.DataContext = ClassInit.ChoicAct.ControlInfo;
        }

        private void Get_Clicked(object sender, RoutedEventArgs e)
        {
            ClassInit.ChoicAct.ChangeToRedNotification();
            ClassInit.ChoicAct.CheckTreeExistence();
        }
    }
}
