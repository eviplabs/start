using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster deleting all fields in the row/column of the selected field of the CurrentPlayer.
    /// </summary>
    public class OptionalBooster : BoosterBase
    {
        // EVIP: compact override of getter for Title returning constant.
        public override string Title { get => $"Optional ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }
        // How many times can the user activate this booster
        private int[] usableCounter = new int[] { 0, 1, 1 };
        public OptionalBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/OptionalBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter = new int[] { 0, 1, 1 };
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0)
                if (selectedField != null && selectedField.Owner == GameViewModel.CurrentPlayer)
                {
                    usableCounter[this.GameViewModel.CurrentPlayer]--;
                    Notify(nameof(Title));
                    int column = selectedField.Column;
                    int row = selectedField.Row;
                    foreach (var f in this.GameViewModel.Model.Fields)
                        if ((f.Column == column) || (f.Row == row))
                        {
                            f.Owner = 0;
                        }
                    return true;
                }
            return false;
        }
    }
}