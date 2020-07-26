using StrategyPatternCustomUserAuth.Entities.Interfaces;
using System.Threading.Tasks;

namespace StrategyPatternCustomUserAuth.Services.RequestUserInfoService.Interfaces
{
    /// <summary>
    /// Интерфейс по работе с текущим пользователем
    /// </summary>
    public interface IRequestUserInfoService
    {
        /// <summary>
        /// Информация и пользователе
        /// </summary>
        Task<IRequestUserInfo> GetUserInfo();

        /// <summary>
        /// Пользователь авторизован
        /// </summary>
        Task<bool> IsAuthorized();

        /// <summary>
        /// Используется для вызова функции, внутри которой не должно быть обращения к данным пользователя
        /// </summary>
        bool BlockUserInfo { get; set; }
    }
}
