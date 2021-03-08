namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);


            //ex.2
            //Console.WriteLine(GetBooksByAgeRestriction(db,Console.ReadLine()));

            //ex.3
            //Console.WriteLine(GetGoldenBooks(db));

            //ex.4
            //Console.WriteLine(GetBooksByPrice(db));

            //ex.5
            //Console.WriteLine(GetBooksNotReleasedIn(db,int.Parse(Console.ReadLine())));

            //ex.6
            //Console.WriteLine(GetBooksByCategory(db,Console.ReadLine()));

            //ex.7
            //Console.WriteLine(GetBooksReleasedBefore(db,Console.ReadLine()));

            //ex.8
            //Console.WriteLine(GetAuthorNamesEndingIn(db,Console.ReadLine()));

            //ex.9
            //Console.WriteLine(GetBookTitlesContaining(db,Console.ReadLine()));

            //ex.10
            //Console.WriteLine(GetBooksByAuthor(db,Console.ReadLine()));

            //ex.11
            //Console.WriteLine(CountBooks(db,int.Parse(Console.ReadLine())));

            //ex.12
            //Console.WriteLine(CountCopiesByAuthor(db));

            //ex.13
            //Console.WriteLine(GetTotalProfitByCategory(db));

            //ex.14
            //Console.WriteLine(GetMostRecentBooks(db));

            //ex.15
            //IncreasePrices(db);

            //ex.16
            //Console.WriteLine(RemoveBooks(db));
        }
        //ex.2
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();

            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var titles = context.Books
                .Where(x => x.AgeRestriction == ageRestriction)
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(x => x.Title)
                .ToList();
            foreach (var title in titles)
            {
                sb.AppendLine(title.Title);
            }

            return sb.ToString().Trim();
        }
        //ex.3
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 5000 && b.EditionType == EditionType.Gold)
                .Select(b => new
                {
                    b.BookId,
                    b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().TrimEnd();
        }
        //ex.4
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }
            return sb.ToString().Trim();
        }
        //ex.5
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    b.Title,
                    b.BookId
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().Trim();
        }
        //ex.6
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            List<string> categories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(b => b.ToLower())
                .ToList();
            List<string> books = new List<string>();
            foreach (var category in categories)
            {
                List<string> currBooks = context
                    .Books
                       .Where(b => b.BookCategories.Any(bc => bc.Category.Name.ToLower() == category))
                        .Select(b => b.Title)
                         .ToList();
                books.AddRange(currBooks);
            }

            foreach (var book in books.OrderBy(b => b))
            {
                sb.AppendLine(book);
            }

            return sb.ToString().Trim();
        }
        //ex.7
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();
            DateTime currDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Date < currDate)
                .Select(b => new
                {
                    b.ReleaseDate,
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().Trim();
        }
        //ex.8
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var names = context.Authors
                .Where(b => b.FirstName.EndsWith(input))
                .Select(b => new
                {
                    FullName = b.FirstName + " " + b.LastName
                })
                .OrderBy(b => b.FullName)
                .ToList();

            foreach (var name in names)
            {
                sb.AppendLine(name.FullName);
            }
            return sb.ToString().Trim();
        }
        //ex.9
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().Trim();
        }
        //ex.10
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    FullName = b.Author.FirstName + " " + b.Author.LastName
                })
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.FullName})");
            }


            return sb.ToString().Trim();
        }
        //ex.11
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var bookCount = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return bookCount;
        }
        //ex.12 
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var numberOfBooks = context.Authors
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,
                    Copies = a.Books.Select(a => a.Copies).Sum()
                })
                .OrderByDescending(b => b.Copies)
                .ToList();

            foreach (var books in numberOfBooks)
            {
                sb.AppendLine($"{books.FullName} - {books.Copies}");
            }

            return sb.ToString().Trim();
        }
        //ex.13
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var profitCategories = context.Categories
                .Select(c => new
                {
                    TotalProfit = c.CategoryBooks.Select(cb => cb.Book.Price * cb.Book.Copies).Sum(),
                    CategoryName = c.Name
                })
               .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.CategoryName)
                .ToList();

            foreach (var category in profitCategories)
            {
                sb.AppendLine($"{category.CategoryName} ${category.TotalProfit:F2}");
            }
            return sb.ToString().Trim();
        }
        //ex.14
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var topcategoriesBooks = context.Categories
                .Select(c => new
                {
                    c.Name,
                    MostReacentBooks = c.CategoryBooks.Select(b => new
                    {
                        b.Book.Title,
                        b.Book.ReleaseDate
                    })
                    .OrderByDescending(b => b.ReleaseDate)
                    .Take(3)
                })
                .OrderBy(c => c.Name)
                .ToList();


            foreach (var category in topcategoriesBooks)
            {
                sb.AppendLine($"--{category.Name}");
                foreach (var books in category.MostReacentBooks)
                {
                    sb.AppendLine($"{books.Title} ({books.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().Trim();
        }
        //ex.15
        public static void IncreasePrices(BookShopContext context)
        {
            var booksToIncrease = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();
            foreach (var book in booksToIncrease)
            {
                book.Price += 5;
            }
            context.SaveChanges();
        }
        //ex.16
        public static int RemoveBooks(BookShopContext context)
        {
            var booksToRemove = context.Books
                 .Where(b => b.Copies < 4200)
                 .ToList();

            context.Books.RemoveRange(booksToRemove);

            context.SaveChanges();

            return booksToRemove.Count;
        }
    }
}
