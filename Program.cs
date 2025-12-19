namespace Simple_Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU ZADAŃ ===");
                Console.WriteLine("1. Prosty kalkulator");
                Console.WriteLine("2. Konwerter temperatur");
                Console.WriteLine("3. Średnia ocen ucznia");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcje (0-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Kalkulator();
                        break;
                    case "2":
                        Temperatura();
                        break;
                    case "3":
                        Srednia();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Nieznana opcja.");
                        break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić do menu...");
                    Console.ReadKey();
                }
            }
        }

        static void Kalkulator()
        {
            Console.WriteLine("\n--- KALKULATOR ---");
            try
            {
                Console.Write("Podaj pierwszą liczbę: ");
                double a = double.Parse(Console.ReadLine());

                Console.Write("Podaj drugą liczbę: ");
                double b = double.Parse(Console.ReadLine());

                Console.Write("Wybierz operację (+, -, *, /): ");
                string op = Console.ReadLine();
                double result = 0;

                if (op == "+") result = a + b;
                else if (op == "-") result = a - b;
                else if (op == "*") result = a * b;
                else if (op == "/")
                {
                    if (b == 0) { Console.WriteLine("Błąd: Dzielenie przez zero!"); return; }
                    result = a / b;
                }
                else { Console.WriteLine("Nieznana operacja."); return; }

                Console.WriteLine($"Wynik: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Błąd: Podano nieprawidłową liczbę.");
            }
        }

        static void Temperatura()
        {
            Console.WriteLine("\n--- KONWERTER TEMPERATUR ---");
            Console.WriteLine("Wybierz kierunek: C (Celsjusz->Fahrenheit) lub F (Fahrenheit->Celsjusz)");
            string kierunek = Console.ReadLine().ToUpper();

            try
            {
                Console.Write("Podaj temperaturę: ");
                double temp = double.Parse(Console.ReadLine());
                double result = 0;

                if (kierunek == "C")
                {
                    // F = C * 1.8 + 32
                    result = temp * 1.8 + 32;
                    Console.WriteLine($"{temp}°C = {result:F2}°F");
                }
                else if (kierunek == "F")
                {
                    // C = (F - 32) / 1.8
                    result = (temp - 32) / 1.8;
                    Console.WriteLine($"{temp}°F = {result:F2}°C");
                }
                else
                {
                    Console.WriteLine("Nieznany kierunek.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Błąd: Podano nieprawidłową wartość.");
            }
        }

        static void Srednia()
        {
            Console.WriteLine("\n--- ŚREDNIA OCEN ---");
            Console.Write("Podaj liczbę ocen do wprowadzenia: ");
            if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
            {
                double sum = 0;
                for (int i = 0; i < qty; i++)
                {
                    Console.Write($"Podaj ocenę nr {i + 1} (1-6): ");
                    if (double.TryParse(Console.ReadLine(), out double grade) && grade >= 1 && grade <= 6)
                    {
                        sum += grade;
                    }
                    else
                    {
                        Console.WriteLine("Błędna ocena (musi być 1-6). Spróbuj ponownie.");
                        i--;
                    }
                }

                double average = sum / qty;
                Console.WriteLine($"Średnia: {average:F2}");

                if (average >= 3.0) Console.WriteLine("Uczeń zdał.");
                else Console.WriteLine("Uczeń nie zdał.");
            }
            else
            {
                Console.WriteLine("Błędna ilość ocen.");
            }
        }
    }
}
