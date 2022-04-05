using System;
using System.Collections.Generic;
using System.Text;

namespace NoteProject
{
    class EmptyNoteException :Exception
    {
        string message;
        string message_item;

       public EmptyNoteException()
        {

        }

        string Change(string mess_item, string mess, string change_item)
        {
            Console.WriteLine("Wystapil blad! "+mess);

            do
            {
                Console.Write("Wprowadź {0} jescze raz: ",mess_item);
                change_item = Console.ReadLine();
                Console.WriteLine();
            }
            while (change_item == "");

            return change_item;
        }

        public string ChangeName()
        {
            string name = "";
            message_item = "nazwe";
            message = "Nazwa nie moze byc pusta!";
            name = Change(message_item, message, name);
            return name;
        }

        public string ChangeTask()
        {
            string task = "";
            message_item = "tresc zadania";
            message = "Tresc zadania nie moze byc pusta!";
            task = Change(message_item, message, task);
            return task;
        }

        public string ChangeEventName()
        {
            string event_name = "";
            message_item = "nazwa wydarzenia";
            message = "Nazwa wydarzenia nie moze byc pusta!";
            event_name = Change(message_item, message, event_name);
            return event_name;
        }

    }
}
