namespace Haruka.Analyzers;

// Holder相关诊断
internal static partial class HarukaDiagnosticDescriptors
{
	internal static readonly DiagnosticDescriptor HARUKA1000 = new(
		"HARUKA1000", "放置注册方法的类必须可以通过名字引用", "类“{0}”不可通过名字引用，无法为其生成服务注册代码",
		"Haruka DI", DiagnosticSeverity.Error, true);

	internal static readonly DiagnosticDescriptor HARUKA1001 = new(
		"HARUKA1001", "放置注册方法的类不能是嵌套类", "不支持为嵌套类“{0}”生成注册服务的代码",
		"Haruka DI", DiagnosticSeverity.Error, true);

	internal static readonly DiagnosticDescriptor HARUKA1002 = new(
		"HARUKA1002", "注册方法不是静态的",
		"类“{0}”中的注册方法“GeneratedRegisterServices()”必须声明为“static partial”",
		"Haruka DI", DiagnosticSeverity.Error, true);

	internal static readonly DiagnosticDescriptor HARUKA1003 = new(
		"HARUKA1003", "注册方法不是分部的",
		"服务注册类“{0}”中需要声明方法“public static partial void GeneratedRegisterServices(IServiceCollection services)”",
		"Haruka DI", DiagnosticSeverity.Error, true,
#pragma warning disable RS1033 // 正确定义诊断说明
		description: "服务注册类“{0}”中需要您显式声明一个注册方法，以表明您有意在该类中生成大量干扰Intellisense的注册代码。" +
		"请注意，诊断消息中展示的函数签名并不是唯一可行的签名，关于注册方法的更多详细信息请参见Haruka文档。");
#pragma warning restore RS1033 // 中文标点也是标点

	internal static readonly DiagnosticDescriptor HARUKA1004 = new(
		"HARUKA1004", "找不到符合约束的服务注册方法", "类“{0}”中没有找到符合服务注册方法约束的“GeneratedRegisterServices()”",
		"Haruka DI", DiagnosticSeverity.Error, true);

	internal static readonly DiagnosticDescriptor HARUKA1005 = new(
		"HARUKA1005", "服务注册方法不能位于C# 14拓展块中",
		"类“{0}”中符合条件的服务注册方法位于C# 14拓展块中，当前不支持为此类拓展方法生成服务注册代码",
		"Haruka DI", DiagnosticSeverity.Error, true);

	internal static readonly DiagnosticDescriptor HARUKA1006 = new(
		"HARUKA1006", "已编写服务注册方法的定义，无法为其生成注册代码",
		"类“{0}”已定义一个符合注册方法签名的同名方法，无法为该类生成注册代码",
		"Haruka DI", DiagnosticSeverity.Error, true);

	internal static readonly DiagnosticDescriptor HARUKA1007 = new(
		"HARUKA1007", "生成注册方法代码时发生内部错误",
		"为类“{0}”生成注册方法时发生内部错误",
		"Haruka DI", DiagnosticSeverity.Error, true);

	internal static readonly DiagnosticDescriptor HARUKA1008 = new(
		"HARUKA1008", "存放注册方法的类必须是分部的",
		"注册类“{0}”必须声明为“partial”",
		"Haruka DI", DiagnosticSeverity.Error, true);
}
