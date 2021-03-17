using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster filling all empty fields with the opponents' color (assuming 2 players).
    /// </summary>
    public class RowColBooster : BoosterBase
    {
        private int[] usableCounter = { 0, 1, 1 };
        // EVIP: compact override of getter for Title returning constant.
        public override string Title { get => $"Nuclear ({usableCounter[base.GameViewModel.CurrentPlayer]})"; }

        public RowColBooster() : base()
        {
            
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (selectedField == null || usableCounter[base.GameViewModel.CurrentPlayer] < 1)
                return false;
            if(selectedField.Owner == base.GameViewModel.CurrentPlayer)
            {
                for(int i=0;i<base.GameViewModel.Fields.Count; i++)
                {
                    base.GameViewModel.Model.Fields[i, selectedField.Column].Owner = 0;
                    base.GameViewModel.Model.Fields[selectedField.Row, i].Owner = 0;
                }
            }
            usableCounter[base.GameViewModel.CurrentPlayer]--;
            return true;
        }
    }
}
