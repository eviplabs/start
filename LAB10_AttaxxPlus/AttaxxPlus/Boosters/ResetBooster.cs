using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{

    public class ResetBooster : BoosterBase
    {
        public override string Title { get => $"ResetBooster ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }
        private int[] usableCounter = new int[] { 0, 1, 1 };

        public ResetBooster() : base() { }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                if (selectedField != null)
                {
                    usableCounter[this.GameViewModel.CurrentPlayer]--;
                    Notify(nameof(Title));
                    for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
                    {
                        this.GameViewModel.Model.Fields[i, selectedField.Column].Owner = 0;
                        this.GameViewModel.Model.Fields[selectedField.Row, i].Owner = 0;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}