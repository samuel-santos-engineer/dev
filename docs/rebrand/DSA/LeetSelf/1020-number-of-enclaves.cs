public class Solution {
    public int NumEnclaves(int[][] grid) {
        /*
walk all directions
stick on ones => land
if land touches boundary desconsider
return number of steps
control visits to avoid inifinite loop
        */
        int rows = grid.Length;
        if (rows == 0) return 0;

        int cols = grid[0].Length;

        int landCells = 0;

        var visited = new HashSet<(int,int)>();

        for(int row = 0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                var (localLand, localTouched) = Walk(grid, row, col, rows, cols, visited);
                landCells += localLand;
            }
        }

        return landCells;
    }

    private static (int,bool) Walk(int[][] grid, int row, int col, int rows, int cols, HashSet<(int,int)> visited)
    {
        if (row < 0 || row == rows) return (0, false);
        if (col < 0 || col == cols) return (0, false);

        bool isLand = grid[row][col] == 1;
        if (!isLand) return (0, false);

        var key = (row, col);

        if (visited.Contains(key)) return (0, false);

        visited.Add(key);

        int lands = 1;
        bool touched = row == 0 || row == rows-1 || col == 0 || col == cols-1;

        //navigate in four directions
        var dirs = new List<(int,int)>()
        {
            (0, 1), //right
            (0, -1), //left
            (-1, 0), //up
            (1, 0), //down
        };

        foreach(var dir in dirs)
        {
            var (vert, horz) = dir;
            
            var (localLands, localTouched) = Walk(grid, vert+row, horz+col, rows, cols, visited);
            if (localTouched) touched = true;
            else lands += localLands;
        }

        if (touched) lands = 0;

        return (lands,touched);
    }
}