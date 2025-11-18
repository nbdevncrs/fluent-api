using System;
using System.Collections.Generic;

namespace ObjectPrinting;

public class PrintingSettings
{
    public HashSet<Type> ExcludedTypes { get; } = [];
    public HashSet<string> ExcludedProperties { get; } = [];
    public Dictionary<Type, Delegate> TypeSerializers { get; } = new();
    public Dictionary<string, Delegate> PropertySerializers { get; } = new();
    public Dictionary<Type, IFormatProvider> TypeCultures { get; } = new();
    public Dictionary<string, int> StringTrimLengths { get; } = new();
}