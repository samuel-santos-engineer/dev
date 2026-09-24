/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    public int Rob(TreeNode root) {

        var mem = new Dictionary<(TreeNode, bool), int>();
        
        int maxSum = Math.Max(
            CheckHouse(root, true, mem),
            CheckHouse(root, false, mem)
        );
        return maxSum;
    } 

    static int CheckHouse(TreeNode n, bool onNode, Dictionary<(TreeNode, bool), int> mem)
    {
        if (n == null) return 0;

        var memKey = (n, onNode);

        if (mem.TryGetValue(memKey, out int maxSum)) return maxSum;

        if (onNode)
        {
            maxSum =
                n.val +
                CheckHouse(n.left, false, mem) +
                CheckHouse(n.right, false, mem);
        }
        else
        {
            int left = Math.Max(CheckHouse(n.left, false, mem), CheckHouse(n.left, true, mem));
            
            int right = Math.Max(CheckHouse(n.right, false, mem), CheckHouse(n.right, true, mem));

            maxSum = left + right;
        }

        mem.Add(memKey, maxSum);
        
        return maxSum;
    }
}