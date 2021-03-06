﻿using System;
using System.Runtime.Serialization;

using BetterCms.Module.Api.Infrastructure;

namespace BetterCms.Module.Api.Operations.Root.Categories.Category.Nodes.Node
{
    /// <summary>
    /// Category node delete response.
    /// </summary>
    [Serializable]
    [DataContract]
    public class DeleteNodeResponse : DeleteResponseBase
    {
    }
}