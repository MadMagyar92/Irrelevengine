using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public static class ClockFactory
    {
        private static Dictionary<int, Timer> registeredTimers;

        public static void Setup()
        {
            registeredTimers = new Dictionary<int, Timer>();
        }

        public static int RegisterTimer(TimeSpan amountOfTime)
        {
            Random random = new Random();
            int retVal = random.Next();

            while (registeredTimers.ContainsKey(retVal))
            {
                retVal = random.Next();
            }

            Timer timer = new Timer(retVal, amountOfTime);
            registeredTimers.Add(retVal, timer);

            return retVal;
        }

        public static TimeSpan GetTimeRemaining(int id)
        {
            Timer timer = registeredTimers[id];
            return timer.GetTimeRemaining();
        }

        public static bool IsTimerFinished(int id)
        {
            Timer timer = registeredTimers[id];
            return timer.IsFinished;
        }

        public static void StartTimer(int id)
        {
            Timer timer = registeredTimers[id];
            timer.Start();
        }

        public static void StopTimer(int id)
        {
            Timer timer = registeredTimers[id];
            timer.Stop();
        }

        // Resets the timer to its initial time. Initial time is set in the constructor and overridden if you call SetTimerWithTime()
        // Note you must call Start() to restart the Timer after calling ResetTimer()
        public static void ResetTimer(int id)
        {
            Timer timer = registeredTimers[id];
            timer.Reset();
        }

        // Note, this overwrites the initial time. If you want to reset to the timer's initial passed in time, please call ResetTimer()
        // Note you must call Start() to restart the Timer after calling SetTimerWithTime()
        public static void SetTimerWithTime(int id, TimeSpan newTime)
        {
            Timer timer = registeredTimers[id];
            timer.SetTimerWithNewTime(newTime);
        }

        public static void Update(GameTime gameTime)
        {
            IEnumerable<KeyValuePair<int, Timer>> enumerable = registeredTimers.AsEnumerable<KeyValuePair<int, Timer>>();

            foreach (KeyValuePair<int, Timer> pair in enumerable)
            {
                pair.Value.Update(gameTime);
            }
        }
    }
}
