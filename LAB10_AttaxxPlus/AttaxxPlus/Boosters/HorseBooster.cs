using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster filling all empty fields with the opponents' color (assuming 2 players).
    /// </summary>
    public class HorseBooster : BoosterBase
    {
        private int[] usableCounter = { 0, 1, 1 };
        // EVIP: compact override of getter for Title returning constant.
        public override string Title { get => $"Horse ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public HorseBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/horse.png"));
        }

        public override void InitializeGame()
        {
            usableCounter[0] = 0;
            usableCounter[1] = 1;
            usableCounter[2] = 1;
        }
        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[GameViewModel.CurrentPlayer] > 0)
            {
                int idx_row = GameViewModel.SelectedField.Model.Row;
                int idx_col = GameViewModel.SelectedField.Model.Column;
                if (GameViewModel.SelectedField.Model.Owner != GameViewModel.CurrentPlayer)
                {
                    return false;
                }
                for (int idx = 0; idx < GameViewModel.Model.Fields.GetLength(0); idx++)
                {
                    GameViewModel.Model.Fields[idx_row, idx].Owner = 0;
                    GameViewModel.Model.Fields[idx, idx_col].Owner = 0;
                }
                usableCounter[GameViewModel.CurrentPlayer] -= 1; ;
                return true;
            }
            return false;
        }
    }
}
