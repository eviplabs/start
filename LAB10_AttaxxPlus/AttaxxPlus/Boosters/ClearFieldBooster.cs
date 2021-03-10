using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{

    // 9. feladat: új booster hozzáadása a projekthez
    public class ClearFieldBooster : BoosterBase
    {

        private int[] usableCounter = new int[] { 0, 1, 1 };
        public override string Title { get => $"Clear ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }

        //9. feladat: surrender booster képe
        public ClearFieldBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/ClearFieldBooster.jpg"));
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
            //9. feladat: amennyiben a játékosnak még van lehetősége, a clear booster a sor és oszlop összes kockáját felszabadítja, kivéve amelyiken a játékos áll.
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0)
            {
                if (selectedField != null)
                {
                    usableCounter[this.GameViewModel.CurrentPlayer]--;
                    for (int i = 0; i < GameViewModel.Fields.Count; i++)
                    {
                        GameViewModel.Model.Fields[i, selectedField.Column].Owner = 0;
                        GameViewModel.Model.Fields[selectedField.Row, i].Owner = 0;
                        //a takarítás után az aktuális mező tulajdonosát visszaállítom
                        //Talán nem ez a legszebb megoldás, de a feltételes indexelésekbe egy kicsit belegabalyodtam
                        GameViewModel.Model.Fields[selectedField.Row, selectedField.Column].Owner = GameViewModel.CurrentPlayer;
                    }
                }
            }
            return false;
        }
    }
}