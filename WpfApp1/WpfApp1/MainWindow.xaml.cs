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

 
            ClassInit = new InitializeClasses()
            {
                Main = this
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
            ExpressionWindow ExWindows = new ExpressionWindow()
            {
                ControlInfo = ClassInit.ChoicAct.ControlInfo
            };
            ExWindows.Show();
            ExWindows.AddDataContext();
        }
    }
}
