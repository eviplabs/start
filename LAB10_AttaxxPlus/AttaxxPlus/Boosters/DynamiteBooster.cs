using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    class DynamiteBooster : BoosterBase
    {
        private int[] usableCounter = new int[4];
        public override string Title { get => $"Explode ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }
        public DynamiteBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/DynamiteBooster.png"));
        }
        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            //Indexelés miatt kell plusz 1
            for (int i = 1; i < MyConstants.numberOfPlayers + 1; i++)
            {
                usableCounter[i] = MyConstants.numberOfDynamiteBooster;
            }
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0 && (selectedField != null))
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
