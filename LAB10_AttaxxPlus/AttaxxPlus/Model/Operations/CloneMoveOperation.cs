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
            //Ha az oszlopok és a sorok különbséges is = 1, akkor átlósan léptünk 1-et
            if (Math.Abs(selectedField.Row - currentField.Row)
                + Math.Abs(selectedField.Column - currentField.Column) == 1
                || (Math.Abs(selectedField.Row - currentField.Row) == 1 && Math.Abs(selectedField.Column - currentField.Column) == 1)
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
