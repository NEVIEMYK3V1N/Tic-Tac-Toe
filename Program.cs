/* Author: Kevin Yang
 * A simple tic tac toe game in console
 */ 

using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

// Initial Variables - needs to be reset to restart a game
string[,] board = new string[3, 3]
{
	{" ", " ", " "},
    {" ", " ", " "},
    {" ", " ", " "}
};
bool isEnding = false;
int counter = 1;
string symbolPlaying = "O";
string toReset = " ";
string winner = " ";

// drawBoard(board) prints the board onto console
static void drawBoard(string[,] board)
{
	Console.Clear();
    Console.WriteLine("You are now playing the simple tic tac toe game!");
    Console.WriteLine("---------------");
    Console.WriteLine("|#|-0-|-1-|-2-|");
	Console.WriteLine("---------------");
    for (int i = 0; i < board.GetLength(0); i++)
	{
        Console.Write("|" + i + "|");
        for (int j = 0; j < board.GetLength(1); j++)
		{
			Console.Write($" {board[i,j]} |");
		}
		Console.WriteLine();
		Console.WriteLine("---------------");
	}
}

// getAndUpdatePlayerInput(symbolPlaying, board) adds the player symbol at the inputted postion
//		on the given board if it is a valid move, else prints an error message and repeates
//		until valid inputs are given
static (string[,], string) getAndUpdatePlayerInput(string symbolPlaying, string[,] board)
{
	int rowEntered = 0;
	int colEntered = 0;
	bool isValidInput = false;
    while (!isValidInput)
	{
        Console.WriteLine($"You are now playing as \"{symbolPlaying}\"");
        Console.Write("Please enter the row number of the position you want to occupy (0 to 2): ");
		bool rowValid = Int32.TryParse(Console.ReadLine(), out rowEntered)
			&& rowEntered >= 0 && rowEntered < board.GetLength(0);
		Console.Write("Please enter the colume number of the position you want to occupy (0 to 2): ");
		bool colValid = Int32.TryParse(Console.ReadLine(), out colEntered)
            && colEntered >= 0 && colEntered < board.GetLength(1);
		isValidInput = (rowValid && colValid
			&& !board[rowEntered, colEntered].Equals("X")
			&& !board[rowEntered, colEntered].Equals("O"));
		if (!isValidInput)
		{
			drawBoard(board);
			Console.WriteLine("-------------------------------------------------");
			Console.WriteLine("The last attempt was not a valid move, try again!");
            Console.WriteLine("-------------------------------------------------");
        }
    }
    int[] inputPosition = {rowEntered, colEntered};
	return updateBoard(inputPosition, board, symbolPlaying);
}

// updateBoard(inputPosition, board, symbolPlaying) changes the board at the given position to be
//		the symbol the player uses and returns the board
static (string[,], string) updateBoard(int[] inputPosition, string[,] board, string symbolPlaying)
{
	int r = inputPosition[0];
	int c = inputPosition[1];
    board[r, c] = symbolPlaying;
    if (symbolPlaying.Equals("X"))
	{
		symbolPlaying = "O";
	}
	else if (symbolPlaying.Equals("O"))
	{
		symbolPlaying = "X";
	}
	return (board, symbolPlaying);
}

// checkIfGameEnds(counter, board) return true if the game terminates and also returns the winner 
//		if a winner exist, and empty string if the game is a draw
//		else return false if the game is unfinished
static (bool, string) checkIfGameEnds(int counter, string[,] board)
{
	string winner = " ";
	bool isGameEnding = false;
    string winConditionX = "";
    string winConditionO = "";
    string diagonal1 = "";
    string diagonal2 = "";
    if (counter > board.GetLength(0)*board.GetLength(1))
	{
        isGameEnding = true;
	}
	for (int i = 0; i < board.GetLength(0); i++)
	{
		winConditionX += "X";
		winConditionO += "O";

		// vertical check
		if ((board[0, i].Equals(board[1, i]) && board[1, i].Equals(board[2, i])
			&& !board[0,i].Equals(" ")))
		{
			winner = board[0, i];
			isGameEnding = true;
		}
		else if((board[i, 0].Equals(board[i, 1]) && board[i, 1].Equals(board[i, 2])
			&& !board[i,0].Equals(" ")))
		{
			winner = board[i, 0];
			isGameEnding = true;
		}
		diagonal1 += board[i, i];
		diagonal2 += board[i, board.GetLength(0)-1-i];
	}
	if (!isGameEnding)
	{
		if (diagonal1.Equals(winConditionO)
			|| diagonal2.Equals(winConditionO)
			)
			{
				winner = "O";
				isGameEnding = true;
			}
		else if (diagonal1.Equals(winConditionX)
			|| diagonal2.Equals(winConditionX))
			{
				winner = "X";
				isGameEnding = true;
			}
	}
	return (isGameEnding, winner);
}

// Main game loop
while (!isEnding)
{
    drawBoard(board);
    (board, symbolPlaying) = getAndUpdatePlayerInput(symbolPlaying, board);
    counter++;
    (isEnding, winner) = checkIfGameEnds(counter, board);
    if (isEnding)
    {
        drawBoard(board);
        Console.WriteLine("The game has ended!");
        if (winner.Equals("X") || winner.Equals("O"))
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine($"The Winner Is Player \"{winner}\"!");
            Console.WriteLine("-------------------------");
        }
        else
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("The Game Is A Draw!");
            Console.WriteLine("-------------------");
        }
        Console.WriteLine("Thank you for playing");
        Console.Write("Press C to start another game or other keys to quit: ");

		// reset game for another play
        toReset = Console.ReadLine();
        if (toReset.ToLower().Equals("c"))
        {
            board = new string[3, 3]
            {
            {" ", " ", " "},
            {" ", " ", " "},
            {" ", " ", " "}
            };
            isEnding = false;
            counter = 1;
            toReset = " ";
            winner = " ";
        }
    }
}