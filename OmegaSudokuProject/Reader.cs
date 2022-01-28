using System;
using System.Collections.Generic;
using System.Text;

namespace OmegaSudokuProject
{
    abstract class Reader
    {
        private IReadable inputReadable;
        
        public Reader(IReadable inputReadable)
        {
            this.inputReadable = inputReadable;
        }

        public int[,] Read()
        {
            return inputReadable.Read();
        }
    }
}
