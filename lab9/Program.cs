using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static object locker = new();

    static void Main()
    {
        // Считываем акции из файла ticker.txt
        string[] tickers = File.ReadAllLines("C:/Users/Полина/source/repos/Lab9/Lab9/ticker.txt");

        Task[] tasks = new Task[tickers.Length];

        for (int i = 0; i < tickers.Length; i++)
        {
            string ticker = tickers[i];
            tasks[i] = Task.Run(() => GetStockData(ticker));
        }

        // Дожидаемся завершения всех задач
        Task.WaitAll(tasks);

        Console.WriteLine("Готово!");
    }

    static void GetStockData(string ticker)
    {
        long startTimestamp = DateTimeOffset.UtcNow.AddYears(-1).ToUnixTimeSeconds();
        long endTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        string url = $"https://query1.finance.yahoo.com/v7/finance/download/{ticker}?period1={startTimestamp}&period2={endTimestamp}&interval=1d&events=history&includeAdjustedClose=true";
    
        using (WebClient client = new WebClient())
        {
            try
            {
                // Скачиваем данные
                string data = client.DownloadString(url);

                // Пропускаем заголовок
                //разбиваем строку подстроки исключая пустые (Environment.NewLine- возвращает символ новой строки для данной платформы)
                string[] lines = data.Split( '\n');
                double average = 0;
                int days = 0;
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] columns = lines[i].Split(',');
                    if (columns.Length > 5)
                    {
                        NumberFormatInfo provider = new NumberFormatInfo();
                        provider.NumberDecimalSeparator = ".";
                        provider.NumberGroupSeparator = ",";
                        double high = Convert.ToDouble(columns[2], provider);
                        double low = Convert.ToDouble(columns[3], provider);

                        average += (high + low) / 2;
                        days++;
                    }
                }

                average /= days;

                lock (locker)
                {
                    // Записываем результаты в файл
                    using (StreamWriter writer = File.AppendText("C:/Users/Полина/source/repos/Lab9/Lab9/result.txt"))
                    {
                        writer.WriteLine($"{ticker}:{average}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла ошибка при получении данных для акции {ticker}: {e.Message}");
            }
        }
    }
}
