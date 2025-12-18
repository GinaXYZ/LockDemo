namespace LockDemo
{
    internal class Program
    {
        private const int AnzahlThreads = 10;
        private const int AnzahlIterationen = 10_000;

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
            Console.WriteLine($"    Ergebnis: {_zeahlerOhneLock:N0}");
            Console.WriteLine($"    Erwartet: {erwarteterEndwert:N0}");
            Console.WriteLine($"    Status: {(differenzOhneLock == 0 ? "Korrekt" : differenzOhneLock + " verloren")} ");
            Console.WriteLine($"    Dauer: {dauerOhneLock.TotalMilliseconds:F2} ms");
            Console.WriteLine();
        }

        private static void InkrementOhneLock()
        {
            for (int i = 0; i < AnzahlIterationen; i++)
            {
                _zeahlerOhneLock++;
            }

        }
    }
}
