public class TimeMap {

    Dictionary<string, List<(int Timestamp, string Value)>> cache = null;

    public TimeMap() {
        this.cache = new Dictionary<string, List<(int, string)>>();        
    }
    
    public void Set(string key, string value, int timestamp) {
        if (!cache.TryGetValue(key, out List<(int, string)> values))
        {
            values = new List<(int, string)>();
            cache.Add(key, values);
        }
        values.Add((timestamp, value));
    }
    
    public string Get(string key, int timestamp) {        
        if (!cache.TryGetValue(key, out List<(int Timestamp, string Value)> values))
        {
            return string.Empty;
        }

        string result = string.Empty;

        int L = 0;
        int R = values.Count - 1;

        while(L <= R)
        {
            int M = L + (R - L) / 2;
            var v = values[M];
            if (v.Timestamp==timestamp) return v.Value;
            if (timestamp > v.Timestamp) 
            {
                result = v.Value;
                L = M + 1;
            }
            else 
            {
                R = M - 1;
            }
        }

        return result;
    }
}

/**
 * Your TimeMap object will be instantiated and called as such:
 * TimeMap obj = new TimeMap();
 * obj.Set(key,value,timestamp);
 * string param_2 = obj.Get(key,timestamp);
 */