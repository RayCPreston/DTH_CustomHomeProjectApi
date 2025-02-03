using DTH.API.Models;

namespace DTH.API.Utility
{
    public static class UserUtil
    {
        /// <summary>
        /// Converts a UserDTO to a User
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>UserDTO</returns>
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO(
                user.Username,
                user.Email,
                user.FirstName,
                user.LastName
            );
        }

        /// <summary>
        /// Converts a UserDTO into a User
        /// </summary>
        /// <param name="userDTO">Userdto</param>
        /// <returns>User</returns>
        public static User ToUser(this UserDTO userDTO)
        {
            return new User
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName
            };
        }

        /// <summary>
        /// Validates a User's required properties
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>bool</returns>
        public static bool IsValid(this User user)
        {
            if(user.Username == null || user.Username.Length == 0)
            {
                return false;
            }
            if (user.Password == null || user.Username.Length == 0)
            {
                return false;
            }
            return true;
        }
    }
}
