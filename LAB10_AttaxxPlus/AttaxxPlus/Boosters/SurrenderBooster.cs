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
            LoadImage(new Uri(@"ms-appx:///Boosters/SurrenderBooster.png"));
        }

        // 8. feladat végig megyek az összes mezőn és beszinezem az ellenkező színre
        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if(this.GameViewModel.Model.NumberOfPlayers == 2)
            {
                for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
                {
                    for (int j = 0; j < this.GameViewModel.Fields.Count; j++)
                    {
                        this.GameViewModel.Model.Fields[i, j].Owner = 3 - this.GameViewModel.CurrentPlayer;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
                {
                    for (int j = 0; j < this.GameViewModel.Fields.Count; j++)
                    {
                        if (this.GameViewModel.Model.Fields[i, j].Owner == this.GameViewModel.CurrentPlayer)
                        {
                            this.GameViewModel.Model.Fields[i, j].Owner = 0;
                        }
                    }
                }
            }
            return true;
        }
    }
}
