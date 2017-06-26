﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanTetris
{
    class Piece_Z : Piece
    {
        public Piece_Z(GameView gView) : base(Config.PIECE_Z_ID, gView)
        {
            // Set width and height for the piece 'Z' which is 4x2.
            pieceWidth  = 3;
            pieceHeight = 2;

            currX = 4;
            currY = 0;

            // Allocate data for the shape 'Z'.
            pieceData = new Color[pieceWidth, pieceHeight];

            for (int x = 0; x < pieceWidth; ++x)
            {
                for (int y = 0; y < pieceHeight; ++y)
                {
                    pieceData[x, y] = freeColor;
                }
            }

            // Fill data for the shape 'Z'.
            pieceData[0, 0] = occupiedColor;
            pieceData[1, 0] = occupiedColor;
            pieceData[1, 1] = occupiedColor;
            pieceData[2, 1] = occupiedColor;

            // Draw the piece at the default cell in the grid.
            drawAt(currX, currY);
        }
    }
}