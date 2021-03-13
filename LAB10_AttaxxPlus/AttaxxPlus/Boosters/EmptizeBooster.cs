using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    public class EmptizeBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter = new int[3] { 1, 1, 1 };

        public override string Title { get => $"Emptize ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        public EmptizeBooster(): base()
        {
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            Array.Fill(usableCounter, 1);
            Notify(nameof(Title));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[GameViewModel.CurrentPlayer] > 0 &&
                GameViewModel.SelectedField != null &&
                GameViewModel.SelectedField.Owner == GameViewModel.CurrentPlayer)
            {
                usableCounter[GameViewModel.CurrentPlayer]--;

                foreach (Field field in GameViewModel.Model.Fields)
                {
                    if (field.Column == GameViewModel.SelectedField.Model.Column ||
                        field.Row == GameViewModel.SelectedField.Model.Row)
                    {
                        field.Owner = 0;
                    }
                }

                Notify(nameof(Title));
                return true;
            }
            return false;
        }
    }
}
