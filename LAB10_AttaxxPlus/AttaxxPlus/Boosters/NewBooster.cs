using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster not doing anything. (But activating it takes a turn.)
    /// Features a player-independent counter to limit the number of activations.
    /// </summary>
    public class NewBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private readonly int[] usableCounter = { 1, 1 };

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"New ({usableCounter[GameViewModel.CurrentPlayer - 1]})"; }

        public NewBooster()
            : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/NewBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[this.GameViewModel.CurrentPlayer - 1] > 0 
                && (selectedField != null))
            {
                usableCounter[this.GameViewModel.CurrentPlayer - 1]--;
                Notify(nameof(Title));

                for (int i = 0; i < GameViewModel.Model.Fields.GetLength(0); i++)
                {
                    GameViewModel.Model.Fields[selectedField.Row, i].Owner = 0;
                    GameViewModel.Model.Fields[i, selectedField.Column].Owner = 0;
                }

                return true;
            }
            return false;
        }
    }
}
