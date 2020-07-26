using StrategyPatternCustomUserAuth.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyPatternCustomUserAuth.Services.RequestUserInfoWrapper
{
    public class RequestUserInfoWrapper
    {
        private readonly Func<Task<IRequestUserInfo>> _userInfoFactory;
        private IRequestUserInfo _userInfo;

        public RequestUserInfoWrapper(Func<Task<IRequestUserInfo>> userInfoFactory)
        {
            _userInfoFactory = userInfoFactory;
        }

        /// <summary>
        /// Получить данные пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<IRequestUserInfo> GetUserInfo()
        {
            if (_userInfo == null)
            {
                _userInfo = await _userInfoFactory();
            }

            return _userInfo;
        }

        /// <summary>
        /// Пользователь авторизован
        /// </summary>
        public async Task<bool> IsAuthorized()
        {
            return await GetUserInfo() != null;
        }
    }
}
