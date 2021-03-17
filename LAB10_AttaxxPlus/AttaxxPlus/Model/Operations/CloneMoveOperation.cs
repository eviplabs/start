using System;

namespace AttaxxPlus.Model.Operations
{
    public class CloneMoveOperation : OperationBase
    {
        public CloneMoveOperation(GameBase game) : base(game)
        {
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (selectedField == null)
                return false;

            // Note: selectedField is always the players own field...
            // EVIP: IsEmpty() is more descriptive than "Owner == 0"
			//4. Feladat
			//if-ben a hiba, ha sor-t és oszlopot is váltunk akkor nem teljesül a feltétel
            int deltarow = Math.Abs(selectedField.Row - currentField.Row);
			int deltacolumn = Math.Abs(selectedField.Column - currentField.Column);
			if ((deltarow + deltacolumn > 0)
				&& (deltarow < 2)
			    && (deltacolumn < 2)
                && !selectedField.IsEmpty()
                && currentField.IsEmpty())
            {
                currentField.Owner = selectedField.Owner;
                // EVIP: using more general helper method implemented by base class
                ChangeOwnerOfOccupiedFieldsAroundField(currentField);
                return true;
            }
            return false;
        }
    }
}
