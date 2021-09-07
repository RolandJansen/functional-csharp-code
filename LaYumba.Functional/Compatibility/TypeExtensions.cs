using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// This is copied from the Rackspace.Collections.Immutable package:
// https://github.com/tunnelvisionlabs/dotnet-collections/blob/master/System.Collections.Immutable/src/System/TypeExtensions.cs
// because for some reasons it doesn't work from the nuget package
namespace System
{
    internal static class TypeExtensions
    {
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }
    }
}
