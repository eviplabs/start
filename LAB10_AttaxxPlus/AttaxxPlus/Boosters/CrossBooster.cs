using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    class CrossBooster : BoosterBase
    {
        private int[] shots = new int[3];

        private readonly int shotsInitValue = 1;

        public override string Title { get => $"CrossBoost ({shots[GameViewModel.CurrentPlayer]})"; }

        public CrossBooster() : base()
        {
        }

        protected override void CurrentPlayerChanged()
        {
            base.CurrentPlayerChanged();
            Notify(nameof(this.Title));
        }

        public override void InitializeGame()
        {
            shots.SetValue(shotsInitValue, 1);
            shots.SetValue(shotsInitValue, 2);
            Notify(nameof(Title));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            if (GameViewModel.SelectedField != null)
            {
                if (GameViewModel.SelectedField.Owner == GameViewModel.CurrentPlayer && shots[GameViewModel.CurrentPlayer] > 0)
                {
                    foreach (Field field in GameViewModel.Model.Fields)
                    {
                        if (field.Column == GameViewModel.SelectedField.Model.Column || field.Row == GameViewModel.SelectedField.Model.Row)
                        {
                            field.Owner = 0;
                        }
                    }
                    shots[GameViewModel.CurrentPlayer]--;
                    Notify(nameof(Title));
                    return true;
                }
            }
            return false;
        }
    }
}
