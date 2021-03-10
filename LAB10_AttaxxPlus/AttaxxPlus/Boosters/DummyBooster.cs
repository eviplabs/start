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
        // 7. feladat a játékosokhoz tartozó boosterek számát egy tömb tárolja
        private int[] usableCounter = new int[] {0, 2, 2};

        // EVIP: overriding abstract property in base class.
        //7. feladat: kiírandó sztring felülírása
        public override string Title { get => $"Dummy ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

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
            usableCounter = new int[] { 0, 2, 2};
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            //7. feladat: játékosspecifikus dummy booster számláló
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                return true;
            }
            // 6. feladat: ez itt true-val tért vissza, akkor is ha elfogyott a booster. így már működik
            return false;
        }
    }
}
