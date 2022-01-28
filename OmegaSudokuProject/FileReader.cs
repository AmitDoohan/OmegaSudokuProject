using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace OmegaSudokuProject
{
    public class FileReader : IReadable, IWritable
    {
        private string fileName;

        public FileReader(string fileName)
        {
            this.fileName = fileName;
        }

        public int[,] Read()
        {
            int size, counter = 0;
            char ch;
            StreamReader reader;
            reader = new StreamReader(fileName);
            do
            {
                ch = (char)reader.Read();
                if (ch != ' ')
                    counter++;
            } while (!reader.EndOfStream);
            size = (int)Math.Sqrt(counter);
            int[,] board = new int[size, size];
            reader = new StreamReader(fileName);
            int i = 0;
            do
            {
                ch = (char)reader.Read();
                if (ch != ' ')
                {
                   board[i / size, i % size] = (ch - '0');
                   i++;
                }
            } while (!reader.EndOfStream);

            reader.Close();
            reader.Dispose();
            return board;
        }

        public bool Write(int[,] resultBoard)
        {
            int numberOfCells = resultBoard.GetLength(0) * resultBoard.GetLength(1);
            string result = "";
            for (int i = 0; i < numberOfCells; i++)
                result += (char)(resultBoard[i / 10, i % 10] + '0');
            File.WriteAllText(fileName, result);
            return true;
        }
    }
}
