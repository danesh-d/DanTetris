# DanTetris

Tetris implementation in C# written by Danesh Daroui!

Version 1.1

# Known issues and bugs

1. When multiple lines are completted, after a **vertical shift**, someitmes some
   columns will hang without falling properly.
2. When two pieces are very close as there is no room for rotation, an 'out of
   bound' exception is thrown when the program tries to draw the piece with the
   white pen before actual drawing.

# Remedies

1. Perform vertical scan over each column and perform replacing as needed
   instead of a complete snapshot as it is done which will result in copy and
   paste a bulk of data.

2. Further investigation is needed, but probably a double check is needed when
   draw is done since when it is drawing with the white pen, any error should
   abort the drawing since the whole process is experimental.

# TODO

1. Add about model dialog box instead of message box.
2. Use a modal dialog box for help instead of message box.
3. Show the next piece before it is shown.
4. The fist piece is always 'O'. Make it random as well!
5. Let the user register and increase the level.
6. Adapt the given score to the level and number of emptied rows at once.
7. Change the color of the completted row before removing. This is partially
   done. The GUI should probably be able to handle messages when a delay is
   injected.
8. Make the drop progressive. Right now a piece is fallen suddenly. Not bad
   though! Think that the original Tetris game was also like that!
9. Refactor the code. Lots of places and lots of loops (like rotation) can be
   optimized to act better and less embarrasing!
