using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster filling all empty fields with the opponents' color (assuming 2 players).
    /// </summary>
    public class CrossClearBooster : BoosterBase
    {
        // EVIP: compact override of getter for Title returning constant.

        private int[] usableCounter = new int[] { 0, 1, 1 };
        public override string Title { get => $"Cross Clear ({usableCounter[base.GameViewModel.CurrentPlayer]})"; }

    public CrossClearBooster() : base()
        {
            //LoadImage(new Uri(@"ms-appx:///Boosters/SurrenderBooster.png"));
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
            if (selectedField != null
                && usableCounter[base.GameViewModel.CurrentPlayer] > 0
                && selectedField.Owner == base.GameViewModel.CurrentPlayer)
            {
                var rows = base.GameViewModel.Model.Fields.GetLength(0);
                var columns = base.GameViewModel.Model.Fields.GetLength(0);
                //cross clear
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (selectedField.Row == i || selectedField.Column == j)
                        {
                            base.GameViewModel.Model.Fields[i, j].Owner = 0;
                        }
                    }
                }
                usableCounter[base.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                return true;
            }
            return false;
        }


    }
}
