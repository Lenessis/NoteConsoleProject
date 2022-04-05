using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NoteProject
{
    class TaskNote :Note
    {
        List<Task> task_list = new List<Task>(); //posiada liste zadan z klasy Task

        //CONSTRUCTOR
        public TaskNote():base()
        { EditNote();  }

        public TaskNote(string name) : base(name)
        { }

        //METHODS

        public void AddTask()
        {
            Task new_task = new Task();
            task_list.Add(new_task);
        } // dodaje zadanie do listy i tworzy nowy obiekt typu Task

        public void AddTask(bool status, string task)
        {
            Task new_task = new Task(status, task);
            task_list.Add(new_task);
        } // przeciążenie metody wyżej, w której dane są przekazywane jako argumenty

        public void RemoveTask(int index)
        {
            task_list.RemoveAt(index);
        } // usuwa zadanie

        public void ChangeStatus(bool status, int index)
        {
            task_list[index].ChangeStatus(status);
        }//zmienia status zadania 

        public override void WriteIntoTheFile(StreamWriter file)
        {
            foreach (var item in task_list)
                file.WriteLine(item);
        }//zapisywanie listy zadań do pliku

        public void LoadFromFile(string line)
        {
            string[] tab = line.Split(" ", 2);
            bool status = Convert.ToBoolean(tab[0]);
            AddTask(status, tab[1]);
        }// czytanie z pliku linii kodu i zamienianie jej tak, aby dało sie zapisać ja na liście zadań

        public override string GetTypeNote()
        {
            return "ZADANIOWA";
        } // Zwraca typ "ZADANIOWA"

        override public void ShowNote()
        {
            base.ShowNote();
            int i= 1;
            foreach (var item in task_list)
            {
                Console.WriteLine(i+" "+item);
                i++;
            }
        } 

        public override void EditNote()
        {
            int option = 99;

            while (option != 0)
            {
                ShowNote();
                Console.WriteLine("\nTask Note Menager - co chcesz zrobić? \n");
                Console.WriteLine("1. Dodaj zadanie");
                Console.WriteLine("2. Usun zadanie");
                Console.WriteLine("3. Zmien status zadania");
                Console.WriteLine("0. Anuluj");
             ChooseOption:
                Console.Write("Wybor: ");
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
                    case 1: AddTask();
                        break;

                    case 2:
                        int index;
                ChangeIndex:
                        Console.Write("Wybierz index zadania, które chcesz usunąć: ");
                        try
                        {
                             index = Convert.ToInt32(Console.ReadLine())-1;
                        }
                        catch
                        {
                            goto ChangeIndex;
                        }                        
                        RemoveTask(index);
                        break;

                    case 3:
                        int index2; bool status;
                    ChangeIndex2:
                        Console.Write("Wybierz index zadania, którego status chcesz zmienic: ");
                        try
                        {
                            index2 = Convert.ToInt32(Console.ReadLine()) -1;
                        }
                        catch
                        {
                            goto ChangeIndex2;
                        }
                        status = task_list[index2].GetStatus();
                        status = (status == false)? true : false;
                        ChangeStatus(status, index2);
                        break;

                    case 0:
                        return;
                    default: goto ChooseOption;
                }

            }
            
        }

        public override string ToString()
        {
            ShowNote();
            return "";
        }
    }
}
