using System.Runtime.InteropServices;

namespace Haruka.Analyzers.Model;

internal readonly record struct ServiceInitialSemantics(
	ClassDeclarationSyntax ServiceDeclSyntax,
	INamedTypeSymbol ServiceSymbol,
	INamedTypeSymbol? AlteredServiceTypeSymbol,
	TypedConstant? KeyConst,
	TypedConstant LifetimeConst,
	int GenericCommaCount,
	string Namespace
)
{
	public readonly bool IsSpecialGeneric => GenericCommaCount != -1;

	public readonly string NormalizedServiceDescriptorPropertyIdentifier
	{
		get
		{
			string className = ServiceSymbol.Name;
			if (IsSpecialGeneric)
				className += $"_of_{GenericCommaCount + 1}";
			
			string dotlessNamespace = Namespace.Replace('.', '_');

			return $"{dotlessNamespace}_{className}_Descriptor";
		}
	}
}

// 诊断需要一边分析一边修改，所以它是可变的。其它诊断模型同理
internal record struct ServiceDiagnostics(
	bool AttributeContractViolation,
	bool IsStaticClass,
	ClassDeclarationSyntax ServiceDeclSyntax,
	Location ServiceNameSyntaxLocation
)
{
	public readonly bool IsNotEmittable => AttributeContractViolation
		|| IsStaticClass;

	public readonly void TryReportDiagnostics(Action<Diagnostic> reportDiagnostic, CancellationToken ct)
	{
		if (AttributeContractViolation)
		{

		}
	}
}

internal readonly record struct ServiceInitialModel(
	ServiceInitialSemantics? Semantics,
	ServiceDiagnostics Diagnostics
);