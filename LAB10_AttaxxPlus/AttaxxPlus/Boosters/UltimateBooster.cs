using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster 
    /// </summary>
    public class UltimateBooster : BoosterBase
    {
        // EVIP: compact override of getter for Title returning constant.
        public override string Title => "Ultimate";

        public UltimateBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/UltimateBooster.png"));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            var currPlayer = this.GameViewModel.Model.CurrentPlayer;
            if (selectedField.Owner == currPlayer)
            {

                return true;
            }
            return false;
        }
    }
}
