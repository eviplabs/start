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
            int ReservedPlayer = 0;

            if(GameViewModel.CurrentPlayer==1)
            { 
                ReservedPlayer = 2;
            }
            else
            {
                ReservedPlayer = 1;
            }

            foreach (var field in GameViewModel.Model.Fields)
            {
                if (field.Owner == 0)
                {
                    field.Owner = ReservedPlayer;
                }
            }

            return true;
        }
    }
}
