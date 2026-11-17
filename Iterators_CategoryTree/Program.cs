using System;
using System.Collections.Generic;

namespace Iterators_CategoryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            // Пункт 4. Дерево из 4 уровней вложенности и 12 узлов.
            Category root = new Category("Электроника");

            Category computers = root.Add("Компьютеры");
            Category laptops = computers.Add("Ноутбуки");
            laptops.Add("Игровые ноутбуки");
            laptops.Add("Ультрабуки");
            computers.Add("Мониторы");

            Category phones = root.Add("Смартфоны");
            phones.Add("Android");
            phones.Add("iOS");

            Category audio = root.Add("Аудио");
            audio.Add("Наушники");
            audio.Add("Колонки");

            Console.WriteLine("=== Дерево категорий ===");
            PrintTree(root, 0);

            Console.WriteLine();
            Console.WriteLine("=== Пункт 5а. Обход всего дерева: foreach по корневой категории ===");

            int nodeNumber = 0;
            foreach (Category category in root)
            {
                nodeNumber++;
                Console.WriteLine($"{nodeNumber,2}. {category.Name}");
            }

            Console.WriteLine($"Всего узлов: {nodeNumber} (включая узлы третьего и четвёртого уровня).");

            Console.WriteLine();
            Console.WriteLine("=== Пункт 5б. Только листья: Leaves() ===");

            int leafNumber = 0;
            foreach (Category leaf in root.Leaves())
            {
                leafNumber++;
                Console.WriteLine($"{leafNumber,2}. {leaf.Name}");
            }

            Console.WriteLine($"Всего листьев: {leafNumber} (категории без собственных подкатегорий).");

            Console.WriteLine();
            Console.WriteLine("Оба обхода реализованы через yield return и рекурсивный вызов");
            Console.WriteLine("самого итератора: промежуточный List<Category> не строится.");
        }

        // Вспомогательный вывод структуры дерева — только для наглядности.
        static void PrintTree(Category category, int level)
        {
            Console.WriteLine(new string(' ', level * 2) + category.Name);

            foreach (Category subcategory in category.Subcategories)
            {
                PrintTree(subcategory, level + 1);
            }
        }
    }
}
