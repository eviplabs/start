using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    class ExtraBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter = { 0, 1, 1 };

        public override string Title { get => $"Extra ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public ExtraBooster()
            : base()
        {
            // EVIP: referencing content resource with Uri.
            //  The image is added to the project as "Content" build action.
            //  See also for embedded resources: https://docs.microsoft.com/en-us/windows/uwp/app-resources/
            // https://docs.microsoft.com/en-us/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code
            LoadImage(new Uri(@"ms-appx:///Boosters/extra.png"));
        }
        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }
        public override void InitializeGame()
        {
            usableCounter[0] = 0;
            usableCounter[1] = 1;
            usableCounter[2] = 1;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {

            if (selectedField != null && usableCounter[GameViewModel.CurrentPlayer] == 1 && selectedField.Owner == GameViewModel.CurrentPlayer)
            {
                usableCounter[GameViewModel.CurrentPlayer]--;
                foreach (var field in GameViewModel.Model.Fields)
                {
                    if (((field.Row == selectedField.Row) || (field.Column == selectedField.Column)) && ((field.Row != selectedField.Row) || (field.Column != selectedField.Column)))
                    {
                        field.Owner = 0;
                    }
                }
                Notify(nameof(Title));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
