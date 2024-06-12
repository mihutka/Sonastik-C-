using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Translator
{
    private const string RussianFilePath = "rus.txt";
    private const string EstonianFilePath = "est.txt";

    // Чтение словаря для перевода с русского на эстонский
    public static Dictionary<string, string> ReadRussianToEstonianDictionary()
    {
        EnsureFileExists(RussianFilePath);
        EnsureFileExists(EstonianFilePath);

        string[] russianWords = File.ReadAllLines(RussianFilePath);
        string[] estonianWords = File.ReadAllLines(EstonianFilePath);

        return CreateDictionary(russianWords, estonianWords);
    }

    // Чтение словаря для перевода с эстонского на русский
    public static Dictionary<string, string> ReadEstonianToRussianDictionary()
    {
        EnsureFileExists(RussianFilePath);
        EnsureFileExists(EstonianFilePath);

        string[] russianWords = File.ReadAllLines(RussianFilePath);
        string[] estonianWords = File.ReadAllLines(EstonianFilePath);

        return CreateDictionary(estonianWords, russianWords);
    }

    // Создание словаря с проверкой на дубликаты
    private static Dictionary<string, string> CreateDictionary(string[] keys, string[] values)
    {
        var dictionary = new Dictionary<string, string>();

        for (int i = 0; i < keys.Length; i++)
        {
            string key = keys[i].Trim().ToLower();
            string value = values[i].Trim().ToLower();

            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
        }

        return dictionary;
    }

    // Добавление слова на русском в файл
    public static void AddWordToRussianDictionary(string word)
    {
        using (StreamWriter sw = File.AppendText(RussianFilePath))
        {
            sw.WriteLine(word.Trim().ToLower());
        }
    }

    // Добавление слова на эстонском в файл
    public static void AddWordToEstonianDictionary(string word)
    {
        using (StreamWriter sw = File.AppendText(EstonianFilePath))
        {
            sw.WriteLine(word.Trim().ToLower());
        }
    }

    private static void EnsureFileExists(string filePath)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
    }
}
