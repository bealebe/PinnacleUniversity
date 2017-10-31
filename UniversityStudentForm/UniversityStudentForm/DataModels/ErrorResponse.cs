using Newtonsoft.Json;

namespace PinnacleUniversity.DataModels
{
    /// <summary>
    /// Object to hold the error message that would come back from a request
    /// </summary>
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public ErrorResponse(string message)
        {
            Message = message;
        }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}