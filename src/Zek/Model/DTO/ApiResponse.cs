namespace Zek.Model.DTO
{
    public class ApiResponse
    {
        public bool Success { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Value { get; set; }
    }
}
