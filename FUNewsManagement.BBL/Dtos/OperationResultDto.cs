namespace FUNewsManagement.BLL.Dtos
{
    public class OperationResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        public static OperationResultDto Success(string message) => new()
        {
            IsSuccess = true,
            Message = message
        };

        public static OperationResultDto Failure(string message) => new()
        {
            IsSuccess = false,
            Message = message
        };
    }
}
