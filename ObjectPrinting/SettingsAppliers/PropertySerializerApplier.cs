namespace ObjectPrinting.SettingsAppliers;

internal class PropertySerializerApplier : ISettingsApplier
{
    public void Apply(PrintingNode root, PrintingSettings settings)
    {
        ApplyRecursive(root, settings);
    }

    private static void ApplyRecursive(PrintingNode node, PrintingSettings settings)
    {
        if (settings.PropertySerializers.TryGetValue(node.Path, out var ser))
        {
            node.Value = (string)ser.DynamicInvoke(node.Value)!;
            node.IsLeaf = true;
            node.Children.Clear();
            return;
        }

        foreach (var child in node.Children)
            ApplyRecursive(child, settings);
    }
}