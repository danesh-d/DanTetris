using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DanTetris
{
    public class Piece : Config
    {
        private bool collision;

        protected Color[,] pieceData;
        protected Color[,] pieceDataRot;
        protected int pieceWidth, pieceHeight;
        protected GameView gView;

        public int currX, currY, shapeID;

        public event EventHandler CollisionDetected;

        public Piece(int shapeID, GameView gView)
        {
            collision = false;

            this.shapeID = shapeID;

            // These variables will point to the left most point of a piece.
            // Set current coordinates for this shape at (5, 0) in the grid.
            currX = initX;
            currY = initY;

            // Set the piece dimensions to the max width and height for a piece.
            pieceWidth = defaultPieceWidth;
            pieceHeight = defaultPieceHeight;

            this.gView = gView;
        }

        // Clear the piece at its current position.
        private void clearPiece()
        {
            for (int i = 0; i < pieceWidth; ++i)
            {
                for (int j = 0; j < pieceHeight; ++j)
                {
                    // Clear the cells which are only and only occupied by this
                    // particular piece.
                    if ((gView.IsOccupied(currX + i, currY + j)) &&
                        (pieceData[i, j] == occupiedColor))
                    {
                        // Free the occupied cells for this piece.
                        gView.SetCell(currX + i, currY + j, 1);
                    }
                }
            }
        }

        // Draw the piece at given coordinates. If 'whitePen' is set to true,
        // then no shape will be drawn but just a complete check will be done to
        // if the actual draw will be done, there will be any collision or not.
        protected bool drawAt(int x, int y, bool whitePen = false)
        {
            if ((x >= 0) && (x + pieceWidth <= GameBoardWidth) &&
                (y >= 0) && (y + pieceHeight <= GameBoardHeight))
            {
                for (int i = 0; i < pieceWidth; ++i)
                {
                    for (int j = 0; j < pieceHeight; ++j)
                    {
                        if (whitePen)
                        {
                            if ((gView.IsOccupied(x + i, y + j)) &&
                                (pieceData[i, j] == occupiedColor))
                            {
                                // Collision detected!
                                collision = true;
                                return false;
                            }
                        }
                        else
                        {
                            if (pieceData[i, j] == occupiedColor)
                            {
                                // Only draw the marked sells.
                                gView.SetCell(x + i, y + j);
                            }
                        }
                    }
                }
            }

            return true;
        }

        // Public functions to trsansform and move each pice.

        // Rotate the piece counterclockwise 90 degrees.
        public void rotate()
        {
            clearPiece();

            pieceDataRot = new Color[pieceHeight, pieceWidth];

            int maxY = -1;

            // Find maximum 'y' for post translation when rotation is done.
            for (int x = 0; x < pieceWidth; ++x)
            {
                for (int y = 0; y < pieceHeight; ++y)
                {
                    if (pieceData[x, y] == occupiedColor)
                    {
                        maxY = (y > maxY) ? y : maxY;
                    }
                }
            }

            // Perform rotation and shift the piece to compensate for the
            // negative 'y' coordinate.
            for (int x = 0; x < pieceWidth; ++x)
            {
                for (int y = 0; y < pieceHeight; ++y)
                {
                    if (pieceData[x, y] == occupiedColor)
                    {
                        // Perform -90 degrees transform.
                        pieceDataRot[maxY - y, x] = pieceData[x, y];
                    }
                }
            }

            // Reverse the dimestions and re-allocate data for the transformed
            // piece on the grid.
            pieceData = new Color[pieceHeight, pieceWidth];

            for (int x = 0; x < pieceHeight; ++x)
            {
                for (int y = 0; y < pieceWidth; ++y)
                {
                    pieceData[x, y] = pieceDataRot[x, y];
                }
            }

            // Make sure that the rotated piece can be drawn. If not, ignore it
            // and recover the piece and draw it again.
            if (!drawAt(currX, currY, true))
            {
                // Collision has happened! The flag is set and no draw will
                // be done. Becuase the piece is already erase, so re-draw it
                // at the same place. But before re-drawing it, re-rotate it.
                pieceData = new Color[pieceWidth, pieceHeight];

                for (int x = 0; x < pieceWidth; ++x)
                {
                    for (int y = 0; y < pieceHeight; ++y)
                    {
                        pieceData[x, y] = pieceDataRot[y, x];
                    }
                }

                drawAt(currX, currY);
                return;
            }

            // It is apparently possible to draw the rotated piece, so draw it
            // and reverse the dimensions.
            int tmp = pieceWidth;
            pieceWidth = pieceHeight;
            pieceHeight = tmp;

            drawAt(currX, currY);
        }

        // Move the piece to the left.
        public void left()
        {
            if (currX > 0)
            {
                clearPiece();

                if (!drawAt(currX - 1, currY, true))
                {
                    // Collision has happened! The flag is set and no draw will
                    // be done. Becuase the piece is already erase, so re-draw it
                    // at the same place.
                    drawAt(currX, currY);
                    return;
                }

                drawAt(--currX, currY);
            }
        }

        // Move the piece to the right.
        public void right()
        {
            if (currX + pieceWidth < GameBoardWidth)
            {
                clearPiece();

                if (drawAt(currX + 1, currY, true) == false)
                {
                    // Collision has happened! The flag is set and no draw will
                    // be done. Becuase the piece is already erase, so re-draw it
                    // at the same place.
                    drawAt(currX, currY);
                    return;
                }

                drawAt(++currX, currY);
            }
        }

        // Called usually at each tick of the timer.
        public void moveDown()
        {
            if (currY + pieceHeight < GameBoardHeight)
            {
                clearPiece();

                // Check with the collision with other pieces at the bottom or
                // the bottom of the board. If there is any collision then set
                // the 'collision' variable to true and fire the collision event.
                if (drawAt(currX, currY + 1, true) == false)
                {
                    // Collision has happened! The flag is set and no draw will
                    // be done. Becuase the piece is already erased, so re-draw it
                    // at the same place.
                    drawAt(currX, currY);
                    return;
                }

                drawAt(currX, ++currY);
            }
            else
            {
                // Special case for collision when the pece is at the botton of
                // the board.
                collision = true;
            }
        }

        // Drop the piece until the first collision is detected, or at the
        // bottom of the board if there is no collision.
        public void drop(Timer t)
        {
            t.Stop();

            while (!collision)
            {
                moveDown();
            }

            t.Start();
        }

        public bool IsCollision()
        {
            return collision;
        }

        public void OnCollisionDetected(EventArgs e)
        {
            if (CollisionDetected != null)
            {
                CollisionDetected(this, e);
            }
        }
    }
}
