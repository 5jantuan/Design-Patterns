List<Book> initialBooks = new List<Book>
{
    new Book { name = "Гарри Поттер", year = 1997, status = false },
    new Book { name = "Война и мир", year = 1869, status = false },
    new Book { name = "1984", year = 1949, status = false },
    new Book { name = "Twilight", year = 2005, status = true },
    new Book { name = "Сумерки", year = 2005, status =false}
};

Library library = new Library(initialBooks);

library.BorrowBook("1984");
library.ShowAllBooks();
library.ReturnBook("1984");
library.ShowAllBooks();

class Book
{
    public required string name = null!;
    public required int year;
    public required bool status;

    public string GetInfo()
    {
        string bookStatus = status ? "Взята" : "Доступна";
        return $"{name} ({year}) - {bookStatus}";
    }
}

class Library
{
    private List<Book> books;

    public Library(List<Book> initialBooks)
    {
        books = initialBooks;
    }
    public void BorrowBook(string name)
    {
        Book book = books.Find(b => b.name == name);
        if (book != null)
            book.status = true;
        else
            Console.WriteLine("Книга не найдена.");
    }

    public void ReturnBook(string name)
    {
        Book book = books.Find(b => b.name == name);
        if (book != null)
            book.status = false;
        else
            throw new ArgumentException("Книга не найдена в библиотеке.");
    }

    public void ShowAllBooks()
    {
        Console.WriteLine("Список книг:");
        foreach (Book book in books)
        {
            Console.WriteLine(book.GetInfo());
        }
    }
}