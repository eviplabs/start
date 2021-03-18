using System;
using AttaxxPlus.Model;
using AttaxxPlus.ViewModel;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster filling all empty fields with the opponents' color (assuming 2 players).
    /// </summary>
    public class NewBooster : BoosterBase
    {
        private int usableCounter = 1;

        // EVIP: compact override of getter for Title returning constant.
        public override string Title { get => $"Delete ({usableCounter})"; }

        public NewBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/Clear.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter = 1;
    }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if(usableCounter == 1)
            {
                usableCounter = 0;
                foreach (var e in GameViewModel.Model.Fields)
                {
                    if (e.Row == selectedField.Row)
                    {
                        e.Owner = 0;
                    }
                    if (e.Column == selectedField.Column)
                    {
                        e.Owner = 0;
                    }
                }
            }
            Notify(nameof(Title));
            return false;
        }
    }
}