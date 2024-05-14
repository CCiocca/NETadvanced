//Consideriamo uno scenario in cui gestisci una biblioteca e hai bisogno di un modo per tenere traccia dei libri presi in prestito. 
//Ogni libro ha un titolo, un autore e un numero di copie disponibili.
//Quando un libro viene preso in prestito, il numero di copie disponibili diminuisce di uno.
//
//Inoltre, si desidera aggiungere funzionalità come la possibilità di estendere la data di scadenza del prestito e di 
//inviare notifiche via email quando un libro è in ritardo.

var book1 = new Book("Principi di biochimica", "Lehninger", 4);

DeadLineBookDecorator decoratedBook = new DeadLineBookDecorator(book1);

decoratedBook.TakeBook();
decoratedBook.ExtendDeadline(7);
decoratedBook.SendExpiryMail();
decoratedBook.ReturnBook();

// classe abs
public class Book
{
    public string Title { get; }
    public string Author { get; }
    public int CopyNumber { get; set; }

    public Book(string title, string author, int copyNumber) 
    {
        Title = title;
        Author = author;
        CopyNumber = copyNumber;
    }

    public virtual void TakeBook()
    {
        if(CopyNumber > 0)
        {
            CopyNumber -= 1;
            Console.WriteLine($"Liibro {Title} preso in prestito. Copie rimanenti {CopyNumber}");
        }
        else
        {
            Console.WriteLine("Non ci sono copie disponibili");
        }

    }
    public virtual void ReturnBook()
    {
        CopyNumber += 1;
        Console.WriteLine($"Libro restituito. Numero di copie disponibili: {CopyNumber}");
    }
}

//decorator
public abstract class BookDecorator : Book
{
    private Book _book;

    public BookDecorator(Book book) : base(book.Title, book.Author, book.CopyNumber)
    {
        _book = book;
    }

    public override void TakeBook()
    {
        _book.TakeBook();
    }

    public override void ReturnBook()
    {
        _book.ReturnBook();
    }
}


//specifici decorator

public class DeadLineBookDecorator : BookDecorator
{
    private DateTime _deadline;
    public DeadLineBookDecorator (Book book) : base(book) 
    { 
        _deadline = DateTime.Now.AddDays(15); //scadenza di 15 gg da quando prendo il libro (aka fun take a book) (numero scelto random)
    }

     public void ExtendDeadline(int extensionDays)
    {
        _deadline = _deadline.AddDays(extensionDays);
        Console.WriteLine($"Scadenza estesa al {_deadline.ToString()}");
    }

    public void SendExpiryMail()
    {
        if (_deadline < DateTime.Now)
        {
            Console.WriteLine("Scadenza oltrepassata, iviata mail di sollecito");
        }
    }
}
