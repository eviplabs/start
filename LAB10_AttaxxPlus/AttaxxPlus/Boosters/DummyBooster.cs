using System;
using System.Linq;
using AttaxxPlus.Model;
using AttaxxPlus.ViewModel;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster not doing anything. (But activating it takes a turn.)
    /// Features a player-independent counter to limit the number of activations.
    /// </summary>
    public class DummyBooster : BoosterBase
    {
        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"Dummy ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        public DummyBooster(GameViewModel gvm)
            : base(gvm)
        {
            // EVIP: referencing content resource with Uri.
            //  The image is added to the project as "Content" build action.
            //  See also for embedded resources: https://docs.microsoft.com/en-us/windows/uwp/app-resources/
            // https://docs.microsoft.com/en-us/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code
            LoadImage(new Uri(@"ms-appx:///Boosters/DummyBooster.png"));
        }

		protected override void CurrentPlayerChanged() {
			base.CurrentPlayerChanged();
			Notify(nameof(Title));
		}

		public override void InitializeGame() {
			initialUsableCounter = 2;
			base.InitializeGame();
			Notify(nameof(Title));
		}

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            Notify(nameof(Title));
            return true;
        }
    }
}
