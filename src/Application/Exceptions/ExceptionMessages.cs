namespace InventoryManagement.Application.Exceptions
{
    public static class ExceptionMessages
    {
        public const string OutOfInventory = "متاسفانه موجودی محصول کافی نیست.";
        public const string QuantityGreaterThanZero = "لطفاً مقدار وارد شده باید بیشتر از صفر باشد.";
        public const string ProductNotFound = "محصول مورد نظر یافت نشد. لطفاً دوباره بررسی کنید.";
        public const string IssuanceDocumentNotFound = "فاکتور مورد نظر یافت نشد. لطفاً دوباره بررسی کنید.";
        public const string BrandNameIsRequired = "نام برند محصول الزامی است.";
        public const string SerialNumbersIsRequired = "شماره سریال الزامی است.";
        public const string ExistIssuanceDocumentItemsIsRequired = "محصول انبار برای ثبت فاکتور الزامی است.";
    }
}
