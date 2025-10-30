using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'swapNodes' function below.
     *
     * The function is expected to return a 2D_INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. 2D_INTEGER_ARRAY indexes
     *  2. INTEGER_ARRAY queries
     */

    public static List<List<int>> swapNodes(List<List<int>> indexes, List<int> queries)
    {
         // Build the tree
        TreeNode root = new TreeNode(1);
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        
        int index = 0;
        while (queue.Count > 0 && index < indexes.Count) {
            TreeNode current = queue.Dequeue();
            
            int leftVal = indexes[index][0];
            int rightVal = indexes[index][1];
            
            if (leftVal != -1) {
                current.Left = new TreeNode(leftVal);
                queue.Enqueue(current.Left);
            }
            
            if (rightVal != -1) {
                current.Right = new TreeNode(rightVal);
                queue.Enqueue(current.Right);
            }
            
            index++;
        }
        
        List<List<int>> result = new List<List<int>>();
        
        // Process each query
        foreach (int k in queries) {
            SwapAtDepth(root, k, 1);
            List<int> inorder = new List<int>();
            InorderTraversal(root, inorder);
            result.Add(inorder);
        }
        
        return result;
    }
    
    private static void SwapAtDepth(TreeNode node, int k, int depth) {
        if (node == null) return;
        
        if (depth % k == 0) {
            TreeNode temp = node.Left;
            node.Left = node.Right;
            node.Right = temp;
        }
        
        SwapAtDepth(node.Left, k, depth + 1);
        SwapAtDepth(node.Right, k, depth + 1);
    }
    
    private static void InorderTraversal(TreeNode node, List<int> result) {
        if (node == null) return;
        
        InorderTraversal(node.Left, result);
        result.Add(node.Value);
        InorderTraversal(node.Right, result);
    }
    
    private class TreeNode {
        public int Value { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        
        public TreeNode(int value) {
            Value = value;
        }
    }

    }

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<int>> indexes = new List<List<int>>();

        for (int i = 0; i < n; i++)
        {
            indexes.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(indexesTemp => Convert.ToInt32(indexesTemp)).ToList());
        }

        int queriesCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> queries = new List<int>();

        for (int i = 0; i < queriesCount; i++)
        {
            int queriesItem = Convert.ToInt32(Console.ReadLine().Trim());
            queries.Add(queriesItem);
        }

        List<List<int>> result = Result.swapNodes(indexes, queries);

        textWriter.WriteLine(String.Join("\n", result.Select(x => String.Join(" ", x))));

        textWriter.Flush();
        textWriter.Close();
    }
}
