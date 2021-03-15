using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    class ClearBooster : BoosterBase
    {

        public override string Title => "Clear";

        public ClearBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/ClearBooster.png"));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (selectedField != null && GameViewModel.CurrentPlayer == selectedField.Owner)
            {
                int row = selectedField.Row;
                int col = selectedField.Column;

                for(int i=0; i< GameViewModel.Model.Fields.GetLength(0); i++)
                {
                    //clear row
                    GameViewModel.Model.Fields[row, i].Owner = 0;
                    //clear col
                    GameViewModel.Model.Fields[i, col].Owner = 0;
                }
                
                return true;
            }
            return false;
        }
     }
}
