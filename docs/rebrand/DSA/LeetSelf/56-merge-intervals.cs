public class Solution {
    public int[][] Merge(int[][] intervals) {
        /*
[start,end]
sort ascending by start
iterate extending the end or adding interval
return mergedList
        */

        intervals.Sort((a,b)=>a[0]-b[0]);

        var mergedList = new List<int[]>(){ intervals[0] };

        for(int i=1; i < intervals.Length; i++)
        {
            var prev = mergedList[mergedList.Count-1];
            var curr = intervals[i];
            if (curr[0] > prev[1]) // new interval
            {
                mergedList.Add(curr);
            }
            else if (curr[1] > prev[1]) // extend interval
            {
                prev[1] = curr[1];
            }
        }

        //foreach(var x in mergedList)
          //  Console.WriteLine($"{x[0]}:{x[1]}");
            
        return mergedList.ToArray();
        
    }
}