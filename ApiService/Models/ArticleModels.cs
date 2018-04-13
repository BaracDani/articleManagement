using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiService.Models
{
    public enum ApprovalStatus
    {
        None,
        InPending,
        Approved,
        Rejected,
        InReview
    };
}
