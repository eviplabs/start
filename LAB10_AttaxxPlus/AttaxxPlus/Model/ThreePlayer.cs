﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttaxxPlus.Model
{
    public class ThreePlayer : GameBase
    {
        public ThreePlayer(int size) : base()
        {
            // Note: InitializeGame expects these objects already created.
            // EVIP: instantiating and initializing 2D array (row, column)
            Fields = new Field[size, size];
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                    Fields[row, col] = new Field() { Row = row, Column = col, Owner = 0 };
            // EVIP: setting property with protected setter
            NumberOfPlayers = 3;

            InitializeGame();
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
            Fields[0, Fields.GetLength(1) - 1].Owner = 3;
        }
    }
}
