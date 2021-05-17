using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.Common.Security
{
    public class SessionContext
    {
        public static readonly string _Session = "__Session__";

        private static IHttpContextAccessor _httpContextAccessor;
        private static ISession _session => _httpContextAccessor.HttpContext.Session;


        public SessionContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static SessionModel SessionModel = new SessionModel();
        public bool SetSession()
        {
            try
            {
                _session.SetObjectAsJson(_Session, JsonConvert.SerializeObject(SessionModel));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SetSession(SessionModel _SessionModel)
        {
            try
            {
                _session.SetObjectAsJson(_Session, JsonConvert.SerializeObject(_SessionModel));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SessionModel Current()
        {
            return _session.GetObjectFromJson<SessionModel>(_Session);
        }
        public static bool Destroy()
        {
            _session.Remove(_Session);
            return true;
        }

    }
    public class SessionModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
