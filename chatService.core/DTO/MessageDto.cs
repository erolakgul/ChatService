namespace chatService.core.DTO
{
    public class MessageDto : Initialized
    {
        /// <summary>
        /// very high, high, medium, low, very low
        /// </summary>
        public string? ImportanceDegree { get; set; }
        /// <summary>
        /// Message Content
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// nickname or username that sends mesage from client to other
        /// </summary>
        public string? NickName { get; set; }
        /// <summary>
        /// the information of "is read ?" for sent the message 
        /// </summary>
        public bool IsRead { get; set; }
    }

    /// <summary>
    /// useable in ImportanceDegree property selection
    /// </summary>
    public enum ImportanceType
    {
        VERYHIGH,
        HIGH,
        MEDIUM,
        LOW,
        VERYLOW
    }
}
