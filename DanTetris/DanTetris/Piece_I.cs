using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanTetris
{
    class Piece_I : Piece
    {
        public Piece_I(GameView gView) : base(Config.PIECE_I_ID, gView)
        {
            // Set width and height for the piece 'I' which is 4x1.
            pieceWidth  = 4;
            pieceHeight = 1;

            // Allocate data for the shape 'I'.
            pieceData = new Color[pieceWidth, pieceHeight];

            // Fill data for the shape 'I'.
            pieceData[0, 0] = occupiedColor;
            pieceData[1, 0] = occupiedColor;
            pieceData[2, 0] = occupiedColor;
            pieceData[3, 0] = occupiedColor;

            // Draw the piece at the default cell in the grid.
            drawAt(currX, currY);
        }
    }
}
