using System.ComponentModel.DataAnnotations;

namespace chatService.core
{
    [Serializable]
    /// <summary>
    /// base class for dt objects
    /// </summary>
    public class Initialized
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? GlobalSessionID { get; set; }
        public Guid LocalSessionID { get; set; }
    }
}
