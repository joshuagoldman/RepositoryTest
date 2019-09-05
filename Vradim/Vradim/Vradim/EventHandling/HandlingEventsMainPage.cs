using System;
using System.Collections.Generic;
using System.Text;
using Vradim.GameLogic;
using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace Vradim
{
    public partial class MainPage : IHandle<Metabolism.AnimalToShowEvent>
    {
        public Task HandleAsync(Metabolism.AnimalToShowEvent animal, CancellationToken cancellationToken)
        {
            return new Task(s => ControlsMainPage.SeasonText.Text = "" , cancellationToken);
        }
    }
}
