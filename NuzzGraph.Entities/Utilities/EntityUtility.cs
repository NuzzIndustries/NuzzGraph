using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuzzGraph.Entities
{
    internal static class EntityUtility
    {
        internal static List<System.Type> AllSimpleTypes { get; set; }

        static EntityUtility()
        {
            AllSimpleTypes = new List<System.Type>();
            AllSimpleTypes.Add(typeof(string));
            AllSimpleTypes.Add(typeof(char));
            AllSimpleTypes.Add(typeof(float));
            AllSimpleTypes.Add(typeof(decimal));
            AllSimpleTypes.Add(typeof(double));
            AllSimpleTypes.Add(typeof(int));
            AllSimpleTypes.Add(typeof(long));
            AllSimpleTypes.Add(typeof(short));
            AllSimpleTypes.Add(typeof(byte));
            AllSimpleTypes.Add(typeof(bool));
            AllSimpleTypes.Add(typeof(char?));
            AllSimpleTypes.Add(typeof(float?));
            AllSimpleTypes.Add(typeof(decimal?));
            AllSimpleTypes.Add(typeof(double?));
            AllSimpleTypes.Add(typeof(int?));
            AllSimpleTypes.Add(typeof(long?));
            AllSimpleTypes.Add(typeof(short?));
            AllSimpleTypes.Add(typeof(byte?));
            AllSimpleTypes.Add(typeof(bool?));
        }

        internal static void ValidateScalar(object value)
        {
            //must be string, int, bool, or decimal
            value.GetType();
        }
    }
}
