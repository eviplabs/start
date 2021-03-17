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

        public SurrenderBooster(GameViewModel gvm) : base(gvm)
        {
			LoadImage(new Uri(@"ms-appx:///Boosters/SurrenderBooster.png"));
		}

        public override bool TryExecute(Field selectedField, Field currentField)
        {
			if (GameViewModel.Model.NumberOfPlayers > 2) {
				return true; // Cannot surrender when there are more than 3 players, go to next player
			}
			foreach (var f in GameViewModel.Model.Fields) {
				if (f.Owner == 0) {
					// currPlayer = 2 -> f.Owner = 1
					// currPlayer = 1 -> f.Owner = 2
					f.Owner = 3 - GameViewModel.Model.CurrentPlayer;
				}
			}
			return true;
        }
    }
}
