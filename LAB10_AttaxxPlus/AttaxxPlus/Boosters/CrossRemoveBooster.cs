using AttaxxPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
    class CrossRemoveBooster : BoosterBase

    {

        private int[] usableCounter = new int[3];
        public override string Title { get => $"Cross ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        public CrossRemoveBooster() :base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/CrossRemove.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            for (int i = 0; i < usableCounter.Length; i++)
            {
                usableCounter[i] = 1;
            }
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
           if (selectedField==null)
            {
                return false;
            }
            if (usableCounter[GameViewModel.CurrentPlayer] > 0)
            {
                usableCounter[GameViewModel.CurrentPlayer]--;

                foreach (var field in GameViewModel.Model.Fields)
                {
                    if (field.Row == selectedField.Row)
                        field.Owner = 0;

                    if (field.Column == selectedField.Column)
                        field.Owner = 0;
                }

                Notify(nameof(Title));
                return true;
            }
            return false;
        }
    }
}
