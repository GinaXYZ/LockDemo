namespace LockDemo
{
    internal class Program
    {

        // Konstanten
        private const int AnzahlThreads = 10;
        private const int AnzahlIterationen = 10_000;

        // Lock-Objekt
        private static readonly object _lockObjekt = new object();

        // Zähler-Variablen
        private static int _zeahlerOhneLock = 0;
        private static int _zaehlerMitLock = 0;
        private static int _zeahlerMitInterlocked = 0;


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int erwarteterEndwert = AnzahlThreads * AnzahlIterationen;

            DateTime startzeitOhneLock = DateTime.Now;

            Task[] tasksOhneLock = new Task[AnzahlThreads];

            for (int i = 0; i < AnzahlThreads; i++)
            {
                tasksOhneLock[i] = Task.Run(InkrementOhneLock);
            }

            Task.WaitAll(tasksOhneLock);

            TimeSpan dauerOhneLock = DateTime.Now - startzeitOhneLock;

            int differenzOhneLock = erwarteterEndwert - _zeahlerOhneLock;

            Console.WriteLine($"Ohne Lock:");
            Console.WriteLine($"    Ergebnis: {_zeahlerOhneLock}");
            Console.WriteLine($"    Erwartet: {erwarteterEndwert}");
            Console.WriteLine($"    Status: {(differenzOhneLock == 0 ? "Korrekt" : differenzOhneLock + " verloren")} ");
            Console.WriteLine($"    Dauer: {dauerOhneLock.TotalMilliseconds:F2} ms");
            Console.WriteLine();

            // Mit Lock

            DateTime startzeitMitLock = DateTime.Now;

            Task[] tasksMitLock = new Task[AnzahlThreads];

            for (int i = 0; i < AnzahlThreads; i++)
            {
                tasksMitLock[i] = Task.Run(InkrementMitLock);
            }

            Task.WaitAll(tasksMitLock);

            TimeSpan dauerMitLock = DateTime.Now - startzeitMitLock;

            int differenzMitLock = erwarteterEndwert - _zaehlerMitLock;

            Console.WriteLine($"Mit Lock:");
            Console.WriteLine($"    Ergebnis: {_zaehlerMitLock}");
            Console.WriteLine($"    Erwartet: {_zaehlerMitLock}");
            Console.WriteLine($"    Status: {(differenzMitLock == 0 ? "Korrekt" : differenzMitLock + " verloren")} ");
            Console.WriteLine($"    Dauer: {dauerMitLock.TotalMilliseconds:F2} ms");
            Console.WriteLine();


            // Mit Interlocked

            DateTime startzeitMitInterlocked = DateTime.Now;

            Task[] tasksMitInterlocked = new Task[AnzahlThreads];

            for (int i = 0; i < AnzahlThreads; i++)
            {
                tasksMitInterlocked[i] = Task.Run(InkrementMitInterlocked);
            }

            Task.WaitAll(tasksMitInterlocked);

            TimeSpan dauerMitInterlocked = DateTime.Now - startzeitMitInterlocked;

            int differenzMitInterlocked = erwarteterEndwert - _zeahlerMitInterlocked;

            Console.WriteLine($"Mit Interlocked:");
            Console.WriteLine($"    Ergebnis: {_zeahlerMitInterlocked}");
            Console.WriteLine($"    Erwartet: {erwarteterEndwert}");
            Console.WriteLine($"    Status: {(differenzMitInterlocked == 0 ? "Korrekt" : differenzMitInterlocked + " verloren")} ");
            Console.WriteLine($"    Dauer: {dauerMitInterlocked.TotalMilliseconds:F2} ms");
            Console.WriteLine();


            static void InkrementOhneLock()
            {
                for (int i = 0; i < AnzahlIterationen; i++)
                {
                    _zeahlerOhneLock++;
                }

            }
            static void InkrementMitLock()
            {
                for (int i = 0; i < AnzahlIterationen; i++)
                {
                    lock (_lockObjekt)
                    {
                        _zaehlerMitLock++;
                    }
                }

            }
            
            static void InkrementMitInterlocked()
            {
                for (int i = 0; i < AnzahlIterationen; i++)
                {
                    Interlocked.Increment(ref _zeahlerMitInterlocked);
                }
            }
            
        }
    }
}
