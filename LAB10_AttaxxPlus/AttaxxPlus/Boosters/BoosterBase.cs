using System;
using System.Linq;
using AttaxxPlus.Model;
using AttaxxPlus.ViewModel;
using Windows.UI.Xaml.Media.Imaging;

namespace AttaxxPlus.Boosters
{
    public abstract class BoosterBase : ObservableObject, IBooster
    {
		// How many times can the user activate this booster
		protected int[] usableCounter;
		protected int initialUsableCounter = 1;

		// EVIP: specialties when instantiated using Activator
		// Warning: Instantiated by Activator using default ctor.
		//  GameViewModel is set after that, so in derived classes,
		//  methods invoked from ctor should not rely on GameViewModel.
		private GameViewModel gameViewModel;
        public GameViewModel GameViewModel
        {
            get => gameViewModel;
            set
            {
                if (gameViewModel != null)
                    gameViewModel.PropertyChanged -= GameViewModel_PropertyChanged;
                gameViewModel = value;
                gameViewModel.PropertyChanged += GameViewModel_PropertyChanged;
            }
        }

        public BitmapImage Image { get; protected set; }
        public abstract string Title { get; }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        public bool CanExecute(object parameter) => true;

        public BoosterBase(GameViewModel gvm)
        {
			GameViewModel = gvm;
            InitializeGame();
        }

        public void Execute(object parameter)
        {
			// EVIP: Elvis operator in chain

			if (usableCounter[GameViewModel.CurrentPlayer] > 0) {
				if (TryExecute(GameViewModel.SelectedField?.Model, null)) {
					usableCounter[GameViewModel.CurrentPlayer]--;
					GameViewModel.EndOfTurn();
				}
			}
        }

        protected void LoadImage(Uri imageFileUri)
        {
            // EVIP: loading an image from Uri
            Image = new BitmapImage
            {
                UriSource = imageFileUri
            };
        }

        private void GameViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
			if (e.PropertyName == "CurrentPlayer") {
				this.CurrentPlayerChanged();
			}
			if (e.PropertyName == "Winner") {
				this.WinnerChanged();
			}
		}

        public abstract bool TryExecute(Field selectedField, Field currentField);

        // EVIP: virtual method and not abstract.
        // It has a default not doing anything,
        //  so override is not mandatory.
        public virtual void InitializeGame() {
			usableCounter = Enumerable.Repeat(initialUsableCounter, GameViewModel.Model.NumberOfPlayers + 1).ToArray();
		}

		// Called when GameViewModel.CurrentPlayer has changed.
		protected virtual void CurrentPlayerChanged() { }

		// Called when GameViewModel.Winner has changed. Useful to reset booster counters.
		protected virtual void WinnerChanged() {
			if (GameViewModel.Winner == null) {
				InitializeGame();
			}
		}
	}
}
