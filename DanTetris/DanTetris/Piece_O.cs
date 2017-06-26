using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanTetris
{
    sealed public class Piece_O : Piece
    {
        public Piece_O(GameView gView) : base(Config.PIECE_O_ID, gView)
        {
            // Set width and height for the piece 'O' which is 2x2.
            pieceWidth  = 2;
            pieceHeight = 2;

            // Allocate data for the shape 'O'.
            pieceData    = new Color[pieceWidth, pieceHeight];

            // Fill data for the shape 'O'.
            pieceData[0, 0] = occupiedColor;
            pieceData[0, 1] = occupiedColor;
            pieceData[1, 0] = occupiedColor;
            pieceData[1, 1] = occupiedColor;

            // Draw the piece at the default cell in the grid.
            drawAt(currX, currY);
        }
    }
}
