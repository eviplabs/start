using AttaxxPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Boosters
{
	public class CrossBooster : BoosterBase
	{
		private int[] usableCounters = { 0, 0, 0 };

		public CrossBooster() :base()
		{
			LoadImage(new Uri(@"ms-appx:///Boosters/CrossBoosters.png"));
		}

		public override void InitializeGame()
		{
			usableCounters = new int[] { 0, 1, 1 };
			Notify(nameof(Title));
		}

		protected override void CurrentPlayerChanged()
		{
			base.CurrentPlayerChanged(); Notify(nameof(Title));
		}

		public override string Title {
			get {
				return $"Cross Booster({usableCounters[GameViewModel.CurrentPlayer]})";
			} 
		}

		public override bool TryExecute(Field selectedField, Field currentField)
		{
			if ( (selectedField != null) && (selectedField.Owner == GameViewModel.CurrentPlayer)&&(usableCounters[GameViewModel.CurrentPlayer] > 0))
			{
				usableCounters[GameViewModel.CurrentPlayer]--;
				Notify(nameof(Title));

				foreach (Field field in GameViewModel.Model.Fields)
				{
					if (field.Row == selectedField.Row || field.Column == selectedField.Column)
					{
						field.Owner = 0;
					}
				}

				return true;
			}
			return false;
		}		
	}
}
