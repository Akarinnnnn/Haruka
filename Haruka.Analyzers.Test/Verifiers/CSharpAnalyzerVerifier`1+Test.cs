using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace Haruka.Analyzers.Test;

public static partial class CSharpSGVerifier<TGenerator>
	where TGenerator : new()
{
	public class Test : CSharpSourceGeneratorTest<TGenerator, DefaultVerifier>
	{
		public Test()
		{
			SolutionTransforms.Add((solution, projectId) =>
			{
				var compOpt = solution.GetProject(projectId)!.CompilationOptions!;

				solution = solution
					.AddMetadataReference(projectId, MetadataReference.CreateFromFile(
						typeof(Attributes.HarukaRegisterationHolderAttribute).Assembly.Location,
						MetadataReferenceProperties.Assembly));

				return solution;
			});

			ReferenceAssemblies = ReferenceAssemblies.Net.Net100
				.WithPackages(
				[new PackageIdentity("Microsoft.Extensions.DependencyInjection.Abstractions", "10.0.8")]
			);
		}
	}
}
