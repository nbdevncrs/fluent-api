using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ObjectPrinting.SettingsAppliers;

namespace ObjectPrinting
{
    internal static class PrintingContext
    {
        public static PrintingNode Build(object? root, PrintingSettings settings)
        {
            var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);

            var rawTree = BuildRawTree(
                root,
                root?.GetType().Name ?? "null",
                root?.GetType().Name ?? "null",
                visited
            );

            ApplySettings(rawTree, settings);
            return rawTree;
        }

        private static PrintingNode BuildRawTree(
            object? value,
            string name,
            string path,
            HashSet<object> visited)
        {
            if (value == null)
                return new PrintingNode(name, path, null, "null") { IsLeaf = true };

            var type = value.GetType();

            if (!type.IsValueType && visited.Contains(value))
                return new PrintingNode(name, path, type, "[Cyclic Reference]")
                {
                    IsLeaf = true,
                    IsCyclic = true
                };

            if (!type.IsValueType)
                visited.Add(value);
            
            if (type.IsPrimitive || value is decimal || value is Guid)
                return Leaf(value, name, path, type);

            if (value is string s)
                return Leaf(s, name, path, type);
            
            if (value is IEnumerable seq)
            {
                var node = new PrintingNode(name, path, type);
                var index = 0;

                foreach (var item in seq)
                {
                    var child = BuildRawTree(
                        item,
                        $"[{index}]",
                        $"{path}[{index}]",
                        visited
                    );
                    node.Children.Add(child);
                    index++;
                }

                return node;
            }
            
            var complex = new PrintingNode(name, path, type);

            foreach (var p in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var childPath = $"{type.Name}.{p.Name}";
                var childValue = p.GetValue(value);

                var child = BuildRawTree(childValue, p.Name, childPath, visited);
                complex.Children.Add(child);
            }

            return complex;
        }

        private static PrintingNode Leaf(object value, string name, string path, Type type) =>
            new(name, path, type, value) { IsLeaf = true };

        private static void ApplySettings(PrintingNode root, PrintingSettings settings)
        {
            var appliers = new ISettingsApplier[]
            {
                new ExcludeApplier(),
                new PropertySerializerApplier(),
                new TypeSerializerApplier(),
                new TrimStringApplier(),
                new CultureApplier()
            };

            foreach (var applier in appliers)
                applier.Apply(root, settings);
        }
    }
}