using System.Text.Json;

namespace OpcjeMenu
{
    public static class Opcje
    {
        private static string path = "ListaZw.json"; //sciezka do pliku ListaZw.json / path zawiera stringa 
        private static List<zwierze> zwierzeta = new List<zwierze>(); 

        static Opcje()
        {
            zwierzeta = JsonSerializer.Deserialize<List<zwierze>>(File.ReadAllText(path)); // JsinSerializer.Deserialize deserializuje dane JSON z pliku listy do listy typu "zwierze"
        }

        public static void menu()
        {
            Console.Clear();
            Console.WriteLine("Wybierz opcje aby uruchomic funkcje\n1. Dodaj zwierzaka do listy \n2. Usun zwierzaka z listy \n3. Sprawdz obecna liste ziwerzakow\n4. Zmien kolor tekstu\n5. Wyjdz");

            int option = Convert.ToInt16(Console.ReadLine()); //liczba wpisana przez użytkownika  zostanie odczytana przez program oraz wyświetli opcje odpowiadającą wpisanej liczbie
            switch (option)
            {
                case 1:
                    dodajZwierzaka();
                    menu();
                    break;

                case 2:
                    UsunZwierzaka();
                    menu();
                    break;

                case 3:
                    dostepneZwierzaki();
                    Console.ReadKey();
                    menu();
                    break;

                case 4:
                    Console.Clear();
                    ZmienKolor();

                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Żegnamy!");
                    break;
                default:
                    Console.WriteLine("Nie ma takiej opcji");
                    Console.ReadKey();
                    menu();
                    break;
            }
        }

        public static void dodajZwierzaka()
        {
            Console.WriteLine("Proszę podać po kolei id, imie, rase, wiek oraz typ");

            Console.Clear();
            Console.WriteLine("Jak wabi się zwierzak?");
            string imie = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Jakiej rasy jest twój zwierzak?");
            string rasa = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("ile lat ma twój zwierzak?");
            int wiek = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Jak zachowuje sie twoj zwierzak?");
            string zachowanie = Console.ReadLine();
            Console.Clear();

            zwierzeta.Add(new zwierze
            {
                Id = zwierzeta.Count + 1,
                Imie = imie,
                Rasa = rasa,
                Wiek = wiek,
                Zachowanie = zachowanie,
            });
            ZapiszDane(); //uruchamia funkcja zapiszDane
        }

        public static void UsunZwierzaka()
        {
            Console.WriteLine("Podaj id zwierzaka do oddania");
            int id = Convert.ToInt32(Console.ReadLine());
            zwierze usunZwierzaka = zwierzeta.First(b => b.Id == id); //"b" to jest pierwszy element z listy spełniający warunek
            zwierzeta.Remove(usunZwierzaka);
            zwierzeta.ForEach(b => b.Id = zwierzeta.IndexOf(b) + 1); // "b" jest elementem który jest obecnie wyświetlany
            ZapiszDane(); //uruchamia funkcja zapiszDane
        }

        public static void dostepneZwierzaki()
        {
            foreach (var zwierze in zwierzeta)
            {
                Console.WriteLine($"Id: {zwierze.Id}"); //wyswietla pobiera ze zeminnej zwierze po kolei Id, Rase, Imie, Wiek oraz Zachowanie i wyswietla je uzytkownikowi
                Console.WriteLine($"Rasa: {zwierze.Rasa}");
                Console.WriteLine($"Imie: {zwierze.Imie}");
                Console.WriteLine($"Wiek: {zwierze.Wiek}");
                Console.WriteLine($"Typ: {zwierze.Zachowanie}\n");
            }
            Console.WriteLine("Proszę nacisnąć dowolny przycisk aby wrócić do menu");
        }


        public static void ZmienKolor() //"public" - oznacza że metoda może yć wowołana z dowolnego miejsca w kodzie  //"static" - jest to metoda należy do samej klasy // "void" - oznacza że metoda nie zwraca żadnej wartości
        {
            Console.WriteLine("Dostepne kolory: \n1. Niebieski\n2. Zielony\n3. Czerwony\n4. Podstawowy kolor(biały)\n Prosze wybrac kolor wpisując jego numer");
            int kolory = Convert.ToInt16(Console.ReadLine());
            switch (kolory)
            {
                case 1:
                    Console.WriteLine("zmieniono kolor na niebieski");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    
                    break;
                case 2:

                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    break;
                case 3:
                    Console.WriteLine("Zmieniono kolor na czerwony");
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    break;
                case 4:
                    Console.WriteLine("Zmieniono kolor na podstawowy(bialy)");
                    Console.ForegroundColor = ConsoleColor.White;
                    
                    break;
            }menu(); //z poziomu kodu - ponownie uruchamia funkcje menu(); // z poziomu uzytkownika - 
        }
        public static void ZapiszDane()
        {
            File.WriteAllText(path, JsonSerializer.Serialize(zwierzeta)); //zapisuje dane do pliku ListaZw.json
        }
    }
}