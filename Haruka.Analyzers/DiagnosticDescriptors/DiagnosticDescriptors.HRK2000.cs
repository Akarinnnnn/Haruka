namespace Haruka.Analyzers;

// Service类本身相关的诊断
internal static partial class HarukaDiagnosticDescriptors
{
	internal static readonly DiagnosticDescriptor HARUKA2000 = new(
		"HARUKA2000", "“ServiceAttribute”特性不符合生成器要求",
		"服务类“{0}”上应用的“Haruka.Attributes.ServiceAttribute”不符合Haruka依赖注入源码生成器的要求",
		"Haruka DI Services", DiagnosticSeverity.Warning, true,
#pragma warning disable RS1033 // 正确定义诊断说明
		description: "Haruka依赖注入源码生成器使用一组特性来定位服务注册代码生成目标，并通过类型名称+命名空间匹配这些特性，" +
		"匹配特性时不考虑程序集的因素。" +
		"如果有意在不引入Haruka特性库的前提下生成源码，请复制生成器对应版本的特性源代码并引入到本地。");
#pragma warning restore RS1033 // 中文标点也是标点

	internal static readonly DiagnosticDescriptor HARUKA2001 = new(
		"HARUKA2001", "静态类无法注册为服务", "静态类“{0}”无法注册为ServiceProvider容器中的服务",
		"Haruka DI Services", DiagnosticSeverity.Error, true);

	internal static readonly DiagnosticDescriptor HARUKA2002 = new(
		"HARUKA2002", "未支持单服务多接口模式", "服务类“{0}”指定了多个接口，当前未支持此类模式",
		"Haruka DI Services", DiagnosticSeverity.Warning, true);

	internal static readonly DiagnosticDescriptor HARUKA2003 = new(
		"HARUKA2003", "为将要生成的ServiceDescriptor属性指定的可访问性无效",
		"为服务“{0}”指定的ServiceDescriptor属性的可访问性字符串必须为“public”，“internal”或“private”",
		"Haruka DI Services", DiagnosticSeverity.Error, true);


	internal static readonly DiagnosticDescriptor HARUKA2004 = new(
		"HARUKA2004", "尚未支持生成服务指定的描述符", "未支持生成服务“{0}”指定的服务描述符，已跳过该服务",
		"Haruka DI Services", DiagnosticSeverity.Warning, true
		);


	internal static readonly DiagnosticDescriptor HARUKA2100 = new(
		"HARUKA2100", "生成服务描述符时发生内部错误",
		"为服务“{0}”生成服务描述符时发生内部错误",
		"Haruka DI", DiagnosticSeverity.Error, true);
}
