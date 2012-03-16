using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SXL.ComponentFramework.Exceptions;

namespace SXL.ComponentFramework
{
    public abstract class GameObject : IUpdate, IDraw
    {
        /// <summary>
        /// Name of the Actor (by default it is the name of the class)
        /// </summary>
        protected String name;


        //private readonly HashSet<Type> subscribedMessageTypes = new HashSet<Type>();

        private readonly Dictionary<Type, List<SubscriberInfo>> subscribers = new Dictionary<Type, List<SubscriberInfo>>();

        /// <summary>
        /// Component holder
        /// </summary>
        private readonly Dictionary<String, GameComponent> components = new Dictionary<String, GameComponent>();


        protected GameObject()
        {
            name = GetType().Name;

            InitializeMessageSubscriptions();
        }

        protected GameObject(String name)
        {
            this.name = name;

            InitializeMessageSubscriptions();
        }

        /// <summary>
        /// Finds all OnMessage override functions that handle specific functions
        /// Works as an automatic subscription system :)
        /// </summary>
        private void InitializeMessageSubscriptions()
        {
            //get the precise derived type
            Type type = GetType();

            //get all the methods
            MethodInfo[] methodInfos = type.GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                //the actorcomponent contains the default OnMessage function, ignore it
                if (methodInfo.Name == "OnMessage" && methodInfo.DeclaringType != typeof(GameComponent))
                {
                    ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                    if (parameterInfos.Length == 1)
                    {
                        Type parameterType = parameterInfos[0].ParameterType;
                        
                        if (parameterType == typeof(GameEvent) || parameterType.IsSubclassOf(typeof(GameEvent)))
                        {
                            Subscribe(new SubscriberInfo(methodInfo,this),parameterType);
                            //subscribedMessageTypes.Add(parameterInfos[0].GetType());
                        }
                    }
                }
            }
        }


        internal void Subscribe(SubscriberInfo subscriberInfo, Type gameMessageType)
        {
            if (!subscribers.ContainsKey(gameMessageType))
                subscribers.Add(gameMessageType, new List<SubscriberInfo>());
            
            subscribers[gameMessageType].Add(subscriberInfo);
        }

        protected virtual void AddComponent(GameComponent gameComponent)
        {
            //adds the component to the dictionary
            components.Add(gameComponent.Name, gameComponent);

            //sets the owner as this one
            gameComponent.Owner = this;

            gameComponent.InitializeMessageSubscriptions();

            gameComponent.Initialize();
        }


        /// <summary>
        /// Update function.
        /// Updates all IUpdate Components
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            foreach (IUpdate actorComponent in components.Values.OfType<IUpdate>())
            {
                actorComponent.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw function.
        /// Draws all IDraw Components
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spritebatch"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            foreach (IDraw actorComponent in components.Values.OfType<IDraw>())
            {
                actorComponent.Draw(gameTime, spritebatch);
            }
        }

        #region Specific Properties
        
        public T GetComponent<T>()
        {
            String componentName = GameComponent.ComponentName(typeof (T));

            if (!components.ContainsKey(componentName))
                throw new MissingComponentException(componentName);

            return (T)(Object)components[componentName];
        }

        #endregion



        protected void Send(GameEvent gameEvent)
        {
            Send(gameEvent, this);
        }

        

        public void Send(GameEvent gameEvent, Object origin)
        {
            //create a gameEvent instance
            gameEvent.Origin = origin;
            //GameEvent GameEvent = new GameEvent(gameEvent, parameters, this);
            
            Type messageType = gameEvent.GetType();
            if(subscribers.ContainsKey(messageType))
            {
                List<SubscriberInfo> messageSubscribers = subscribers[messageType];
                foreach (SubscriberInfo messageSubscriber in messageSubscribers)
                {
                    messageSubscriber.OnMessage(gameEvent);
                    //GameMessage2 m = (typeof(GameMessage2)) gameEvent;

                    //(subscriberTypes[messageSubscriber])Convert.ChangeType(messageSubscriber,subscriberTypes[messageSubscriber]).OnMessage(@gameEvent);
                }
            }

            if(subscribers.ContainsKey(typeof(GameEvent)))
            {
                List<SubscriberInfo> messageSubscribers = subscribers[typeof(GameEvent)];
                foreach (SubscriberInfo messageSubscriber in messageSubscribers)
                {
                    messageSubscriber.OnMessage(gameEvent);
                }
            }

            //pass it to the components first)
            /*foreach (GameComponent actorComponent in components.Values)
            {
                actorComponent.Receive(@gameEvent);
            }*/

            //receive all the messages
            //Receive(@gameEvent);
        }

        /*public void Receive(GameEvent GameEvent)
        {
            OnMessage(GameEvent);
        }*/

        public virtual void OnMessage(GameEvent gameEvent)
        {
        }
    }
}
