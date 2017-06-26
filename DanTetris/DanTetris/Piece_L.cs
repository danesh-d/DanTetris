using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanTetris
{
    sealed class Piece_L : Piece
    {
        public Piece_L(GameView gView) : base(Config.PIECE_L_ID, gView)
        {
            // Set width and height for the piece 'L' which is 4x2.
            pieceWidth  = 3;
            pieceHeight = 2;

            currX = 4;
            currY = 1;

            // Allocate data for the shape 'L'.
            pieceData = new Color[pieceWidth, pieceHeight];

            for (int x = 0; x < pieceWidth; ++x)
            {
                for (int y = 0; y < pieceHeight; ++y)
                {
                    pieceData[x, y] = freeColor;
                }
            }

            // Fill data for the shape 'L'.
            pieceData[0, 1] = occupiedColor;
            pieceData[1, 1] = occupiedColor;
            pieceData[2, 1] = occupiedColor;
            pieceData[2, 0] = occupiedColor;

            // Draw the piece at the default cell in the grid.
            drawAt(currX, currY);
        }
    }
}
