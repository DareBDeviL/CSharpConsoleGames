﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    internal class Food : Field
    {
        private Coordinates foodCords;
        private char symbol;
       

        public Food()
        {
            this.symbol = '@';
            this.foodCords = new Coordinates();
           // int row = this.test;
        }

        public char Symbol { get => this.symbol; }
        public Coordinates FoodCords { get => foodCords; }

        public void FoodGenerator(Queue<Coordinates> snakeElements)
        {
            Random generator = new Random();
            foodCords.Row = generator.Next(this.InfoWindow + 2, this.consoleRow - 1);
            foodCords.Col = generator.Next(0, this.ConsoleCol - 2);

            if (snakeElements.Any(s => s.Row == this.foodCords.Row && s.Col == this.foodCords.Col))
            {
                FoodGenerator(snakeElements);
            }
        }
    }
}