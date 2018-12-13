using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Enum
{
    public enum ArticleStatus
    {
        [Description("Inactive")]
        Inactive,
        [Description("Unpublished")]
        Unpublished,
        [Description("Published")]
        Published,
        [Description("Deleted")]
        Deleted
    }
}
