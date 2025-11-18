using System;
using System.Reflection;

namespace ObjectPrinting.Configs;

public static class PropertyPrintingConfigExtensions
{
    public static PrintingConfig<TOwner> Trim<TOwner>(
        this PropertyPrintingConfig<TOwner, string> config,
        int maxLen)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxLen);

        var parent = ((IChildPrintingConfig<TOwner, string>)config).ParentConfig;
        
        var path = config.PropertyPath;
        
        parent.Settings.StringTrimLengths[path] = maxLen;

        return parent;
    }
}