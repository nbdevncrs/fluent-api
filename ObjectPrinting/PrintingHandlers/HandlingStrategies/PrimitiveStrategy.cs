using System;
using ObjectPrinting.PrintingHandlers.HandlingStrategies.Interfaces;

namespace ObjectPrinting.PrintingHandlers.HandlingStrategies;

internal class PrimitiveStrategy : IStrategy
{
    public bool CanHandle(ValueContext context)
    {
        var type = context.Type;
        if (type == null) return false;
        return type.IsPrimitive || context.Value is decimal || context.Value is Guid;
    }

    public string Print(ValueContext context, Func<ValueContext, string> recurse)
    {
        return context.Value?.ToString() ?? "null";
    }
}