using System;
using System.Collections.Generic;
using System.Text;

namespace NoteProject
{
    class Task
    {
        string task;
        bool status;

        //CONSTRUCTOR
        public Task()
        {
            status = false;

            Console.Write("Dodaj zadanie: ");
            string n_task = Console.ReadLine();
            if(n_task =="")
            {
                throw new EmptyNoteException();
            }
            else
            {
                task = n_task;
            }
        } // zadanie tworzy użytkownik

        public Task(bool status, string task)
        {
            this.status = status;
            this.task = task;
        } 

        //METHODS

        public bool GetStatus()
        {
            return status;
        } // zwraca status

        public void ChangeStatus(bool new_status)
        {
            this.status = new_status;
        } // zmienia status

        public override string ToString()
        {
                return status + " "+ task;
 
        }
    }
}
