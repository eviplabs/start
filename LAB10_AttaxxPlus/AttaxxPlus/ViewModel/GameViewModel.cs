using System.Collections.ObjectModel;
using System.Linq;
using System;
using AttaxxPlus.Model;
using Windows.UI.Popups;

namespace AttaxxPlus.ViewModel
{
    public class GameViewModel : ObservableObject
    {
        public readonly GameBase Model;

        public GameViewModel(GameBase model) : base()
        {
            this.Model = model;
            Model.PropertyChanged += Model_PropertyChanged;
            // EVIP: complex step extracted to separate method
            InitFieldsBasedOnModel();
            // EVIP: instantiating nested view model class
            BoosterListViewModel = new BoosterListViewModel(this);
        }

        #region Field management
        // Note: We cannot bind an ItemControl element to List<FieldViewModel> so we create a wrapper class FieldViewModelList.
        public ObservableCollection<FieldViewModelList> Fields { get; set; }

        private void InitFieldsBasedOnModel()
        {
            var fieldCommand = new FieldCommand(this);
            Fields = new ObservableCollection<FieldViewModelList>();
            // EVIP: getting sizes of two dimensional array
            for (int row = 0; row < Model.Fields.GetLength(0); row++)
            {
                var rowList = new FieldViewModelList();
                for (int col = 0; col < Model.Fields.GetLength(1); col++)
                {
                    // EVIP: compact object initialization.
                    rowList.Add(
                        new FieldViewModel(Model.Fields[row, col])
                            { FieldCommand = fieldCommand }
                        );
                }
                Fields.Add(rowList);
            }
        }

        // EVIP: property with further setter responsibilities.
        private FieldViewModel selectedField;
        public FieldViewModel SelectedField
        {
            get => selectedField;
            set
            {
                if (selectedField != value)
                {
                    // Note: both old and new value can be null!
                    if (selectedField != null)
                        selectedField.IsSelected = false;
                    selectedField = value;
                    if (selectedField != null)
                        selectedField.IsSelected = true;
                    Notify();
                }
            }
        }
        #endregion

        public int CurrentPlayer { get => Model.CurrentPlayer; }

        // EVIP: nullable type for winner
        public int? Winner { get => Model.Winner; }
        public bool IsGameRunning { get => Model.IsGameRunning; }

        #region INPC forwarding
        // EVIP: INPC forwarding for a list of properties stored in a string[].
        private readonly string[] dependentPropertyNames =
            { nameof(GameBase.Winner), nameof(GameBase.IsGameRunning),
              nameof(GameBase.CurrentPlayer) };

        private void Model_PropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Note: forwarding INPC of wrapped model properties.
            // Using array instead of a list of "if" conditions.
            if (dependentPropertyNames.Contains(e.PropertyName))
                Notify(e.PropertyName);
        }
        #endregion

        public FieldCommand FieldCommand { get; private set; }

        internal async void EndOfTurn()
        {
            SelectedField = null;
            Model.EndOfTurn();

            if (!Model.IsGameRunning)
            {
                // EVIP: MessageDialog and ShowAsync, using async-await pattern
                // EVIP: string interpolation
                MessageDialog m = new MessageDialog(
                    $"Game over. Winner is player {Model.Winner}");
                await m.ShowAsync();
                Model.InitializeGame();
                foreach (var b in BoosterListViewModel.Boosters)
                    b.InitializeGame();
            }
        }

        public readonly BoosterListViewModel BoosterListViewModel;
    }
}
