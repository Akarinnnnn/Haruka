using Microsoft.Extensions.DependencyInjection;

namespace Haruka.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ServiceAttribute(ServiceLifetime lifetime) : Attribute
{
	public ServiceLifetime Lifetime { get; } = lifetime;

	public object? Key { get; set; } = null;

	public Type? ServiceType { get; set; } = null;
}