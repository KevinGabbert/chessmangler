//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Board
//{
//    public class Square                   
//    {
//        public Square(int x, int y)
//        {
//            int[] position = new int[2] { x, y };
//        }
//        public int black
//        {
//            get
//            { return black; }
//            set
//            { black = value; }
//        }
//        public int white
//        {
//            get
//            { return white; }
//            set
//            { white = value; }
//        }
//        public string name
//        {
//            get
//            { return name; }
//            set
//            { this.name = value; }
//        }
//        public int number
//        {
//            get
//            { return this.number; }
//            set
//            { this.number = value; }
//        }
//        public int color
//        {
//            get
//            { return this.color; }
//            set
//            { this.color = value; }
//        }
//        public int piece
//        {
//            get
//            { return this.piece; }
//            set
//            { piece = value; }
//        }        
//    }

//    public class board
//    {
//        board(int x, int y)
//        {
//            // constructor takes two ints, makes the board
//            Square[,] board = new Square[x, y];

//            //initialize all squares created

//            for (int row = 0; row == x; row++)
//            {
//                for (int column = 0; column == y; column++)
//                {
//                    board[row, column] = new Square(row,column);
//                    board[row, column].piece = 0;
//                    board[row, column].black = 0;
//                    board[row, column].white = 0;
//                    board[row, column].number = (x*y)+x;
//                    board[row, column].name = (char)(97+row)+column.ToString();
                    
//                    if (((board[row, column].number)%2)==0)
//                    {board[row, column].color = 1;}
//                    else
//                    {board[row, column].color = 0;}
//                }
//            }           


//        }
//    }     
//}
