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

        public SurrenderBooster() : base()
        {
            // EVIP: referencing content resource with Uri.
            //  The image is added to the project as "Content" build action.
            //  See also for embedded resources: https://docs.microsoft.com/en-us/windows/uwp/app-resources/
            // https://docs.microsoft.com/en-us/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code
            LoadImage(new Uri(@"ms-appx:///Boosters/SurrenderBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }


        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (this.GameViewModel.CurrentPlayer < this.GameViewModel.Model.NumberOfPlayers)
                this.GameViewModel.Model.CurrentPlayer++;
            else
                this.GameViewModel.Model.CurrentPlayer = 1;

            foreach (Field field in this.GameViewModel.Model.Fields)
            {
                if (field.IsEmpty())
                    field.Owner = this.GameViewModel.CurrentPlayer;
            }
            return true;
        }
    }
}
