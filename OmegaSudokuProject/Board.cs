using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudokuProject
{
    public class Board
    {
        private int[,] sudokuBoard;//the board
        private int size;//sqrt of cells number =no. of rows for example
        private int subSize;//sqrt of size
        //arrays of rows/cols/boxes(for convinience i call all these 'groups')
        //each number in cell represents group(row/col/..)
        //indices of the bits that are set are the numbers in that group
        private int[] rows;
        private int[] cols;
        private int[] boxes;
        private static int[] BitsSetTable256=new int[256];

       

        
        public int[,] SudokuBoard {
            get
            {
                return sudokuBoard;
            }
            set
            {
                sudokuBoard = value;
            }
        }
        public int Size { get; set; }
        public int SubSize { get; set; }
        public int[] Rows { get; set; }
        public int[] Cols { get; set; }
        public int[] Boxes { get; set; }

        //The function get a matrix that represents s board  and its size. If its legal, builds new board. Otherwise, the board is null.
        public Board(int[,] board, int size)
        {   
            this.size = size;
            subSize = (int)Math.Sqrt(size);
            rows=new int[size];
            cols = new int[size];
            boxes = new int[size];

            bool boardValid = true;
            for (int i = 0; i < size && boardValid; i++)
            {
                for (int j = 0; j < size && boardValid; j++)
                {
                    if (board[i, j] != 0)
                    {
                        if (!IsValueValid(i, j, board[i, j]))
                            boardValid = false;
                        else
                            InserValue(i, j, board[i, j]);
                    }
                }
            }
            if (boardValid)
                sudokuBoard = board;
        }

        //static constructor for BitsSetTable256
        static Board()
        {
            // To initially generate the
            // table algorithmically
            BitsSetTable256[0] = 0;
            for (int i = 0; i < 256; i++)
            {
                BitsSetTable256[i] = (i & 1) +
                BitsSetTable256[i / 2];
            }
        }
       
        // Function to return the count of set bits in n
        private int CountSetBits(int n)
        {
            return (BitsSetTable256[n & 0xff] +
                    BitsSetTable256[(n >> 8) & 0xff] +
                    BitsSetTable256[(n >> 16) & 0xff] +
                    BitsSetTable256[n >> 24]);
        }

        //The function get indices of cell- row and col and returns the number of the cell's box
        private int GetBoxNum(int row, int col)
        {
            int startRow = row - (row % subSize);//start row for sub board(box)
            int startCol = col - (col % subSize);//start col for sub board(box)
            return startRow + startCol / subSize;
        }
        
        //The function get a gruop(=number from an array that represent a certain row/col/box) and a value and returns if its already exists
        private bool DoesExistInGroup(int group,int value)
        {
            int mask = 1 << (value - 1);
            return (group & mask) != 0;
        }
        
        //The function get indices of cell- row and col and its value and returns whether it is leagal to insert the value
        public bool IsValueValid(int row,int col,int value)
        {
            int numOfBox = GetBoxNum(row, col);
            bool x = DoesExistInGroup(rows[row], value);
            bool x2 = DoesExistInGroup(boxes[numOfBox], value);
            bool x3 = DoesExistInGroup(cols[col], value);
            return !(DoesExistInGroup(rows[row], value) || DoesExistInGroup(boxes[numOfBox], value) || DoesExistInGroup(cols[col], value));
        }
       
        //The function get indices of cell- row and col and its value and insertrs the value to array of rows,cols,boxes
        public void InserValue(int row, int col, int value)
        {
            int numOfBox = GetBoxNum(row, col);
            int mask = 1 << (value - 1);
            rows[row] |= mask;//set bit of current number in the current row in rows array
            cols[col] |= mask;//set bit of current number in the current column in cols array
            boxes[numOfBox] |= mask;//set bit of current number in the current box in boxes array
        }
        
        //The function get indices of cell- row and col and return number of possibe values there
        private int GetPossible(int row, int col)
        {
            int rowData = rows[row];
            int colData = cols[col];
            int boxData = boxes[GetBoxNum(row, col)];
            int mask = (int)Math.Pow(2, size) - 1;//mask is number that its size-first-bits are set
                                                    //for eaxmple: size=5 -> masx=00011111
            int possibleValues = (rowData ^ mask) & (colData ^ mask) & (boxData ^ mask);//indices of '1' bits(+1) are values that are possible in that cell
                                                                                        //for example: possubleValues=010010  -> 2,5 are possible values
            //int count = 0;
            //while (possibleValues!=0)
            //{
            //    count++;
            //    possibleValues &= possibleValues - 1;    // clear most right ON bit
            //}
            //return count;                                                                    
            return possibleValues;
        }
      
        public int CountPossible(int row, int col)
        {
            return CountSetBits(GetPossible(row,col));
        }
        //The function get acell and return its only value if exists, if not returns -1
        public int FindHidden(int row, int col)
        {
            int possible = GetPossible(row, col);
            double powerOF2 = Math.Log(2, possible);
            if (powerOF2 % 1 == 0)
                return (int)powerOF2 + 1;
            return -1;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------
        public int GetRow(int row)
        {
            return rows[row];
        }
        public int GetCol(int col)
        {
            return cols[col];
        }
        public int GetBox(int box)
        {
            return boxes[box];
        }
        public int GetRowBySubIndecies(int boxNum,int cellNumInBox)
        {
            int boxStartRow = subSize * (boxNum / subSize);
            int rowInBox = cellNumInBox / subSize;
            return boxStartRow + rowInBox;
        }
        public int GetColBySubIndecies(int boxNum,int cellNumInBox)
        {
            int boxStartRow = subSize * (boxNum % subSize);
            int rowInBox = cellNumInBox % subSize;
            return boxStartRow + rowInBox;
        }
        //The function get group and return its status: 0: full | 0>: missing 1 cell and returns its value | <0: missing more than 1 cell
        public int GetGroupStatus(int group)
        {
            int full = (int)Math.Pow(2, size) - 1;
            if (group == full)
                return 0;
            int diff = full - group;
            double powerOF2 = Math.Log(2, diff);
            if (powerOF2 % 1 == 0)
                return (int)powerOF2 + 1;
            return -1;
        }

    }
}
