using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster filling all empty fields with the opponents' color (assuming 2 players).
    /// </summary>
    public class SurrenderBooster : BoosterBase
    {
        // EVIP: compact override of getter for Title returning constant.
        public override string Title => "Surrender";

        //8. feladat: surrender booster képe
        public SurrenderBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/SurrenderBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            //8. feladat: surrender esetén a másik játékos megkapja az összes mezőt
            int NewOwner = (GameViewModel.CurrentPlayer == 1) ? 2 : 1;

            for (int i = 0; i < GameViewModel.Fields.Count; i++)
            {
                for (int j = 0; j< GameViewModel.Fields.Count; j++)
                {
                    GameViewModel.Model.Fields[i, j].Owner = NewOwner;
                }
            }
            return true;
        }
    }
}
