using System.ComponentModel.DataAnnotations;

namespace PremierRoom.API;

public class ApiResponse<TData> where TData : class
{
    [Required]
    public required TData Data { get; set; }

    private ApiResponse() { }

    public static ApiResponse<TData> From(TData response)
    {
        return new ApiResponse<TData>
        {
            Data = response
        };
    }
}

