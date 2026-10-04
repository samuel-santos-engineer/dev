public class Solution {
    public int[] TopKFrequent(int[] nums, int k) {
        var counter = new Dictionary<int, int>();
        foreach(int n in nums)
        {
            if (!counter.TryGetValue(n, out int q))
            {
                counter.Add(n, 1);
            }
            else counter[n]++;
        }
        return counter.OrderByDescending(x=>x.Value).Take(k).Select(x=>x.Key).ToArray();
    }
}