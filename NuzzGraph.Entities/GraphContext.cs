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
using NuzzGraph.Entities.Attributes;
using NuzzGraph.Core;

namespace NuzzGraph.Entities 
{
    public partial class GraphContext : BrightstarEntityContext {
    	private static readonly EntityMappingStore TypeMappings;
    	
    	static GraphContext() 
    	{
    		TypeMappings = new EntityMappingStore();
    		var provider = new ReflectionMappingProvider();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.IFunction));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.IFunction, NuzzGraph.Entities.Function>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.IFunctionParameter));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.IFunctionParameter, NuzzGraph.Entities.FunctionParameter>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.INode));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.INode, NuzzGraph.Entities.Node>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.INodePropertyDefinition));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.INodePropertyDefinition, NuzzGraph.Entities.NodePropertyDefinition>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.INodeType));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.INodeType, NuzzGraph.Entities.NodeType>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.IPropertyDefinition));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.IPropertyDefinition, NuzzGraph.Entities.PropertyDefinition>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.IRelationshipType));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.IRelationshipType, NuzzGraph.Entities.RelationshipType>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.IScalarType));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.IScalarType, NuzzGraph.Entities.ScalarType>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.ISystemNode));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.ISystemNode, NuzzGraph.Entities.SystemNode>();
    		provider.AddMappingsForType(TypeMappings, typeof(NuzzGraph.Entities.IType));
    		TypeMappings.AddImplMapping<NuzzGraph.Entities.IType, NuzzGraph.Entities.Type>();
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
    		Functions = 	new BrightstarEntitySet<NuzzGraph.Entities.IFunction>(this);
    		FunctionParameters = 	new BrightstarEntitySet<NuzzGraph.Entities.IFunctionParameter>(this);
    		Nodes = 	new BrightstarEntitySet<NuzzGraph.Entities.INode>(this);
    		NodePropertyDefinitions = 	new BrightstarEntitySet<NuzzGraph.Entities.INodePropertyDefinition>(this);
    		NodeTypes = 	new BrightstarEntitySet<NuzzGraph.Entities.INodeType>(this);
    		PropertyDefinitions = 	new BrightstarEntitySet<NuzzGraph.Entities.IPropertyDefinition>(this);
    		RelationshipTypes = 	new BrightstarEntitySet<NuzzGraph.Entities.IRelationshipType>(this);
    		ScalarTypes = 	new BrightstarEntitySet<NuzzGraph.Entities.IScalarType>(this);
    		SystemNodes = 	new BrightstarEntitySet<NuzzGraph.Entities.ISystemNode>(this);
    		Types = 	new BrightstarEntitySet<NuzzGraph.Entities.IType>(this);
    
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.IFunction> Functions
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.IFunctionParameter> FunctionParameters
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.INode> Nodes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.INodePropertyDefinition> NodePropertyDefinitions
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.INodeType> NodeTypes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.IPropertyDefinition> PropertyDefinitions
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.IRelationshipType> RelationshipTypes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.IScalarType> ScalarTypes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.ISystemNode> SystemNodes
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<NuzzGraph.Entities.IType> Types
    	{
    		get; private set;
    	}
    	
    
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class Function : SystemNode, IFunction 
    {
    	public Function(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Function() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.IFunction
    
    	public System.String FunctionBody
    	{
            		get { return GetRelatedProperty<System.String>("FunctionBody"); }
            		set { SetRelatedProperty("FunctionBody", value); }
    	}
    
    	public NuzzGraph.Entities.NodeType DeclaringType
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("DeclaringType"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("DeclaringType", value); }
    	}
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.FunctionParameter> Parameters
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.FunctionParameter>("Parameters"); }
    		set { SetRelatedObjects("Parameters", value); }
    								}
    
    	public NuzzGraph.Entities.Function Overrides
    	{
            get { return (NuzzGraph.Entities.Function)GetRelatedObject<NuzzGraph.Entities.IFunction>("Overrides"); }
            set { SetRelatedObject<NuzzGraph.Entities.IFunction>("Overrides", value); }
    	}
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.Function> OverridenBy
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.Function>("OverridenBy"); }
    		set { SetRelatedObjects("OverridenBy", value); }
    								}
    	#endregion
    	#region Implementation of NuzzGraph.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class FunctionParameter : PropertyDefinition, IFunctionParameter 
    {
    	public FunctionParameter(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public FunctionParameter() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.IFunctionParameter
    
    	public NuzzGraph.Entities.Function DeclaringFunction
    	{
            get { return (NuzzGraph.Entities.Function)GetRelatedObject<NuzzGraph.Entities.IFunction>("DeclaringFunction"); }
    	}
    
    	public NuzzGraph.Entities.Type ParameterType
    	{
            get { return (NuzzGraph.Entities.Type)GetRelatedObject<NuzzGraph.Entities.IType>("ParameterType"); }
    	}
    	#endregion
    	#region Implementation of NuzzGraph.Entities.IPropertyDefinition
    
    	public NuzzGraph.Entities.ScalarType PropertyType
    	{
            get { return (NuzzGraph.Entities.ScalarType)GetRelatedObject<NuzzGraph.Entities.IScalarType>("PropertyType"); }
            set { SetRelatedObject<NuzzGraph.Entities.IScalarType>("PropertyType", value); }
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
    	#region Implementation of NuzzGraph.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class Node : BrightstarEntityObject, INode 
    {
    	public Node(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Node() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class NodePropertyDefinition : PropertyDefinition, INodePropertyDefinition 
    {
    	public NodePropertyDefinition(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public NodePropertyDefinition() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.INodePropertyDefinition
    
    	public NuzzGraph.Entities.NodeType DeclaringType
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("DeclaringType"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("DeclaringType", value); }
    	}
    
    	public System.String InternalUri
    	{
            		get { return GetRelatedProperty<System.String>("InternalUri"); }
            		set { SetRelatedProperty("InternalUri", value); }
    	}
    	#endregion
    	#region Implementation of NuzzGraph.Entities.IPropertyDefinition
    
    	public NuzzGraph.Entities.ScalarType PropertyType
    	{
            get { return (NuzzGraph.Entities.ScalarType)GetRelatedObject<NuzzGraph.Entities.IScalarType>("PropertyType"); }
            set { SetRelatedObject<NuzzGraph.Entities.IScalarType>("PropertyType", value); }
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
    	#region Implementation of NuzzGraph.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class NodeType : Type, INodeType 
    {
    	public NodeType(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public NodeType() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.INodeType
    
    	public System.Boolean IsAbstract
    	{
            		get { return GetRelatedProperty<System.Boolean>("IsAbstract"); }
            		set { SetRelatedProperty("IsAbstract", value); }
    	}
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.NodeType> SuperTypes
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.NodeType>("SuperTypes"); }
    		set { SetRelatedObjects("SuperTypes", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.RelationshipType> AllowedOutgoingRelationships
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.RelationshipType>("AllowedOutgoingRelationships"); }
    		set { SetRelatedObjects("AllowedOutgoingRelationships", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.RelationshipType> AllowedIncomingRelationships
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.RelationshipType>("AllowedIncomingRelationships"); }
    		set { SetRelatedObjects("AllowedIncomingRelationships", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.NodePropertyDefinition> Properties
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.NodePropertyDefinition>("Properties"); }
    		set { SetRelatedObjects("Properties", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.Function> Functions
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.Function>("Functions"); }
    		set { SetRelatedObjects("Functions", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.NodeType> SubTypes
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.NodeType>("SubTypes"); }
    		set { SetRelatedObjects("SubTypes", value); }
    								}
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.Node> AllNodes
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.Node>("AllNodes"); }
    		set { SetRelatedObjects("AllNodes", value); }
    								}
    	#endregion
    	#region Implementation of NuzzGraph.Entities.IType
    	#endregion
    	#region Implementation of NuzzGraph.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class PropertyDefinition : SystemNode, IPropertyDefinition 
    {
    	public PropertyDefinition(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public PropertyDefinition() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.IPropertyDefinition
    
    	public NuzzGraph.Entities.ScalarType PropertyType
    	{
            get { return (NuzzGraph.Entities.ScalarType)GetRelatedObject<NuzzGraph.Entities.IScalarType>("PropertyType"); }
            set { SetRelatedObject<NuzzGraph.Entities.IScalarType>("PropertyType", value); }
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
    	#region Implementation of NuzzGraph.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class RelationshipType : SystemNode, IRelationshipType 
    {
    	public RelationshipType(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public RelationshipType() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.IRelationshipType
    
    	public System.Boolean SupportsMany
    	{
            		get { return GetRelatedProperty<System.Boolean>("SupportsMany"); }
            		set { SetRelatedProperty("SupportsMany", value); }
    	}
    
    	public NuzzGraph.Entities.NodeType OutgoingFrom
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("OutgoingFrom"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("OutgoingFrom", value); }
    	}
    
    	public NuzzGraph.Entities.NodeType IncomingTo
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("IncomingTo"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("IncomingTo", value); }
    	}
    
    	public System.String InternalUri
    	{
            		get { return GetRelatedProperty<System.String>("InternalUri"); }
            		set { SetRelatedProperty("InternalUri", value); }
    	}
    	#endregion
    	#region Implementation of NuzzGraph.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class ScalarType : Type, IScalarType 
    {
    	public ScalarType(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public ScalarType() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.IScalarType
    	public System.Collections.Generic.ICollection<NuzzGraph.Entities.PropertyDefinition> PropertiesContainingType
    	{
    		get { return GetRelatedObjects<NuzzGraph.Entities.PropertyDefinition>("PropertiesContainingType"); }
    		set { SetRelatedObjects("PropertiesContainingType", value); }
    								}
    	#endregion
    	#region Implementation of NuzzGraph.Entities.IType
    	#endregion
    	#region Implementation of NuzzGraph.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class SystemNode : Node, ISystemNode 
    {
    	public SystemNode(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public SystemNode() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
namespace NuzzGraph.Entities 
{
    
    [Entity]
    public partial class Type : SystemNode, IType 
    {
    	public Type(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Type() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of NuzzGraph.Entities.IType
    	#endregion
    	#region Implementation of NuzzGraph.Entities.ISystemNode
    	#endregion
    	#region Implementation of NuzzGraph.Entities.INode
    
    	public NuzzGraph.Entities.NodeType TypeHandle
    	{
            get { return (NuzzGraph.Entities.NodeType)GetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle"); }
            set { SetRelatedObject<NuzzGraph.Entities.INodeType>("TypeHandle", value); }
    	}
    
    	public System.String Label
    	{
            		get { return GetRelatedProperty<System.String>("Label"); }
            		set { SetRelatedProperty("Label", value); }
    	}
    	#endregion
    }
}
