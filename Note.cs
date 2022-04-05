using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NoteProject
{
    //klasa abstrakcyjna, matka dla wszystkich notatek
     abstract class Note
    {
        protected string name;
        protected string type;

        //CONSTRUCTORS
        public Note() 
        {
            Console.Write("Podaj nazwe notatki: ");
            this.name = Console.ReadLine();
            if (name == "")
                throw new EmptyNoteException();
        } //konstruktor służący do tworzenia nowych notatek i nadawania im nazwy, jest dziedziczony i służy (w dalszej części) do kontatktu z użytkownikiem 

        public Note(string name) 
        {
            this.name = name;
            if (name == "")
                throw new EmptyNoteException();
        } //konstruktor służący do tworzenia obiektów w kodzie jako matki dla innych notatek, jest używany tylko w metodach, użytkownik nie ma do niego dostępu

        //METHODS

        //VIRTUAL

        virtual public string GetName() 
        {
            return name;
        } //zwraca nazwę notatki
        virtual public string GetTypeNote() //zwraca typ notatki 
        {
            return type;
        } 

        virtual public void ChangeName()
        {
            Console.Write("Podaj nowa nazwe notatki: ");
            string new_name = Console.ReadLine();

            if (new_name =="")
                throw new EmptyNoteException();
        } // zmienia nazwę

        virtual public void ShowNote()
        {
            Console.WriteLine("NAZWA: "+name);
        } // pokazuje notatkę
        virtual public void WriteIntoTheFile(StreamWriter file)
        {

        }// wpisuje do pliku, główne zastosowanie w notatkach zadaniowych

        //ABSTRACT

        abstract public void EditNote(); //edycja
        public abstract override string ToString(); //konwertowanie na typ string

    }
}
