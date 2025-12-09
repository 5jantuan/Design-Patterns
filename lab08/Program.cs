List<Book> books = new List<Book>
{
    new Book { name = "Гарри Поттер", year = 1997, status = false },
    new Book { name = "Война и мир", year = 1869, status = false },
    new Book { name = "1984", year = 1949, status = false },
    new Book { name = "Twilight", year = 2005, status = true },
    new Book { name = "Сумерки", year = 2005, status = false }
};

Functions.BorrowBook(books, "1984");
Functions.ShowAllBooks(books);

Functions.ReturnBook(books, "1984");
Functions.ShowAllBooks(books);


class Book
{
    public required string name;
    public required int year;
    public required bool status;

    public string GetInfo()
    {
        string bookStatus = status ? "Взята" : "Доступна";
        return $"{name} ({year}) - {bookStatus}";
    }
}