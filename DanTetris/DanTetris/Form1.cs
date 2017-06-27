using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanTetris
{
    public partial class Form1 : Form
    {
        // This is the main game's timer. A function will be triggered at each
        // tick to update the game's board which is eventually moving down a
        // piece or free, collision check and/or removing complete rows and give
        // some score to the player.
        static Timer myTimer = new Timer();

        private GameView board;
        private Piece piece;
        private int numCompleteLines = 0;
        private bool gameOver = false;

        public Form1()
        {
            InitializeComponent();
        }

        // At each tick of this timer the game board will be updated.
        private void TimerEventProcessor(Object myObject,
                                         EventArgs myEventArgs)
        {
            Random rnd = new Random();
            int tetrisShapeID = rnd.Next(0, 7);    // Choose one of the 7 Tetris shapes (0-6).
            int y1, y2;

            y1 = piece.currY;
            piece.moveDown();
            y2 = piece.currY;

            // If vertical position after moving down is still same and standing
            // at either 0th or 1st row, then there is no more room for new
            // pieces and hence the game is over.
            if ((y1 == y2) && (y1 <= 1))
            {
                myTimer.Stop();
                gameOver = true;
                MessageBox.Show("Game Over! The score is " + numCompleteLines.ToString());
            }

            if (piece.IsCollision() && !gameOver)
            {
                piece.OnCollisionDetected(new EventArgs());

                // Scan the board for the score and remove complete lines.
                myTimer.Stop();
                numCompleteLines += board.RemoveCompleteLines();
                myTimer.Start();

                switch (tetrisShapeID)
                {
                    case 0:
                        piece = new Piece_O(board);
                        break;
                    case 1:
                        piece = new Piece_I(board);
                        break;
                    case 2:
                        piece = new Piece_S(board);
                        break;
                    case 3:
                        piece = new Piece_Z(board);
                        break;
                    case 4:
                        piece = new Piece_L(board);
                        break;
                    case 5:
                        piece = new Piece_J(board);
                        break;
                    case 6:
                        piece = new Piece_T(board);
                        break;
                }
            }
        }

        // Initialize the game board, star up the timer, create the first piece, etc.
        private void GameBoard_Load(object sender, EventArgs e)
        {
            board = new GameView(this);

            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 500;
            //myTimer.Start();

            piece = new Piece_O(board);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    // Move the piece to the left.
                    piece.left();
                    break;
                case Keys.Right:
                    // Move the piece to the right.
                    piece.right();
                    break;
                case Keys.Down:
                    // Perform drop of the piece.
                    piece.drop(myTimer);
                    break;
                case Keys.Space:
                    // Perform rotation.
                    piece.rotate();
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myTimer.Start();
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Move left a piece ---> Left arrow key\nMove right a piece ---> Right arrow key\nDrop a piece ---> Down arrow key\nRotate a piece ---> Space key");
        }
    }
}
