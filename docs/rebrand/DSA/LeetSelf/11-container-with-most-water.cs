public class Solution {
    public int MaxArea(int[] height) {
        int L=0;
        int R=height.Length-1;

        int max = 0;

        while(L < R)
        {
            int hL = height[L];
            int hR = height[R];
            int A = (R-L) * Math.Min(hL, hR);
            max = Math.Max(A, max);
            if (hR >= hL) L++;
            else R--;
        }

        return max;
    }
}