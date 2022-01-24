using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicionaryConsoleCSS
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> currentDictionary = new Dictionary<string, string>();// создаем текущий словарь
            bool isUkranianEnglish = false; // логическая переменная обозначающая украино-английский словарь
            bool isEnglishkranian = false;// англо-украинский

            while (true)
            {
                currentDictionary = readFromFile();// получить с файла словарь и записать в переменную
                Console.WriteLine("Enter the value from menu" +
                    "\n1.Create new dictionary" +
                    "\n2. Add word to dictionary" +
                    "\n3. Edit word in dictionary"
                    +"\n4. Delete word from current dirictory" +
                    "\n5. Show all dictionary" +
                    "\n6. Export dictionary to file" +
                    "\n7. Exit");
                int choise = 0;
                if (!Int32.TryParse(Console.ReadLine(), out choise))// проверка на число введенное пользователем
                {
                    Console.WriteLine("Incorrect input");// если не число
                    continue;
                }
                switch(choise)// выбираем согласно условия
                {
                    case 1:
                        Console.WriteLine("Enter 1 if this ukraninan to enlish or 2 - if this english to ukranian");
                        int val = int.Parse(Console.ReadLine());// выбор по критерию укр-анг или англо-укр
                        if (val == 1)// если критерий равен 1
                            isUkranianEnglish = true;
                        if (val == 2)// если критерий равен 2
                            isEnglishkranian = true;
                        createDictionary(isUkranianEnglish, isEnglishkranian);// создать словарь

                        break;
                    case 2:
                        addWordToDictionary(currentDictionary);// добавить до словаря
                        break;
                    case 3:
                        editWord(currentDictionary);// изменить слово
                        break;
                    case 4:
                        deleteWord(currentDictionary);// удалить слово
                        break;
                    case 5:
                        printDictionary(currentDictionary);// печтать словаря
                        break;
                    case 6:
                        exportToFile(currentDictionary);// экспортировать до файла
                        break;
                    case 7:
                        Environment.Exit(0);// выход
                        break;
                    default:
                        Console.WriteLine("No such propreties, try again!");
                        break;

                }
                Console.ReadKey();
                Console.Clear();

            }
        }

        private static void exportToFile(Dictionary<string, string> currentDictionary)
        {
            Console.WriteLine("Enter path or name file");
            string path = Console.ReadLine();




            foreach (var wd in currentDictionary)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(wd.Key + "|" + wd.Value);// записываем в файл, путь которого добавили
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void printDictionary(Dictionary<string, string> currentDictionary)
        {
            foreach (var wd in currentDictionary)
            {
                Console.WriteLine($"{wd.Key} - {wd.Value}");// выводим словарь на экран
            }
        }

        private static void deleteWord(Dictionary<string, string> currentDictionary)
        {
            Console.WriteLine("Enter word to delete");
            string word = Console.ReadLine();// слово
            bool finded = false;

            foreach (var wd in currentDictionary)
            {
                if (wd.Key == word)// если ключ в словаре равен слову
                {
                    finded = true;
                    break;
                }
            }
            if (finded == true)
            {
                currentDictionary.Remove(word);// удалить слово
                Console.WriteLine("This discribe sucsesfully deleted!");
            }
        }

        private static void editWord(Dictionary<string, string> currentDictionary)// изменить слово
        {
            Console.WriteLine("Enter word to edit");
            string word = Console.ReadLine();
            bool finded = false;
            string key = "";
            foreach (var wd in currentDictionary)// ищем ключ по нашему слову
            {
                if (wd.Value == word)
                {
                    finded = true;
                    key = wd.Key;
                    break;

                }
            }
            if (finded == true)
            {

                Console.WriteLine("Enter new translated text");
                string nText = Console.ReadLine();

                currentDictionary[key] = nText;// меняем новый текст
            }


            // записываем в словарь

            string writePath = @"dir.txt";
           

     

            foreach (var wd in currentDictionary)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(wd.Key + "|" + wd.Value);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            }

        private static Dictionary<string, string> readFromFile()// считать с файла
        {
            Dictionary<string, string> currentDictionary = new Dictionary<string, string>();

            string path = @"dir.txt";

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    string[] args = line.Split('|');// получаем и разбиваем по разделителю |
                    if (args.Length == 2)
                    {
                        currentDictionary[args[0]] = args[1];// добавляем в словарь: слово - перевод
                    }
                }
            }
            return currentDictionary;

        }

        private static void addWordToDictionary(Dictionary<string, string> currentDictionary)// добавляем слово в слвоарь
        {

            string writePath = @"dir.txt";
            string word = "";
            Console.WriteLine("Enter abroad word:");
            word = Console.ReadLine();

            Console.WriteLine("Enter translate to this word");
            string translate = Console.ReadLine();

                 bool fd = currentDictionary.ContainsKey(word);
            if (fd == true)
            {
                string value = currentDictionary[word];
                currentDictionary[word] = value + ", " + translate;
            }
            else
            {
                currentDictionary[word] = translate;
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(word + "|" + currentDictionary[word]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void createDictionary(bool isUkranianEnglish, bool isEnglishkranian)// создаем словарь
        {
            int val = 0;
            if (isUkranianEnglish)
                val = 1;
            if (isEnglishkranian)
                val = 2;

           
        }


    }
}
