using ObjectPrinting.PrintingHandlers.ApplyingSettings.Interfaces;

namespace ObjectPrinting.PrintingHandlers.ApplyingSettings.Appliers;

internal class TrimStringApplier : ISettingsApplier
{
    public ApplierResult Apply(ValueContext context)
    {
        if (context.Type != typeof(string) || context.Value is not string s)
            return ApplierResult.NotApplied;

        var settings = context.Settings;
        
        if (settings.StringTrimLengths.TryGetValue(context.Path, out var propertyMax))
        {
            var trimmed = s.Length <= propertyMax ? s : s[..propertyMax];
            return ApplierResult.Modified(trimmed);
        }
        
        if (settings.GlobalStringTrimLength > 0)
        {
            var max = settings.GlobalStringTrimLength;
            var trimmed = s.Length <= max ? s : s[..max];
            return ApplierResult.Modified(trimmed);
        }
        
        return ApplierResult.NotApplied;
    }
}