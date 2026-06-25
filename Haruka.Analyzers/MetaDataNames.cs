namespace Haruka.Analyzers;

// D大写方便intellisense
internal static class MetaDataNames
{
	internal const string ServiceAttribute = "Haruka.Attributes.ServiceAttribute";
	internal const string ProviderAttribute = "Haruka.Attributes.ProviderAttribute";
	internal const string ConfigureAttribute = "Haruka.Attributes.ConfigureAttribute";
	internal const string RegisterationHolderAttribute = "Haruka.Attributes.HarukaRegisterationHolderAttribute";

	internal const string MSExtServiceLifetime = "ServiceLifetime";
	internal const string MSExtIServiceCollection = "IServiceCollection";

	internal const string NamespaceMsExtDI = "Microsoft.Extensions.DependencyInjection";
}
