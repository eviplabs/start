using AttaxxPlus.Model;
using AttaxxPlus.ViewModel;
using Windows.UI.Xaml.Controls;

namespace AttaxxPlus.View
{
    // EVIP: almost empty view. Instantiates view model and model.
    public sealed partial class GameView : UserControl
    {
        public GameViewModel ViewModel;

        public GameView()
        {
            this.InitializeComponent();
            ViewModel = new GameViewModel(new MultiGame(5));
        }
    }
}
