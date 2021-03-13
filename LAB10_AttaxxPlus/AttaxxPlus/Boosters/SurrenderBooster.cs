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

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (this.GameViewModel.Model.NumberOfPlayers == 2)
            {
                for (int k = 0; k < this.GameViewModel.Fields.Count; k++)
                {
                    for (int l = 0; l < this.GameViewModel.Fields.Count; l++)
                    {
                        this.GameViewModel.Model.Fields[k, l].Owner = 3 - this.GameViewModel.CurrentPlayer; 
                    }
                }
            }
            else
            {
                for (int k = 0; k < this.GameViewModel.Fields.Count; k++)
                {
                    for (int l = 0; l < this.GameViewModel.Fields.Count; l++)
                    {
                        if (this.GameViewModel.Model.Fields[k, l].Owner == this.GameViewModel.CurrentPlayer)
                        {
                            this.GameViewModel.Model.Fields[k, l].Owner = 0;
                        }
                    }
                }
            }

            return true;
        }
    }
}
