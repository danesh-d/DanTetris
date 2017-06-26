using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanTetris
{
    public class GameView : Config
    {
        private Label[] labels;
        private bool[] virtualGrid;
        private int width, height;

        public GameView(Form1 GameForm)
        {
            this.width = GameBoardWidth;
            this.height = GameBoardHeight;
            labels = new Label[width * height];
            virtualGrid = new bool[width * height];

            int labelCnt = 0;

            for (int i = offset; i < width + offset; i++)
            {
                for (int j = offset; j < height + offset; j++)
                {
                    labels[labelCnt] = new Label();
                    labels[labelCnt].Size = new Size(22, 22);
                    labels[labelCnt].Location = new Point(i * 20, j * 20);
                    labels[labelCnt].BackColor = freeColor;
                    labels[labelCnt].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    GameForm.Controls.Add(labels[labelCnt]);

                    ++labelCnt;
                }
            }

            ClearGrid();
        }

        // Clear the whole grid.
        private void ClearGrid()
        {
            for (int x = 0; x < GameBoardWidth; ++x)
            {
                for (int y = 0; y < GameBoardHeight; ++y)
                {
                    SetCell(x, y, 1);
                }
            }
        }

        private void ClearVirtualGrid()
        {
            for (int x = 0; x < GameBoardWidth; ++x)
            {
                for (int y = 0; y < GameBoardHeight; ++y)
                {
                    SetVCell(x, y, true);
                }
            }
        }

        private void SetVCell(int x, int y, bool clear = false)
        {
            virtualGrid[x * height + y] = !clear;
        }

        private bool GetVCell(int x, int y)
        {
            return virtualGrid[x * height + y];
        }

        private bool IsCoorValid(int x, int y)
        {
            if ((x >= width) || (y >= height))
            {
                // Coordinate(s) is/are out of range.
                return false;
            }

            return true;
        }

        // Check whether the cell at the given coordinate is in a complete row
        // or not.
        public bool IsComplete(int x, int y)
        {
            if (!IsCoorValid(x, y))
            {
                return false;
            }

            return (labels[x * height + y].BackColor == completeColor);
        }

        private void RemoveCompleteRows()
        {
            int ind = 0;
            ClearVirtualGrid();

            // First go through the board and mark the first complete row and
            // make it empty.
            for (int j = 0; j < GameBoardHeight; ++j)
            {
                for (int i = 0; i < GameBoardWidth; ++i)
                {
                    if (IsComplete(i, j))
                    {
                        // Free this row as this complete.
                        for (int k = 0; k < GameBoardWidth; ++k)
                        {
                            SetCell(k, j, 1);
                        }

                        // Save the index of the freed row.
                        ind = j;

                        break;
                    }
                }
            }

            // From the first row (at top) until the freed row, save all rows in
            // the virtual grid.
            for (int y = 0; y < ind; ++y)
            {
                for (int x= 0; x < GameBoardWidth; ++x)
                {
                    if (IsOccupied(x, y))
                    {
                        SetCell(x, y, 1);
                        SetVCell(x, y);
                    }
                }
            }

            // Copy the virtual grid back to the main grid by shifting all rows
            // on toward the bottom.
            for (int y = 0; y < ind; ++y)
            {
                for (int x = 0; x < GameBoardWidth; ++x)
                {
                    if (GetVCell(x, y))
                    {
                        SetCell(x, y + 1);
                    }
                }
            }
        }

        // Set a cell as occupied.
        public bool SetCell(int x, int y, int nonOccuCell = 0)
        {
            Color color;

            if (!IsCoorValid(x, y))
            {
                return false;
            }

            // The cell can either be set as occupied or as free.
            if (nonOccuCell > 0)
            {
                if (nonOccuCell == 1)
                {
                    color = freeColor;
                }
                else if (nonOccuCell == 2)
                {
                    color = completeColor;
                }
                else
                {
                    // Invalid color.
                    return false;
                }
            }
            else
            {
                color = occupiedColor;
            }

            // The data is stored in column-major format so find the correct
            // cell, mark it as set (black) and return true as success.
            labels[x * height + y].BackColor = color;

            return true;
        }

        // Check whether the cell at the given coordinate is occupied or not.
        public bool IsOccupied(int x, int y)
        {
            if (!IsCoorValid(x, y))
            {
                return false;
            }

            return (labels[x * height + y].BackColor == occupiedColor);
        }

        // This function will check the whole board and will remove all
        // complete lines so far and will return number of complete lines which
        // were remove during a recent scan.
        public int RemoveCompleteLines()
        {
            int lines = 0;

            for (int j = 0; j < GameBoardHeight; ++j)
            {
                bool flag = true;

                for (int i = 0; i < GameBoardWidth; ++i)
                {
                    if (IsOccupied(i, j) == false)
                    {
                        flag = false;
                        break;
                    }
                }

                // A row is full, make it marked as complete and remove it. Then
                // move all rows abve one row down.
                if (flag)
                {
                    for (int k = 0; k < GameBoardWidth; ++k)
                    {
                        SetCell(k, j, 2);
                    }

                    ++lines;

                    Thread.Sleep(500);
                }

                RemoveCompleteRows();
            }

            return lines;
        }
    }
}
