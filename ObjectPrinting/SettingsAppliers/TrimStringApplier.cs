namespace ObjectPrinting.SettingsAppliers;

internal class TrimStringApplier : ISettingsApplier
{
    public void Apply(PrintingNode root, PrintingSettings settings)
    {
        ApplyRecursive(root, settings);
    }

    private static void ApplyRecursive(PrintingNode node, PrintingSettings settings)
    {
        if (node.Type == typeof(string)
            && node.Value is string s
            && settings.StringTrimLengths.TryGetValue(node.Path, out var max))
        {
            node.Value = s.Length <= max ? s : s[..max];
            node.IsLeaf = true;
            node.Children.Clear();
            return;
        }

        foreach (var child in node.Children)
            ApplyRecursive(child, settings);
    }
}