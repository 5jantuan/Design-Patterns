static class Functions
{
    public static void BorrowBook(List<Book> books, string name)
    {
        Book? book = books.Find(b => b.name == name);
        if (book is null)
        {
            Console.WriteLine("Книга не найдена.");
            return;
        }

        book.status = true;
    }

    public static void ReturnBook(List<Book> books, string name)
    {
        Book? book = books.Find(b => b.name == name);
        if (book is null)
        {
            Console.WriteLine("Книга не найдена в библиотеке.");
            return;
        }

        book.status = false;
    }

    public static void ShowAllBooks(List<Book> books)
    {
        Console.WriteLine("Список книг:");
        foreach (Book book in books)
        {
            Console.WriteLine(book.GetInfo());
        }
    }
}
