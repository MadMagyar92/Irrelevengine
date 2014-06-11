using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Completely_Irrelevant
{
    public class GamePadController : IController
    {
        private GamePadState newButtons;
        private GamePadState oldButtons;
        private Dictionary<Actions, List<Buttons>> actionToButtonMap;

        public GamePadController()
        {
            newButtons = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
            oldButtons = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
            actionToButtonMap = new Dictionary<Actions, List<Buttons>>();
            AddDefaultMappings();
        }

        private void AddDefaultMappings()
        {
            actionToButtonMap.Add(Actions.Quit, new List<Buttons>() { Buttons.Back });
            actionToButtonMap.Add(Actions.MoveLeft, new List<Buttons>() { Buttons.DPadLeft });
            actionToButtonMap.Add(Actions.MoveRight, new List<Buttons>() { Buttons.DPadRight });
            actionToButtonMap.Add(Actions.Jump, new List<Buttons>() { Buttons.B });
            actionToButtonMap.Add(Actions.PickUp, new List<Buttons>() { Buttons.A });
            actionToButtonMap.Add(Actions.Drink, new List<Buttons>() { Buttons.X });
            actionToButtonMap.Add(Actions.Reset, new List<Buttons>() { Buttons.Start });
        }

        public void Update()
        {
            oldButtons = newButtons;
            newButtons = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
        }

        public bool IsActionActive(Actions action)
        {
            bool returnValue = false;

            if (action != Actions.Drink && action != Actions.Reset)
            {
                foreach (Buttons button in actionToButtonMap[action])
                {
                    returnValue = returnValue || oldButtons.IsButtonDown(button);
                }
            }

            else
            {
                foreach (Buttons button in actionToButtonMap[action])
                {
                    returnValue = returnValue || (newButtons.IsButtonUp(button) && oldButtons.IsButtonDown(button));
                }
            }

            return returnValue;
        }

        public bool IsConnected()
        {
            return newButtons.IsConnected;
        }
    }
}
