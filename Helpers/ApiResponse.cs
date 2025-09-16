namespace be_learn_dotnet.Helpers
{
  public class ApiResponse
  {
    public string? Status { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    // Opsional: hanya untuk list dengan pagination
    public int? Page { get; set; }
    public int? Size { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }
    public int? CurrentPage { get; set; }
    public int? TotalPage { get; set; }

    // Untuk response sukses tanpa pagination
    public static ApiResponse Success(string message = "success", object? data = null)
    {
      return new ApiResponse
      {
        Status = "success",
        Message = message,
        Data = data
      };
    }

    // Untuk response sukses dengan pagination
    public static ApiResponse SuccessWithPagination(
      string message = "success",
      object? data = null,
      int? page = null,
      int? size = null,
      int? from = null,
      int? to = null,
      int? currentPage = null,
      int? totalPage = null
    )
    {
      return new ApiResponse
      {
        Status = "success",
        Message = message,
        Data = data,
        Page = page,
        Size = size,
        From = from,
        To = to,
        CurrentPage = currentPage,
        TotalPage = totalPage
      };
    }

    // Untuk error response
    public static ApiResponse Error(string message = "error", object? data = null)
    {
      return new ApiResponse
      {
        Status = "error",
        Message = message,
        Data = data
      };
    }
  }
}
