using System;
using System.Collections.Generic;

namespace ObjectPrinting;

public class PrintingNode(string name, string path, Type? type, object? value = null)
{
    public string Name { get; } = name;
    public string Path { get; } = path;
    public Type? Type { get; } = type;
    public object? Value { get; set; } = value;
    public bool IsLeaf { get; set; }
    public bool IsCyclic { get; set; }
    public List<PrintingNode> Children { get; } = [];
}