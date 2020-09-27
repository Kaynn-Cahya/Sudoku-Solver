using System;
using System.Collections.Generic;

namespace SudokuSolver {
    public class SudokuGrid {

        internal class Subgrid { 
            // The ranges this subgrid should cover in the main grid.
            public int MinX { get; private set; }
            public int MaxX { get; private set; }

            public int MinY { get; private set; }
            public int MaxY { get; private set; }

            private SudokuGrid gridRef;

            public Subgrid(SudokuGrid reference, int minX, int maxX, int minY, int maxY) {
                MinX = minX;
                MaxX = maxX;
                MinY = minY;
                MaxY = maxY;

                gridRef = reference;
            }

            public bool IsValid() {
                List<int> values = new List<int>();

                for (int x = MinX; x < MaxX; ++x) {
                    for (int y = MinY; y < MaxY; ++y) {
                        int currValue = gridRef.grid[x, y];

                        if (currValue == 0) {
                            continue;
                        } else if (values.Contains(currValue)) {
                            return false;
                        } else {
                            values.Add(currValue);
                        }
                    }
                }

                return true;
            }

            public bool IsInSubgrid(int X, int Y) {
                return X >= MinX && X < MaxX && Y >= MinY && Y < MaxY;
            }

            /// <summary>
            /// Fetch numbers that is used in this subgrid
            /// </summary>
            public List<int> GetOccupiedNumbers() {
                List<int> occupying = new List<int>();

                for (int x = MinX; x < MaxX; ++x) {
                    for (int y = MinY; y < MaxY; ++y) {
                        var currValue = gridRef.grid[x, y];

                        if (currValue != 0 && !occupying.Contains(currValue)) {
                            occupying.Add(currValue);
                        }
                    }
                }

                return occupying;
            }
        }

        internal int[,] grid = new int[9,9];

        internal HashSet<Subgrid> subgrids;

        public SudokuGrid(int[,] populatedGrid) {
            PopulateGrid(populatedGrid);

            CreateSubgrids();
        }

        public SudokuGrid() {
            ResetGrid();

            CreateSubgrids();
        }

        public int[,] GetGrid() {
            int[,] deepCopy = new int[9, 9];

            for (int x = 0; x < 9; ++x) {
                for (int y = 0; y < 9; ++y) {
                    deepCopy[x, y] = int.Parse(grid[x, y].ToString());
                }
            }

            return deepCopy;
        }

        /// <summary>
        /// Empties the entire grid.
        /// </summary>
        public void ResetGrid() {
            for (int x = 0; x < 9; ++x) {
                for (int y = 0; y < 9; ++y) {
                    grid[x, y] = 0;
                }
            }
        }

        public void PopulateGrid(int[,] populatedGrid) {
            ValidatePopulatedGrid();

            grid = populatedGrid;

            #region Local_Function

            void ValidatePopulatedGrid() {
                for (int x = 0; x < 9; ++x) {
                    for (int y = 0; y < 9; ++y) {
                        int currValue = populatedGrid[x, y];

                        if (currValue > 9 || currValue < 0) {
                            throw new ArgumentOutOfRangeException("populatedGrid", "Values inside the grid can only be 0-9. (0 representing empty)");
                        }
                    }
                }
            }

            #endregion
        }

        private void CreateSubgrids() {
            subgrids = new HashSet<Subgrid>();

            for (int x = 0; x < 3; ++x) {
                for (int y = 0; y < 3; ++y) {
                    int minX = x * 3;
                    int maxX = minX + 3;

                    int minY = y * 3;
                    int maxY = minY + 3;

                    Subgrid currSubgrid = new Subgrid(this, minX, maxX, minY, maxY);
                    subgrids.Add(currSubgrid);
                }
            }
        }

        public bool IsInCorrectState() {
            foreach (var subgrid in subgrids) {
                if (!subgrid.IsValid()) {
                    return false;
                }
            }

            if (RowsHaveDuplicates()) {
                return false;
            } else if (ColumnsHaveDuplicates()) {
                return false;
            }

            return true;

            #region Local_Function

            bool RowsHaveDuplicates() {
                for (int y = 0; y < 9; ++y) {
                    List<int> values = new List<int>();

                    for (int x = 0; x < 9; ++x) {
                        int currValue = grid[x, y];

                        if (currValue == 0) {
                            continue;
                        } else if (values.Contains(currValue)) {
                            return true;
                        } else {
                            values.Add(currValue);
                        }
                    }
                }

                return false;
            }

            bool ColumnsHaveDuplicates() {
                for (int x = 0; x < 9; ++x) {
                    List<int> values = new List<int>();

                    for (int y = 0; y < 9; ++y) {
                        int currValue = grid[x, y];

                        if (currValue == 0) {
                            continue;
                        } else if (values.Contains(currValue)) {
                            return true;
                        } else {
                            values.Add(currValue);
                        }
                    }
                }

                return false;
            }

            #endregion
        }
    }
}
