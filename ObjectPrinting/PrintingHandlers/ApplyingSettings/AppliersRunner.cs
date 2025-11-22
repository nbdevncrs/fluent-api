using ObjectPrinting.PrintingHandlers.ApplyingSettings.Appliers;
using ObjectPrinting.PrintingHandlers.ApplyingSettings.Interfaces;

namespace ObjectPrinting.PrintingHandlers.ApplyingSettings;

internal class ApplierRunner
{
    private readonly ISettingsApplier[] appliers =
    [
        new ExcludeSettingsApplier(),
        new TrimStringSettingsApplier(),
        new SerializerSettingsApplier(),
        new CultureSettingsApplier()
    ];

    public ApplierResult Run(ValueContext context)
    {
        var currentValue = context.Value;
        var anyApplied = false;

        foreach (var applier in appliers)
        {
            var localContext = new ValueContext
            {
                Value = currentValue,
                Type = currentValue?.GetType() ?? context.Type,
                Path = context.Path,
                Indent = context.Indent,
                Settings = context.Settings,
                Visited = context.Visited
            };

            var res = applier.Apply(localContext);
            if (!res.Applied)
                continue;

            anyApplied = true;

            if (res.Exclude)
                return ApplierResult.Excluded();

            if (res.Serialized != null)
                return ApplierResult.SerializedValue(res.Serialized);

            if (res.NewValue != null)
                currentValue = res.NewValue;
        }

        return !anyApplied ? ApplierResult.NotApplied : ApplierResult.Modified(currentValue);
    }
}