using IndividuelltProjekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IndividuelltProjekt.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public bool Available { get; set; }
        public List<Loan> Loans { get; set; } = new();
        public Book()
        {
            if (Author != null)
                Author = "";
            if (Title != null)
                Title = "";
        }
        public static void AddBook(long isbn, string author, string title, bool available)
        {
            using (var context = new BookContext())
            {
                var book = new Book {Id = isbn, Author = author, Title = title, Available = available };
                context.Books.Add(book);
                context.SaveChanges();
            }
        }
        //Lista alla tillgängliga böcker
        public static void ListAllAvailableBooks()
        {
            using (var context = new BookContext())
            {
                //skapa en lista och lägg böckerna för att sedan läsa ut dom en efter en..
                var availableBooks = context.Books.Where(b => b.Available).ToList();
                foreach (var book in availableBooks)
                    Console.WriteLine($"\n***| Titel: {book.Title}\n***| Författare: {book.Author}\n***| ISBN: {book.Id}\n***| Finns tillgänglig för lån! ");
            }
        }
        public static void ListAllBooks()
        {
            using (var context = new BookContext())
            {
                //skapa en lista och lägg böckerna för att sedan läsa ut dom en efter en..
                var allBooks = context.Books.ToList();
                foreach (var book in allBooks)
                {
                    if(book.Available == true)
                        Console.WriteLine($"\n***| Titel: {book.Title}\n***| Författare: {book.Author}\n***| ISBN: {book.Id}\n***| Finns tillgänglig för lån! ");
                    else
                        Console.WriteLine($"\n***| Titel: {book.Title}\n***| Författare: {book.Author}\n***| ISBN: {book.Id}\n***| För tillfället utlånad! ");
                }
                    
            }
        }
        public static bool CheckBookAvaliable(bool available)
        {
            using (var context = new BookContext())
            {
                return context.Books.Any(b => b.Available == available);
            }
        }
        //Ska bygga om dessa nedan så dom visar böckerna här ifrån som void metoder för att städa program.cs
        public static Book GetBookByAuthor(string author)
        {
            using (var context = new BookContext())
            {
                return context.Books.FirstOrDefault(b => b.Author == author)!;
            }
        }
        public static Book GetBookByTitle(string title)
        {
            using (var context = new BookContext())
            {
                return context.Books.FirstOrDefault(b => b.Title == title)!;
            }
        }
        public static Book GetBookByISBN(long isbn)
        {
            using (var context = new BookContext())
            {
                return context.Books.FirstOrDefault(b => b.Id == isbn)!;
            }
        }
        public static Book UpdateISBN(long isbn, long updateISBN)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Id == isbn);
                if (book == null)
                    return null!;

                book.Id = updateISBN;

                context.SaveChanges();

                return book;
            }
        }
        public static Book UpdateAuthor(string author, string updateAuthor)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Author == author);
                if (book == null)
                    return null!;

                book.Author = updateAuthor;

                context.SaveChanges();

                return book;
            }
        }
        public static Book UpdateTitle(string title, string updatetitle)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Title == title);
                if (book == null)
                    return null!;

                book.Title = title;

                context.SaveChanges();

                return book;
            }
        }
        //public static Book UpdateAvaliable(long isbn, bool checkout)
        //{
        //    using (var context = new BookContext())
        //    {
        //        var book = context.Books.FirstOrDefault(b => b.ISBN == isbn);
        //        //var isAvaliable = context.Books.FirstOrDefault(b => b.Avaliable == checkout);
        //        if (book.Avaliable == true)
        //        {
        //            book.Avaliable = false;
        //        }
        //        else
        //            book.Avaliable = true;
        //        context.SaveChanges();
        //        return book;

        //    }
        //}
        public static void DeleteBook(long isbn)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Id == isbn);
                context.Books.Remove(book!);
                context.SaveChanges();
            }
        }
    }
}
