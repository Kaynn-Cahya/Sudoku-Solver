using System;
using System.Runtime.CompilerServices;

using SudokuSolver;

namespace SudokuTest {
    class Program {
        static void Main(string[] args) {
            SudokuGrid sudokuGrid = new SudokuGrid();

            sudokuGrid.PrintGrid();

            Console.WriteLine("\r\nPress any key to quit...");
            Console.ReadKey();
        }

    }
}
