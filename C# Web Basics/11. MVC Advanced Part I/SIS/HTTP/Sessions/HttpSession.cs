using HTTP.Common;
using System.Collections.Generic;

namespace HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private Dictionary<string, object> sessionParameters;

        public HttpSession(string id)
        {
            this.sessionParameters = new Dictionary<string, object>();
            this.Id = id;
        }

        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            // overwrite, cause there are cases, in which we need to overwrite session parameter - e.g. shopping cart
            this.sessionParameters[name] = parameter;
        }

        public void ClearParameters()
        {
            this.sessionParameters.Clear();
        }

        public bool ContainsParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return this.sessionParameters.ContainsKey(name);
        }

        public object GetParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            if (this.ContainsParameter(name))
            {
                return this.sessionParameters[name];
            }

            // throw exception vs return null;
            return null;
        }
    }
}
