﻿namespace AdventOfCode2022.day8;

internal class Graph
{
    public IReadOnlyList<TreeNode> Nodes { get; }
    
    public IReadOnlyDictionary<TreeNode, IReadOnlyDictionary<Direction, EdgeNode>> EdgeNodes { get; }

    public Graph(
        IReadOnlyList<TreeNode> nodes,
        IReadOnlyDictionary<TreeNode, IReadOnlyDictionary<Direction, EdgeNode>> edgeNodes)
    {
        Nodes = nodes;
        EdgeNodes = edgeNodes;
    }
}
