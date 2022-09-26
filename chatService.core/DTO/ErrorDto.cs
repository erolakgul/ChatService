namespace chatService.core.DTO
{
    [Serializable]
    /// <summary>
    /// the class what will be returned to clients in error situations
    /// </summary>
    public class ErrorDto : Initialized
    {
        /// <summary>
        /// error reason (catch ex info)
        /// </summary>
        public string? ErrorReason { get; set; }
        /// <summary>
        /// error code (catch ex info)
        /// </summary>
        public string? ErrorCode { get; set; }
        /// <summary>
        /// the count of error that client had after the app run
        /// </summary>
        public int CountOfError { get; set; }
    }
}
