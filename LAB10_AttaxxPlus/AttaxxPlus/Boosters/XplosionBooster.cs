using AttaxxPlus.Model;
using System;

namespace AttaxxPlus.Boosters
{
    public class XplosionBooster : BoosterBase
    {
        public override string Title { get => $"XPlosion ({usableCounter[this.GameViewModel.CurrentPlayer]})"; }
        private int[] usableCounter = { 0, 1, 1 };
        public XplosionBooster() : base() {
            LoadImage(new Uri(@"ms-appx:///Boosters/XplosionBooster.png"));
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
            if (usableCounter[this.GameViewModel.CurrentPlayer] > 0 && !(selectedField == null))
            {
                usableCounter[this.GameViewModel.CurrentPlayer]--;            
                for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
                {
                    this.GameViewModel.Model.Fields[i, selectedField.Column].Owner = 0;
                }
                for (int i = 0; i < this.GameViewModel.Fields.Count; i++)
                {
                    this.GameViewModel.Model.Fields[selectedField.Row, i].Owner = 0;
                }
                Notify(nameof(Title));
                return true;
            }
            return false;
        }
    }
}
