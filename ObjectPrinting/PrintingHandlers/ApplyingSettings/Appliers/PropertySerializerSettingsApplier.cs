using ObjectPrinting.PrintingHandlers.ApplyingSettings.Interfaces;

namespace ObjectPrinting.PrintingHandlers.ApplyingSettings.Appliers;

internal class PropertySerializerSettingsApplier : ISettingsApplier
{
    public ApplierResult Apply(ValueContext context)
    {
        return !context.Settings.PropertySerializers.TryGetValue(context.Path, out var serializer)
            ? ApplierResult.NotApplied
            : ApplierResult.SerializedValue(serializer(context.Value));
    }
}