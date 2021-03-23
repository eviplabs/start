using System;
using AttaxxPlus.Model;
using AttaxxPlus.ViewModel;

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
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            foreach (Field field in GameViewModel.Model.Fields)
            {
                field.Owner =(GameViewModel.CurrentPlayer)%2+1;
            }
            return true;
        }
    }
}
