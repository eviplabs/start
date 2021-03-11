using AttaxxPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
    class BombBooster : BoosterBase
    {
        private int[] usableCounter = { 0, 1, 1 };
        public override string Title { get => $"Detonate ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public BombBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/BombBooster.png"));
        }

        public override void InitializeGame()
        {
            usableCounter[1] = 1;
            usableCounter[2] = 1;
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0 && !(selectedField == null))
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
            return false;
        }
    }
}
