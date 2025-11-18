namespace ObjectPrinting.SettingsAppliers;

internal class ExcludeApplier : ISettingsApplier
{
    public void Apply(PrintingNode root, PrintingSettings settings)
    {
        ApplyRecursive(root, settings);
    }

    private static void ApplyRecursive(PrintingNode node, PrintingSettings settings)
    {
        node.Children.RemoveAll(c => settings.ExcludedProperties.Contains(c.Path)
                                     || (c.Type != null && settings.ExcludedTypes.Contains(c.Type)));

        foreach (var child in node.Children)
            ApplyRecursive(child, settings);
    }
}