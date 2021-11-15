
namespace Axon.Domain.ValueObjects
{
    public class UserRoleTypes
    {

        public static int Admin = 1;
        public static int User = 2;

        public static string GetRoleNameById(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "Admin";
                case 2:
                    return "User";
                default:
                    return "Anonymous";
            }
        }
        public static int GetRoleIdByName(string roleTitle)
        {
            var lowerCaseRoleTitle = roleTitle.ToLower();
            switch (lowerCaseRoleTitle)
            {
                case "admin":
                    return Admin;
                case "user":
                    return User;
                default:
                    return 0;
            }
        }
    }
}
