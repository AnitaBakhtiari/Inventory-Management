namespace InventoryManagement.Application.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public int ErrorCode { get; protected set; }

        public BusinessException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

    }
}
