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
            int winnerPlayer = GameViewModel.CurrentPlayer == 1 ? 2 : 1;
            foreach (Field field in GameViewModel.Model.Fields)
            {
                if (field.IsEmpty())
                {
                    field.Owner = winnerPlayer;
                }
            }
            return true;
        }
    }
}
