using System;
using ObjectPrinting.PrintingHandlers.ApplyingSettings.Interfaces;

namespace ObjectPrinting.PrintingHandlers.ApplyingSettings.Appliers;

internal class CultureSettingsApplier : ISettingsApplier
{
    public ApplierResult Apply(ValueContext context)
    {
        if (context.Type == null ||
            !context.Settings.TypeCultures.TryGetValue(context.Type, out var culture) ||
            context.Value is not IFormattable f)
            return ApplierResult.NotApplied;

        var str = f.ToString(null, culture);
        return ApplierResult.SerializedValue(str);
    }
}