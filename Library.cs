using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace NoteProject
{
    //klasa obsługująca wszystkie notatki
    class Library
    {
        List<Note> note_list = new List<Note>();
        List<string> file_name_note = new List<string>();
        string default_pathfile;

        //CONSTRUCTORS
        public Library()
        {
            default_pathfile = "./../../../NoteFiles/";
            LoadNotesFromFiles();
        } // w konstruktorze następuje ładowanie listy plików z folderu do listy file_name_note oraz jest ustawienie ścieżki domyślnej do tego folderu

        //METHODS

        public void CreateNoteFile()
        {
            int option;
            Note new_note;
            StreamWriter note_file;

            Console.WriteLine("Wybierz rodzaj notatki jaka chcesz utworzyc");
            Console.WriteLine("1. Notatka tesktowa");
            Console.WriteLine("2. Notatka terminowa");
            Console.WriteLine("3. Notatka zadaniowa");
            Console.WriteLine("0. Anuluj");
        ChooseOption:
            Console.Write("Wybierz: ");
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Blad przy wybieraniu opcji!"); goto ChooseOption;
            }           

            switch (option)
            {
                case 1://TEXT NOTE
                    try
                    {
                        new_note = new TextNote();                       
                    }
                    catch(EmptyNoteException e)
                    {
                        new_note = new TextNote(e.ChangeName()); 
                    }
                    note_file = new StreamWriter(default_pathfile + new_note.GetName() + ".textnote.txt");
                    note_file.WriteLine(new_note);
                    note_list.Add(new_note);
                    note_file.Close();
                    break;

                case 2://DATETIME NOTE
                    try
                    {
                        new_note = new DatetimeNote();
                    }
                    catch(EmptyNoteException e)
                    {
                        new_note = new DatetimeNote(e.ChangeName());
                    }
                    note_file = new StreamWriter(default_pathfile + new_note.GetName() + ".datetimenote.txt");
                    note_file.WriteLine(new_note);
                    note_list.Add(new_note);
                    note_file.Close();
                    break;

                case 3://TASK NOTE
                    try
                    {
                        new_note = new TaskNote();
                    }
                    catch(EmptyNoteException e)
                    {
                        new_note = new TaskNote(e.ChangeName());
                    }
                    note_file = new StreamWriter(default_pathfile + new_note.GetName() + ".tasknote.txt");
                    new_note.WriteIntoTheFile(note_file);
                    note_list.Add(new_note);
                    note_file.Close();
                    break;

                case 0: //CANCEL
                    return;

                default:
                    goto ChooseOption;
            }
        } // wyświetla menu do tworzenia 3 rodzajów notatek; tworzy plik notatki z odpowiednim dopiskiem do rozszerzenia tak, aby notatki były rozróżnialne 

        public void OpenNote(int index)
        {
            index--;
            note_list[index].ShowNote();
        }// otwiera konkretna notatke z listy po indexie

        public void EditNote(int index)
        {
            char answer;  index--;

            note_list[index].EditNote();
            Answer:
            Console.Write("Czy chcesz zapisać plik? [y/n]: ");
            try
            {
                answer = Convert.ToChar(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wystapil blad");
                goto Answer;
            }
            switch(answer)
            {
                case 'y':
                    SaveNoteFile(note_list[index]);  break;
                case 'n':   break;
                default:    goto Answer;
            }
        } // edytuje konkretna notatke z listy po indexie; pyta o to, czy ma zapisać notatke

        private void SaveNoteFile(Note note)
        {
            string end_tail_txt;
            switch(note.GetTypeNote())
            {
                case "TEKSTOWA":
                    end_tail_txt = ".textnote.txt";  break;

                case "TERMINOWA":
                    end_tail_txt = ".datetimenote.txt";   break;

                case "ZADANIOWA":
                    end_tail_txt = ".tasknote.txt";   break;

                default:
                    Console.WriteLine("Wystapil blad!");  return;
            }
            StreamWriter file = new StreamWriter(default_pathfile+note.GetName()+end_tail_txt);

            if(end_tail_txt == ".tasknote.txt")
                note.WriteIntoTheFile(file);
            else
                file.Write(note);

            file.Close();
        }//zapisuje notatke z konkretnym rozszerzeniem, notatka zadaniowa jest zapisywana troche inaczej ze wzgledu na to, iż korzysta z klasy Task

        public void SaveAllNotes()
        {
            foreach (var item in note_list)
                SaveNoteFile(item);
            Console.WriteLine("Pomyślnie zapisano pliki!");
        } // zapisuje wszystkie notatki

        public void ShowNoteFromList() // wypisuje liste notatek (typ notatki i jej nazwe) z listy, tak, aby użytkownik mógł wybrać, którą notatkę chce edytować itp
        {
            int i = 1;
            Console.WriteLine("--- LISTA NOTATEK ---");

            foreach (Note item in note_list)
            {
                Console.WriteLine(i + ". "+item.GetTypeNote() + " " + item.GetName());
                i++;
            }
        } 

        // METHODS WHICH ARE RESPONSIBLE FOR FILE SERVICES

        private void NoteFilesFromDirectory()
        {
            file_name_note = Directory.GetFiles(default_pathfile).ToList();
            List<string> temp = new List<string>();

            foreach (var item in file_name_note)
                temp.Add(item.Replace(default_pathfile, ""));

            file_name_note = temp;

        } //czyta nazwy wszystkich plików z folderu domyślnego

        private void LoadNotesFromFiles()
        {
            NoteFilesFromDirectory();
            StreamReader file;      string line;

            foreach (var item in file_name_note)
            {
                file = new StreamReader(default_pathfile+item);
                if(item.Contains(".textnote.txt")==true) //TEXT NOTE
                {
                   TextNote temp = new TextNote(item.Replace(".textnote.txt",""));

                    while((line = file.ReadLine())!=null)
                        temp.EditNote(line);

                    note_list.Add(temp);
                }

                else if(item.Contains(".datetimenote.txt") == true) //DATETIME NOTE
                {
                    DatetimeNote temp = new DatetimeNote(item.Replace(".datetimenote.txt", ""));
                    int i = 0;

                    while ((line = file.ReadLine()) != null)
                    {
                        switch(i)
                        {
                            case 0:
                                temp.EditNote(line);
                                i++; break;

                            case 1:
                                temp.EditNoteDate(line);
                                i++; break;

                            default: i++; continue;
                        }
                    }
                    note_list.Add(temp);
                }

                else if(item.Contains(".tasknote.txt") == true) //TASK NOTE
                {
                    TaskNote temp = new TaskNote(item.Replace(".tasknote.txt", ""));

                    while ((line = file.ReadLine()) != null)
                        temp.LoadFromFile(line);

                    note_list.Add(temp);
                }
                else
                     continue;
                file.Close();
            }
        } //ładuje zawartość notatek z plików znajdujących sie w katalogu

        public void ShowNoteFilesFromDirectory()
        {
            Console.WriteLine("---Notatki jako pliki z folderu ---");
            int i = 1;

            foreach (var item in file_name_note)
            {
                Console.WriteLine(i+". "+item);
                i++;
            }
            Console.WriteLine();
        } // pokazuje jakie pliki znajduja się w katalogu 

    }
}
