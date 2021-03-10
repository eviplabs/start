using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{

    public class TNTBooster : BoosterBase
    {

        private int[] usableCounter = new int[] { 0, 1, 1 };


        public override string Title { get => $"TNTBooster ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public TNTBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/186135-bomb.png"));
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
        //#9. Új Booster
        // A választott mező sorában az összes mező tulajdonosát nullába állítom, majd ugyan ezt az oszlopokra is
        //végül a kiválasztott mezőt tulajdonosát visszaállítom az eredetire.
        public override bool TryExecute(Field selectedField, Field currentField)
        {

            if(selectedField != null &&  usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                if ( selectedField.Owner == GameViewModel.CurrentPlayer)
                {
                    for (int i = 0; i < GameViewModel.Fields.Count; i++)
                    {
                        GameViewModel.Model.Fields[selectedField.Row, i].Owner = 0;
                    }
                    for (int i = 0; i < GameViewModel.Fields.Count; i++)
                    {
                        GameViewModel.Model.Fields[i, selectedField.Column].Owner = 0;
                    }
                    GameViewModel.Model.Fields[selectedField.Row, selectedField.Column].Owner = GameViewModel.CurrentPlayer;
                }
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                return true;
            }
            return false;
        }
    }
}
