using System.Collections;
using System.Collections.Generic;

namespace Iterators_CategoryTree
{
    public class Category : IEnumerable<Category>
    {
        public string Name { get; private set; }

        public List<Category> Subcategories { get; private set; }

        public Category(string name)
        {
            Name = name;
            Subcategories = new List<Category>();
        }

        public Category Add(string name)
        {
            Category subcategory = new Category(name);
            Subcategories.Add(subcategory);
            return subcategory;
        }

        // Пункт 2. Обход через yield return: сначала сама категория, затем ВСЕ
        // вложенные подкатегории на любом уровне. Рекурсия идёт через сам
        // перечислитель: перебор подкатегории — это снова foreach по Category,
        // то есть повторный вызов GetEnumerator(). Никакой List<T> со всеми
        // элементами заранее не строится: значения отдаются по одному.
        public IEnumerator<Category> GetEnumerator()
        {
            yield return this;

            foreach (Category subcategory in Subcategories)
            {
                foreach (Category node in subcategory)
                {
                    yield return node;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Пункт 3. Leaves() рекурсивно возвращает только категории без
        // собственных подкатегорий — на любом уровне глубины. Метод вызывает
        // сам себя у каждой подкатегории, а не собирает список заранее.
        public IEnumerable<Category> Leaves()
        {
            if (Subcategories.Count == 0)
            {
                yield return this;
            }
            else
            {
                foreach (Category subcategory in Subcategories)
                {
                    foreach (Category leaf in subcategory.Leaves())
                    {
                        yield return leaf;
                    }
                }
            }
        }
    }
}
