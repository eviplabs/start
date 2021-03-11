using System;
using System.Collections.Generic;
using System.Linq;

namespace AttaxxPlus.Model
{
    /// <summary>
    /// Base class for a game. Provides matrix of playing field, current player, 
    /// basic game operations, and end-of-game condition check.
    /// </summary>
    public abstract class GameBase : ObservableObject
    {
        public GameBase()
        {
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


            //Check if player 2 or player 1 is surrounded
            List<(int, int)> positions1 = FindPos(1);            
            bool allSorrounded1 = true;
            foreach (var item in positions1)
            {
                if (!CheckSurround(item.Item1, item.Item2))
                    allSorrounded1 = false;
            }
            if (allSorrounded1)
            {
                Winner = 2;
                return true;
            }

            List<(int, int)> positions2 = FindPos(2);
            bool allSorrounded2 = true;
            foreach (var item in positions2)
            {
                if (!CheckSurround(item.Item1, item.Item2))
                    allSorrounded2 = false;              
            }
            if (allSorrounded2)
            {
                Winner = 1;
                return true;
            }

            return false;
        }        

        private List<(int row, int col)>  FindPos(int player)
        {
            List<(int, int)> pos = new List<(int, int)>();
            foreach (var item in Fields)
            {
                if (item.Owner == player)
                {
                    pos.Add((item.Row, item.Column));
                }
            }
            return pos;
        }

        private bool CheckSurround(int row, int col)
        {
            List<Field> surroundings = CaluclateSurroundings(row, col);

            foreach (var item in surroundings)
            {
                if (item.Owner == 0)
                {
                    return false;
                }
            }
            return true;

        }

        private List<Field> CaluclateSurroundings(int row, int col)
        {
            List<Field> surroundings = new List<Field>();

            int deltaRow;
            int deltaCol;
            foreach (var item in Fields)
            {
                deltaRow = Math.Abs(item.Row - row);
                deltaCol = Math.Abs(item.Column - col);

                if (deltaRow == 0 && deltaCol == 0)
                    continue;

                if ((deltaRow <= 2 && deltaCol == 0) ||
                    (deltaRow == 0 && deltaCol <= 2) ||
                    (deltaRow == 1 && deltaCol == 1))
                {
                    surroundings.Add(item);
                }
            }

            return surroundings;
        }

    }
}
