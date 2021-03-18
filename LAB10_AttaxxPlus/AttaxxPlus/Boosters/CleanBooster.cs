using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    public class CleanBooster : BoosterBase
    {
        private int[] usableCounter = { 0, 1, 1 };

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"Clean ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        public CleanBooster()
            : base()
        {
            // EVIP: referencing content resource with Uri.
            //  The image is added to the project as "Content" build action.
            //  See also for embedded resources: https://docs.microsoft.com/en-us/windows/uwp/app-resources/
            // https://docs.microsoft.com/en-us/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code
            LoadImage(new Uri(@"ms-appx:///Boosters/CleanBooster.png"));
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            usableCounter = new int[] { 0, 1, 1 };
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (usableCounter[GameViewModel.CurrentPlayer] > 0
                 && (selectedField.Row != 0)
                 && (selectedField.Column != 0))
            {
                usableCounter[GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));

                for (int RowColumn = 0; RowColumn < GameViewModel.Model.Fields.GetLength(0); RowColumn++)
                {
                    GameViewModel.Model.Fields[selectedField.Row, RowColumn].Owner = 0;
                    GameViewModel.Model.Fields[RowColumn, selectedField.Column].Owner = 0;
                }

                return true;
            }
            return false;
            
        }
    }
}
