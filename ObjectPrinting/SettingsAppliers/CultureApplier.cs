using System;

namespace ObjectPrinting.SettingsAppliers;

internal class CultureApplier : ISettingsApplier
{
    public void Apply(PrintingNode root, PrintingSettings settings)
    {
        ApplyRecursive(root, settings);
    }

    private static void ApplyRecursive(PrintingNode node, PrintingSettings settings)
    {
        if (node.Type != null && settings.TypeCultures.TryGetValue(node.Type, out var culture))
        {
            if (node.Value is IFormattable f)
            {
                node.Value = f.ToString(null, culture);
                node.IsLeaf = true;
                node.Children.Clear();
                return;
            }
        }

        foreach (var child in node.Children)
            ApplyRecursive(child, settings);
    }
}