namespace ObjectPrinting.SettingsAppliers;

internal interface ISettingsApplier
{
    void Apply(PrintingNode root, PrintingSettings settings);
}