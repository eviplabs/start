using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster not doing anything. (But activating it takes a turn.)
    /// Features a player-independent counter to limit the number of activations.
    /// </summary>
    public class ProBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter = { 0, 1, 1 };

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"ProBooster ({usableCounter[GameViewModel.CurrentPlayer]})"; }

        public ProBooster()
            : base()
        {
            // EVIP: referencing content resource with Uri.
            //  The image is added to the project as "Content" build action.
            //  See also for embedded resources: https://docs.microsoft.com/en-us/windows/uwp/app-resources/
            // https://docs.microsoft.com/en-us/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code
            LoadImage(new Uri(@"ms-appx:///Boosters/DummyBooster.png"));
        }
        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }
        public override void InitializeGame()
        {
            usableCounter[1] = 1;
            usableCounter[2] = 1;
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            if (usableCounter[GameViewModel.CurrentPlayer] > 0)
            {
                if(selectedField != null && selectedField.Owner == GameViewModel.CurrentPlayer)
                {
                    for (int i = 0; i <  GameViewModel.Fields.Count; i++)
                    {
                        GameViewModel.Model.Fields[selectedField.Row, i].Owner = 0;
                    }
                    for (int i = 0; i < GameViewModel.Fields.Count; i++)
                    {
                        GameViewModel.Model.Fields[i, selectedField.Column].Owner = 0;
                    }
                    usableCounter[GameViewModel.CurrentPlayer]--;
                    Notify(nameof(Title));
                    return true;
                }
            }
            return false;
        }
    }
}