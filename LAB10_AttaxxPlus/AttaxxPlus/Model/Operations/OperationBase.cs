namespace AttaxxPlus.Model.Operations
{
    // EVIP: abstract base class implementing an interface
    //  and providing helper methods and game model reference.
    public abstract class OperationBase : IOperation
    {
        // EVIP: base class stores dependency in protected field.
        protected GameBase game;
        public OperationBase(GameBase game)
        {
            this.game = game;
        }

        // EVIP: abstract method
        public abstract bool TryExecute(Field selectedField, Field currentField);

        // EVIP: could also be ChangeColorAroundField, but we should talk
        //  in the domain of the data model. Maybe in some scenarios, the
        //  ownership will not be indicated by colors...
        //  Besides, a field does not have a color here, only Owner ID.
        protected void ChangeOwnerOfOccupiedFieldsAroundField(Field field)
        {
            for(int row = field.Row-1; row <= field.Row+1; row++)
                if (row >= 0 && row < game.Fields.GetLength(0))
                    for (int column = field.Column - 1; column <= field.Column + 1; column++)
                        if (column >= 0 && column < game.Fields.GetLength(1))
                            if (!game.Fields[row, column].IsEmpty())
                                game.Fields[row, column].Owner = field.Owner;
        }
    }
}
