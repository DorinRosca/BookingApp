namespace Booking.Application.Responses
{
     public class BaseResponse
     {
          public BaseResponse()
          {
               Success = true;
               IsEmpty = false;
          }
          public BaseResponse(string message)
          {
               Success = true;
               BaseMessage = message;
          }

          public BaseResponse(string message, bool success)
          {
               Success = success;
               BaseMessage = message;
          }
          public bool IsEmpty { get; set; }
          public bool Success { get; set; }
          public string BaseMessage { get; set; } = string.Empty;
          public List<string>? ValidationErrors { get; set; }
     }
}
