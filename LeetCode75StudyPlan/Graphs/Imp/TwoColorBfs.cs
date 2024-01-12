﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode75StudyPlan.Graphs.Imp;

internal class TwoColorBfs : Breadth1stSearcher
{
    public enum NodeColor
    {
        UNCOLORED = 0,
        BLACK,
        WHITE
    }

    private NodeColor[] _colors;
    public bool Bipartite { get; private set; }
    public TwoColorBfs(IGraph<int> graph) : base(graph)
    {
        _colors = new NodeColor[graph.VertexCount];            
        Run();
    }

    protected override void Run()
    {
        InitSeach();
        Bipartite = true;

        for (int i = 0; i < _graph.VertexCount; i++)
        {
            if (!_discovared[i])
            {
                _colors[i] = NodeColor.WHITE;
                BfsFrom(i);
            }
        }
    }

    protected override void ProcessEdge(int v, int y)
    {
        if (_colors[v] == _colors[y])
            Bipartite = false;

        _colors[y] = _colors[v] switch
        {
            NodeColor.WHITE => NodeColor.BLACK,
            NodeColor.BLACK => NodeColor.WHITE,
            _ => NodeColor.UNCOLORED
        };
    }

    public NodeColor[] Colors => _colors.ToArray();
}
