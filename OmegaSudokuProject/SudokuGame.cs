using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OmegaSudokuProject
{
    public class SudokuGame
    {
        public static void Start()
        {
            Console.WriteLine("------------------S U D O K O------------------\n" +
                "Welcome to sudoku solver\n" +
                "You may choose your input:\n" +
                "\t- for console type c\n" +
                "\t- for file type f\n" +
                "\t- for exit type e");
            string choice, filePath= "C:\\Users\\Amit\\Documents\\omega\\OmegaSudokuProject\\OmegaSudokuProject\\16x16 hard board.txt";
            bool chosen = false;
            //Default read and write is console reader:
            ConsoleReader cr = new ConsoleReader();
            //Reader reader; 
            //Writer writer; 
            IReadable read = cr;
            IWritable write = cr;

            do
            {
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "c":

                        Console.WriteLine("Enter board:");
                        chosen = true;
                        break;

                    case "f":

                        //Open window for choosing file and get file path:
                        //OpenFileDialog ofd = new OpenFileDialog();
                        //ofd.Filter = "txt files (*.txt)|*.txt| All files(*.*)|*.*";
                        //if (ofd.ShowDialog() == DialogResult.OK)
                       // {
                            //filePath = ofd.FileName;
                            //FileReader fileIO = new FileReader(filePath);
                            //reader = new Reader(fileIO);
                            //writer = new Writer(fileIO);
                            read = new FileReader(filePath);
                            //write = new FileReader(filePath);
                        //}
                        chosen = true;
                        break;

                    case "e":

                        //exit
                        Environment.Exit(0);
                        break;
                }
            }
            while (!chosen);

            int[,] board = read.Read();
            SudokuSolver.PrintBoard(board);
            Board b = new Board(board, board.GetLength(0));
            SudokuSolver.SolveWithBits2(ref b);
            SudokuSolver.PrintBoard(b.SudokuBoard);
           // bool success = SudokuSolver.SolveBckTracking2(ref b);
           // if (success)
            //    SudokuSolver.PrintBoard(b.SudokuBoard);
            //write.Write(board);

            //-------------------------------------------------------------------------------------------
            //int size = board.GetLength(0);
            //int[,] oldBoard = new int[size, size];
            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        oldBoard[i, j] = board[i, j];
            //    }
            //}
            //SudokuSolver.PrintBoard(board);
            //Console.WriteLine("\n\n");
            //int row = 0, col = 0, x = 0;
            //bool isPossible = SudokuSolver.IsLegal(board, row, col, x);
            //int countPossible = SudokuSolver.CountLegal(board, row, col);
            //int[] min = SudokuSolver.FindMin(board);
            //Console.WriteLine($"({row},{col}) possible:{countPossible}\nmin: ({min[0]},{min[1]})");
            //bool solve = SudokuSolver.Solve(ref board);
            //int[,] newBoard = new int[size, size];
            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        newBoard[i, j] = board[i, j];
            //    }
            //}
            //Console.WriteLine($"solved: {solve}\n");
            //SudokuSolver.PrintBoard(board);
            //Console.WriteLine(SudokuSolver.IsBoardLegal(newBoard));
            //Console.WriteLine(SudokuSolver.IsBoardLegal2(board, oldBoard));
            //-------------------------------------------------------------------------------------------
            //write.Write(result);
            //int[,] result = SudokuSolver.Solve(board);
            //write.Write(result);
        }
    }
}
