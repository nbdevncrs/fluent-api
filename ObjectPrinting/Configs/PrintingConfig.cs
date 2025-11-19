using System;
using System.Linq.Expressions;

namespace ObjectPrinting.Configs;

public class PrintingConfig<TOwner>(PrintingSettings settings)
{
    public readonly PrintingSettings Settings = settings;

    public TypePrintingConfig<TOwner, TPropType> For<TPropType>()
    {
        return new TypePrintingConfig<TOwner, TPropType>(this);
    }

    public PropertyPrintingConfig<TOwner, TPropType> For<TPropType>(Expression<Func<TOwner, TPropType>> selector)
    {
        return new PropertyPrintingConfig<TOwner, TPropType>(this, selector);
    }

    public string PrintToString(TOwner obj)
    {
        var context = PrintingContext.Build(obj, Settings);
        return PrintingFormatter.Format(context);
    }
}