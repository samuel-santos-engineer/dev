public class Solution {
    public int FindCircleNum(int[][] isConnected) {
        /*
        adjacency list cities:dictionary[src:int,dest:HashSet<int>]

        provinces=list<cities:HashSet>
        iterate cities
        foreach city in cities
        if provinces is empty => province.add(cities[city]) #AddCitiesToProvince
        else 
            foreach province in provinces
                if city in province then 
                    province.add(cities[city]) #AddCitiesToProvince
                    city found
                    break
                end if
            end foreach
            if city not found then
                province.add(cities[city]) #AddCitiesToProvince
            end if
        end else

        #AddCitiesToProvince(province, city)
        connCities:HashSet<int>
        GetConnectedCities(city,connCities)
        province.add(connectedCities)

        #GetConnectedCities(city,connCities)
        connCities.Add(city)
        foreach c in cities[city].values
            GetConnectedCities(c,connCities)
        endfor

        */

        List<HashSet<int>> provinces = new List<HashSet<int>>();

        Dictionary<int, HashSet<int>> cities = BuildCities(isConnected);

        foreach(var city in cities.Keys)
        {
            // Console.WriteLine($"{city}:{string.Join(",", cities[city])}");

            bool found = false;
            if (provinces.Count > 0)
            {
                HashSet<int> provinceFound = null;
                foreach(var province in provinces)
                {
                    if (ProvinceContainsCity(cities, province, city))
                    {
                        AddCitiesToProvince(cities, province, city);
                        provinceFound = province;
                        found = true;
                        break;
                    }
                    
                }
                if (provinceFound != null && provinces.Count > 1)
                    MergeProvinces(provinces, provinceFound);
            }
            if (!found)
            {
                var province = new HashSet<int>();
                provinces.Add(province);
                AddCitiesToProvince(cities, province, city);
            }
        }

        /*
        foreach(var p in provinces)
        {
            Console.WriteLine($"{string.Join(",", p)}");
        }
        */

        return provinces.Count;
    }

    static void MergeProvinces(List<HashSet<int>> provinces, HashSet<int> provinceFound)
    {
        HashSet<int> provinceDest = null;

        foreach(var p in provinces)
        {
            if (p == provinceFound) continue;

            foreach(var f in provinceFound)
            {
                if (p.Contains(f))
                {
                    provinceDest = p;
                    break;
                }
            }
            if (provinceDest != null) break;
        }
        if (provinceDest != null)
        {
            foreach(var city in provinceFound)
            {
                provinceDest.Add(city);
            }
            provinces.Remove(provinceFound);
        }
    }

    static bool ProvinceContainsCity(Dictionary<int, HashSet<int>> cities, HashSet<int> province, int city)
    {
        if (cities.TryGetValue(city, out HashSet<int> dst))
        {
            foreach(int d in dst)
            {
                if (province.Contains(d))
                {
                    return true;
                }
            }
        }
        return false;
    }

    static void AddCitiesToProvince(Dictionary<int, HashSet<int>> cities, HashSet<int> province, int city)
    {
        var connCities = GetConnectedCities(cities, city);
        foreach(var c in connCities)
        {
            province.Add(c);
        }

    }

    static HashSet<int> GetConnectedCities(Dictionary<int, HashSet<int>> cities, int city)
    {
        HashSet<int> connCities = new HashSet<int>();
        connCities.Add(city);
        if (cities.TryGetValue(city, out HashSet<int> dst))
        {
            foreach(var d in dst)
            {
                connCities.Add(d);
            }
        }
        return connCities;
    }

    static Dictionary<int, HashSet<int>> BuildCities(int[][] isConnected)
    {
        Dictionary<int, HashSet<int>> cities = new Dictionary<int, HashSet<int>>();

        int rows = isConnected.Length;
        if (rows == 0) return cities;
        int cols = isConnected[0].Length;
        for(int i = 0; i < rows; i++)
            for(int j = 0; j < cols; j++)
            {
                if (isConnected[i][j] == 1)
                {
                    if (!cities.ContainsKey(i)) cities.Add(i, new HashSet<int>()); //src
                    if (!cities.ContainsKey(j)) cities.Add(j, new HashSet<int>()); //dst
                    cities[i].Add(j); // src => dst
                }
            }
        return cities;
    }
}