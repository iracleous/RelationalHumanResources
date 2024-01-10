namespace RelationalHumanResources.Dtos
{
    public class ApiResult<T>
    {
        public T? Data { get; set; }
        public int  Status { get; set; }
        public string Message { get; set; }=string.Empty;
    }
}
