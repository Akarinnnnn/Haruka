namespace Haruka.Analyzers.Model;

internal readonly record struct ServiceSemantics(
	ClassDeclarationSyntax ServiceDeclSyntax,
	Location ClassNameLocation,
	INamedTypeSymbol ServiceSymbol,
	INamedTypeSymbol? AlteredServiceTypeSymbol,
	TypedConstant? KeyConst,
	TypedConstant LifetimeConst,

	ServiceDescriptorOverload SDOverload,
	int GenericCommaCount,
	string Namespace,
	string ClassName,
	string? AccessibilityString
)
{
	public readonly string GenericBracketString
	{
		get
		{
			if (!IsSpecialGeneric)
				return "";

			string commas = new string(',', GenericCommaCount);
			return $"<{commas}>";
		}
	}

	public readonly string? AlteredServiceTypeReferenceString
	{
		get
		{
			if (AlteredServiceTypeSymbol is null)
				return null;

			return $"global::" +
				$"{AlteredServiceTypeSymbol.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}." +
				$"{AlteredServiceTypeSymbol.Name}" +
				$"{(AlteredServiceTypeSymbol.Arity == 0
					? ""
					: $"<{new string(',', AlteredServiceTypeSymbol.Arity - 1)}>")}";
		}
	}

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
	bool MultipleInterfaceNotSupported,

	string Namespace,
	string ClassName,
	string? PropAccessibilityString,
	ClassDeclarationSyntax ServiceDeclSyntax,
	Location ServiceNameSyntaxLocation
)
{
	private static readonly string?[] s_validAccessibilityStringValue = ["public", "internal", "private", null];

	public readonly bool IsNotEmittable => AttributeContractViolation
		|| IsStaticClass
		|| MultipleInterfaceNotSupported;

	public readonly bool IsServiceDescriptorPropAccessibilityValid =>
		s_validAccessibilityStringValue.Contains(PropAccessibilityString);

	public readonly void TryReportDiagnostics(Action<Diagnostic> reportDiagnostic, CancellationToken ct)
	{
		ct.ThrowIfCancellationRequested();

		if (AttributeContractViolation)
		{
			reportDiagnostic(Diagnostic.Create(HarukaDiagnosticDescriptors.HARUKA2000,
				ServiceNameSyntaxLocation, ClassName));
			return;
		}

		if (IsStaticClass)
		{
			reportDiagnostic(Diagnostic.Create(HarukaDiagnosticDescriptors.HARUKA2002,
				ServiceDeclSyntax.Modifiers.First(m => m.IsKind(SyntaxKind.StaticKeyword)).GetLocation(),
				ClassName));
		}

		if (MultipleInterfaceNotSupported)
		{
			reportDiagnostic(Diagnostic.Create(HarukaDiagnosticDescriptors.HARUKA2002,
				ServiceNameSyntaxLocation, ClassName));
		}

		if (!IsServiceDescriptorPropAccessibilityValid)
		{
			reportDiagnostic(Diagnostic.Create(HarukaDiagnosticDescriptors.HARUKA2003,
				ServiceNameSyntaxLocation, ClassName));
		}
	}
}

internal readonly record struct ServiceModel(
	ServiceSemantics? Semantics,
	ServiceDiagnostics Diagnostics
);