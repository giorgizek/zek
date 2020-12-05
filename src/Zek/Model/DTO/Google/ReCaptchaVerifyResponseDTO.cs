using System;
using System.Text.Json.Serialization;

namespace Zek.Model.DTO.Google
{
    public class ReCaptchaVerifyResponseDTO
    {
        public bool? Success { get; set; }

        [JsonPropertyName("challenge_ts")]
        public DateTime? ChallengeLoadTimestamp { get; set; }

        public string Hostname { get; set; }
        
        public string Action { get; set; }

        public decimal? Score { get; set; }

        [JsonPropertyName("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
