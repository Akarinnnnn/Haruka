using System;
using System.Collections.Generic;
using System.Text;

namespace Haruka.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class HarukaRegisterationHolderAttribute : Attribute
{
}
