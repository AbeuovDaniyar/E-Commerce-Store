using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CapstoneProject.Helper
{
    public static class Session
    {

        public static T GetObjectFromJson<T>(this ISession session, string key) 
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value); 
        }

        internal static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
