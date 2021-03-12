﻿using System;
using AttaxxPlus.Model;
using AttaxxPlus.ViewModel;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster not doing anything. (But activating it takes a turn.)
    /// Features a player-independent counter to limit the number of activations.
    /// </summary>
    public class DummyBooster : BoosterBase
    {
        // How many times can the user activate this booster
        private int[] usableCounter;

        // EVIP: overriding abstract property in base class.
        public override string Title { get => $"Dummy ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        public DummyBooster(GameViewModel gameViewModel)
            : base(gameViewModel)
        {
            // EVIP: referencing content resource with Uri.
            //  The image is added to the project as "Content" build action.
            //  See also for embedded resources: https://docs.microsoft.com/en-us/windows/uwp/app-resources/
            // https://docs.microsoft.com/en-us/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast#reference-an-image-or-other-asset-from-xaml-markup-and-code
            LoadImage(new Uri(@"ms-appx:///Boosters/DummyBooster.png"));
            usableCounter = new int[this.GameViewModel.Model.NumberOfPlayers + 1];
            InitializeGame();
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            for(int i= 0; i <= this.GameViewModel.Model.NumberOfPlayers; i++)
            {
                usableCounter[i] = 2;
            }
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            // Note: if you need a player-dependent counter, use this.GameViewModel.CurrentPlayer.
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                usableCounter[this.GameViewModel.CurrentPlayer]--;
                Notify(nameof(Title));
                return true;
            }
            return false;
        }
    }
}
