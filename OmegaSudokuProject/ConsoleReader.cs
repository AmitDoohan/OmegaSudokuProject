using System;
using System.Collections.Generic;
using System.Text;

namespace OmegaSudokuProject
{
    public class ConsoleReader : IReadable, IWritable
    {
        public ConsoleReader() { }

        public int[,] Read()
        {
            int size, counter = 0;
            char ch;
            string input = Console.ReadLine();
            int c = input.Length;
            input.Replace(" ", "");
            size = (int)Math.Sqrt(input.Length);
            int[,] board = new int[size, size];
            for (int i = 0; i < input.Length; i++)
            {
                ch = input[i];
                if (ch != ' ')
                {
                    board[counter / size, counter % size] = (ch - '0');
                    counter++;
                }
            }
            return board;
        }

        public bool Write(int[,] resultBoard)
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
                {
                    int x = resultBoard[i, j];
                    char val = (char)(x + '0');
                    Console.Write($"   {val}");
                }
                Console.WriteLine("\n");
            }
            return true;
        }
    }
}
