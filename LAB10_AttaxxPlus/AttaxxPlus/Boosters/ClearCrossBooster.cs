using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster not doing anything. (But activating it takes a turn.)
    /// Features a player-independent counter to limit the number of activations.
    /// </summary>
    public class ClearCrossBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter = { 0, 1, 1 };

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"ClearCross ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public ClearCrossBooster()
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
            usableCounter[0] = 0;
            usableCounter[1] = 1; // [1..] is available from C# 8.0
            usableCounter[2] = 1;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (!(selectedField is null))
            {
                if (usableCounter[this.GameViewModel.CurrentPlayer] > 0 && selectedField.Owner == GameViewModel.CurrentPlayer)
                {
                    foreach (Field field in GameViewModel.Model.Fields)
                    {
                        if (field.Column == selectedField.Column || field.Row == selectedField.Row)
                            field.Owner = 0;
                    }
                    usableCounter[this.GameViewModel.CurrentPlayer]--;
                    Notify(nameof(this.Title));
                    return true;
                }
            }
            return false;
        }
    }
}
