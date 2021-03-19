using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster clearing all fileds in a row and coloumn determined by a player's own field (assuming 2 players).
    /// </summary>
    public class UltiBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter = { 0, 1, 1 };

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"Ultimate Power ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public UltiBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/UltiBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter[1] = 1;
            usableCounter[2] = 1;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            //9. Feladat
            if (GameViewModel.SelectedField == null)
            {
                return false;
            }
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                if (GameViewModel.SelectedField.Owner == GameViewModel.CurrentPlayer)
                {
                    for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
                    {
                        this.GameViewModel.Model.Fields[this.GameViewModel.SelectedField.Model.Row, i].Owner = 0;
                        this.GameViewModel.Model.Fields[i, this.GameViewModel.SelectedField.Model.Column].Owner = 0;
                    }
                }
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                return true;
            }

            return false;
        }
    }
}
