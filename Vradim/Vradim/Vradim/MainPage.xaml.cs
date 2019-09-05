using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Vradim.GameLogic;
using Vradim.Models;
using Caliburn.Micro;
using System.Threading;

namespace Vradim
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPageControls ControlsMainPage { get; set; }
        private IEventAggregator _events;
        public MainPage()
        {
            InitializeComponent();
            _events.SubscribeOnUIThread(this);
            ControlsMainPage = new MainPageControls();
            this.BindingContext = ControlsMainPage;
            ControlsMainPage.SeasonText.Text = "winter";
        }

        private void SeasonText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var seasonText = e.NewTextValue.ToLower();

            ControlsMainPage.SeasonImage.Source = ImageSource.FromResource(
                Metabolism.gwtBackGroundImage(seasonText));

            

        }

    }
}
