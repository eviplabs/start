using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    public class ClearBooster : BoosterBase
    {
        public override string Title { get => $"Boost Clear ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }
        private int[] usableCounter = { 0, 1, 1 };
        public ClearBooster() : base() { }
        // 9. feladat a körök végén értesíteni kell a title string tartalmának megváltozásáról
        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }
        //9. feladat clear booster megvalósítása minden sort és oszlopot üresre váltunk (owner = 0)
        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0 && !(selectedField == null))
            {
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
                {
                    this.GameViewModel.Model.Fields[i, selectedField.Column].Owner = 0;
                }
                for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
                {
                    this.GameViewModel.Model.Fields[selectedField.Row, i].Owner = 0;
                }
                return true;
            }
            return false;
        }
    }
}
