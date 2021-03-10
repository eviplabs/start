namespace AttaxxPlus.Model
{
    /// <summary>
    /// Represents one field of the game.
    /// </summary>
    public class Field : ObservableObject
    {
        // EVIP: property without explicit private member
        public int Row { get; set; }
        public int Column { get; set; }

        private int owner = 0;
        /// <summary>
        /// 0: empty
        /// In the basic game, only player 1 and 2 exist, but we may want to
        ///     extend this to further players later.
        /// </summary>
        /// EVIP: As the number of players is unknown here,
        ///     an enum would not be a good choice.
        public int Owner
        {
            get => owner;
            set
            {
                if (owner != value)
                {
                    owner = value;
                    Notify();
                }
            }
        }

        // EVIP: Helper method indicating emptyness.
        public bool IsEmpty() => (owner == 0);
    }
}
