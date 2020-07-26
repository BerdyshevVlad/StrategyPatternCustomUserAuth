using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StrategyPatternCustomUserAuth.Services.RequestMessageAccessor
{
    public class RequestMessageAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestMessageAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Текущий HttpRequestMessage с проверкой на null
        /// </summary>
        /// <exception cref="System.Exception">Текущий HttpRequestMessage равен null</exception>
        public HttpRequest CurrentMessage
        {
            get
            {
                HttpRequest httpRequestMessage = GetMessage();

                if (httpRequestMessage == null)
                {
                    throw new System.Exception("Доступ к данному свойству должен происходить только внутри HTTP запроса." +
                        "При необходимости запуска метода данный сервис нужно подменить.");
                }

                return httpRequestMessage;
            }
        }

        /// <summary>
        /// Текущий HttpRequestMessage без проверки на null
        /// </summary>
        public HttpRequest CurrentMessageNullable => GetMessage();

        private HttpRequest GetMessage()
        {
            return _httpContextAccessor.HttpContext.Request;
        }
    }
}
