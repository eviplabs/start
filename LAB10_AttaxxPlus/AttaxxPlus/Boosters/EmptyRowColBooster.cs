using AttaxxPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster deleting all elements in the selected row and column.
    /// </summary>
    public class EmptyRowColBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private readonly int[] usableCounter = { 0, 1, 1 };

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"CrossClean ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        public EmptyRowColBooster() : base()
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
            Notify(nameof(this.Title));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[GameViewModel.CurrentPlayer] > 0)
            {
                usableCounter[GameViewModel.CurrentPlayer]--;
                ClearRowCol();
                Notify(nameof(Title));
                return true;
            }
            return false;
        }

        private void ClearRowCol()
        {
            foreach (var item in GameViewModel.Model.Fields)
            {
                if ((item.Row == GameViewModel.SelectedField.Model.Row || item.Column == GameViewModel.SelectedField.Model.Column) && item.Owner != 0)
                {
                    item.Owner = 0;                                  
                }
            }
        }
    }
}
