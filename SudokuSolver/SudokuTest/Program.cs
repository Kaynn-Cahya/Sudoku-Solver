using System;
using System.Runtime.CompilerServices;

using SudokuSolver;

namespace SudokuTest {
    class Program {
        static void Main(string[] args) {
            // TODO: Test cases

            SudokuGrid sudokuGrid = new SudokuGrid();

            sudokuGrid.PrintGrid();

            Console.WriteLine("\r\nSolving...\r\n");

            Solver.SolveSudoku(sudokuGrid);

            sudokuGrid.PrintGrid();

            if (sudokuGrid.IsInCorrectState()) {
                Console.WriteLine("\r\nCorrect!");
            } else {
                Console.WriteLine("\r\nWrong!");
            }

            Console.WriteLine("\r\nPress any key to quit...");
            Console.ReadKey();
        }

    }
}
