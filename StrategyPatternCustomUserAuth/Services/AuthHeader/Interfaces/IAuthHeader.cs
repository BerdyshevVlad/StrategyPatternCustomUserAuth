namespace StrategyPatternCustomUserAuth.Services.AuthHeader.Interfaces
{
    public interface IAuthHeader
    {
        /// <summary>
        /// Содержимое заголовка
        /// </summary>
        string Content { get; }

        bool IsHeaderProvided { get; }
    }
}
