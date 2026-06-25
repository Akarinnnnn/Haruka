using System.Collections.Immutable;

namespace Haruka.Analyzers.Model;

internal readonly record struct HolderModel(HolderDiagnostics Diagnostics,
	HolderSemantics? Semantics)
{
}

internal readonly record struct HolderSemantics(
	ClassDeclarationSyntax HolderDeclSyntax,
	MethodDeclarationSyntax RegisterMethodDeclSyntax,
	INamedTypeSymbol HolderSymbol,
	string Namespace, string ClassName,
	bool ServicesParamHasThisModifier,
	string ServicesParamName,
	bool RegisterMethodIsIntenral
);

internal record struct HolderDiagnostics(
	bool IsRegisterMethodNotDeclared,
	bool IsNestedClass,
	bool IsPartial, bool IsNotReferencable,

	bool IsRegistererNotStatic, bool IsRegistererNotPartial,
	bool IsRegistererContractViolation,
	bool IsRegisterMethodInExtBlock,

	ClassDeclarationSyntax HolderDeclSyntax,
	Location HolderNameSyntaxLocation,
	Location? PartialRegisterMethodNameLocation,
	Location? DefinedRegistererLocation,
	ImmutableArray<Location>? InvalidRegistererLocations
)
{
	public readonly bool IsNotEmittable =>
		IsRegisterMethodNotDeclared
		|| IsNotReferencable
		|| !IsPartial
		|| IsNestedClass

		|| IsRegistererNotStatic || IsRegistererNotPartial
		|| IsRegistererContractViolation
		|| IsRegisterMethodInExtBlock
		|| InvalidRegistererLocations is not null
		|| DefinedRegistererLocation is not null;

	public void TryReportDiagnostics(Action<Diagnostic> reportDiagnostic, CancellationToken ct)
	{
		ct.ThrowIfCancellationRequested();
		string holderName = HolderDeclSyntax.Identifier.ValueText;
		if (IsNotReferencable)
		{
			reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.HARUKA1000, HolderDeclSyntax.GetLocation()));
		}

		if (IsNestedClass)
		{
			reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.HARUKA1001, HolderNameSyntaxLocation,
				holderName));
		}

		if (!IsPartial)
		{
			reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.HARUKA1008,
				HolderNameSyntaxLocation,
				holderName));
		}

		if (IsRegistererNotStatic)
		{
			reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.HARUKA1002,
				HolderNameSyntaxLocation,
				holderName));
		}

		if (IsRegistererNotPartial)
		{
			reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.HARUKA1003,
				HolderNameSyntaxLocation,
				holderName));
		}

		ct.ThrowIfCancellationRequested();
		if (IsRegisterMethodNotDeclared || IsRegistererContractViolation) // 有点屎山了
		{
			reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.HARUKA1004,
				HolderNameSyntaxLocation, InvalidRegistererLocations,
				holderName));
		}

		if (IsRegisterMethodInExtBlock)
		{
			reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.HARUKA1005,
				PartialRegisterMethodNameLocation,
				holderName));
		}

		ct.ThrowIfCancellationRequested();
		if (DefinedRegistererLocation is Location definedRegisterNotNull)
		{
			reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.HARUKA1006,
				definedRegisterNotNull,
				holderName));
		}
	}
}