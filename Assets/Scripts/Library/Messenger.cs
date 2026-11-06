using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.Utility;

namespace Swimmer.Library
{
    public static class Messenger
    {
        private static readonly Dictionary<string, BroadcastHandler> BROADCAST_HANDLERS = new Dictionary<string, BroadcastHandler>();
        private static readonly Dictionary<string, BroadcastBundleHandler> BROADCAST_BUNDLE_HANDLERS = new Dictionary<string, BroadcastBundleHandler>();

        private static readonly Bundle EMPTY_BUNDLE = new Bundle();


        public static void AddListener(string eventType, BroadcastHandler callback)
        {
            if (!BROADCAST_HANDLERS.ContainsKey(eventType))
                BROADCAST_HANDLERS.Add(eventType, null);

            BROADCAST_HANDLERS[eventType] += callback;
        }
        public static void AddListener(string eventType, BroadcastBundleHandler callback)
        {
            if (!BROADCAST_BUNDLE_HANDLERS.ContainsKey(eventType))
                BROADCAST_BUNDLE_HANDLERS.Add(eventType, null);

            BROADCAST_BUNDLE_HANDLERS[eventType] += callback;
        }

        public static void RemoveListener(string eventType, BroadcastHandler callback)
        {
            BROADCAST_HANDLERS[eventType] -= callback;
            if (BROADCAST_HANDLERS[eventType] == null)
                BROADCAST_HANDLERS.Remove(eventType);
        }

        public static void RemoveListener(string eventType, BroadcastBundleHandler callback)
        {
            BROADCAST_BUNDLE_HANDLERS[eventType] -= callback;
            if (BROADCAST_BUNDLE_HANDLERS[eventType] == null)
                BROADCAST_BUNDLE_HANDLERS.Remove(eventType);
        }
        
        public static void RemoveListenerIfHas(string eventType, BroadcastHandler callback)
        {
            if (Has(eventType, callback))
            {
                BROADCAST_HANDLERS[eventType] -= callback;
                if (BROADCAST_HANDLERS[eventType] == null)
                    BROADCAST_HANDLERS.Remove(eventType);
            }
        }
        public static void RemoveListenerIfHas(string eventType, BroadcastBundleHandler callback)
        {
            if (Has(eventType, callback))
            {
                BROADCAST_BUNDLE_HANDLERS[eventType] -= callback;
                if (BROADCAST_HANDLERS[eventType] == null)
                    BROADCAST_HANDLERS.Remove(eventType);
            }
        }

        public static void Broadcast(string eventType, Bundle bundle = null)
        {
            if (BROADCAST_HANDLERS.ContainsKey(eventType))
                BROADCAST_HANDLERS[eventType]();
            if (BROADCAST_BUNDLE_HANDLERS.ContainsKey(eventType))
                BROADCAST_BUNDLE_HANDLERS[eventType](bundle ?? EMPTY_BUNDLE);
        }

        public static bool Has(string eventType, BroadcastHandler callback)
        {
            if (BROADCAST_HANDLERS.ContainsKey(eventType) && BROADCAST_HANDLERS.ContainsValue(callback))
            {
                return true;
            }

            return false;
        }
        public static bool Has(string eventType, BroadcastBundleHandler callback)
        {
            if (BROADCAST_HANDLERS.ContainsKey(eventType) && BROADCAST_BUNDLE_HANDLERS.ContainsValue(callback))
            {
                return true;
            }

            return false;
        }


        public delegate void BroadcastHandler();
        public delegate void BroadcastBundleHandler(Bundle bundle);
    }


    public class Bundle
    {
        private readonly Dictionary<string, object> _data = new Dictionary<string, object>();

        public T Get<T>(string key)
        {
            return (T) (_data.ContainsKey(key) ? _data[key] : typeof(T).DefaultValue());
        }

        public Bundle Set(string key, object value)
        {
            _data[key] = value;
            return this;
        }
    }
}