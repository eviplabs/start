using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    class ClearBooster : BoosterBase
    {
        private int[] usableCounter = { 1, 1, 1, 1 };
       
        public override string Title { get => $"Clear ({usableCounter[GameViewModel.CurrentPlayer] })"; }
        public ClearBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/ClearBooster.png"));
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
            if (usableCounter[GameViewModel.CurrentPlayer] > 0 && selectedField != null && GameViewModel.CurrentPlayer == selectedField.Owner)
            {
                int row = selectedField.Row;
                int col = selectedField.Column;

                for (int i = 0; i < GameViewModel.Model.Fields.GetLength(0); i++)
                {
                    //clear row
                    GameViewModel.Model.Fields[row, i].Owner = 0;
                    //clear col
                    GameViewModel.Model.Fields[i, col].Owner = 0;
                }

                usableCounter[GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                return true;
            }
            return false;
        }
     }
}
