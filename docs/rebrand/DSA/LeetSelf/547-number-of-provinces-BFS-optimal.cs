public class Solution {
    /*
0:0,1
1:0,1
2:2
bfs + visited

i=city, j=neighbor
loop in city and mark neighbors as visited
    */
    public int FindCircleNum(int[][] isConnected) {

        int n = isConnected.Length;
        if (n == 0) return 0;

        int provinces = 0;

        var visited = new bool[n];

        for(int i=0; i < isConnected.Length; i++)
        {
            if (visited[i]) continue;

            provinces++;
            var queue = new Queue<int>();
            queue.Enqueue(i);

            while(queue.Count > 0)
            {
                var city = queue.Dequeue();
                for(int neighbor=0; neighbor < isConnected[city].Length; neighbor++)
                {
                    if (isConnected[city][neighbor] == 1 && !visited[neighbor])
                    {
                        visited[neighbor] = true;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return provinces;        
    }
}