using AttaxxPlus.Model;
using AttaxxPlus.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
    class ClearBoost : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter = new int[] { 0, 1, 1 };

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"ClearTiles ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public ClearBoost() : base()
        {
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter[0] = 0;
            usableCounter[1] = 1;
            usableCounter[2] = 1;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            /*
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                return true;
            }
            return false;
            */
            if(selectedField != null && usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                int row = selectedField.Row;
                int col = selectedField.Column;

                foreach (FieldViewModelList act_row in this.GameViewModel.Fields)
                {
                    foreach (FieldViewModel field in act_row)
                    {
                        if (field.Model.Row == row || field.Model.Column == col)
                        {
                            field.Model.Owner = 0;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
