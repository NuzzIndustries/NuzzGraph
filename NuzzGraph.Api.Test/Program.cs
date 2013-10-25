﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NuzzGraph.Entities;

namespace NuzzGraph.Api.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // define a connection string
            const string connectionString = "type=http;endpoint=http://localhost:8090/brightstar;storeName=nuzzgraph";


            // if the store does not exist it will be automatically
            // created when a context is created
            var ctx = new GraphContext(connectionString);

            var nodes1 = ctx.Nodes.Create();

            // save the data
            ctx.SaveChanges();


            // open a new context, not required
            ctx = new GraphContext(connectionString);
        }
    }
}