using ObjectPrinting.PrintingHandlers.ApplyingSettings.Interfaces;

namespace ObjectPrinting.PrintingHandlers.ApplyingSettings.Appliers;

internal class TrimStringApplier : ISettingsApplier
{
    public ApplierResult Apply(ValueContext context)
    {
        if (context.Type != typeof(string) ||
            context.Value is not string s ||
            !context.Settings.StringTrimLengths.TryGetValue(context.Path, out var max))
            return ApplierResult.NotApplied;

        var trimmed = s.Length <= max ? s : s[..max];
        return ApplierResult.Modified(trimmed);
    }
}