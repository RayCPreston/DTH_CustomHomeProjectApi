using System.Net.Http.Headers;
using System.Text;
using DTH.API.Models;
using DTH.API.Services.UserServices;
using Microsoft.Extensions.Primitives;

namespace DTH.API.Utility
{
    public static class HeaderUtil
    {
        /// <summary>
        /// Validates the Authorization header is not null, is basic authentication, and
        /// the user credentials exist in the user DB.
        /// </summary>
        /// <param name="headers">IHeaderDictionary</param>
        /// <returns>bool</returns>
        public static bool AuthorizationHeaderNotNullAndValid(this IHeaderDictionary headers, GetUserService _getUserService)
        {
            StringValues authHeaderValues;
            if (!headers.TryGetValue("Authorization", out authHeaderValues))
            {
                return false;
            }
            AuthenticationHeaderValue authHeaderValue = AuthenticationHeaderValue.Parse(authHeaderValues.ToString());
            if (authHeaderValue.Scheme != "Basic")
            {
                return false;
            }
            if (!authHeaderValue.IsValidUser(_getUserService))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the user credentials exist and match the user credentials in the user DB.
        /// </summary>
        /// <param name="authHeaderValue">AuthenticationHeaderValue</param>
        /// <returns>bool</returns>
        /// <exception cref="Exception">Thrown when GetUserService is null</exception>
        public static bool IsValidUser(this AuthenticationHeaderValue authHeaderValue, GetUserService _getUserService)
        {
            string? authString = authHeaderValue.Parameter;
            if (authString == null || authString.Length == 0)
            {
                return false;
            }
            byte[] data = Convert.FromBase64String(authString);
            string decodedString = Encoding.UTF8.GetString(data);
            string[] credentialsArray = decodedString.Split(':');
            if (credentialsArray.Length != 2)
            {
                return false;
            }
            string username = credentialsArray[0];
            string password = credentialsArray[1];
            if (_getUserService == null)
            {
                throw new Exception("Failed to find GetUserService in HeaderUtil.");
            }
            User? user = _getUserService.GetUser(username);
            if (user == null)
            {
                return false;
            }
            if (password == null || password != user.Password)
            {
                return false;
            }
            return true;
        }
    }
}
