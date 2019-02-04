using System;
using Notifications.Common.Interfaces;
using Notifications.Common.Models;

namespace Notifications.Utils
{
    public class BodyParser : IBodyParser
    {
        public string ParseEventBody(string body, EventData data)
        {
            var engine = new Stringy.Stringy();

            foreach (var prop in data.GetType().GetProperties())
            {
                engine.Set(prop.Name, prop.GetValue(data, null));
            }

            return engine.Execute(body);

        }
    }
}
