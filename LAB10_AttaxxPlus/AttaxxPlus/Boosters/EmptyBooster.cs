using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster not doing anything. (But activating it takes a turn.)
    /// Features a player-independent counter to limit the number of activations.
    /// </summary>
    public class EmptyBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter = new int[] { 0, 1, 1 };


        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"Empty ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        public EmptyBooster()
            : base()
        {

        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter = new int[] { 0, 1, 1 };
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[GameViewModel.CurrentPlayer] > 0 && selectedField.Owner == GameViewModel.CurrentPlayer)
            {
                usableCounter[GameViewModel.CurrentPlayer]--;
                // Empty all of the fields with the same cols and rows as the selected fields', except the selected one itself
                foreach (Field field in GameViewModel.Model.Fields)
                {
                    if (field.Row == selectedField.Row || field.Column == selectedField.Column)
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
