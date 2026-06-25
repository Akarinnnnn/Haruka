namespace Haruka.Analyzers;


[Serializable]
internal class HarukaICEReportedException : Exception
{
	internal HarukaICEReportedException(Exception inner) : base("生成器其它模块已报告诊断", inner) { }
	protected HarukaICEReportedException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}