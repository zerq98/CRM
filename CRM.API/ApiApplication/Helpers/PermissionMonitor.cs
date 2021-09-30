using ApiDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Helpers
{
    public static class PermissionMonitor
    {
        public static async Task<bool> CheckPermissionsAsync(IUserRepository userRepository,string userId,string requiredPermission)
        {
            var claims = await userRepository.GetUserClaims(userId);

            var haveAccess = false;

            foreach (var claim in claims)
            {
                if (claim == requiredPermission) haveAccess = true;
            }

            return haveAccess;
        }
    }
}
