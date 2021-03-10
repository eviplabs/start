using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster 
    /// </summary>
    public class UltimateBooster : BoosterBase
    {
        // How many times can users activate this booster
        private int[] usableCounter = { -1, 1, 1 };

        // EVIP: compact override of getter for Title returning constant.
        public override string Title { get => $"Ultimate ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public UltimateBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/UltimateBooster.png"));
        }
        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }
        public override void InitializeGame()
        {
            usableCounter[0] = -1;
            usableCounter[1] = 1;
            usableCounter[2] = 1;
        }
        public override bool TryExecute(Field selectedField, Field currentField)
        {
            selectedField = this.GameViewModel.SelectedField.Model;
            var currPlayer = this.GameViewModel.Model.CurrentPlayer;
            if (usableCounter[currPlayer] > 0)
            {
                if ((!selectedField.IsEmpty()) && (selectedField.Owner == currPlayer))
                {
                    foreach (var currField in this.GameViewModel.Model.Fields)
                    {
                        if ((currField.Column == selectedField.Column) || (currField.Row == selectedField.Row))
                            currField.Owner = currPlayer;
                    }
                    usableCounter[currPlayer]--;
                    Notify(nameof(Title));
                    return true;
                }
            }
            
            return false;
        }
    }
}
