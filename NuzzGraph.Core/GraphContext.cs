﻿ 

// -----------------------------------------------------------------------
// <autogenerated>
//    This code was generated from a template.
//
//    Changes to this file may cause incorrect behaviour and will be lost
//    if the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework;

using System.Text;
using NuzzGraph.Core.Attributes;
using NuzzGraph.Core;
using NuzzGraph.Core.Utilities;

namespace NuzzGraph.Core 
{
    public partial class GraphContext : BrightstarEntityContext {
    	private static readonly EntityMappingStore TypeMappings;
    	
    	static GraphContext() 
    	{
    		TypeMappings = new EntityMappingStore();
    		var provider = new ReflectionMappingProvider();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.IFunction));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.IFunction, NuzzGraph.Core.Entities.Function>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.IFunctionParameter));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.IFunctionParameter, NuzzGraph.Core.Entities.FunctionParameter>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.INode));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.INode, NuzzGraph.Core.Entities.Node>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.INodePropertyDefinition));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.INodePropertyDefinition, NuzzGraph.Core.Entities.NodePropertyDefinition>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.INodeType));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.INodeType, NuzzGraph.Core.Entities.NodeType>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.IPropertyDefinition));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.IPropertyDefinition, NuzzGraph.Core.Entities.PropertyDefinition>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.IRelationshipType));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.IRelationshipType, NuzzGraph.Core.Entities.RelationshipType>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.IScalarType));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.IScalarType, NuzzGraph.Core.Entities.ScalarType>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.ISystemNode));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.ISystemNode, NuzzGraph.Core.Entities.SystemNode>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Core.Entities.IType));
    		TypeMappings.AddImplMapping<NuzzGraph.Core.Entities.IType, NuzzGraph.Core.Entities.Type>();
    		EntityUtility.ProcessMappings(TypeMappings);
    	}
    	
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar
    	/// Data Object Store connection
    	/// </summary>
    	/// <param name="dataObjectStore">The connection to the Brightstar Data Object Store that will provide the entity objects</param>
    	public GraphContext(IDataObjectStore dataObjectStore) : base(TypeMappings, dataObjectStore)
    	{
    		InitializeContext();
    	}
    
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar connection string
    	/// </summary>
    	/// <param name="connectionString">The connection to be used to connect to an existing BrightstarDB store</param>
    	/// <param name="enableOptimisticLocking">OPTIONAL: If set to true optmistic locking will be applied to all entity updates</param>
        /// <param name="updateGraphUri">OPTIONAL: The URI identifier of the graph to be updated with any new triples created by operations on the store. If
        /// not defined, the default graph in the store will be updated.</param>
        /// <param name="datasetGraphUris">OPTIONAL: The URI identifiers of the graphs that will be queried to retrieve entities and their properties.
        /// If not defined, all graphs in the store will be queried.</param>
        /// <param name="versionGraphUri">OPTIONAL: The URI identifier of the graph that contains version number statements for entities. 
        /// If not defined, the <paramref name="updateGraphUri"/> will be used.</param>
    	public GraphContext(
    	    string connectionString, 
    		bool? enableOptimisticLocking=null,
    		string updateGraphUri = null,
    		IEnumerable<string> datasetGraphUris = null,
    		string versionGraphUri = null
        ) : base(TypeMappings, connectionString, enableOptimisticLocking, updateGraphUri, datasetGraphUris, versionGraphUri)
    	{
    		InitializeContext();
    	}
    
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar
    	/// connection string retrieved from the configuration.
    	/// </summary>
    	public GraphContext() : base(TypeMappings)
    	{
    		InitializeContext();
    	}
    	
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar
    	/// connection string retrieved from the configuration and the
    	//  specified target graphs
    	/// </summary>
        /// <param name="updateGraphUri">The URI identifier of the graph to be updated with any new triples created by operations on the store. If
        /// set to null, the default graph in the store will be updated.</param>
        /// <param name="datasetGraphUris">The URI identifiers of the graphs that will be queried to retrieve entities and their properties.
        /// If set to null, all graphs in the store will be queried.</param>
        /// <param name="versionGraphUri">The URI identifier of the graph that contains version number statements for entities. 
        /// If set to null, the value of <paramref name="updateGraphUri"/> will be used.</param>
    	public GraphContext(
    		string updateGraphUri,
    		IEnumerable<string> datasetGraphUris,
    		string versionGraphUri
    	) : base(TypeMappings, updateGraphUri:updateGraphUri, datasetGraphUris:datasetGraphUris, versionGraphUri:versionGraphUri)
    	{
    		InitializeContext();
    	}
    	
    	private void InitializeContext() 
    	{
    		Functions = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.IFunction>(this);
    		FunctionParameters = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.IFunctionParameter>(this);
    		Nodes = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.INode>(this);
    		NodePropertyDefinitions = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.INodePropertyDefinition>(this);
    		NodeTypes = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.INodeType>(this);
    		PropertyDefinitions = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.IPropertyDefinition>(this);
    		RelationshipTypes = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.IRelationshipType>(this);
    		ScalarTypes = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.IScalarType>(this);
    		SystemNodes = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.ISystemNode>(this);
    		Types = 	new BrightstarEntitySet<NuzzGraph.Core.Entities.IType>(this);
    
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.IFunction> Functions
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.IFunctionParameter> FunctionParameters
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.INode> Nodes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.INodePropertyDefinition> NodePropertyDefinitions
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.INodeType> NodeTypes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.IPropertyDefinition> PropertyDefinitions
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.IRelationshipType> RelationshipTypes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.IScalarType> ScalarTypes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.ISystemNode> SystemNodes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Core.Entities.IType> Types
    	{
    		get; private set;
    	}
    	
    
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class Function : SystemNode, IFunction 
    {
    	public Function(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Function() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.IFunction
    
    	public System.String FunctionBody
    	{
            		get { return GetRelatedProperty<System.String>("FunctionBody"); }
            		set { SetRelatedProperty("FunctionBody", value); }
    	}
    
    	public NuzzGraph.Core.Entities.INodeType DeclaringType
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("DeclaringType"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("DeclaringType", value); }
    	}
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.IFunctionParameter> Parameters
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.IFunctionParameter>("Parameters"); }
    		set { SetRelatedObjects("Parameters", value); }
    								}
    
    	public NuzzGraph.Core.Entities.IFunction Overrides
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.IFunction>("Overrides"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.IFunction>("Overrides", value); }
    	}
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.IFunction> OverridenBy
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.IFunction>("OverridenBy"); }
    		set { SetRelatedObjects("OverridenBy", value); }
    								}
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class FunctionParameter : PropertyDefinition, IFunctionParameter 
    {
    	public FunctionParameter(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public FunctionParameter() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.IFunctionParameter
    
    	public NuzzGraph.Core.Entities.IFunction DeclaringFunction
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.IFunction>("DeclaringFunction"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.IFunction>("DeclaringFunction", value); }
    	}
    
    	public NuzzGraph.Core.Entities.IType ParameterType
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.IType>("ParameterType"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.IType>("ParameterType", value); }
    	}
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.IPropertyDefinition
    
    	public NuzzGraph.Core.Entities.IScalarType PropertyType
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.IScalarType>("PropertyType"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.IScalarType>("PropertyType", value); }
    	}
    
    	public System.Boolean IsNullable
    	{
            		get { return GetRelatedProperty<System.Boolean>("IsNullable"); }
            		set { SetRelatedProperty("IsNullable", value); }
    	}
    
    	public System.Object DefaultValue
    	{
            		get { return (object)GetRelatedProperty<System.Object>("DefaultValue"); }
            		set 
            		{ 
            			EntityUtility.ValidateScalar(value);
            			SetRelatedProperty("DefaultValue", value); 
            		} //Convert to DB	
    	}
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class Node : BrightstarEntityObject, INode 
    {
    	public Node(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Node() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class NodePropertyDefinition : PropertyDefinition, INodePropertyDefinition 
    {
    	public NodePropertyDefinition(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public NodePropertyDefinition() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.INodePropertyDefinition
    
    	public NuzzGraph.Core.Entities.INodeType DeclaringType
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("DeclaringType"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("DeclaringType", value); }
    	}
    
    	public System.String InternalUri
    	{
            		get { return GetRelatedProperty<System.String>("InternalUri"); }
            		set { SetRelatedProperty("InternalUri", value); }
    	}
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.IPropertyDefinition
    
    	public NuzzGraph.Core.Entities.IScalarType PropertyType
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.IScalarType>("PropertyType"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.IScalarType>("PropertyType", value); }
    	}
    
    	public System.Boolean IsNullable
    	{
            		get { return GetRelatedProperty<System.Boolean>("IsNullable"); }
            		set { SetRelatedProperty("IsNullable", value); }
    	}
    
    	public System.Object DefaultValue
    	{
            		get { return (object)GetRelatedProperty<System.Object>("DefaultValue"); }
            		set 
            		{ 
            			EntityUtility.ValidateScalar(value);
            			SetRelatedProperty("DefaultValue", value); 
            		} //Convert to DB	
    	}
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class NodeType : Type, INodeType 
    {
    	public NodeType(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public NodeType() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.INodeType
    
    	public System.Boolean IsAbstract
    	{
            		get { return GetRelatedProperty<System.Boolean>("IsAbstract"); }
            		set { SetRelatedProperty("IsAbstract", value); }
    	}
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.INodeType> SuperTypes
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.INodeType>("SuperTypes"); }
    		set { SetRelatedObjects("SuperTypes", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.IRelationshipType> AllowedOutgoingRelationships
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.IRelationshipType>("AllowedOutgoingRelationships"); }
    		set { SetRelatedObjects("AllowedOutgoingRelationships", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.IRelationshipType> AllowedIncomingRelationships
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.IRelationshipType>("AllowedIncomingRelationships"); }
    		set { SetRelatedObjects("AllowedIncomingRelationships", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.INodePropertyDefinition> Properties
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.INodePropertyDefinition>("Properties"); }
    		set { SetRelatedObjects("Properties", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.IFunction> Functions
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.IFunction>("Functions"); }
    		set { SetRelatedObjects("Functions", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.INodeType> SubTypes
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.INodeType>("SubTypes"); }
    		set { SetRelatedObjects("SubTypes", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.INode> AllNodes
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.INode>("AllNodes"); }
    		set { SetRelatedObjects("AllNodes", value); }
    								}
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.IType
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class PropertyDefinition : SystemNode, IPropertyDefinition 
    {
    	public PropertyDefinition(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public PropertyDefinition() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.IPropertyDefinition
    
    	public NuzzGraph.Core.Entities.IScalarType PropertyType
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.IScalarType>("PropertyType"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.IScalarType>("PropertyType", value); }
    	}
    
    	public System.Boolean IsNullable
    	{
            		get { return GetRelatedProperty<System.Boolean>("IsNullable"); }
            		set { SetRelatedProperty("IsNullable", value); }
    	}
    
    	public System.Object DefaultValue
    	{
            		get { return (object)GetRelatedProperty<System.Object>("DefaultValue"); }
            		set 
            		{ 
            			EntityUtility.ValidateScalar(value);
            			SetRelatedProperty("DefaultValue", value); 
            		} //Convert to DB	
    	}
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class RelationshipType : SystemNode, IRelationshipType 
    {
    	public RelationshipType(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public RelationshipType() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.IRelationshipType
    
    	public System.Boolean SupportsMany
    	{
            		get { return GetRelatedProperty<System.Boolean>("SupportsMany"); }
            		set { SetRelatedProperty("SupportsMany", value); }
    	}
    
    	public NuzzGraph.Core.Entities.INodeType OutgoingFrom
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("OutgoingFrom"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("OutgoingFrom", value); }
    	}
    
    	public NuzzGraph.Core.Entities.INodeType IncomingTo
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("IncomingTo"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("IncomingTo", value); }
    	}
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class ScalarType : Type, IScalarType 
    {
    	public ScalarType(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public ScalarType() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.IScalarType
    	public System.Collections.Generic.ICollection<NuzzGraph.Core.Entities.IPropertyDefinition> PropertiesContainingType
    	{
    		get { return GetRelatedObjects<NuzzGraph.Core.Entities.IPropertyDefinition>("PropertiesContainingType"); }
    		set { SetRelatedObjects("PropertiesContainingType", value); }
    								}
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.IType
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class SystemNode : Node, ISystemNode 
    {
    	public SystemNode(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public SystemNode() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Core.Entities 
{
    
    [Entity]
    public partial class Type : SystemNode, IType 
    {
    	public Type(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Type() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Core.Entities.IType
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Core.Entities.INode
    
    	public NuzzGraph.Core.Entities.INodeType TypeHandle
    	{
            get { return GetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Core.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
