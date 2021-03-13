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
            foreach (var i in GameViewModel.Model.Fields)
            {
                if(i.IsEmpty())
                {
                    if(GameViewModel.Model.CurrentPlayer == 1)
                    {
                        i.Owner = 2;
                    }
                        
                    else
                    {
                        i.Owner = 1;
                    }
                }
            }
            return true;
        }
    }
}
