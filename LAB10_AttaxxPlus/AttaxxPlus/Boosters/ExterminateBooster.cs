using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster not doing anything. (But activating it takes a turn.)
    /// Features a player-independent counter to limit the number of activations.
    /// </summary>
    public class ExterminateBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter;
        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"Exterminate ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public ExterminateBooster()
            : base()
        {
            // EVIP: referencing content resource with Uri.
            //  The image is added to the project as "Content" build action.
            //  See also for embedded resources: https://docs.microsoft.com/en-us/windows/uwp/app-resources/
            // https://docs.microsoft.com/en-us/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code
            LoadImage(new Uri(@"ms-appx:///Boosters/ExterminateBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter = new int[this.GameViewModel.Model.NumberOfPlayers + 1];
            Array.Fill(usableCounter, 2);
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (selectedField != null 
                && selectedField.Owner == this.GameViewModel.CurrentPlayer 
                && usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));

                for (int row = 0; row < this.GameViewModel.Model.Fields.GetLength(0); row++)
                {
                    this.GameViewModel.Model.Fields[row, selectedField.Column].Owner = 0;
                }

                for (int col = 0; col < this.GameViewModel.Model.Fields.GetLength(1); col++)
                {
                    this.GameViewModel.Model.Fields[selectedField.Row, col].Owner = 0;
                }

                return true;
            }

            return false;
        }
    }
}
