﻿using System;
using System.Text;

namespace Snake
{
    internal class Field 
    {
       private readonly  int consoleRow;
       private readonly  int consoleCol;
       private readonly  int infoWindow;

        public Field(Coordinates fieldSize)
        {
            this.infoWindow = 2;                                      // Two Rows by default
            this.consoleRow = 1 + infoWindow + 1 + fieldSize.Row + 1; // One is borders size.
            this.consoleCol = 1 + fieldSize.Col + 1;

            SetConsoleSettings();
        }
       
        public  int ConsoleRow { get => consoleRow; }
        public  int ConsoleCol { get => consoleCol; }
        public  int InfoWindow { get => infoWindow; }

        private void SetConsoleSettings()
        {
            Console.CursorVisible = false;
            Console.Title = "Snake v1.2";
            Console.WindowHeight = consoleRow;
            Console.WindowWidth = consoleCol;
            Console.BufferHeight = consoleRow;
            Console.BufferWidth = consoleCol;
            Console.OutputEncoding = Encoding.UTF8;
        }
    }
}
