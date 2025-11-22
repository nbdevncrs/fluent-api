using System;
using System.Collections;
using System.Text;
using ObjectPrinting.PrintingHandlers.HandlingStrategies.Interfaces;

namespace ObjectPrinting.PrintingHandlers.HandlingStrategies
{
    internal class EnumerableStrategy : IStrategy
    {
        public bool CanHandle(ValueContext context)
        {
            return context.Value switch
            {
                null or string => false,
                _ => context.Value is IEnumerable
            };
        }

        public string Print(ValueContext context, Func<ValueContext, string> recurse)
        {
            var sequence = (IEnumerable)context.Value!;
            var type = context.Type ?? sequence.GetType();
            var sb = new StringBuilder();
            sb.AppendLine($"{type.Name} [");

            var i = 0;
            foreach (var item in sequence)
            {
                var childContext = new ValueContext
                {
                    Value = item,
                    Type = item?.GetType(),
                    Path = $"{context.Path}[{i}]",
                    Indent = context.Indent,
                    Settings = context.Settings,
                    Visited = context.Visited
                };

                var printed = recurse(childContext);
                var prefix = new string('\t', context.Indent + 1);

                if (printed == "")
                {
                    sb.Append(prefix).Append($"[{i}] = ").AppendLine("null");
                }
                else if (printed.Contains(Environment.NewLine))
                {
                    sb.Append(prefix).Append($"[{i}] = ").AppendLine();
                    var lines = printed.Split([Environment.NewLine], StringSplitOptions.None);
                    foreach (var line in lines)
                        sb.Append(prefix).Append('\t').AppendLine(line);
                }
                else
                {
                    sb.Append(prefix).Append($"[{i}] = ").AppendLine(printed);
                }

                i++;
            }

            sb.Append(new string('\t', context.Indent)).Append("]");
            return sb.ToString();
        }
    }
}