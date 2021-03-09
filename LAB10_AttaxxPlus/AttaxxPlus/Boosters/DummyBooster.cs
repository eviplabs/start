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
        // 7. feladat itt létrehozom tömbként és, ahogy le volt írva az tömb 2. és 3. elemét látja player-ként, bár ezt a tömb -1 indexezéssel is el lehetne érni, csak ott nem a memória,
        // hanem a sebbesség csökkenne, igazából a memória, azért is jobb, mert az csak 1 plusz bájt ami nagyon kevés, a processzor-t sem terhelné túl, viszont az sok fölös számítás.
        // csak ezt a tömböt írtam át mindenhol.
        private int[] usableCounter = {0, 2, 2, 2 };

        // EVIP: overriding abstract property in base class.
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
            usableCounter[1] = 2;
            usableCounter[2] = 2;
            usableCounter[3] = 2;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                return true;
            }
            // 6. feladat mindig igazzal tért vissza, még akkor is ha elfogyott a lépés, ezt false-ra állítva jól működik
            return false;
        }
    }
}
