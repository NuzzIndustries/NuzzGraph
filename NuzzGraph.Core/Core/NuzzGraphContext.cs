using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework;
using NuzzGraph.Core.Entities;

namespace NuzzGraph.Core
{
    public partial class NuzzGraphContext : GraphContext
    {
        /*
        internal INode BindDataObject(IDataObject dataObject, System.Type bindType)
        {
            List<BrightstarEntityObject> trackedObjects;
            if (_trackedObjects.TryGetValue(dataObject.Identity, out trackedObjects))
            {
                T matchObject = trackedObjects.OfType<T>().FirstOrDefault();
                if (matchObject == null)
                {
                    matchObject = (INode)Activator.CreateInstance(bindType, this, dataObject);
                }
                return matchObject;
            }
            return ((T)Activator.CreateInstance(bindType, this, dataObject));
        }*/

        public NuzzGraphContext(string connectionString)
            : base(connectionString)
        {
        }

        internal INode _LoadNodeFromObject(IDataObject obj)
        {
            throw new NotImplementedException();
        }
    }
}
