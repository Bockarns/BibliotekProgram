using IndividuelltProjekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividuelltProjekt.Models
{
    public class Book
    {
        public int Id { get; set; }
        public long ISBN { get; set; }
        public string Auther { get; set; }
        public string Title { get; set; }
        public bool Avaliable { get; set; }
        public Book()
        {
            if (Auther != null)
                Auther = "";
            if (Title != null)
                Title = "";
        }
        public static void AddBook(long isbn, string auther, string title, bool avaliable)
        {
            using (var context = new BookContext())
            {
                var book = new Book {ISBN = isbn, Auther = auther, Title = title, Avaliable = avaliable };
                context.Books.Add(book);
                context.SaveChanges();
            }
        }
        public static bool CheckBookAvaliable(bool avaliable)
        {
            using (var context = new BookContext())
            {
                return context.Books.Any(b => b.Avaliable == avaliable);
            }
        }
        public static Book GetBookByAuther(string auther)
        {
            using (var context = new BookContext())
            {
                return context.Books.FirstOrDefault(b => b.Auther == auther)!;
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
                return context.Books.FirstOrDefault(b => b.ISBN == isbn)!;
            }
        }
        public static Book UpdateAuther(string auther, string updateauther)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Auther == auther);
                if (book == null)
                    return null!;

                book.Auther = updateauther;

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
        public static void DeleteBook(long isbn)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.ISBN == isbn);
                context.Books.Remove(book!);
                context.SaveChanges();
            }
        }
    }
}
