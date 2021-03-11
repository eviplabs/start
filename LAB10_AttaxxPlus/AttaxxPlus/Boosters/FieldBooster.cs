using AttaxxPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
    class FieldBooster : BoosterBase
    {
        private int[] usableCounter = { 2, 1, 1 }; //First index is not used
        public override string Title => $"FieldBooster ({usableCounter[this.GameViewModel.CurrentPlayer]})";

        public FieldBooster() : base()
        {
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (selectedField == null)
            {
                return false;
            }

            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {               
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                foreach (var currentfield in this.GameViewModel.Model.Fields)
                    if ( (selectedField.Column == currentfield.Column) || (selectedField.Row == currentfield.Row) )
                        currentfield.Owner = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void InitializeGame()
        {
            usableCounter[0] = 0; //Not used
            usableCounter[1] = 1;
            usableCounter[2] = 1;
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

    }
}
