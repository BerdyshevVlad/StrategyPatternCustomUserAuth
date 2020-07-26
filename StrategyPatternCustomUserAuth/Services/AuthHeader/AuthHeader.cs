using StrategyPatternCustomUserAuth.Services.AuthHeader.Interfaces;

namespace StrategyPatternCustomUserAuth.Services.AuthHeader
{
    /// <summary>
    /// Заголовок аутентификации
    /// </summary>
    public class AuthHeader : IAuthHeader
    {
        public AuthHeader(string content)
        {
            Content = content;
        }

        /// <summary>
        /// Содержимое заголовка
        /// </summary>
        public string Content { get; }

        public bool IsHeaderProvided => !string.IsNullOrWhiteSpace(Content);
    }
}
