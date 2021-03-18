using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster not doing anything. (But activating it takes a turn.)
    /// Features a player-independent counter to limit the number of activations.
    /// </summary>
    public class DummyBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int usableCounter = 2;
        private int[] usableCounters = { 0, 0, 0 };

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"Dummy ({usableCounter})"; }

        public DummyBooster()
            : base()
        {
            // EVIP: referencing content resource with Uri.
            //  The image is added to the project as "Content" build action.
            //  See also for embedded resources: https://docs.microsoft.com/en-us/windows/uwp/app-resources/
            // https://docs.microsoft.com/en-us/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code
            LoadImage(new Uri(@"ms-appx:///Boosters/DummyBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            usableCounter = usableCounters[GameViewModel.CurrentPlayer];
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter = 2;
            usableCounters = new int[] { 0,2,2};
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            int alma = usableCounters[GameViewModel.CurrentPlayer];
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            if (usableCounters[GameViewModel.CurrentPlayer] > 0)
            {
                usableCounters[GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                // ugyis rogton hivodik a InitializeGame(), nem kell ez ide
                //usableCounter = usableCounters[GameViewModel.CurrentPlayer];
                return true;
            }
            return false;
        }
    }
}
