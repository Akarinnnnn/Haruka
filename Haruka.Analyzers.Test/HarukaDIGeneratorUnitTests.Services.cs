// DO NOT convert line delimiter to LF OR checkout as LF
// Generator hard coded CRLF in the writer, make this file LF will break all tests
using Microsoft.CodeAnalysis.Testing;
using VerifyCS = Haruka.Analyzers.Test.CSharpSGVerifier<Haruka.Analyzers.DependencyInjectionAnalyzer>;

namespace Haruka.Analyzer.Test;

public partial class HarukaDIGeneratorTests
{
	[Fact]
	public async Task SingleServiceSingletonSingleFile()
	{
		const string sourceWithHolderAndService =
@"
using Haruka;
using Microsoft.Extensions.DependencyInjection;


";
		const string testHolderGCs = /*lang=c#-test*/
@"
";
		const string test_Service_GCs = /*lang=c#-test*/
@"
";

		await VerifyCS.VerifyAsync(TestContext.Current.CancellationToken, sourceWithHolderAndService, [
			("TestHolder.g.cs", testHolderGCs),
			("Test_Service.g.cs", test_Service_GCs)
		]);
	}
}
