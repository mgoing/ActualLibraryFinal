using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ActualLibraryFinal                //Next TODO: adding a book with the relevant sections in their relevant methods as well as save and update working correctly
{
    internal class Program
    {
        public static string filePath = "fullBookList.txt";
        // List<NewBook> bookList = LoadBooksFromFile(filePath); //does need to be reused elsewhere or just once. 
        static void Main(string[] args)
        {



            string filePath = "fullBookList.txt"; //file path to hold the Books
            Library baseLibrary = new Library();
            baseLibrary.LoadBooksFromFile(filePath);

            menuHandlerClass menu = new menuHandlerClass();
            menu.DisplayMainMenuFirst();

            //tester
            foreach (var NewBook in baseLibrary.bookList)
            {
                Console.WriteLine($"Title: {NewBook.bookTitle} Author: {NewBook.bookTitle}");
            }
            //end tester



        }




        public class NewBook
        {
            public string bookTitle { get; set; }

            public string bookAuthor { get; set; }

            public string bookGenre { get; set; }

            public string bookSummary { get; set; }

            public long bookIBSN { get; set; }

            public bool bookAvail { get; set; }

            public string bookAgeRange { get; set; }

            public string bookPubDate { get; set; }



            public static string CreateBookTitle()
            {
                Console.Clear();
                Console.WriteLine("Please enter the title of the new book:");
                return Console.ReadLine();
            }

            public static string CreateBookAuthor()
            {
                Console.Clear();
                Console.WriteLine("Please enter the author of the new book:");
                return Console.ReadLine();
            }

            public static string CreateBookGenre()
            {
                Console.Clear();
                Console.WriteLine("Please enter the genre of the new book:");
                Console.WriteLine("1. Mystery ");
                Console.WriteLine("2. Romance ");
                Console.WriteLine("3. Horror ");
                Console.WriteLine("4. Biography/History ");
                Console.WriteLine("5. Informational ");
                Console.WriteLine("6. Sci-Fi ");
                Console.WriteLine("7. Fantasy ");
                Console.WriteLine("8. Other ");

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 8)
                    {
                        return choice switch
                        {
                            1 => "Mystery",
                            2 => "Romance",
                            3 => "Horror",
                            4 => "Biography/History",
                            5 => "Informational",
                            6 => "Sci-Fi",
                            7 => "Fantasy",
                            8 => "Other",
                            _=> throw new ArgumentOutOfRangeException()
                        };
                    }
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
                }
            }

            public static string CreateBookSummary()
            {
                Console.Clear();
                Console.WriteLine("Please write a short summary of the book:");
                Console.WriteLine("Note: Please make the summary under 100 characters.");
                string tempSummary = Console.ReadLine();
                while (tempSummary.Length >= 101) //Potentially change
                {
                    tempSummary = "";
                    Console.WriteLine("Please rewrite under 100 characters.");
                    tempSummary = Console.ReadLine();
                }
                
                return tempSummary;
            }

            public static long CreateBookISBN()
            {
                Console.WriteLine("Please enter the 13-digit IBSN code:");
                Console.WriteLine("Note: Does not have to be accurate but must be 13 digits");
                long tempIBSN = 0;
                string inputIBSN;

                while (true)
                {
                    inputIBSN = Console.ReadLine();

                    if (inputIBSN.Length == 13 && long.TryParse(inputIBSN, out tempIBSN))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input. Please enter an IBSN with 13 digits");
                    }
                }
                return long.Parse(Console.ReadLine());
            }

            public static bool CreateBookAvail()
            {
                bool bookAvail = true; //sets defailt availability after at creation to available
                return  bookAvail;
            }

            public static string CreateBookAgeRange()
            {
                Console.WriteLine("Please choose the appropriate age range:");
                Console.WriteLine("Note: Please enter the corresponding number of your choice");
                Console.WriteLine("1. Children");
                Console.WriteLine("2. Pre-Teen");
                Console.WriteLine("3. Teenager");
                Console.WriteLine("4. Young Adult");
                Console.WriteLine("5. Adult");
                string input = Console.ReadLine();


                int inputValue = 0;
                bool success = int.TryParse(input, out inputValue);

                bool valid = success && (1 <= inputValue && inputValue <= 5);
                while (!valid)
                {
                    Console.WriteLine("Invalid Input. Try again...");
                    Console.Write("Please enter a number 1-5: ");
                    input = Console.ReadLine();
                    success = int.TryParse(input, out inputValue);
                    valid = success && 1 <= inputValue && inputValue <= 5;
                }

                int tempChoiceNum = int.Parse(input);
                string tempAgeRange = "";

                Dictionary<int, Action> menuActions = new Dictionary<int, Action> //potentially need to add romanceGenre++ depending on search fuction
                {
                    { 1, () => tempAgeRange = "Children"},
                    { 2, () => tempAgeRange = "Pre-Teen" },
                    { 3, () => tempAgeRange = "Teenager"},
                    { 4, () => tempAgeRange = "Young Adult" },
                    { 5, () => tempAgeRange = "Adult" },
                };
 
                return input;
            }

            public static DateTime CreateBookPubDate()
            {
                
                    DateTime date;
                    string input;
                    int currentYear = DateTime.Now.Year;

                    while (true)
                    {
                        Console.Write("Please enter the book publication date in the format DD/MM/YYYY: ");
                        input = Console.ReadLine();

                        // Attempt to parse the input date, specifying the format and culture info
                        if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                        {
                            // Additional check to ensure the year is within the specified range
                            if (date.Year >= 1500 && date.Year <= currentYear)
                            {
                                return date;  // Valid date within the range
                            }
                            else
                            {
                                Console.WriteLine($"Year must be between 1500 and {currentYear}. Please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Please ensure the date is in DD/MM/YYYY format.");
                        }
                    }

                
            }




            public string updateBookGenre() //TODO Add how to choose the appropriate book to be updated
            {
                Console.Clear();
                //TODO Add the current gene to be changed
                Console.WriteLine("Please enter the new genre of your book:");
                Console.WriteLine("Please enter the corresponding number of the appropriate genre:");
                Console.WriteLine("1. Mystery ");
                Console.WriteLine("2. Romance ");
                Console.WriteLine("3. Horror ");
                Console.WriteLine("4. Biography/History ");
                Console.WriteLine("5. Informational ");
                Console.WriteLine("6. Sci-Fi ");
                Console.WriteLine("7. Fantasy ");
                Console.WriteLine("8. Other ");

                string tempChoice = Console.ReadLine();
                int tempChoiceNum = int.Parse(tempChoice);
                string tempGenre = "";


                Dictionary<int, Action> menuActions = new Dictionary<int, Action> //potentially need to add romanceGenre++ depending on search fuction
                {
                    { 1, () => tempGenre = "Mystery"},
                    { 2, () => tempGenre = "Romance" },
                    { 3, () => tempGenre = "Horror"},
                    { 4, () => tempGenre = "Biography/History" },
                    { 5, () => tempGenre = "Informational" },
                    { 6, () => tempGenre = "Sci-Fi" },
                    { 7, () => tempGenre = "Fantasy" },
                    { 8, () => tempGenre = "Other" },
                };
                return tempChoice;
            }

            public void updateBookSummary()
            {

            }

            public void updateBookAgeRange()
            {

            }

            public void updateBookAvail()
            {

            }




        }//CLASSNEWBOOK

        
        public class Library //add string filepath?
        {
            public List<NewBook> bookList { get; private set; }

            public void RegisterNewBook()
            {
                Console.Clear();
                Console.WriteLine("Generating Form...");

                NewBook newBook = new NewBook
                {
                    bookTitle = NewBook.CreateBookTitle(),
                    bookAuthor = NewBook.CreateBookAuthor(),
                    bookGenre = NewBook.CreateBookGenre(),
                    bookSummary = NewBook.CreateBookSummary(),
                    bookIBSN = NewBook.CreateBookISBN(),
                    bookAvail = NewBook.CreateBookAvail(),
                    bookAgeRange = NewBook.CreateBookAgeRange(),
                    bookPubDate = NewBook.CreateBookPubDate().ToString("dd/MM/yyyy")
                };

                bookList.Add(newBook);
                saveBooksToFile(newBook);
                Console.WriteLine("New book successfully added!");
            }
            public void LoadBooksFromFile(string filePath)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found. Library will start empty.");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    return;
                }
                try
                {
                    bookList = File.ReadAllLines(filePath)
                        .Skip(1)
                        .Where(line => !string.IsNullOrWhiteSpace(line))
                        .Select(line =>
                        {
                            var parts = line.Split(',');

                            return new NewBook
                            {
                                bookTitle = parts[0],
                                bookAuthor = parts[1],
                                bookGenre = parts[2],
                                bookSummary = parts[3],
                                bookIBSN = long.Parse(parts[4]),
                                bookAvail = bool.Parse(parts[5]),
                                bookAgeRange = parts[6],
                                bookPubDate = parts[7],
                            };
                        })
                        .ToList();
                    Console.WriteLine($"Successfully loaded {bookList.Count} books from file.");
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading books from file: {ex.Message}");
                }


            }



            public static void saveBooksToFile(NewBook book) //should it be static and void and what about filePath and true
            {
                string bookEntry = $"{book.bookTitle},{book.bookAuthor},{book.bookGenre},{book.bookSummary},{book.bookIBSN},{book.bookAvail},{book.bookAgeRange},{book.bookPubDate}";

                // Write the entry to the file, appending at the end.
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(bookEntry);
                }
                Console.WriteLine("Saving...");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Book successfully saved to file.");
            }

            //book management
            public void addNewBook(NewBook brandNewBook)         //does the information in parenthesis need to change to (NewBook ___whateverThisVariableIsNamed____)
            {
                Program.baseLibrary.RegisterNewBook();
            }

            public void updateBook(NewBook updatedBook)
            {

            }
            public void deleteBook(NewBook bookToBeDeleted)
            {

            }

            //Utility
            public void getBookGenreCounts()
            {

            }
            public void getBookAgeRangeCounts()
            {

            }
            public void getTotalBookCounts()
            {

            }
            public void getOldestBook()
            {

            }
            public void getYoungestBook()
            {

            }
            //TODO: add anymore fact generators

            //search
            // public void searchBooks(string query) //choose to (search all or list all) or (filter by category)
            //public void filterBooks(string filterType, string filterValue)


            //book actions
            //public void withdrawOrReturnBook(string ISBN, bool isReturning)

        }


       class menuHandlerClass                         //class has all the menu options for main menu, returning to main menu(without welcome)
        {

            private Dictionary<int, Action> menuActions;

            public void MenuHandler()
            {
                menuActions = new Dictionary<int, Action>
                {
                        { 1, AddNewBooks},
                        { 2, SearchForBooks},
                        { 3, WithdrawOrReturn},
                        { 4, EditBooks},
                        { 5, SurpriseFact},
                };
            }


            public void DisplayMainMenuFirst()
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("|                            Welcome to Malcolm's Library!                           |");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("Please navigate by entering the corresponding number of your choice from the options below.\n");
                Console.WriteLine("1. Enter a new book to be stored in library\n");
                Console.WriteLine("2. Search through catalogue\n");
                Console.WriteLine("3. Withdraw an existing book or return current book\n");
                Console.WriteLine("4. Edit current book details\n");
                Console.WriteLine("5. Surprise\n");

                bool validChoice = false;
                while (!validChoice)
                {
                    Console.Write("Enter your choice: ");
                    if (int.TryParse(Console.ReadLine(), out int choice) && menuActions.ContainsKey(choice))
                    {
                        menuActions[choice]();
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    }
                }

            }

           // private void AddNewBooks() =>  RegisterNewBook(); //TODO change this to allow the menue to call it
            private void SearchForBooks() => Console.WriteLine("Pulling up search..."); //add this console to start of addNewBook() and then call it here?? & do same below?
            private void WithdrawOrReturn() => Console.WriteLine("Gathering Options...");
            private void EditBooks() => Console.WriteLine("Pulling up library...");
            private void SurpriseFact() => Console.WriteLine("Generating facts...");


            public void DisplayMainMenu()
            {
                Console.WriteLine("----------MAIN MENU----------");
                Console.WriteLine("Please navigate by entering the corresponding number of your choice (1 or 2...) from the options below.");
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("1. Enter a new book to be stored in library\n");
                Console.WriteLine("2. Search through catalogue\n");
                Console.WriteLine("3. Withdraw an existing book or return current book\n");
                Console.WriteLine("4. Edit current book details\n");
                Console.WriteLine("5. Surprise");

                if (int.TryParse(Console.ReadLine(), out int choice) && menuActions.ContainsKey(choice))
                {
                    menuActions[choice]();
                }
                else
                {
                    Console.WriteLine("Invalid Choice. Please make an appropriate choice.");
                }

            }



        }




    }//CLASSPROGRAM
}//NAMESPACE




//TODO: Book class (With Get and Set & with methods like updateGenre()-update summary, age range, genre, availability
//TODO:Library Class With several methods & list<> booklist and filepath to CSV
        //TODO: AddBook() – Adds a new book to bookList and updates the CSV file.
        //TODO: UpdateBook() – Updates an existing book entry or the entire booklist in the CSV file.
        //TODO: DeleteBook() – Removes a book from bookList and updates the CSV file.
        //TODO: SaveBooksToFile() – Saves the entire book list to the CSV file.
        //TODO: LoadBooksFromFile() – Loads books from the CSV file into bookList.
        //TODO: GetGenreCounts() – Tracks the total number of books in each genre.
        //TODO: GetAgeRangeCounts() – Tracks the total number of books in each age range.
        //TODO: GetTotalBookCount() – Tracks the total number of books in the library.
        //TODO: SearchBooks() – General search functionality for books.
        //TODO: FilterBooks() – Filter books by genre, age range, or other parameters.
        //TODO: WithdrawOrReturnBook() – Handles updating the availability of books (e.g., for borrowing/returning).
        //TODO: GenerateRandomFact() – Generates a random library fact, such as "oldest book", "most popular genre", etc.
//TODO: Library facts class: stores and generates facts
//TODO: add rest of todo lmao