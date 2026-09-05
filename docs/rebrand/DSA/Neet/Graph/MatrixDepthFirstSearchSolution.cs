// Matrix Depth-First Search
public class MatrixDepthFirstSearchSolution {
    public int CountPaths(int[][] grid) {

        int q = DepthFirstSearch(grid, 0, 0, new HashSet<string>());

        return q;
    }

    static int DepthFirstSearch(int[][] grid, int row, int col, HashSet<string> visited)
    {
        int rows = grid.Length;
        if (rows == 0) return 0;

        int cols = grid[0].Length;
        if (row < 0 || row >= rows) return 0;
        if (col < 0 || col >= cols) return 0;

        if (grid[row][col] == 1) return 0;

        string k = string.Concat(row, "-", col);
        if (visited.Contains(k)) return 0;

        if (row == rows-1 && col == cols-1) return 1; // bottom right

        visited.Add(k);

        int q = 0;

        q += DepthFirstSearch(grid, row + 1, col, visited);
        q += DepthFirstSearch(grid, row - 1, col, visited);
        q += DepthFirstSearch(grid, row, col + 1, visited);
        q += DepthFirstSearch(grid, row, col - 1, visited);

        visited.Remove(k); // backtracking

        return q;
    }
}
