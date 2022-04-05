using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace NoteProject
{
    class AlarmClock
    {
         EventHandler alarmEvent;
         Timer timer;
         DateTime alarmTime;
         bool enabled;

        //CONSTRUCTOR

        public AlarmClock(DateTime alarmTime)
        {
            this.alarmTime = alarmTime;

            timer = new Timer();
            timer.Elapsed += timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();

            enabled = true;
        } // ustawia timer według podanej daty

        //METHODS

        void timer_Elapsed(object sender, ElapsedEventArgs e) //sprawdza, kiedy upłynął czas do podanej daty przez użytkownika
        {
            if (enabled && DateTime.Now > alarmTime)
            {
                enabled = false;
                OnAlarm();
                timer.Stop();
            }
        }

        protected virtual void OnAlarm()
        {
            if (alarmEvent != null)
                alarmEvent(this, EventArgs.Empty);
        } // włacza alarm

        public event EventHandler Alarm
        {
            add { alarmEvent += value; }
            remove { alarmEvent -= value; }
        }
    }
}
