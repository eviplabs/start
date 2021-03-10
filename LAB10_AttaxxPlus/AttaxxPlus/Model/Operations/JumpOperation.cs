using System;

namespace AttaxxPlus.Model.Operations
{
    public class JumpOperation : OperationBase
    {
        // EVIP: ctor receives dependence on GameBase
        //  and forwards to base class.
        public JumpOperation(GameBase game) : base(game)
        {
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (selectedField == null)
                return false;
            int deltaRow = Math.Abs(selectedField.Row - currentField.Row);
            int deltaCol = Math.Abs(selectedField.Column - currentField.Column);

            if (deltaRow == 1 || deltaCol == 1)
                return false;
            if (deltaRow != 2 && deltaCol != 2)
                return false;

            if (selectedField.Owner == 0)
                return false;
            if (currentField.Owner != 0)
                return false;
            currentField.Owner = selectedField.Owner;
            selectedField.Owner = 0;
            ChangeOwnerOfOccupiedFieldsAroundField(currentField);
            return true;
        }
    }
}
