using AttaxxPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
    class RowBooster : BoosterBase
    {

        private int[] playerCounter = { 0, 1, 1 };
        public override string Title { get => $"Set Row ({playerCounter[GameViewModel.CurrentPlayer]})"; }

        public RowBooster() : base()
        { }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            playerCounter[1] = 1;
            playerCounter[2] = 1;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (selectedField == null)
            {
                return false;
            }
            else
            {
                if (playerCounter[GameViewModel.CurrentPlayer] > 0)
                {
                    playerCounter[GameViewModel.CurrentPlayer]--;
                    Notify(nameof(Title));
                    for (int row = 0; row < GameViewModel.Model.Fields.GetLength(1); row++)
                        GameViewModel.Model.Fields[row, selectedField.Column].Owner = 0;
                    return true;
                }
            }
            return false;
        }
    }
}
