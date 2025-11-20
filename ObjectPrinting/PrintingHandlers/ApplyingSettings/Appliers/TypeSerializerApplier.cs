using ObjectPrinting.PrintingHandlers.ApplyingSettings.Interfaces;

namespace ObjectPrinting.PrintingHandlers.ApplyingSettings.Appliers;

internal class TypeSerializerApplier : ISettingsApplier
{
    public ApplierResult Apply(ValueContext context)
    {
        if (context.Type == null ||
            !context.Settings.TypeSerializers.TryGetValue(context.Type, out var serializer))
            return ApplierResult.NotApplied;

        return ApplierResult.SerializedValue(serializer(context.Value));
    }
}