using ObjectPrinting.Configs;

namespace ObjectPrinting;

public interface IChildPrintingConfig<TOwner, TPropType>
{
    PrintingConfig<TOwner> ParentConfig { get; }
}