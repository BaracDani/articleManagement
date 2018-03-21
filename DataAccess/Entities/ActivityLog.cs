using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Operation
    {
        public const string Create = "Create";
    }

    [Table("ActivityLog")]
    public class ActivityLog : BaseEntity
    {
        public long UserId { get; set; }

        public long EntityId { get; set; }

        public string Operation { get; set; }

        public string OnTime { get; set; }


        public string OldValue { get; set; }

        public string NewValue { get; set; }

        [NotMapped]
        public override string Table => nameof(ActivityLog);
    }
}