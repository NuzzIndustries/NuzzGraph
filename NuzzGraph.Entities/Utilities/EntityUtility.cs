using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BrightstarDB.EntityFramework;

namespace NuzzGraph.Entities
{
    internal static class EntityUtility
    {
        internal static List<System.Type> AllSimpleTypes { get; set; }
        public static readonly string AssemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        internal static List<System.Type> AllCLRTypes { get; private set; }
        

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
            //var props = typeof(GraphContext).GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(IEntitySet<>));

            AllCLRTypes = typeof(NuzzGraph.Entities.IType).Assembly
                .GetTypes()
                .Where(x => x.IsInterface)
                .Where(x => x.Namespace == typeof(INodeType).Namespace)
                .Where(x => x.GetCustomAttributes(typeof(BrightstarDB.EntityFramework.EntityAttribute), false).Count() == 1)
                .ToList();
        }

        public static void SpecifyURIMappings(EntityMappingStore mappings)
        {
            var f = mappings.GetType().GetField("_propertyHints", BindingFlags.NonPublic | BindingFlags.Instance);
            var pHints = f.GetValue(mappings) as Dictionary<PropertyInfo, PropertyHint>;

            //Load Types
            foreach (var type in AllCLRTypes)
            {
                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!pHints.ContainsKey(property))
                        continue;

                    if (!type.Name.StartsWith("I"))
                        throw new InvalidOperationException("Expected interface name starting with I");

                    var hint = pHints[property];
                    var uri = string.Format("http://www.nuzzgraph.com/Entities/{0}/Properties/{1}", type.Name.Substring(1), property.Name);
                    pHints[property] = new PropertyHint(hint.MappingType, uri);
                }
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
