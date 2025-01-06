namespace Enplace.Service.Entities
{
    public class TracedEntity
    {
        public DateTime CreatedOn { get; set; }
        public int? CreatedByID { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedByID { get; set; }
    }
}
