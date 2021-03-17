using AttaxxPlus.Model.Operations;

namespace AttaxxPlus.Model
{
    // EVIP: deriving from GameBase to specify exact situation and startup playing field.
    public class AIGame : GameBase
    {
		private readonly int AI_PLAYER_IDX = 2;

		public AIGame(int size) : base()
        {
            // Note: InitializeGame expects these objects already created.
            // EVIP: instantiating and initializing 2D array (row, column)
            Fields = new Field[size, size];
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                    Fields[row, col] = new Field() { Row = row, Column = col, Owner = 0 };
            // EVIP: setting property with protected setter
            NumberOfPlayers = 2;
			base.PropertyChanged += GameBase_PropertyChanged;

			InitializeGame();
        }

		private void GameBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(CurrentPlayer)) {
				if (CurrentPlayer == AI_PLAYER_IDX) {
					RunAI();
				}
			}
		}

		private void RunAI() {
			// We could do much better by evaluating multiple steps in the future.
			(var source, var target, var score) = GetBestMove(AI_PLAYER_IDX);

			if (jump.GetExecutionScore(source, target) > cloneMove.GetExecutionScore(source, target)) {
				jump.TryExecute(source, target);
			} else {
				cloneMove.TryExecute(source, target);
			}

			EndOfTurn();
		}

		// This is called both upon startup (ctor) and upon starting a new game.
		public override void InitializeGame()
        {
            // EVIP: We should not re-create fields as they are already data binded to view models.
            for (int row = 0; row < Fields.GetLength(0); row++)
                for (int col = 0; col < Fields.GetLength(1); col++)
                    Fields[row, col].Owner = 0;

            Fields[0, 0].Owner = 1;
			Fields[Fields.GetLength(0) - 1, Fields.GetLength(1) - 1].Owner = 2;
		}
    }
}
