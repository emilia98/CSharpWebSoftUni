using HTTP.Exceptions;
using System;

namespace HTTP.Common
{
    public class CoreValidator
    {
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ThrowIfNullOrEmpty(string text, string name)
        {
            if(string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"{name} cannot be null or empty.", name);
            }
        }

        public static void ThrowIfNullOrEmptyInQueryParams(string text, string name)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new BadRequestException(string.Format("The query {0} cannot be null or empty!", name));
            }
        }

        public static void ThrowIfNullOrEmptyInRequestBody(string text, string name)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new BadRequestException(string.Format("The request body {0} cannot be null or empty!", name));
            }
        }
    }
}
