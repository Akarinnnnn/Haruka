using Microsoft.Extensions.DependencyInjection;

namespace Haruka.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class ProviderAttribute(ServiceLifetime lifetime) : Attribute
{
	public ServiceLifetime Lifetime { get; } = lifetime;

	public object? Key { get; set; } = null;

	public Type? ServiceType { get; set; } = null;
}
