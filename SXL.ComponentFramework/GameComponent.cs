using System;
using System.Reflection;

namespace SXL.ComponentFramework
{
    public abstract class GameComponent
    {
        protected GameObject owner;

        protected String name;

        protected GameComponent()
        {
            //Reflection, does it only once
            name = ComponentName(GetType());
        }

        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Finds all OnMessage override functions that handle specific functions
        /// Works as an automatic subscription system :)
        /// </summary>
        internal void InitializeMessageSubscriptions()
        {
            //get the precise derived type
            Type type = GetType();

            //get all the methods
            MethodInfo[] methodInfos = type.GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                //the actorcomponent contains the default OnMessage function, ignore it
                if(methodInfo.Name == "OnMessage" && methodInfo.DeclaringType != typeof(GameComponent))
                {
                    ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                    if(parameterInfos.Length == 1)
                    {
                        Type parameterType = parameterInfos[0].ParameterType;
                        if (parameterType == typeof(GameEvent) || parameterType.IsSubclassOf(typeof(GameEvent)))
                        {
                            owner.Subscribe(new SubscriberInfo(methodInfo, this), parameterType);
                            //subscribedMessageTypes.Add();
                        }
                    }
                }
            }
        }

        public static String ComponentName(Type type)
        {
            return type.Name.Replace("Component", "");
        }

        /// <summary>
        /// Allows actor ownership edition by the actor class, 
        /// but not any entities inside another assembly
        /// </summary>
        internal GameObject Owner
        {
            set { owner = value; }
        }

        public String Name
        {
            get { return name; }
        }

        public T GetComponent<T>()
        {
            return owner.GetComponent<T>();
        }

        protected void Send(GameEvent gameEvent)
        {
            owner.Send(gameEvent, this);
        }
        
        /*internal void Receive(GameEvent GameEvent)
        {
            //if the class has a function dealing with this kind of @event, deliver the @event
            if(subscribedMessageTypes.Contains(GameEvent.GetType()))
                OnMessage(GameEvent);
        }*/

        public virtual void OnMessage(GameEvent gameEvent)
        {
        }
    }
}
