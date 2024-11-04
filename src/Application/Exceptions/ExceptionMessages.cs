namespace InventoryManagement.Application.Exceptions
{
    public static class ExceptionMessages
    {
        public const string OutOfInventory = "متاسفانه موجودی محصول کافی نیست.";
        public const string QuantityGreaterThanZero = "لطفاً مقدار وارد شده باید بیشتر از صفر باشد.";
        public const string ProductNotFound = "محصول مورد نظر یافت نشد. لطفاً دوباره بررسی کنید.";
        public const string InventoryChangeNotFound = "فاکتور مورد نظر یافت نشد. لطفاً دوباره بررسی کنید.";
    }
}
