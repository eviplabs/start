using AttaxxPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
    public class RowColBooster : BoosterBase
    {
        private int[] usableCounter = new int[3];

        public override string Title => $"DestroyRowCol ({usableCounter[this.GameViewModel.CurrentPlayer]})";

        public RowColBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/bomb.png"));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if ((selectedField is null)
                || (selectedField.Owner != this.GameViewModel.CurrentPlayer)
                || (usableCounter[this.GameViewModel.CurrentPlayer] == 0))
                return false;

            usableCounter[this.GameViewModel.CurrentPlayer]--;
            Notify(nameof(Title));

            int row = selectedField.Row;
            int col = selectedField.Column;
            foreach (var f in this.GameViewModel.Model.Fields)
                if ((f.Row == row) || (f.Column == col))
                    f.Owner = 0;

            return true;
        }

        public override void InitializeGame()
        {
            usableCounter[0] = 0;
            usableCounter[1] = 1;
            usableCounter[2] = 1;
            Notify(nameof(this.Title));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }
    }
}
