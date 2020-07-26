using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StrategyPatternCustomUserAuth.Services.RequestUserInfoService.Interfaces;

namespace StrategyPatternCustomUserAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRequestUserInfoService _userInfoService;

        public ValuesController(IRequestUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        /// <summary>
        /// Делая запрос, обязательно укажи хедер Authorization : auth(любая строка)
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            var userInfo = await _userInfoService.GetUserInfo();
            return  $"{userInfo.Login} {userInfo.Name}";
        }
    }
}
