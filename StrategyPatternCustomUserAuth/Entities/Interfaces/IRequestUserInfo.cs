using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyPatternCustomUserAuth.Entities.Interfaces
{
    public interface IRequestUserInfo
    {
        /// <summary>
        /// Логин
        /// </summary>
        string Login { get; }

        /// <summary>
        /// ФИО
        /// </summary>
        string Name { get; }
    }
}
