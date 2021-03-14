using AttaxxPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
    public class EmptyBooster: BoosterBase
    {
        private int[] usableCounter = { 0, 1, 1 };
        public override string Title => $"Clean ({usableCounter[GameViewModel.CurrentPlayer]})";

        public EmptyBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/EmptyBooster.png"));
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
            if (usableCounter[GameViewModel.CurrentPlayer] > 0 && selectedField != null)
            {
                usableCounter[GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                foreach (Field field in GameViewModel.Model.Fields)
                {
                    if (field.Row == selectedField.Row || field.Column == selectedField.Column)
                        field.Owner = 0;
                }

                return true;


            }
            return false;
        }
    }
}
