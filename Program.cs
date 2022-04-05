using System;

namespace NoteProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int option = 99;
            Library notes_library = new Library();

            while(option!=0)
            {
                Console.Clear();
                Menu();
            MENU:
                Console.Write("Wybierz: ");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Wybieranie nie powiodło się. Spróbuj jeszcze raz!");
                    goto MENU;
                }

                switch(option)
                {
                    case 1:
                        Console.Clear();
                        notes_library.CreateNoteFile();
                        PressKey();
                        break;

                    case 2:
                        Console.Clear();
                        notes_library.ShowNoteFilesFromDirectory();
                        PressKey();
                        break;

                    case 3:
                        Console.Clear();
                        notes_library.ShowNoteFromList();
                        PressKey();
                        break;

                    case 4:
                        Console.Clear();
                        int index = ReturnIndex(notes_library);            
                        notes_library.OpenNote(index);
                        PressKey();
                        break;

                    case 5:
                        Console.Clear();
                        int index2 = ReturnIndex(notes_library);
                        Console.Clear();
                        notes_library.EditNote(index2);
                        PressKey();
                        break;

                    case 6:
                        notes_library.SaveAllNotes();
                        PressKey();
                        break;

                    case 0:
                        return;

                    default:
                        Console.Clear(); goto MENU;
                }
            }           
        }

        static void Menu()
        {
            Console.WriteLine("******* NOTE PROJECT *******\n");
            Console.WriteLine("1. Stwórz notatkę");
            Console.WriteLine("2. Wyświetl pliki z folderu NoteFiles");
            Console.WriteLine("3. Wyświetl notatki");
            Console.WriteLine("4. Pokaż zawartość notatki.");
            Console.WriteLine("5. Edytuj notatke.");
            Console.WriteLine("6. Zapisz wszystkie notatki.\n");

            Console.WriteLine("0. Wyjscie\n\n");
        }//pokazuje opcje menu 

        static int ReturnIndex(Library library)
        {
            int index;
        INDEX:
            Console.Clear();
            library.ShowNoteFromList();
            Console.Write("\n Wybierz indeks notatki: ");
            try
            {
                index = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Podano bledny index. Podaj go jeszcze raz!");
                PressKey();
                goto INDEX;
            }
            return index;
        }//sprawdza liczbę wpisaną przez użytkownika i obsługuje wyjatek oraz zwraca index

        static void PressKey()
        {
            Console.WriteLine("Nacisnij dowolny przycisk, aby kontynuowac");
            Console.ReadKey();
        } //wyświetla komunikat i oczekuje na wciśnięcie klawiasza (taka pauza)
    }
}
