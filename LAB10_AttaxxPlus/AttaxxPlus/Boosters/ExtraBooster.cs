using System;
using AttaxxPlus.Model;
using AttaxxPlus.ViewModel;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster filling all empty fields with the opponents' color (assuming 2 players).
    /// </summary>
    public class ExtraBooster : BoosterBase
    {
        readonly int counterMaxValue = 1;
        private int[] usableCounter = new int[3];
        // EVIP: compact override of getter for Title returning constant.
        public override string Title { get => $"Extra ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        public ExtraBooster() : base()
        {
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            for (int i = 1; i < 3; i++)
            {
                usableCounter[i] = counterMaxValue;
            }
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if(selectedField == null)
            {
                return false;
            }
            if (selectedField.Owner == GameViewModel.CurrentPlayer && usableCounter[GameViewModel.CurrentPlayer] != 0)
            {
                int rowNumber = selectedField.Row;
                int columnNumber = selectedField.Column;

                foreach (FieldViewModelList rows in GameViewModel.Fields)
                {
                    foreach (FieldViewModel column in rows)
                    {
                        if (column.Model.Row == rowNumber || column.Model.Column == columnNumber)
                            column.Model.Owner = 0;
                    }
                }
                usableCounter[GameViewModel.CurrentPlayer] -= 1;
                Notify(nameof(Title));
            }
            return false;
        }
    }
}
