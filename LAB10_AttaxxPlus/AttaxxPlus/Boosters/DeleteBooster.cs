using System;
using AttaxxPlus.Model;


namespace AttaxxPlus.Boosters
{
    public class DeleteBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter = { 0, 1, 1 };
        
        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"Delete ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public DeleteBooster()
            : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/DeleteBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter[0] = 0;
            usableCounter[1] = 1;
            usableCounter[2] = 1;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0 && !(selectedField == null))
            {
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                for(int k = 0; k < this.GameViewModel.Fields.Count; k++)
                {
                    this.GameViewModel.Model.Fields[k, selectedField.Column].Owner = 0;
                    this.GameViewModel.Model.Fields[selectedField.Row, k].Owner = 0;
                }

                Notify(nameof(Title));
                return true;
            }
            return false;
        }
    }
}