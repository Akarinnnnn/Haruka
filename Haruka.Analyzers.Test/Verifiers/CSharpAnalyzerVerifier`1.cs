using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Haruka.Analyzers.Test;

public static partial class CSharpSGVerifier<TGenerator>
	where TGenerator : new()
{
	public static async Task VerifyAsync(CancellationToken ct, [StringSyntax("c#-test")] string source,
		(string filename, string generated)[] exceptedGenerated,
		params DiagnosticResult[] expectedDiag)
	{
		var test = new Test
		{
			TestCode = source,
		};

		test.ExpectedDiagnostics.AddRange(expectedDiag);
		test.TestState.GeneratedSources.AddRange(exceptedGenerated
			.Select(TransformGeneratedSource));
		await test.RunAsync(ct);
	}

	public static (string filename, SourceText content) TransformGeneratedSource((string filename, string generated) g)
		=> (Path.Combine(typeof(TGenerator).Assembly.GetName().Name!,
					typeof(TGenerator).FullName!,
					g.filename),
				SourceText.From(g.generated, Encoding.Unicode));
}
