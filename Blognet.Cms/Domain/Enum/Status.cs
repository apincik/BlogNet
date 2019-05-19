using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Blognet.Cms.Domain.Enum
{
    public enum Status
    {
        [Description("Inactive")]
        Inactive,
        [Description("Active")]
        Active,
        [Description("Deleted")]
        Deleted
    }
}
