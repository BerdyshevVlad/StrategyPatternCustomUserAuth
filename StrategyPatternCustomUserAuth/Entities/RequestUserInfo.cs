using StrategyPatternCustomUserAuth.Entities.Interfaces;

namespace StrategyPatternCustomUserAuth.Entities
{
    public class RequestUserInfo : IRequestUserInfo
    {
        public string Login { get; }
        public string Name { get; }

        /// <summary>
        ///  Default constructor
        /// </summary>
        public RequestUserInfo(
            string login,
            string name)
        {
            Login = login;
            Name = name;
        }
    }
}
