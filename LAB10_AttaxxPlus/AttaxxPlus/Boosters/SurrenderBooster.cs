﻿using System;
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
            //8. Feladat
            for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
            {
                for (int j = 0; j < this.GameViewModel.Fields.Count; j++)
                {
                    if (this.GameViewModel.Model.Fields[i, j].Owner == 0)
                    {
                        this.GameViewModel.Model.Fields[i, j].Owner = 3 - this.GameViewModel.CurrentPlayer;

                    }
                }
            }
            return true;
        }
    }
}
