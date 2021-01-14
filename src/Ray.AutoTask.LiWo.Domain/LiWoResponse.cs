namespace Ray.AutoTask.LiWo.Domain
{
    public class LiWoResponse
    {
        public bool Status { get; set; }

        public ErrorResponse Error { get; set; }

        public string TraceId { get; set; }
    }

    public class LiWoResponse<T> : LiWoResponse
    {
        public T Data { get; set; }
    }

    public class ErrorResponse
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public string RawMessage { get; set; }

        public string ExtraData { get; set; }
    }
}
