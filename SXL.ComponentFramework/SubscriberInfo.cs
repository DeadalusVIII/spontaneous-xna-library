using System;
using System.Reflection;
using FastInvoker;

namespace SXL.ComponentFramework
{
    internal class SubscriberInfo
    {
        private readonly FastInvokeHandler handler;
        private readonly Object subscriber;

        public SubscriberInfo(MethodInfo info, object subscriber)
        {
            this.subscriber = subscriber;
            handler = BaseMethodInvoker.GetMethodInvoker(info);
        }

        public void OnMessage(GameEvent gameEvent)
        {
            handler.Invoke(subscriber, gameEvent);
        }
    }
}
