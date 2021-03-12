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
        // 8. feladat SurrenderBooster készítése
        public SurrenderBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/SurrenderBooster.png"));
        }
        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
            {
                for (int j = 0; j < this.GameViewModel.Fields.Count; j++)
                {
                    if (this.GameViewModel.Model.Fields[i, j].Owner == 0)
                    {
                        if (this.GameViewModel.CurrentPlayer == 1)
                        {
                            this.GameViewModel.Model.Fields[i, j].Owner = 2;
                        }
                        else
                        {
                            this.GameViewModel.Model.Fields[i, j].Owner = 1;
                        }
                    }
                }
            }
            return true;
        }
    }
}

