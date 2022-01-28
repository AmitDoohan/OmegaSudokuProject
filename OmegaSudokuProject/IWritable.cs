using System;
using System.Collections.Generic;
using System.Text;

namespace OmegaSudokuProject
{
    public interface IWritable
    {
        bool Write(int[,] board);
    }
}
