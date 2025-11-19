namespace ObjectPrinting.SettingsAppliers;

internal class TypeSerializerApplier : ISettingsApplier
{
    public void Apply(PrintingNode root, PrintingSettings settings)
    {
        ApplyRecursive(root, settings);
    }

    private static void ApplyRecursive(PrintingNode node, PrintingSettings settings)
    {
        if (node.Type != null && settings.TypeSerializers.TryGetValue(node.Type, out var ser))
        {
            node.Value = ser(node.Value);
            node.IsLeaf = true;
            node.Children.Clear();
            return;
        }

        foreach (var child in node.Children)
            ApplyRecursive(child, settings);
    }
}