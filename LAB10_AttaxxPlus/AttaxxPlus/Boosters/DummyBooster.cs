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
        //7. Feladat
        //Azért 2 elem, hogy ne használjunk fölöslegesen memóriát
        //inkább az indexet csökkentjük 1-el (CurrentPlayer)
        private int[] usableCounters = new int[] {2,2};

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"Dummy ({usableCounters[this.GameViewModel.CurrentPlayer - 1]})"; }

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
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            //7. Feladat
            int[] usableCounters = new int[] {2,2};
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            //7. Feladat
            if (usableCounters[this.GameViewModel.CurrentPlayer - 1] > 0)
            {
                usableCounters[this.GameViewModel.CurrentPlayer - 1]--;
                Notify(nameof(Title));
                return true;
            }
            //6. Feladat
            //a TryExecute ugyanazt adja vissza ha van még lehetöség mint ha nincs
            return false;
        }
    }
}
