using System;
using ObjectPrinting.Configs.Interfaces;

namespace ObjectPrinting.Configs.Extensions;

public static class TypePrintingConfigExtensions
{
    public static PrintingConfig<TOwner> Trim<TOwner>(
        this TypePrintingConfig<TOwner, string> config,
        int maxLen)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxLen);

        var parent = ((IChildPrintingConfig<TOwner, string>)config).ParentConfig;
        
        parent.Settings.GlobalStringTrimLength = maxLen;

        return parent;
    }
}