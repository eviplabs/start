using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    //9. Feladat
    //Bonus Booster
    //SelectedField sor és oszlop összes field-jét üresre állítja
    //ha saját színü a SelectedField
    //csak egyszer használható
    public class BonusBooster : BoosterBase
    {
        public override string Title => "Surrender";

        private int[] usableCounters = new int[] {1,1};

        public BonusBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/BonusBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            int[] usableCounters = new int[] {1,1};
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounters[this.GameViewModel.CurrentPlayer - 1] > 0)
            {
                if((selectedField != null) && (selectedField.Owner == this.GameViewModel.CurrentPlayer))
                {
                    //oszlop elemeinek kiürésítése
                    for(int x = 0; x < this.GameViewModel.Model.Fields.GetLength(0); x++)
                    {
                        this.GameViewModel.Model.Fields[selectedField.Row, x].Owner = 0;
                    }
                    //sor elemeinek kiürésítése
                    for(int x = 0; x < this.GameViewModel.Model.Fields.GetLength(1); x++)
                    {
                        this.GameViewModel.Model.Fields[x, selectedField.Column].Owner = 0;
                    }
                    usableCounters[this.GameViewModel.CurrentPlayer - 1]--;
                }
            }
            return false;
        }
    }
}
