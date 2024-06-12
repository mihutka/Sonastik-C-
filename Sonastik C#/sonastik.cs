using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, string> rusEstDictionary = Translator.ReadRussianToEstonianDictionary();
        Dictionary<string, string> estRusDictionary = Translator.ReadEstonianToRussianDictionary();

        while (true)
        {
            Console.WriteLine("Выберите функцию 1-6:");
            Console.WriteLine("1. Перевод слов с русского на эстонский");
            Console.WriteLine("2. Перевод слов с эстонского на русский");
            Console.WriteLine("3. Добавление слов в словарь");
            Console.WriteLine("4. Игра 'Проверка знаний'");
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
                    AddWordsToDictionary(rusEstDictionary, estRusDictionary);
                    // Перезагрузка словарей после добавления новых слов
                    rusEstDictionary = Translator.ReadRussianToEstonianDictionary();
                    estRusDictionary = Translator.ReadEstonianToRussianDictionary();
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

    static void TranslateWord(Dictionary<string, string> dictionary, string inputMessage, string outputMessage)
    {
        Console.WriteLine(inputMessage);
        string word = Console.ReadLine().ToLower().Trim();
        if (dictionary.TryGetValue(word, out string translation))
        {
            Console.WriteLine($"{outputMessage} {translation}");
        }
        else
        {
            Console.WriteLine("Перевод не найден, попробуйте еще раз.");
        }
    }

    static void AddWordsToDictionary(Dictionary<string, string> rusEstDictionary, Dictionary<string, string> estRusDictionary)
    {
        Console.WriteLine("Введите слово на русском:");
        string russianWord = Console.ReadLine().ToLower().Trim();
        Console.WriteLine("Введите слово на эстонском:");
        string estonianWord = Console.ReadLine().ToLower().Trim();

        if (!rusEstDictionary.ContainsKey(russianWord))
        {
            Translator.AddWordToRussianDictionary(russianWord);
            Translator.AddWordToEstonianDictionary(estonianWord);
            rusEstDictionary[russianWord] = estonianWord;
            estRusDictionary[estonianWord] = russianWord;
            Console.WriteLine("Слово успешно добавлено.");
        }
        else
        {
            Console.WriteLine("Слово уже существует в словаре.");
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
