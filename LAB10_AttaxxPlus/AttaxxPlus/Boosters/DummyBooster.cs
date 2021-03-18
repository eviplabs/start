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
        private int usableCounterPlayer1 = 2;
        private int usableCounterPlayer2 = 2;

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
            base.CurrentPlayerChanged();

            if (GameViewModel.CurrentPlayer == 1)
            {
                usableCounter = usableCounterPlayer1;
            }
            else
            {
                usableCounter = usableCounterPlayer2;
            }

            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter = 2;
            usableCounterPlayer1 = 2;
            usableCounterPlayer2 = 2;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            if (usableCounter > 0)
            {
                usableCounter--;

                if (GameViewModel.CurrentPlayer == 1)
                {
                    usableCounterPlayer1 = usableCounter;
                }
                else
                {
                    usableCounterPlayer2 = usableCounter;
                }

                Notify(nameof(Title));
                return true;
            }
            return false;
        }
    }
}
