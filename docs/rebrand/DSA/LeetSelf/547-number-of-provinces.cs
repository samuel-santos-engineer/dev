public class Solution {
    public int FindCircleNum(int[][] isConnected) {
/*
map cities with neighbors => city=>neighbors => map<province,city[]>
merge
    loop until all cities stays in one province
*/
        if (isConnected.Length == 0) return 0;

        Dictionary<int, HashSet<int>> map = MapCities(isConnected);

        Merge(map);

        // Print(map);

        return map.Count;
    }

    private static void Merge(Dictionary<int, HashSet<int>> map)
    {
        Queue<int> provinces = new Queue<int>();
        provinces.Enqueue(map.First().Key);

        while(provinces.Count > 0)
        {
            int province = provinces.Dequeue();

            if (!map.ContainsKey(province)) continue;

            List<int> removeProvinces = new List<int>();
            foreach(var m in map)
            {
                if (m.Key == province) continue;

                HashSet<int> cities = m.Value;

                if (!cities.Contains(province)) continue;

                foreach(int city in cities)
                {
                    map[province].Add(city);
                }

                provinces.Enqueue(province);

                removeProvinces.Add(m.Key);
            }
            foreach(var r in removeProvinces)
                map.Remove(r);
            Dictionary<int, int> counts = new Dictionary<int, int>();
            HashSet<int> exists = new HashSet<int>();
            HashSet<int> dups = new HashSet<int>();
            foreach(var m in map)
            {
                foreach(var neighbor in m.Value)
                {
                    if (dups.Contains(neighbor)) continue;
                    if (exists.Contains(neighbor)) dups.Add(neighbor);
                    else exists.Add(neighbor);
                }
            }
            foreach(var neighbor in dups)
            {
                if (!map.ContainsKey(neighbor))
                {
                    var ms = new HashSet<int>();
                    ms.Add(neighbor);
                    map.Add(neighbor, ms);
                }
                provinces.Enqueue(neighbor);
            }
        }

    }

    static void Print(Dictionary<int, HashSet<int>> map)
    {
        foreach(var m in map)
            Console.WriteLine($"{m.Key}:{string.Join(",", m.Value)}");
    }

    private static Dictionary<int, HashSet<int>> MapCities(int[][] isConnected)
    {
        Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>();

        void AddCity(int city, int neighbor)
        {
            if (!map.TryGetValue(city, out HashSet<int> neighbors))
            {
                neighbors = new HashSet<int>();
                map.Add(city, neighbors);
            }
            neighbors.Add(neighbor);
        }

        for(int i= 0; i < isConnected.Length; i++)
        {
            for(int j=0; j < isConnected[i].Length; j++)
            {
                AddCity(i, i);
                AddCity(j, j);
                if (isConnected[i][j] == 1)
                {
                    AddCity(i, j);
                    AddCity(j, i);
                }
            }
        }

        return map;
    }
}