using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionSupportSystem.PresentationLayer
{
    public static class Mediator
    {
        private static readonly IDictionary<string, List<Action<object>>> PlDict =
            new Dictionary<string, List<Action<object>>>();

        public static void Subscribe(string token, Action<object> callback)
        {
            if (!PlDict.ContainsKey(token))
            {
                var list = new List<Action<object>> {callback};
                PlDict.Add(token, list);
            }
            else
            {
                var found = false;
                foreach (var item in PlDict[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    PlDict[token].Add(callback);
            }
        }

        public static void Unsubscribe(string token, Action<object> callback)
        {
            if (PlDict.ContainsKey(token))
                PlDict[token].Remove(callback);
        }

        public static void Notify(string token, object args = null)
        {
            if (!PlDict.ContainsKey(token)) return;
            foreach (var callback in PlDict[token])
                callback(args);
        }
    }
}
