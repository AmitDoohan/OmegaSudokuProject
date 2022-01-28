using System;
using System.Collections.Generic;
using System.Text;

namespace OmegaSudokuProject
{
    abstract class Writer
    {
        private IWritable outputWritable;

        public Writer(IWritable outputWritable)
        {
            this.outputWritable = outputWritable;
        }

        public bool Write(int[,] board)
        {
            return outputWritable.Write(board);
        }
    }
}
