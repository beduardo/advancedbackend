namespace advancedbackend.domain.responsemodel {
    public class ErrorMessageResponse {
        public string Error { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
    }
}