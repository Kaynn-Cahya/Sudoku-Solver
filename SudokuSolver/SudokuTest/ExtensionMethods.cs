using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SudokuTest {
    public static class ExtensionMethods {

        const string newline = "\r\n";

        public static void PrintGrid(this SudokuGrid sudokuGrid) {
            int[,] grid = sudokuGrid.GetGrid();

            Console.ForegroundColor = ConsoleColor.White;
            for (int y = 0; y < 9; ++y) {
                for (int x = 0; x < 9; ++x) {
                    int currValue = grid[x, y];

                    if (currValue == 0) {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    } else {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write($" {currValue} ");

                    if ((x + 1) % 3 == 0) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"|");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.Write(newline);

                if ((y + 1) % 3 == 0) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("------------------------------");
                    Console.Write(newline);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
