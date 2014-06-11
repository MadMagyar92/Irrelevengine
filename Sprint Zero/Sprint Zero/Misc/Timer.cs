using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class Timer
    {
        public bool IsFinished { get; private set; }
        public int Id { get; private set; }

        private bool hasStarted;
        private TimeSpan timeRemaining;
        private TimeSpan initialTime;

        public Timer(int id, TimeSpan amountOfTime)
        {
            this.Id = id;
            this.timeRemaining = amountOfTime;
            this.initialTime = amountOfTime;
            this.IsFinished = false;
            this.hasStarted = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsFinished && hasStarted)
            {
                TimeSpan timeSpan = gameTime.ElapsedGameTime;

                timeRemaining = timeRemaining.Subtract(timeSpan);

                if (timeRemaining.Hours <= 0 && timeRemaining.Minutes <= 0 && timeRemaining.Milliseconds <= 0)
                {
                    IsFinished = true;
                }
            }
        }

        public void Reset()
        {
            timeRemaining = initialTime;
            IsFinished = false;
            hasStarted = false;
        }

        public void SetTimerWithNewTime(TimeSpan amountOfTime)
        {
            timeRemaining = amountOfTime;
            initialTime = amountOfTime;
            IsFinished = false;
            hasStarted = false;
        }

        public TimeSpan GetTimeRemaining()
        {
            return timeRemaining;
        }

        public void Start()
        {
            hasStarted = true;
        }

        public void Stop()
        {
            hasStarted = false;
        }
    }
}
