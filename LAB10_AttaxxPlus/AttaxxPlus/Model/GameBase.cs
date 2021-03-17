using AttaxxPlus.Model.Operations;
using System.Linq;

namespace AttaxxPlus.Model
{
    /// <summary>
    /// Base class for a game. Provides matrix of playing field, current player, 
    /// basic game operations, and end-of-game condition check.
    /// </summary>
    public abstract class GameBase : ObservableObject
    {
		protected readonly CloneMoveOperation cloneMove;
		protected readonly JumpOperation jump;

		public GameBase()
        {
			cloneMove = new CloneMoveOperation(this);
			jump = new JumpOperation(this);
		}

        public abstract void InitializeGame();

        public Field[,] Fields { get; protected set; }
        // EVIP: property with protected setter (will be set by derived classes ctor)
        public int NumberOfPlayers { get; protected set; }

        // EVIP: property with private member and getter-setter.
        private int currentPlayer = 1;
        public int CurrentPlayer
        {
            get => currentPlayer;
            set
            {
                if (currentPlayer != value)
                {
                    currentPlayer = value;
                    Notify();
                }
            }
        }

        private int? winner = null;
        /// <summary>
        /// null means the game is still running.
        /// 0 means draw.
        /// </summary>
        public int? Winner
        {
            get => winner;
            private set
            {
                if (winner != value)
                {
                    winner = value;
                    Notify();
                    // EVIP: setter notifies about change of multiple properties
                    Notify(nameof(IsGameRunning));
                }
            }
        }

        // EVIP: read-only property as body expression
        public bool IsGameRunning => Winner is null;

        public void EndOfTurn()
        {
            if (!CheckGameOver())
            {
                if (CurrentPlayer < NumberOfPlayers)
                    CurrentPlayer++;
                else
                    CurrentPlayer = 1;
            }
        }

		protected (Field, Field, int) GetBestMove(int currPlayer) {
			int maxScore = -1;
			Field maxScoreSource = null;
			Field maxScoreTarget = null;
			foreach (var selField in Fields) {
				if (selField.Owner == currPlayer) {
					foreach (var currField in Fields) {
						int tmpScore = cloneMove.GetExecutionScore(selField, currField);
						if (tmpScore > maxScore) {
							maxScore = tmpScore;
							maxScoreSource = selField;
							maxScoreTarget = currField;
						}
						tmpScore = jump.GetExecutionScore(selField, currField);
						if (tmpScore > maxScore) {
							maxScore = tmpScore;
							maxScoreSource = selField;
							maxScoreTarget = currField;
						}
					}
				}
			}
			return (maxScoreSource, maxScoreTarget, maxScore);
		}

        private bool CheckGameOver()
        {
            Winner = null;  // Game can have been reinitialized since last check.
            // Count fields for every player (player 0 is empty field)
            int[] counts = new int[NumberOfPlayers+1];
            foreach (var f in Fields)
                counts[f.Owner]++;

            int totalFieldCount = Fields.GetLength(0) * Fields.GetLength(1);

            int playerWithMaxFields = 0;
            int maxFieldCountPerPlayer = 0;
            for(int i=1; i<counts.Length; i++)
                if (counts[i]> maxFieldCountPerPlayer)
                {
                    playerWithMaxFields = i;
                    maxFieldCountPerPlayer = counts[i];
                }

            // Is there only one player with fields?
            int othersFieldCount = totalFieldCount - counts[0] - maxFieldCountPerPlayer;
            if (othersFieldCount == 0)
            {
                Winner = playerWithMaxFields;
                return true;
            }

            if (maxFieldCountPerPlayer == totalFieldCount || counts[0]==0)
            {
                // Check for draw
                // EVIP: Linq Count with condition
                Winner = (counts.Count(c => c==maxFieldCountPerPlayer) == 1)
                    ? playerWithMaxFields : 0;
                return true;
            }

			int numPlayersWithPossibleMoves = 0;
			for (int playerIdx = 1; playerIdx <= NumberOfPlayers; playerIdx++) {
				(var _, var _, int score) = GetBestMove(playerIdx);
				if (score != -1) {
					numPlayersWithPossibleMoves++;
				}
			}
			if (numPlayersWithPossibleMoves <= 1) {
				Winner = playerWithMaxFields;
				return true;
			}

			return false;
        }
    }
}
