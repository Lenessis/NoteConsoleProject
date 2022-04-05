using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NoteProject
{
    class DatetimeNote :Note
    {
        string event_name;
        DateTime dateTime;

        //CONSTRUCTOR
        public DatetimeNote() :base()
        {
            Console.Write("Wprowadz nazwe wydarzenia: ");
            event_name = Console.ReadLine();
            if(event_name=="")
            {
                throw new EmptyNoteException();
            }
            ChangeDateTime();
        }

        public DatetimeNote(string name) : base(name)
        { }

        //METHODS

        public void SetRemainder()
        {
            AlarmClock alarm = new AlarmClock(dateTime);
            alarm.Alarm += (sender, e) => Console.WriteLine("\nKOMUNIKAT ALARMU \n MASZ NOWE WYDARZENIE: "+event_name);
            Console.WriteLine("Ustawiono alarm!");
        } // ustawia alarm, który wyświetla się w konsoli, gdy przyjdzie odpowiedni czas 

        private void ChangeDateTime()
        {
            int day, month, year, hour, minute, day_limit =31;
            Console.WriteLine("Wprowadz date");
        Ex0:
            try
            {
                Console.Write("Podaj rok: ");
                year = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Blad!"); goto Ex0;
            }
        Ex1:
            try
            {
                do
                {
                    Console.Write("Podaj miesiac: ");
                    month = Convert.ToInt32(Console.ReadLine());
                } while (month < 1 || month > 12);
                
                switch(month)
                {
                    case 1: case 3: case 5: case 7:  case 8: case 10: case 12:
                        day_limit = 31;  break;

                    case 2:
                        if(year%4==0)
                        day_limit = 29; day_limit = 28;  break;

                    case 4: case 6:  case 9: case 11:
                        day_limit = 30; break;
                }
            }
            catch
            {
                Console.WriteLine("Blad!"); goto Ex1;
            }

        Ex2:
            try
            {
                do
                {
                    Console.Write("Podaj dzien: ");
                    day = Convert.ToInt32(Console.ReadLine());
                } while (day < 1 || day > day_limit);
            }
            catch
            {
                Console.WriteLine("Blad!"); goto Ex2;
            }
        Ex3:
            try
            {
                do
                {
                    Console.Write("Podaj godzine: ");
                    hour = Convert.ToInt32(Console.ReadLine());
                } while (hour < 0 || hour > 23);               
            }
            catch
            {
                Console.WriteLine("Blad!"); goto Ex3;
            }
        Ex4:
            try
            {
                do
                {
                    Console.Write("Podaj minute: ");
                    minute = Convert.ToInt32(Console.ReadLine());
                } while (minute < 0 || minute > 59);        
            }
            catch
            {
                Console.WriteLine("Blad!"); goto Ex4;
            }
            dateTime = new DateTime(year, month, day, hour, minute, 0);
        } // zmienia date i sprawdzajac, czy dane zostaly poprawnie podane

        private void ChangeEventName()
        {
            Console.Write("Wprowadz nowa nazwe wydarzenia: "); 
            string n_event_name = Console.ReadLine();

            if(n_event_name == "")
                throw new EmptyNoteException();
            else
                event_name = n_event_name;
        } // zmienia nazwe wydarzenia

        public override string GetTypeNote()
        {
            return "TERMINOWA";
        } // zwraca typ "TERMINOWA"

        public override void ShowNote()
        {
            base.ShowNote();
            Console.WriteLine("Nazwa wydarzenia "+event_name);
            Console.WriteLine("Data wydarzenia "+ dateTime);
        }

        public override void EditNote()
        {
            int option;
            ShowNote();
            Console.WriteLine("\nCo chcesz edytować?");
            Console.WriteLine("1. Zmiana nazwy wydarzenia.");
            Console.WriteLine("2. Zmiana daty.");
            Console.WriteLine("3. Ustaw alarm.");
            ChooseOption:
            Console.Write("Twój wybór: ");
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Blad przy wybieraniu opcji!"); goto ChooseOption;
            }
            switch(option)
            {
                case 1:
                    ChangeEventName();  break;
                case 2:
                    ChangeDateTime(); break;
                case 3:
                    SetRemainder(); break;
                default:
                    goto ChooseOption;
            }
        }

        public void EditNote(string name)
        {
            event_name = name;
        }

        public void EditNoteDate(string date)
        {
            char[] char_tab = new char[] { '.', ' ', ':' };
            string[] tab_date = date.Split(char_tab);
            int dd = Convert.ToInt32(tab_date[0]);
            int mm = Convert.ToInt32(tab_date[1]);
            int yy = Convert.ToInt32(tab_date[2]);
            int hh = Convert.ToInt32(tab_date[3]);
            int min = Convert.ToInt32(tab_date[4]);

            DateTime temp = new DateTime(yy, mm, dd, hh, min, 0);
            dateTime = temp;
        } // zamienia string na format DateTime

        public override string ToString()
        {
            return event_name + "\n" + dateTime;
        }

    }
}
