using AttaxxPlus.Model.Operations;
using AttaxxPlus.ViewModel;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace AttaxxPlus.Boosters
{
    // EVIP: interface implementing several other interfaces
    // EVIP: for compactness, a Booster is both view model and command.
    public interface IBooster : IOperation, ICommand, INotifyPropertyChanged
    {
        // Note: needed to set the view model after calling the parameterless
        //  constructor (instantiated by Activator using reflection)
        GameViewModel GameViewModel { get; set; }

        // EVIP: interface does not require setter
        BitmapImage Image { get; }

        string Title { get; }

        void InitializeGame();
    }
}
