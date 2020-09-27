using System;
using System.Collections.Generic;

namespace SudokuSolver {
    public static class Solver {

        /// <summary>
        /// Manipulate the given reference of sudokugrid to solve it.
        /// </summary>
        /// <param name="sudokuGrid"></param>
        public static void SolveSudoku(SudokuGrid sudokuGrid) {
            if (!sudokuGrid.IsInCorrectState()) {
                throw new ArgumentException("sudokuGrid", "Ensure that the sudoku grid is in a valid state first!");
            }

            bool solve = RecursiveSolver(sudokuGrid, 0, 0);

            if (!solve) {
                throw new InvalidOperationException("Failed to solve this sudoku board!");
            }
        }

        private static bool RecursiveSolver(SudokuGrid sudokuGrid, int x, int y) {
            // Reached the end of the entire grid.
            if (y > 8) {
                return true;
            }

            bool endOfColumn = (x == 8);

            int nextY = endOfColumn ? (y + 1) : y;
            int nextX = endOfColumn ? 0 : (x + 1);

            if (sudokuGrid.grid[x, y] != 0) {
                return RecursiveSolver(sudokuGrid, nextX, nextY);
            }

            List<int> usableNumbers = GetUsableNumbers(sudokuGrid, x, y);
            foreach (var num in usableNumbers) {
                sudokuGrid.grid[x, y] = num;

                bool solveResult = RecursiveSolver(sudokuGrid, nextX, nextY);
                if (solveResult) {
                    return true;
                }

                sudokuGrid.grid[x, y] = 0;
            }

            return false;
        }

        /// <summary>
        /// Fetch a list of numbers we can use for this current tile.
        /// </summary>
        private static List<int> GetUsableNumbers(SudokuGrid sudokuGrid, int currX, int currY) {
            List<int> usable = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            VerifyByRow();

            VerifyByColumn();

            VerifyUsableValuesInSubgrid();

            return usable;

            #region Local_Function

            void VerifyByRow() {
                for (int x = 0; x < 9; ++x) {
                    usable.Remove(sudokuGrid.grid[x, currY]);
                }
            }

            void VerifyByColumn() {
                for (int y = 0; y < 9; ++y) {
                    usable.Remove(sudokuGrid.grid[currX, y]);
                }
            }

            void VerifyUsableValuesInSubgrid() {
                SudokuGrid.Subgrid subGrid = null;
                foreach (var sub in sudokuGrid.subgrids) {
                    if (sub.IsInSubgrid(currX, currY)) {
                        subGrid = sub;
                        break;
                    }
                }

                foreach (var usedValues in subGrid.GetOccupiedNumbers()) {
                    usable.Remove(usedValues);
                }
            }

            #endregion
        }

    }
}
