using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        Dictionary<string, string> rusEstDictionary = LoadDictionary("rus.txt", "est.txt");
        Dictionary<string, string> estRusDictionary = LoadDictionary("est.txt", "rus.txt");

        while (true)
        {
            Console.WriteLine("Выберите функцию 1-6:");
            Console.WriteLine("1. Перевод слов с русского на эстонский");
            Console.WriteLine("2. Перевод слов с эстонского на русский");
            Console.WriteLine("3. Добавление слов в словарь");
            Console.WriteLine("4. Игра 'Проверка знаний'");
            Console.WriteLine("5. Прослушивание слова (не реализовано в C#)");
            Console.WriteLine("6. Выход из программы");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    TranslateWord(rusEstDictionary, "Введите слово на русском: ", "Перевод на эстонский: ");
                    break;
                case 2:
                    TranslateWord(estRusDictionary, "Введите слово на эстонском: ", "Перевод на русский: ");
                    break;
                case 3:
                    AddWordsToDictionary("Введите слово на русском: ", "Введите слово на эстонском: ", rusEstDictionary, estRusDictionary);
                    break;
                case 4:
                    TestKnowledge(rusEstDictionary);
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Некорректный ввод, попробуйте снова.");
                    break;
            }
        }
    }

    static Dictionary<string, string> LoadDictionary(string file1, string file2)
    {
        string[] words1 = File.ReadAllLines(file1, Encoding.UTF8);
        string[] words2 = File.ReadAllLines(file2, Encoding.UTF8);
        return words1.Zip(words2, (w1, w2) => new { Key = w1.Trim().ToLower(), Value = w2.Trim().ToLower() })
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    static void TranslateWord(Dictionary<string, string> dictionary, string inputMessage, string outputMessage)
    {
        Console.WriteLine(inputMessage);
        string word = Console.ReadLine().ToLower().Trim();
        string translation = dictionary.GetValueOrDefault(word, "Перевод не найден, попробуйте еще раз");

        if (translation == "Перевод не найден, попробуйте еще раз")
        {
            Console.WriteLine(translation);
            AddWordsToDictionary("Введите слово на русском: ", "Введите слово на эстонском: ", dictionary, dictionary);
        }
        else
        {
            Console.WriteLine($"{outputMessage} {translation}");
        }
    }

    static void AddWordsToDictionary(string message1, string message2, Dictionary<string, string> dic1, Dictionary<string, string> dic2)
    {
        Console.WriteLine(message1);
        string word1 = Console.ReadLine().ToLower().Trim();
        Console.WriteLine(message2);
        string word2 = Console.ReadLine().ToLower().Trim();

        if (!dic1.ContainsKey(word1))
        {
            dic1[word1] = word2;
            File.AppendAllText("rus.txt", word1 + "\n", Encoding.UTF8);
            File.AppendAllText("est.txt", word2 + "\n", Encoding.UTF8);
        }
    }

    static void TestKnowledge(Dictionary<string, string> dictionary)
    {
        Console.WriteLine("Проверка знаний");
        Console.WriteLine("Введите количество раундов:");
        int rounds = Convert.ToInt32(Console.ReadLine());
        int correctAnswers = 0;

        Random random = new Random();
        List<string> keys = dictionary.Keys.ToList();

        for (int i = 0; i < rounds; i++)
        {
            string randomKey = keys[random.Next(keys.Count)];
            Console.WriteLine("Как переводится слово: " + randomKey);
            string userAnswer = Console.ReadLine().ToLower().Trim();
            if (dictionary[randomKey] == userAnswer)
            {
                correctAnswers++;
                Console.WriteLine("Правильно!");
            }
            else
            {
                Console.WriteLine("Неправильно, правильный ответ: " + dictionary[randomKey]);
            }
        }

        Console.WriteLine($"Правильно: {correctAnswers}, Неправильно: {rounds - correctAnswers}");
        Console.WriteLine($"Процент правильных ответов: {(int)((double)correctAnswers / rounds * 100)}%");
    }
}
