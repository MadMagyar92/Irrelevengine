using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public abstract class ItemSet : IUpdatable, IDrawable
    {
        public List<ICollidable> Collidables;
        public List<IReceiver> Receivers;
        public List<ISender> Senders;

        protected InvokingMode mode;
        protected bool hasNotifiedReceivers;

        protected ItemSet(InvokingMode invokingMode)
        {
            this.Receivers = new List<IReceiver>();
            this.Senders = new List<ISender>();
            this.Collidables = new List<ICollidable>();
            this.mode = invokingMode;
            this.hasNotifiedReceivers = false;
        }

        protected ItemSet()
        {
            this.Receivers = new List<IReceiver>();
            this.Senders = new List<ISender>();
            this.mode = InvokingMode.Single;
            this.hasNotifiedReceivers = false;
        }

        public virtual void RegisterReceiver(IReceiver receiver)
        {
            Receivers.Add(receiver);

            if (receiver is ICollidable)
            {
                Collidables.Add((ICollidable)receiver);
            }
        }

        public virtual void RegisterSender(ISender sender)
        {
            Senders.Add(sender);

            if (sender is ICollidable)
            {
                Collidables.Add((ICollidable)sender);
            }
        }

        public virtual void RegisterReceiver(List<IReceiver> receivers)
        {
            foreach (IReceiver receiver in receivers)
            {
                this.Receivers.Add(receiver);

                if (receiver is ICollidable)
                {
                    Collidables.Add((ICollidable)receiver);
                }
            }
        }

        public virtual void RegisterSender(List<ISender> senders)
        {
            foreach (ISender sender in senders)
            {
                this.Senders.Add(sender);

                if (sender is ICollidable)
                {
                    Collidables.Add((ICollidable)sender);
                }
            }
        }

        public virtual void Update()
        {
            if ((!hasNotifiedReceivers && mode == InvokingMode.Single) || mode == InvokingMode.Multiple)
            {

                bool activateReceivers = true;
                List<string> messages = new List<string>();

                foreach (ISender sender in Senders)
                {
                    activateReceivers = activateReceivers && sender.ShouldNotifyReceivers;
                    messages.Add(sender.MessageToReceivers);
                }

                if (activateReceivers)
                {
                    SignalReceivers(messages);
                    hasNotifiedReceivers = true;
                }
            }

            foreach (ISender sender in Senders)
            {
                sender.Update();
            }

            foreach (IReceiver receiver in Receivers)
            {
                receiver.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach(ISender sender in Senders)
            {
                sender.Draw(spriteBatch, camera);
            }

            foreach (IReceiver receiver in Receivers)
            {
                receiver.Draw(spriteBatch, camera);
            }
        }

        public virtual void SignalReceivers(List<string> messages)
        {
            foreach (IReceiver receiver in Receivers)
            {
                receiver.Receive(messages);
            }
        }

        public virtual void SetInvokingMode(InvokingMode invokingMode)
        {
            this.mode = invokingMode;
        }
    }
}
