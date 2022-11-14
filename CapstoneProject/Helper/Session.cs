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

        /// <summary>
        /// Parses Json into Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns>T</returns>
        public static T GetObjectFromJson<T>(this ISession session, string key) 
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value); 
        }

        /// <summary>
        /// Parses object into Json
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Sets cart session to empty
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        public static void SetSessionToNull(this ISession session, string key) 
        {
            session.SetString(key, "");
        }
    }
}
