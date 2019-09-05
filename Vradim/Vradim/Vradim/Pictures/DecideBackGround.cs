using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vradim.MarkupExtensions
{
    class DecideBackGround : IMarkupExtension
    {
        public string Season { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return ImageSource.FromResource(
                Season == "fall" ? "Vradim.MarkupExtensions.MarkupPictures.Fall.jpg" :
                Season == "winter" ? "Vradim.MarkupExtensions.MarkupPictures.Winter.jpg" :
                Season == "summer" ? "Vradim.MarkupExtensions.MarkupPictures.Summer.jpg" :
                Season == "spring" ? "Vradim.MarkupExtensions.MarkupPictures.Spring.jpg" :
                                           "Vradim.MarkupExtensions.MarkupPictures.Vradim_Picture.jpg");
        }
    }
}
