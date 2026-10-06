public class Solution {
    public int[] SortedSquares(int[] nums) {
        int[] rs = new int[nums.Length];

        int L = 0;
        int R = nums.Length - 1;

        for(int i=nums.Length-1; i >= 0; i--)
        {
            if (Math.Abs(nums[L]) > Math.Abs(nums[R]))
            {
                rs[i] = nums[L] * nums[L];
                L++;
            }
            else
            {
                rs[i] = nums[R] * nums[R];
                R--;
            }
        }

        return rs;        
    }
}