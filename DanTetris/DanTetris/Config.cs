using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanTetris
{
    // This class contails all configuration data of the game.
    public class Config
    {
        // Dimensions of the game board.
        protected readonly int GameBoardWidth  = 10;
        protected readonly int GameBoardHeight = 20;

        // Offset of the board from the edge of the form.
        protected readonly int offset = 4;

        // Color of the free/occupied/completted cells.
        protected readonly Color freeColor     = Color.Red;
        protected readonly Color occupiedColor = Color.Black;
        protected readonly Color completeColor = Color.Yellow;

        // Each piece's upper-left most poiont will coincide with (5, 0).
        protected readonly int initX = 4;
        protected readonly int initY = 0;

        // Default dimensions of a piece.
        protected readonly int defaultPieceWidth  = 4;
        protected readonly int defaultPieceHeight = 2;

        // Unique IDs of different shapes.
        public static readonly byte PIECE_O_ID = 0;
        public static readonly byte PIECE_I_ID = 1;
        public static readonly byte PIECE_S_ID = 2;
        public static readonly byte PIECE_Z_ID = 3;
        public static readonly byte PIECE_L_ID = 4;
        public static readonly byte PIECE_J_ID = 5;
        public static readonly byte PIECE_T_ID = 6;
    }
}
