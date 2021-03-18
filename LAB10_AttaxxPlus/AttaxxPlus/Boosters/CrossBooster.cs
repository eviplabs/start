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
			throw new NotImplementedException();
		}
	}
}
