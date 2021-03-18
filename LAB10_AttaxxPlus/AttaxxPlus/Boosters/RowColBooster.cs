using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster filling all empty fields with the opponents' color (assuming 2 players).
    /// </summary>
    public class RowColBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter = { 0, 1, 1 };

        // EVIP: compact override of getter for Title returning constant.
        public override string Title { get => $"Row,Col RST ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        public RowColBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/RowColBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter[0] = 1;
            usableCounter[1] = 1;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[GameViewModel.CurrentPlayer] > 0)
            {
                usableCounter[GameViewModel.CurrentPlayer]--;
                foreach (var i in GameViewModel.Model.Fields)
                {
                    if (i.Column == selectedField.Column || i.Row == selectedField.Row)
                    {
                        i.Owner = 0;
                    }
                }
                return true;
            }
            return false;
        }
    }
}