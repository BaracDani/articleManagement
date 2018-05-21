using Business.Views;
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

    public enum ReviewStatus
    {
        InPending,
        Reviewed,
        Abstain
    }
    public enum ReviewPoints
    {
        Neutral,
        StrongReject,
        Reject,
        Approve,
        StrongApprove
    }

    public class UsersToReview
    {
        public List<string> UserIds { get; set; }
        public ArticleView Article { get; set; }
    }

    public class ReviewArticleRequest
    {
        public ArticleView Article { get; set; }
        public int ReviewPoints { get; set; }

        public string Comment { get; set; }
    }
}
