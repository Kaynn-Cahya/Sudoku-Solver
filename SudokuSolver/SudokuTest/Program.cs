using System;

using SudokuSolver;

namespace SudokuTest {
    class Program {
        static void Main(string[] args) {
            int[,] grid = new int[9,9] {
                { 0, 5, 0, 0, 0, 8, 0, 0, 0 },
                { 2, 0, 0, 0, 0, 5, 1, 0, 6 },
                { 0, 0, 8, 2, 0, 0, 5, 0, 0 },
                { 3, 9, 0, 0, 0, 0, 8, 0, 0 },
                { 0, 0, 5, 0, 0, 0, 3, 0, 7 },
                { 0, 0, 0, 0, 0, 0, 0, 6, 1 },
                { 5, 3, 9, 1, 0, 7, 0, 0, 8 },
                { 7, 0, 0, 0, 0, 3, 4, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };

            SudokuGrid sudokuGrid = new SudokuGrid(grid);

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
