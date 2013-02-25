﻿using System.Collections.Generic;
using BetterCms.Core.DataContracts;

namespace BetterCms.Core.DataServices
{
    public interface ITagApiService
    {
        IList<ITag> GetTags();
    }
}
