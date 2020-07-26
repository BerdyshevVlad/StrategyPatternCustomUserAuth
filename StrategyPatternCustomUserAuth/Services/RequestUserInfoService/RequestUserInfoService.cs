using StrategyPatternCustomUserAuth.Entities.Interfaces;
using StrategyPatternCustomUserAuth.Services.RequestUserInfoService.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace StrategyPatternCustomUserAuth.Services.RequestUserInfoService
{
    public class RequestUserInfoService: IRequestUserInfoService
    {
        // To prevent security problems in singletons
        private readonly Func<Services.RequestUserInfoWrapper.RequestUserInfoWrapper> _getRequestUserInfo;


        /// <summary>
        /// Конструктор
        /// </summary>
        public RequestUserInfoService(IServiceProvider container)
        {
            _getRequestUserInfo = () => container.GetService<RequestUserInfoWrapper.RequestUserInfoWrapper>();
        }

        public async Task<bool> IsAuthorized()
        {
            return await _getRequestUserInfo().IsAuthorized();
        }

        public bool BlockUserInfo { get; set; } = false;


        public async Task<IRequestUserInfo> GetUserInfo()
        {
            var userInfo = await _getRequestUserInfo().GetUserInfo();
            if (userInfo == null)
            {
                throw new Exception(HttpStatusCode.Unauthorized.ToString());
            }

            return userInfo;
        }
    }
}

