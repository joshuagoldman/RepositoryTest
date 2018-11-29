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
    public partial class MainWindow : Window, INotifyPropertyChanged
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
        }

        public void Generate_Click(object sender, RoutedEventArgs e)
        {
            ClassInit.GenAct.PerformActions();
            OnPropertyChanged("Main");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Loaded_Window(object sender, RoutedEventArgs e)
        {
            Main = new MainWindow();
            ClassInit = new InitializeClasses()
            {
                Main = Main
            };

            ClassInit.PerformInitiliazation();

            OnPropertyChanged("Main");
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
