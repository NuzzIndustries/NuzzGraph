using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.EntityFramework;

namespace NuzzGraph.Entities
{
    internal static class EntityUtility
    {
        internal static List<System.Type> AllSimpleTypes { get; set; }
        public static readonly string AssemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

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

            EntitySets = new Dictionary<Type, IEntitySet>();
            var props = typeof(GraphContext).GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(IEntitySet<>));
            foreach (var prop in props)
            {
            }
            
        }

        internal static void ValidateScalar(object value)
        {
            //must be string, int, bool, or decimal
            value.GetType();
        }

        private static bool _NodeTypesInitialized = false;
        internal static bool NodeTypesInitialized
        {
            get
            {
                if (IsSeedMode)
                    return _NodeTypesInitialized;
                return true;
            }
            set
            {
                _NodeTypesInitialized = value;
            }
        }

        internal static bool IsSeedMode
        {
            get { return AssemblyName == "NuzzGraph.Seed"; }
        }

        static Dictionary<Type, IEntitySet> EntitySets = new Dictionary<Type, IEntitySet>();

        internal static void AddNodeToContext(GraphContext context, INode node)
        {
            var acc = FastMember.TypeAccessor.Create(typeof(GraphContext));
            string nameOfSet = PluralizeName(node.GetType().Name);
            dynamic set = acc[context, nameOfSet];
            set.Add((dynamic)node);
        }

        private static string PluralizeName(string name)
        {
            return name + "s";
        }
    }
}
