using StrategyPatternCustomUserAuth.Entities;
using StrategyPatternCustomUserAuth.Entities.Interfaces;
using System.Threading.Tasks;

namespace StrategyPatternCustomUserAuth.Services.UserMainInfoService
{
    /// <summary>
    /// Иммитация получения юзера со стороннего сервиса
    /// </summary>
    public class UserMainInfoService
    {
        public async Task<IRequestUserInfo> GetUserAsync(string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                return null;
            }

            return new RequestUserInfo("LoginName", "UserName");
        }
    }
}
