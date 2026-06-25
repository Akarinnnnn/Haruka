namespace Haruka.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ConfigurerAttribute() : Attribute
{
	public bool GenerateInternalRegisterMethod { get; set; } = false;

	public bool DisableAutomicRegisteration { get; set; } = false;
}