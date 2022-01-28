using System;
using System.Collections.Generic;
using System.Text;

namespace OmegaSudokuProject
{
    public static class SudokuSolver
    {
        public static bool IsLegal(int[,] board, int row, int col, int x)
        {
            int size = board.GetLength(0);
            int subSize = (int)Math.Sqrt(size);

            //check if row and column contains x already
            for (int i = 0; i < size; i++)
            {
                if (board[row, i] == x || board[i, col] == x)
                    return false;
            }
            //check if sub-board contains x already
            int startRow = row - (row % subSize);//start row for sub board
            int startCol = col - (col % subSize);//start col for sub board
            for (int i = 0; i < subSize; i++)
            {
                for (int j = 0; j < subSize; j++)
                {
                    if (board[startRow + i, startCol + j] == x)
                        return false;
                }
            }
            return true;
        }
        public static int CountLegal(int[,] board, int row, int col)
        {
            int size = board.GetLength(0);
            int countLegalValues = 0;
            for (int i = 1; i <= size; i++)
            {
                if (IsLegal(board, row, col, i))
                    countLegalValues++;
            }
            return countLegalValues;
        }
        public static int[] FindMin(int[,] board)
        {
            int size = board.GetLength(0);
            //int subSize = (int)Math.Sqrt(size);
            int[] minSquare = { -1, -1 };
            int minCount = size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == 0)
                    {
                        int currentSquareCount = CountLegal(board, i, j);
                        //if (currentSquareCount == 0)
                        //    return null;//board unsolveable
                        if (currentSquareCount < minCount)
                        {
                            minCount = currentSquareCount;
                            minSquare[0] = i;
                            minSquare[1] = j;
                        }
                    }
                }
            }
            return minSquare;
        }
        public static bool Solve(ref int[,] board)
        {
            int size = board.GetLength(0);
            int subSize = (int)Math.Sqrt(size);

            int[] minSquare = FindMin(board);
            int r = minSquare[0], c = minSquare[1];

            if (r == -1)
            {
                return true;
            }
            for (int i = 1; i <= size; i++)
            {
                if (IsLegal(board, r, c, i))
                {
                    board[r, c] = i;
                    if (Solve(ref board))
                        return true;
                    board[r, c] = 0;
                }
            }
            return false;
        }
        public static bool PrintBoard(int[,] resultBoard)
        {
            int size = resultBoard.GetLength(0);
            int numberofcells = size * size;
            int square;
            Console.Write(" ");
            for (int i = 1; i <= size; i++)
            {
                Console.Write($"   {i}");
            }
            Console.Write("\n");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{i + 1}");
                for (int j = 0; j < size; j++)
                //Console.Write($"   {resultBoard[i, j]}");
                {

                    int x = resultBoard[i, j];
                    char val = (char)(x + '0');
                    Console.Write($"   {val}");
                }
                Console.WriteLine("\n");
            }
            return true;
        }
        public static bool IsBoardLegal(int[,] board)
        {
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int value = board[i, j];
                    board[i, j] = 0;
                    if (!IsLegal(board, i, j, value))
                        return false;
                }
            }
            return true;
        }
        public static bool IsBoardLegal2(int[,] resultBoard, int[,] board)
        {
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] != 0)
                        if (board[i, j] != resultBoard[i, j])
                            return false;
                }
            }
            return true;
        }

        //---------------------------------------------------------------
        public static void SolveWithBits2(ref Board board)
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    int hidden = board.FindHidden(i, j);
                    if (hidden != -1)
                    {
                        board.SudokuBoard[i, j] = hidden;
                        board.InserValue(i, j, hidden);
                    }
                }
            }

            //for (int i = 0; i < board.Size; i++)
            //{
            //    int box = board.GetBox(i), row = board.GetRow(i),col = board.GetCol(i);
            //    int rowStatus = board.GetGroupStatus(row);
            //    int colStatus = board.GetGroupStatus(col);
            //    int boxStatus = board.GetGroupStatus(box);

            //    for (int j = 0; j < board.Size; j++)
            //    {
            //        if (rowStatus > 0 && board.SudokuBoard[i, j] == 0)
            //            board.SudokuBoard[i, j] = rowStatus;        
            //        if (colStatus > 0 && board.SudokuBoard[j, i] == 0)
            //            board.SudokuBoard[j, i] = rowStatus;
            //        int r = board.GetRowBySubIndecies(i, j);
            //        int c = board.GetColBySubIndecies(i, j);
            //        if (rowStatus > 0 && board.SudokuBoard[r, c] == 0)
            //            board.SudokuBoard[r, c] = rowStatus;
            //    }           
            //}
        }//o(n^2)
        public static bool SolveBckTracking2(ref Board board)
        {
            int[] minSquare = FindMin2(ref board);//o(n^2)
            int r = minSquare[0], c = minSquare[1];

            if (r == -1)//if finished
            {
                return true;
            }
            int hidden = board.FindHidden(r, c);//o(1)
            if (hidden != -1)
            {
                board.SudokuBoard[r, c] = hidden;//o(1)
                board.InserValue(r, c, hidden);//o(1)
            }
            else
                for (int i = 1; i <= board.Size; i++)//o(n)
                {
                    if (board.IsValueValid(r, c, i))//o(1)
                    {
                        board.SudokuBoard[r, c] = i;//o(1)
                        if (SolveBckTracking2(ref board))
                            return true;
                        board.SudokuBoard[r, c] = 0;//o(1)
                    }
                }
            return false;
        }
        public static int[] FindMin2(ref Board board)
        {
            int[] minSquare = { -1, -1 };
            int minCount = board.Size;
            for (int i = 0; i < board.Size; i++)//total: o(n^2)
            {
                int rowStatus = board.GetGroupStatus(board.GetRow(i));//o(1)
                if (rowStatus != 0)
                    for (int j = 0; j < board.Size; j++)
                    {
                        if (board.SudokuBoard[i, j] == 0)//total: o(n)
                        {
                            int currentSquareCount = board.CountPossible(i, j);//o(1)
                            if (currentSquareCount == 1)
                            {
                                minCount = currentSquareCount;
                                minSquare[0] = i;
                                minSquare[1] = j;
                                return minSquare;
                            }
                            if (currentSquareCount < minCount)
                            {
                                minCount = currentSquareCount;
                                minSquare[0] = i;
                                minSquare[1] = j;
                            }
                        }
                    }
            }
            return minSquare;
        }//o(n^2)

    }
}
