using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Completely_Irrelevant
{
    public class KeyboardController : IController
    {
        private KeyboardState newKeys, oldKeys;
        private Dictionary<Actions, List<Keys>> actionToKeyMap;

        public KeyboardController()
        {
            oldKeys = Keyboard.GetState();
            newKeys = Keyboard.GetState();
            actionToKeyMap = new Dictionary<Actions, List<Keys>>();
            AddDefaultMappings();
        }

        private void AddDefaultMappings()
        {
            actionToKeyMap.Add(Actions.Quit, new List<Keys>() { Keys.Escape } );
            actionToKeyMap.Add(Actions.MoveLeft, new List<Keys>() { Keys.Left, Keys.A } );
            actionToKeyMap.Add(Actions.MoveRight, new List<Keys>() { Keys.Right, Keys.D } );
            actionToKeyMap.Add(Actions.Jump, new List<Keys>() { Keys.Up, Keys.W } );
            actionToKeyMap.Add(Actions.PickUp, new List<Keys>() { Keys.E } );
            actionToKeyMap.Add(Actions.Drink, new List<Keys>() { Keys.Q } );
            actionToKeyMap.Add(Actions.Reset, new List<Keys>() { Keys.T });
        }
            
        public void Update()
        {
            oldKeys = newKeys;
            newKeys = Keyboard.GetState();
        }

        public bool IsActionActive(Actions action)
        {
            bool returnValue = false;

            if (action != Actions.Drink && action != Actions.Reset)
            {
                foreach (Keys key in actionToKeyMap[action])
                {
                    returnValue = returnValue || newKeys.IsKeyDown(key);
                }
            }

            else
            {
                foreach (Keys key in actionToKeyMap[action])
                {
                    returnValue = returnValue || (newKeys.IsKeyUp(key) && oldKeys.IsKeyDown(key));
                }
            }
                
            return returnValue;
        }

        public bool IsConnected()
        {
            return true;
        }
    }
}
