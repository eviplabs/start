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

        public SurrenderBooster() : base()
        {
            //8. Feladat
            //kép betöltés
            LoadImage(new Uri(@"ms-appx:///Boosters/SurrenderBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }
        
        //8. Feladat
        //Surrender-nél nincs inicializálás (counterek sincsenek)

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            //8. Feladat
            //Surrender esetén a másik játékos megkapja az összes fennmaradt mezöt
            //Surrender mindig sikeresen fut le
            //Külön értékként kezelem a sorok és oszlopok számát,
            //hiszen nem lehet abból kiindulni, hogy négyzetes a pálya
            int winner = GameViewModel.CurrentPlayer ^ 3;
            for(int x = 0; x < GameViewModel.Fields.Count; x++)
            {
                for(int y = 0; y < GameViewModel.Fields[x].Count; y++)
                {
                    if(GameViewModel.Model.Fields[x, y].IsEmpty())
                    {
                        GameViewModel.Model.Fields[x, y].Owner = winner;
                    }
                }
            }
            return true;
        }
    }
}
