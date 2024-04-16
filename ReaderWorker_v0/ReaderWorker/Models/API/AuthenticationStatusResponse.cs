using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReaderWorker.Models.API
{
    public class AuthenticationStatusResponse
    {
        /// <summary>
        /// Authentication status of the transponder key
        /// </summary>
        [JsonPropertyName("authenticated")]
        public bool Authenticated { get; set; }

        /// <summary>
        /// Authenticated permission
        /// </summary>
        [JsonPropertyName("permission")]
        public Permission Permission { get; set; }

        /// <summary>
        /// Status of authentication process
        /// </summary>
        [JsonPropertyName("authenticationStatus")]
        public AuthenticationStatus AuthenticationStatus { get; set; }

        /// <summary>
        /// Reason for failed authentication
        /// </summary>
        [JsonPropertyName("failureReason")]
        public AuthenticationFailureReason FailureReason { get; set; }

        /// <summary>
        /// Security ID
        /// </summary>
        [JsonPropertyName("securityId")]
        public string securityId { get; set; }

        /// <summary>
        /// Order number of the transponder key
        /// </summary>
        [JsonPropertyName("orderNo")]
        public uint OrderNumber { get; set; }

        /// <summary>
        /// Serial number of the transponder key
        /// </summary>
        [JsonPropertyName("serialNo")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// User data
        /// </summary>
        [JsonPropertyName("userData")]
        public List<UserDataValue> UserData { get; set; }
    }

}
