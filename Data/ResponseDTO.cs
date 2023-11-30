namespace JobFixa.Data
{
    public class ResponseDTO
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public ResponseDTO(bool status, string message)
        {
            this.Status = status;
            this.Message = message ??
            throw new ArgumentNullException(nameof(message));

        }
    }
}
