using System;
using System.Collections.Generic;
using System.Text;

namespace NoteProject
{
    class TextNote :Note
    {
        string text; // notatka przetrzymuje tylko tekst napisany przez użytkownika

        //CONSTRUCTORS
        public TextNote():base()
        {
            Console.WriteLine("TEXT");
            text = Console.ReadLine();
        }
        public TextNote(string name) : base(name)
        {        }

        //METHODS

        public override string GetTypeNote()
        {
            return "TEKSTOWA";
        } //zwraca typ "TEKSTOWA"

        override public void ShowNote() 
        {
            base.ShowNote();
            Console.WriteLine(text);
        } //pokazuje notatke

        override public void EditNote()
        {
            int option;

            Console.WriteLine("Chcesz zmienic czy dopisac tekst?");
            Console.WriteLine("1. Zmienic");
            Console.WriteLine("2. Dopisac");
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
                case 1:
                    Console.Clear();
                    ShowNote();
                    text = Console.ReadLine();
                    break;
                case 2:
                    Console.Clear();
                    ShowNote();
                    text += Console.ReadLine();
                    break;
                default:  goto ChooseOption;
            }
        } //edycja

        public void EditNote(string text)
        {
            this.text  += text;
        } // dopisywanie tekstu do już powstałego

        public override string ToString()
        {
            return text;
        } // konwertuje notatke na typ string

    }
}
