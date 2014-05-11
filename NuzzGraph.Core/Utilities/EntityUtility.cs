using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core;
using NuzzGraph.Core.Entities;

namespace NuzzGraph.Core.Utilities
{
    internal static class EntityUtility
    {
        internal static List<System.Type> AllSimpleTypes { get; set; }
        public static readonly string AssemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        internal static List<System.Type> AllCLRTypes { get; private set; }

        private static bool MappingsLoaded { get; set; }

        private static Dictionary<INodeType, System.Type> _CLRTypeMap;
        internal static Dictionary<INodeType, System.Type> CLRTypeMap 
        { 
            get
            {
                if (!MappingsLoaded)
                    throw new InvalidOperationException("Cannot use CLRTypeMap until Mappings are loaded.");

                if (_CLRTypeMap == null)
                {
                    lock (typeof(EntityUtility))
                    {
                        if (_CLRTypeMap == null)
                            _CLRTypeMap = BuildCLRTypeMap();
                    }
                }

                return _CLRTypeMap;
            }
        }

        private static Dictionary<System.Type, INodeType> _CLRTypeMapInverse;
        internal static Dictionary<System.Type, INodeType> CLRTypeMapInverse
        {
            get
            {
                if (_CLRTypeMapInverse == null)
                {
                    lock(typeof(EntityUtility))
                    {
                        if (_CLRTypeMapInverse == null)
                        {
                            _CLRTypeMapInverse = new Dictionary<System.Type,INodeType>();
                            foreach(var typeNode in CLRTypeMap.Keys)
                            {
                                var clrType = CLRTypeMap[typeNode];
                                _CLRTypeMapInverse[clrType] = typeNode;
                            }
                        }
                    }
                }
                return _CLRTypeMapInverse;
            }
        }

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

            AllCLRTypes = typeof(NuzzGraph.Core.Entities.IType).Assembly
                .GetTypes()
                .Where(x => x.IsInterface)
                .Where(x => x.Namespace == typeof(INodeType).Namespace)
                .Where(x => x.GetCustomAttributes(typeof(BrightstarDB.EntityFramework.EntityAttribute), false).Count() == 1)
                .ToList();
        }

        private static Dictionary<INodeType, System.Type> BuildCLRTypeMap()
        {
            Dictionary<string, INodeType> _nodeTypes = null;

            using (var con = ContextFactory.New())
            {
                _nodeTypes = con.NodeTypes.ToDictionary(x => x.Label);
            }

            return AllCLRTypes.ToDictionary(x => _nodeTypes[x.Name.Substring(1)]);
        }

        public static void ProcessMappings(EntityMappingStore mappings)
        {
            //CorrectMappings(mappings);
            SpecifyURIMappings(mappings);
            MappingsLoaded = true;
        }

        /// <summary>
        /// Fixes the BrightstarDB 'issue' caused by non-interface type aliases being interpreted as Properties instad of Arcs
        /// </summary>
        /// <param name="mappings"></param>
        private static void CorrectMappings(EntityMappingStore mappings)
        {
            var allhints = mappings.GetType().GetField("_propertyHints", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(mappings) as Dictionary<PropertyInfo, PropertyHint>;

            foreach (var pInfo in allhints.Keys.ToList())
            {
                //Load existing info
                var hint = allhints[pInfo];
                var maptype = hint.MappingType;
                if (hint.MappingType != PropertyMappingType.Property)
                    continue;

                //Set new property hint
                maptype = GetMappingType(pInfo);
                hint = new PropertyHint(maptype, hint.SchemaTypeUri);
                allhints[pInfo] = hint;
            }
        }

        private static PropertyMappingType GetMappingType(PropertyInfo pInfo)
        {
            if (EntityUtility.IsScalar(pInfo.PropertyType))
                return PropertyMappingType.Property;
            else if (pInfo.GetCustomAttributes(typeof(InversePropertyAttribute), false).Count() > 0)
                return PropertyMappingType.InverseArc;
            else
                return PropertyMappingType.Arc;
        }

        /// <summary>
        /// Fixes the BrightstarDB issue caused by ambiguous URI references being specified for properties
        /// </summary>
        /// <param name="mappings"></param>
        private static void SpecifyURIMappings(EntityMappingStore mappings)
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
                    if (hint.MappingType == PropertyMappingType.InverseArc)
                        continue; //do inverse properties in second pass

                    var uri = DataUtility.GetUri(type.IsInterface ? type.Name.Substring(1) : type.Name, property.Name);
                    pHints[property] = new PropertyHint(hint.MappingType, uri);
                }
            }

            //Second pass for inverse properties
            foreach (var type in AllCLRTypes)
            {
                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!pHints.ContainsKey(property))
                        continue;

                    if (!type.Name.StartsWith("I"))
                        throw new InvalidOperationException("Expected interface name starting with I");

                    var hint = pHints[property];
                    if (hint.MappingType != PropertyMappingType.InverseArc)
                        continue; //not an inverse property

                    var att = property.GetCustomAttributes(typeof(InversePropertyAttribute), false).Single() as InversePropertyAttribute;
                    string pName = att.InversePropertyName;

                    //Load the inverse property info, and set the URI accordingly
                    System.Type inverseType = null;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                        inverseType = property.PropertyType.GetGenericArguments()[0];
                    else
                        inverseType = property.PropertyType;

                    var inverseProperty = inverseType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.Name == pName).Single();

                    var uri = string.Format(Constants.NodeUriFormatBase, inverseType.IsInterface ? inverseType.Name.Substring(1) : inverseType.Name, inverseProperty.Name);
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
                BuildCLRTypeMap();
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

        internal static bool IsScalar(System.Type propertyType)
        {
            return AllSimpleTypes.Contains(propertyType) || propertyType == typeof(object);
        }

        private static string PluralizeName(string name)
        {
            return name + "s";
        }

        /// <summary>
        /// Given a base INode, creates a new object of a derived type if the NodeType property is not of type Node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal static INode RebindEntity(Node node)
        {
            if (node.TypeHandle == null)
                throw new InvalidOperationException("TypeHandle information not loaded.  Ensure that TypeHandle is retrieved as part of your Sparql query.");

            DateTime now = DateTime.Now;
            
            System.Type targetType = null;
            if (CLRTypeMap.ContainsKey(node.TypeHandle))
                targetType = CLRTypeMap[node.TypeHandle];

            //Get generic method
            var minfo = typeof(BrightstarEntityObject).GetMethod("Become", BindingFlags.Public | BindingFlags.Instance);
            minfo = minfo.MakeGenericMethod(targetType);

            var targetNode = (INode)minfo.Invoke(node, null);

            var time = DateTime.Now - now;


            return targetNode;
        }
    }
}
