




//TODO Flesh out fact system
//TODO test adding a new book
//TODO test deleting a new book
//TODO test withdrawing a new book
//TODO improve UX for navigation and handling User input
//TODO fix age range









//TODO Flesh out fact system
//TODO test adding a new book
//TODO test deleting a new book
//TODO test withdrawing a new book
//TODO improve UX for navigation and handling User input
//TODO fix age range




using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using static ActualLibraryFinal.Program;

namespace ActualLibraryFinal
{
    internal class Program
    {
        // Define the file path for storing books
        public static string filePath = "fullBookList.txt";

        static void Main(string[] args)
        {
            // Initialize the Library with the file path
            Library baseLibrary = new Library(filePath);
            baseLibrary.LoadBooksFromFile();

            // Initialize the menu handler with the Library instance
            menuHandlerClass menu = new menuHandlerClass(baseLibrary);


            // Main loop to keep the menu active until the user decides to exit
            try
            {
                bool exit = false;
                while (!exit)
                {
                    menu.DisplayMainMenuFirst();

                    Console.WriteLine("\nDo you want to return to the main menu? (yes/no)");
                    string input = Console.ReadLine().ToLower();


                    while (input != "yes" && input != "no")
                    {
                        Console.WriteLine("Please enter \"yes\" or \"no\":");
                        input = Console.ReadLine().ToLower();
                    }


                    if (input == "no")
                    {
                        exit = true;
                        Console.WriteLine("Exiting Malcolm's library. Goodbye!");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors if any
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            finally
            {

                Environment.Exit(0);
            }
        }






        // NewBook class which holds all the book parameters
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

            public void displayInfo()
            {
                Console.WriteLine($"{bookTitle}");
                Console.WriteLine($"Written by: {bookAuthor} on {bookPubDate}.");
                Console.WriteLine($"Genre: {bookGenre}\nSuggested age range: {bookAgeRange}");
                Console.WriteLine($"ISBN: {bookIBSN}");

                Console.WriteLine($"Summary: {bookSummary}\n");
                if (bookAvail == true)
                {
                    Console.WriteLine("AVAILABLE ");
                }
                else
                {
                    Console.WriteLine("Currently unavailable. Contact librarian for more information");
                }
                Console.WriteLine("-----------------------------\n");

            }

            // Method to create a new book title
            public static string CreateBookTitle()
            {
                string titleString = ("=== Registering a New Book ===\n");
                Utility.ClearScreenKeepLine(titleString);
                Console.WriteLine("Please enter the title of the new book:");
                return Console.ReadLine();
            }

            // Method to create a new book author
            public static string CreateBookAuthor()
            {
                string titleString = ("=== Registering a New Book ===\n");
                Utility.ClearScreenKeepLine(titleString);
                Console.WriteLine("Please enter the author of the new book:");
                return Console.ReadLine();
            }

            // Method to create a new book genre with validation
            public static string CreateBookGenre()
            {
                string titleString = ("=== Registering a New Book ===\n");
                Utility.ClearScreenKeepLine(titleString);
                Console.WriteLine("Please choose the genre of the new book:");
                Console.WriteLine("1. Mystery");
                Console.WriteLine("2. Romance");
                Console.WriteLine("3. Horror");
                Console.WriteLine("4. Biography/History");
                Console.WriteLine("5. Informational");
                Console.WriteLine("6. Sci-Fi");
                Console.WriteLine("7. Fantasy");
                Console.WriteLine("8. Other");

                string tempGenre = "";
                bool testBool = true;

                while (testBool)
                {
                    Console.Write("Enter your choice (1-8): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 8)
                    {
                        // Assign the genre based on the user's choice
                        switch (choice)
                        {
                            case 1: tempGenre = "Mystery"; break;
                            case 2: tempGenre = "Romance"; break;
                            case 3: tempGenre = "Horror"; break;
                            case 4: tempGenre = "Biography/History"; break;
                            case 5: tempGenre = "Informational"; break;
                            case 6: tempGenre = "Sci-Fi"; break;
                            case 7: tempGenre = "Fantasy"; break;
                            case 8: tempGenre = "Other"; break;
                        }
                        testBool = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
                    }
                }

                return tempGenre;
            }

            // Method to create a new book summary with character limit
            public static string CreateBookSummary()
            {
                string titleString = ("=== Registering a New Book ===\n");
                Utility.ClearScreenKeepLine(titleString);
                Console.WriteLine("Please write a short summary of the book:");
                Console.WriteLine("Note: Please make the summary under 100 characters.");
                string tempSummary = Console.ReadLine();
                while (tempSummary.Length >= 101)
                {
                    Console.WriteLine("Summary too long. Please rewrite under 100 characters.");
                    tempSummary = Console.ReadLine();
                }
                return tempSummary;
            }

            // Method to create a new book ISBN with validation
            public static long CreateBookISBN()
            {
                string titleString = ("=== Registering a New Book ===\n");
                Utility.ClearScreenKeepLine(titleString);
                Console.WriteLine("Please enter the 13-digit ISBN code:");
                Console.WriteLine("Note: Must be exactly 13 digits.");

                while (true)
                {
                    string inputIBSN = Console.ReadLine();
                    if (inputIBSN.Length == 13 && long.TryParse(inputIBSN, out long tempIBSN))
                    {
                        return tempIBSN;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a 13-digit ISBN.");
                    }
                }
            }

            // Method to set book availability (default is available)
            public static bool CreateBookAvail()
            {
                return true;                                                                    // Book is available upon creation
            }

            // Method to create a new book age range with validation
            public static string CreateBookAgeRange()
            {
                string titleString = ("=== Registering a New Book ===\n");
                Utility.ClearScreenKeepLine(titleString);
                Console.WriteLine("Please choose the appropriate age range:");
                Console.WriteLine("1. Children");
                Console.WriteLine("2. Pre-Teen");
                Console.WriteLine("3. Teenager");
                Console.WriteLine("4. Young Adult");
                Console.WriteLine("5. Adult");

                while (true)
                {
                    Console.Write("Enter your choice (1-5): ");
                    string input = Console.ReadLine();
                    int choice = int.Parse(input);
                    if (int.TryParse(input, out choice) && choice >= 1 && choice <= 5)
                    {
                        string tempAgeRange = "";

                        Dictionary<int, Action> menuActions = new Dictionary<int, Action>               //potentially need to add romanceGenre++ depending on search fuction
                                        {
                                            { 1, () => tempAgeRange = "Children"},
                                            { 2, () => tempAgeRange = "Pre-Teen" },
                                            { 3, () => tempAgeRange = "Teenager"},
                                            { 4, () => tempAgeRange = "Young Adult" },
                                            { 5, () => tempAgeRange = "Adult" },
                                        };

                        return tempAgeRange;
                    }
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                }
            }

            // Method to create a new book publication date with validation
            public static DateTime CreateBookPubDate()
            {
                string titleString = ("=== Registering a New Book ===\n");
                Utility.ClearScreenKeepLine(titleString);
                DateTime date;
                int currentYear = DateTime.Now.Year;

                while (true)
                {
                    Console.Write("Please enter the book publication date (DD/MM/YYYY): ");
                    string input = Console.ReadLine();

                    if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        if (date.Year >= 1500 && date.Year <= currentYear)
                        {
                            return date;
                        }
                        else
                        {
                            Console.WriteLine($"Year must be between 1500 and {currentYear}. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please use DD/MM/YYYY.");
                    }
                }
            }









            public static List<NewBook> SearchForBook(List<NewBook> bookList, string filterType)
            {
                List<NewBook> matchingBooks = new List<NewBook>();
                string searchTerm = "";


                if (filterType.Equals("genre", StringComparison.OrdinalIgnoreCase))
                {
                    // Use CreateBookGenre to select the genre
                    searchTerm = CreateBookGenre();
                }
                else if (filterType.Equals("age range", StringComparison.OrdinalIgnoreCase))
                {
                    // Prompt for specific age range
                    Console.WriteLine("Please select a specific age range:");
                    Console.WriteLine("1. Children");
                    Console.WriteLine("2. Pre-Teen");
                    Console.WriteLine("3. Teenager");
                    Console.WriteLine("4. Young Adult");
                    Console.WriteLine("5. Adult");

                    string ageRangeChoice = Console.ReadLine();
                    searchTerm = AgeRangeSelection(ageRangeChoice);
                }
                else
                {
                    // For other filter types (title, author, publication date)
                    Console.WriteLine($"Enter the search term for {filterType}:");
                    searchTerm = Console.ReadLine();
                }

                // Filter based on the specified search term and filter type
                matchingBooks = bookList.Where(book =>
                    (filterType.Equals("title", StringComparison.OrdinalIgnoreCase) && book.bookTitle.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (filterType.Equals("author", StringComparison.OrdinalIgnoreCase) && book.bookAuthor.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (filterType.Equals("genre", StringComparison.OrdinalIgnoreCase) && string.Equals(book.bookGenre, searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (filterType.Equals("age range", StringComparison.OrdinalIgnoreCase) && string.Equals(book.bookAgeRange, searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (filterType.Equals("publication date", StringComparison.OrdinalIgnoreCase) && book.bookPubDate.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();

                return matchingBooks;
            }
            private static string AgeRangeSelection(string choice)
            {
                string selectedAgeRange = "";

                switch (choice)
                {
                    case "1":
                        selectedAgeRange = "Children";
                        break;
                    case "2":
                        selectedAgeRange = "Pre-Teen";
                        break;
                    case "3":
                        selectedAgeRange = "Teenager";
                        break;
                    case "4":
                        selectedAgeRange = "Young Adult";
                        break;
                    case "5":
                        selectedAgeRange = "Adult";
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                return selectedAgeRange;
            }

            public static List<NewBook> GeneralSearch(List<NewBook> bookList, string searchTerm)
            {
                // Convert search term to lowercase for case-insensitive matching
                searchTerm = searchTerm.ToLower();

                // Search across all major fields for matches
                List<NewBook> matchingBooks = bookList.Where(book =>
                    book.bookTitle.ToLower().Contains(searchTerm) ||
                    book.bookAuthor.ToLower().Contains(searchTerm) ||
                    book.bookGenre.ToLower().Contains(searchTerm) ||
                    book.bookPubDate.ToLower().Contains(searchTerm)
                ).ToList();

                return matchingBooks;
            }

            public static void SearchMenu(List<NewBook> bookList)
            {
                Console.WriteLine("Would you like to run a (1) Filtered Search, (2) General Search or (3) List all Books?");
                string searchTypeChoice = Console.ReadLine();

                if (searchTypeChoice == "1")
                {
                    // Filtered Search
                    Console.WriteLine("Enter the filter type (title, author, genre, publication date, age range):");
                    string filterType = Console.ReadLine();

                    List<NewBook> filteredResults = SearchForBook(bookList, filterType);

                    if (filteredResults.Any())
                    {
                        Console.WriteLine("Books found:");
                        foreach (var book in filteredResults)
                        {
                            book.displayInfo();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books found with the given search criteria.");
                    }
                }
                else if (searchTypeChoice == "2")
                {
                    // General Search
                    Console.WriteLine("Enter the search term:");
                    string searchTerm = Console.ReadLine();

                    List<NewBook> generalResults = GeneralSearch(bookList, searchTerm);

                    if (generalResults.Any())
                    {
                        Console.WriteLine("Books found:");
                        foreach (var book in generalResults)
                        {
                            book.displayInfo();
                        }
                    }

                    else
                    {
                        Console.WriteLine("No books found matching the search term.");
                    }
                }
                else if (searchTypeChoice == "3")
                {
                    foreach (var book in bookList)
                    {
                        book.displayInfo();
                    }


                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1 for Filtered Search, 2 for General Search or 3 for Full Book List.");
                }
            }

        }









        // new Library class
        public class Library
        {
            public List<NewBook> bookList { get; private set; }
            private string filePath;

            // Constructor to initialize the Library with a file path
            public Library(string path)
            {
                filePath = path;
                bookList = new List<NewBook>();
            }

            // Method to register a new book
            public void RegisterNewBook()
            {
                Console.Clear();


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
                SaveEntireLibraryToFile();                                                      // Save the entire library for consistency
                Console.WriteLine("\nNew book successfully added!");
            }

            // Method to load books from the file
            public void LoadBooksFromFile()
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found. Creating a new library file...");
                    File.WriteAllText(filePath, "Title, Author, Genre, Summary, ISBN, Availability, AgeRange, PublicationDate\n");
                    return;

                }

                try
                {
                    bookList = File.ReadAllLines(filePath)
                        .Skip(1)
                        .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("Title", StringComparison.OrdinalIgnoreCase))
                        .Select(line =>
                        {
                            var parts = line.Split(',');

                            if (parts.Length != 8)
                                throw new FormatException($"Invalid line format: {line}");

                            return new NewBook
                            {
                                bookTitle = parts[0],
                                bookAuthor = parts[1],
                                bookGenre = parts[2],
                                bookSummary = parts[3],
                                bookIBSN = long.TryParse(parts[4], out var isbn) ? isbn : 0,
                                bookAvail = bool.TryParse(parts[5], out var avail) && avail,
                                bookAgeRange = string.IsNullOrWhiteSpace(parts[6]) ? "Unknown" : parts[6],
                                bookPubDate = parts[7],
                            };




                        })
                        .ToList();
                    Console.WriteLine($"Successfully loaded {bookList.Count} book(s) from file.");
                    Console.WriteLine("Press any button to continue...");
                    Console.ReadLine();
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error loading book(s) from file: {ex.Message}");
                    bookList = new List<NewBook>();
                }

            }


            public void SaveEntireLibraryToFile()
            {                                                                                                   // Method to save the entire library to the file
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath, false))                             // Overwriting the file
                    {

                        writer.WriteLine("Title,Author,Genre,Summary,ISBN,Availability,AgeRange,PublicationDate");

                        foreach (var book in bookList)
                        {
                            writer.WriteLine($"{book.bookTitle},{book.bookAuthor},{book.bookGenre},{book.bookSummary},{book.bookIBSN},{book.bookAvail},{book.bookAgeRange},{book.bookPubDate}");
                        }
                    }
                    Console.WriteLine("Library saved successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving library: {ex.Message}");
                }
            }



            public void UpdateBook()                                                                                     //method to update a single book
            {

                bool menuGuide = false;

                while (!menuGuide)
                {
                    Console.WriteLine("Would you like to (1) edit a current book or (2) delete a book from the library?");
                    string deleteOrEdit = (Console.ReadLine());



                    if (deleteOrEdit == "1")
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("=== Update an Existing Book ===\n");
                            Console.Write("Enter the Title of the book to update: ");
                            string titleInput = Console.ReadLine();


                            var matchingBooks = bookList
                                .Where(b => b.bookTitle.IndexOf(titleInput, StringComparison.OrdinalIgnoreCase) >= 0)
                                .ToList();

                            if (!matchingBooks.Any())                                                                    // Find all books with titles that contain the input (case-insensitive)
                            {
                                Console.WriteLine("No books found with the given title.");
                                return;
                            }


                            Console.WriteLine("\nMatching Books:");
                            for (int i = 0; i < matchingBooks.Count; i++)                                                       // If multiple matches, allow the user to choose
                            {
                                Console.WriteLine($"{i + 1}. {matchingBooks[i].bookTitle} by {matchingBooks[i].bookAuthor}");
                            }

                            Console.Write("\nEnter the number of the book you want to update: ");
                            if (!int.TryParse(Console.ReadLine(), out int selectedIndex) || selectedIndex < 1 || (selectedIndex > matchingBooks.Count))
                            {
                                Console.WriteLine("Invalid selection.");
                                return;
                            }


                            var book = matchingBooks[selectedIndex - 1];                                                // Get the selected book
                            string lineToKeep = ($"\nEditing Book: {book.bookTitle} by {book.bookAuthor}\n");

                            // Prompt user for each field, allowing them to press ENTER to keep current value
                            book.bookTitle = GetUpdatedField("Title", book.bookTitle);
                            //        Utility.ClearScreenKeepLine(lineToKeep);
                            book.bookAuthor = GetUpdatedField("Author", book.bookAuthor);
                            //        Utility.ClearScreenKeepLine(lineToKeep);
                            Console.WriteLine($"Current Genre: {book.bookGenre}");
                            Console.WriteLine("Enter new genre or press ENTER to keep current value:");
                            string genreInput = Console.ReadLine();
                            if (!string.IsNullOrEmpty(genreInput))
                            {
                                string newGenre = NewBook.CreateBookGenre();                                     // reusing old method that adds the genre of a new book
                                                                                                                 //            Utility.ClearScreenKeepLine(lineToKeep);
                                book.bookGenre = newGenre;
                            }


                            //        Utility.ClearScreenKeepLine(lineToKeep);
                            Console.WriteLine($"Current Age Range: {book.bookAgeRange}");
                            Console.WriteLine("Enter new age range or press ENTER to keep current value:");
                            string ageRangeInput = Console.ReadLine();
                            if (!string.IsNullOrEmpty(ageRangeInput))
                            {
                                string newAgeRange = NewBook.CreateBookAgeRange();                                      // reusing old method created to add the age range of a new book
                                book.bookAgeRange = newAgeRange;
                            }


                            //        Utility.ClearScreenKeepLine(lineToKeep);
                            Console.WriteLine($"Current Summary: {book.bookSummary}");
                            Console.WriteLine("Enter new summary (max 100 characters) or press ENTER to keep current value:");
                            string summaryInput = Console.ReadLine();                                                               //check summary length < 100 characters
                            if (!string.IsNullOrEmpty(summaryInput))
                            {
                                while (summaryInput.Length > 100)
                                {
                                    Console.WriteLine("Summary is too long. Please enter a summary of 100 characters or less:");
                                    summaryInput = Console.ReadLine();
                                }
                                book.bookSummary = summaryInput;
                            }

                            //        Utility.ClearScreenKeepLine(lineToKeep);
                            book.bookPubDate = GetUpdatedField("Publication Date", book.bookPubDate);
                            //        Utility.ClearScreenKeepLine(lineToKeep);
                            book.bookAvail = PromptForAvailability(book.bookAvail);

                            SaveEntireLibraryToFile();                                                                          // Save changes to the entire library
                            Console.WriteLine("\nBook updated successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error updating book: {ex.Message}");
                        }
                        menuGuide = true;
                    }
                    else if (deleteOrEdit == "2")
                    {
                        DeleteBook();
                        menuGuide = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter 1 or 2: ");
                        deleteOrEdit = Console.ReadLine();
                    }
                }
            }

            // Helper method to get updated field values
            private string GetUpdatedField(string fieldName, string currentValue)
            {
                Console.Write($"Enter new {fieldName} (or press ENTER to keep \"{currentValue}\"): ");
                string input = Console.ReadLine();
                return string.IsNullOrWhiteSpace(input) ? currentValue : input;
            }

            // Helper method to update availability
            private bool PromptForAvailability(bool currentAvail)
            {
                Console.Write($"Is the book available? (current: {(currentAvail ? "Yes" : "No")}) Enter 'yes' or 'no' (or press ENTER to keep current): ");
                string input = Console.ReadLine().ToLower();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return currentAvail;
                }
                else if (input == "yes")
                {
                    return true;
                }
                else if (input == "no")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Keeping current availability.");
                    return currentAvail;
                }
            }


            public void DeleteBook()                                                            //TODO change edit C/P to delete and implement actual deleting
            {
                Console.Clear();
                Console.WriteLine("=== Delete an Existing Book ===\n");
                Console.Write("Enter the Title of the book to delete: ");
                string titleInput = Console.ReadLine();

                // Find all books with titles that contain the input (case-insensitive)
                var matchingBooks = bookList
                    .Where(b => b.bookTitle.IndexOf(titleInput, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (!matchingBooks.Any())
                {
                    Console.WriteLine("No books found with the given title.");
                    return;
                }

                // If multiple matches, allow the user to choose
                Console.WriteLine("\nMatching Books:");
                for (int i = 0; i < matchingBooks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {matchingBooks[i].bookTitle} by {matchingBooks[i].bookAuthor}");
                }

                Console.Write("\nEnter the number of the book you want to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int selectedIndex) || selectedIndex < 1 || (selectedIndex > matchingBooks.Count))
                {
                    Console.WriteLine("Invalid selection.");
                    return;
                }
                // Get the selected book
                var book = matchingBooks[selectedIndex - 1];
                Console.WriteLine($"\nDeleting Book: {book.bookTitle} by {book.bookAuthor}\n");

                // Remove the book from the original bookList using the same book's details
                bookList.Remove(book);

                // Save the updated bookList to the file
                SaveEntireLibraryToFile();

                Console.WriteLine("Book successfully deleted!");
            }

            // Additional utility methods can be added here

            public void WithdrawOrReturnBook()
            {

                string stringToKeep = ("=== Withdraw or return an Existing Book ===\n");
                Utility.ClearScreenKeepLine(stringToKeep);

                Console.Write("Enter the Title of desired book: ");
                string titleInput = Console.ReadLine();


                var matchingBooks = bookList
                    .Where(b => b.bookTitle.IndexOf(titleInput, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (!matchingBooks.Any())                                                                    // Find all books with titles that contain the input (case-insensitive)
                {
                    Console.WriteLine("No books found with the given title.");
                    return;
                }


                Console.WriteLine("\nMatching Books:");
                for (int i = 0; i < matchingBooks.Count; i++)                                                       // If multiple matches, allow the user to choose
                {
                    Console.WriteLine($"{i + 1}. {matchingBooks[i].bookTitle} by {matchingBooks[i].bookAuthor}");
                }

                Console.Write("\nEnter the number of the book you want to update: ");
                if (!int.TryParse(Console.ReadLine(), out int selectedIndex) || selectedIndex < 1 || (selectedIndex > matchingBooks.Count))
                {
                    Console.WriteLine("Invalid selection.");
                    return;
                }


                var book = matchingBooks[selectedIndex - 1];                                                // Get the selected book
                DateTime dateOfCheckOut = DateTime.Now;

                Utility.ClearScreenKeepLine(stringToKeep);
                if (book.bookAvail == true)
                {

                    Console.WriteLine($"{book.bookTitle} is currently AVAILABLE ");
                    Console.WriteLine("Would you like to withdraw it? (Yes/No)");
                    bool nav = false;
                    while (!nav)
                    {
                        string navDecide = Console.ReadLine();
                        if (navDecide == "yes")
                        {
                            book.bookAvail = false;
                            Console.WriteLine($"You have now checked out {book.bookTitle} on {dateOfCheckOut}");
                            nav = true;
                        }
                        else if (navDecide == "no")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter: \"Yes\" or \"No\"");
                            navDecide = Console.ReadLine().ToLower();
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{book.bookTitle} is currently UNAVAILABLE ");
                    Console.WriteLine($"If you have checked {book.bookTitle} out, would you like to return it?");
                    Console.WriteLine("If not, enter \"no\" to return to main menu");
                    bool nav = false;
                    while (!nav)
                    {
                        string navDecide = Console.ReadLine().ToLower();
                        if (navDecide == "yes")
                        {
                            book.bookAvail = false;
                            Console.WriteLine($"You have now returned {book.bookTitle} on {dateOfCheckOut}");
                            nav = true;
                        }
                        else if (navDecide == "no")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter: \"Yes\" or \"No\"");
                            navDecide = Console.ReadLine().ToLower();
                        }
                    }
                }

            }

        }
















        class menuHandlerClass
        {                                                                                               // new class
            private Dictionary<int, Action> menuActions;
            private Library library;

            private LibraryFacts libraryFacts;



            public menuHandlerClass(Library lib)                                                    //new class constructor to connect to library class
            {
                library = lib;
                libraryFacts = new LibraryFacts(lib);
                InitializeMenuActions();
            }


            private void InitializeMenuActions()
            {
                menuActions = new Dictionary<int, Action>                                                // Initialize the menu actions dictionary that holds the connector methods
                {
                    { 1, AddNewBooks },
                    { 2, SearchForBooks },
                    { 3, WithdrawOrReturn },
                    { 4, EditBooks },
                    { 5, SurpriseFact },
                };
            }


            public void DisplayMainMenuFirst()                                                                      // Method to display the main menu and handle user input
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("|                            Welcome to Malcolm's Library!                           |");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("Please navigate by entering the corresponding number of your choice from the options below.\n");
                Console.WriteLine("1. Enter a new book to be stored in library");
                Console.WriteLine("2. Search through catalogue");
                Console.WriteLine("3. Withdraw an existing book or return current book");
                Console.WriteLine("4. Edit current book details or delete entire book");
                Console.WriteLine("5. Surprise\n");

                bool validChoice = false;
                while (!validChoice)
                {
                    Console.Write("Enter your choice: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int choice) && menuActions.ContainsKey(choice))
                    {
                        Console.Clear();
                        menuActions[choice]();                                                               // Invoke the selected action which holds the connector methods below
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.\n");                   //catch to only allow approved input
                    }
                }
            }

            // Action methods corresponding to menu options


            private void AddNewBooks()                                                      //connector method to allow the "menu" to run adding books method
            {
                Console.WriteLine("Reserving space for a new book...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                library.RegisterNewBook();
            }


            private void SearchForBooks()                                                       //connector method to allow the "menu" to run search method
            {
                Console.WriteLine("Pulling up search functionality...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                NewBook.SearchMenu(library.bookList);

            }


            private void WithdrawOrReturn()
            {                                                                                   //connector method to allow the "menu" to run the withdraw/return method

                Console.WriteLine("Gathering options...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                library.WithdrawOrReturnBook();
            }


            private void EditBooks()
            {
                Console.WriteLine("Gathering editable books...");                                           // Connector method to allow the "menu" to run the editing method
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                library.UpdateBook();
            }


            private void SurpriseFact()                                                                     //Connector method to allow the "menu" to run surprise fact method
            {
                Console.WriteLine("Generating a surprise facts...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();

                Console.WriteLine("Welcome to the Surprise Fact section!");
                bool continueFacts = true;

                while (continueFacts)
                {
                    libraryFacts.GenerateRandomFact();
                    Console.WriteLine("\nWould you like to see another fact? (yes/no)");
                    string input = Console.ReadLine().ToLower();

                    while (input != "yes" && input != "no")
                    {
                        Console.WriteLine("Please enter 'yes' or 'no':");
                        input = Console.ReadLine().ToLower();
                    }

                    continueFacts = input == "yes";
                }

                Console.WriteLine("Returning to the main menu...");

                //TODO  Implement surprise fact generation here
            }
        }
    }

    internal class LibraryFacts
    {
        private Library library;                                                                    // Dependency on the Library class

        public LibraryFacts(Library lib)
        {
            library = lib;
        }

        public void GenerateRandomFact()
        {
            var random = new Random();
            var facts = new List<Func<string>>                                                                  // List of fact-generating functions
        {
            () => $"Total number of books: {library.bookList.Count}",
            () => $"Most popular genre: {GetMostPopularGenre()}",
            () => $"Oldest book: {GetOldestBook()}",
            () => "Fun fact: Libraries have existed for over 4,000 years!",
            () => $"Most popular age range: {GetMostPopularAgeRange()}", 
            () => $"{returnArt()}",

        };

            int randomIndex = random.Next(facts.Count);
            Console.WriteLine(facts[randomIndex]());
        }

        private string GetMostPopularGenre()
        {
            return library.bookList
                .GroupBy(book => book.bookGenre)
                .OrderByDescending(group => group.Count())
                .FirstOrDefault()?.Key ?? "Unknown";
        }

        private string GetMostPopularAgeRange()
        {
            return library.bookList
                .GroupBy(book => book.bookAgeRange)
                .OrderByDescending(group => group.Count())
                .FirstOrDefault()?.Key ?? "Unknown";

        }



        private string GetOldestBook()
        {
            return library.bookList
                .OrderBy(book => DateTime.ParseExact(book.bookPubDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                .FirstOrDefault()?.bookTitle ?? "No books in the library.";
        }
        private string returnArt()
        {
            Random ranNum = new Random();
            
            var facts = new List<Func<string>>                                                                 
        {
            () => "    _____\r\n   /    /|_ ___________________________________________\r\n  /    // /|                                          /|\r\n (====|/ //   An apple a day...            _QP_      / |\r\n  (=====|/     keeps the teacher at bay   (  ' )    / .|\r\n (====|/                                   \\__/    / /||\r\n/_________________________________________________/ / ||\r\n|  _____________________________________________  ||  ||\r\n| ||                                            | ||\r\n| ||                                            | ||\r\n| |                                             | |  pjb",
            () => ",         ,\r\n|\\\\\\\\ ////|\r\n| \\\\\\V/// |\r\n|  |~~~|  |\r\n|  |===|  |\r\n|  |j  |  |\r\n|  | g |  |\r\n \\ |  s| /\r\n  \\|===|/\r\n   '---'",
            () => "     __...--~~~~~-._   _.-~~~~~--...__\r\n    //               `V'               \\\\ \r\n   //                 |                 \\\\ \r\n  //__...--~~~~~~-._  |  _.-~~~~~~--...__\\\\ \r\n //__.....----~~~~._\\ | /_.~~~~----.....__\\\\\r\n====================\\\\|//====================\r\n                dwb `---`",
            () => "        ____\r\n     .-\" +' \"-.    __,  ,___,\r\n    /.'.'A_'*`.\\  (--|__| _,,_ ,_\r\n   |:.*'/\\-\\. ':|   _|  |(_||_)|_)\\/\r\n   |:.'.||\"|.'*:|  (        |  | _/\r\n    \\:~^~^~^~^:/          __,  ,___,\r\n     /`-....-'\\          (--|__| _ |' _| _,   ,\r\njgs /          \\           _|  |(_)||(_|(_|\\//_)\r\n    `-.,____,.-'          (               _/",
            () => " _____________________________________________\r\n|.'',        Public_Library_Halls         ,''.|\r\n|.'.'',                                 ,''.'.|\r\n|.'.'.'',                             ,''.'.'.|\r\n|.'.'.'.'',                         ,''.'.'.'.|\r\n|.'.'.'.'.|                         |.'.'.'.'.|\r\n|.'.'.'.'.|===;                 ;===|.'.'.'.'.|\r\n|.'.'.'.'.|:::|',             ,'|:::|.'.'.'.'.|\r\n|.'.'.'.'.|---|'.|, _______ ,|.'|---|.'.'.'.'.|\r\n|.'.'.'.'.|:::|'.|'|???????|'|.'|:::|.'.'.'.'.|\r\n|,',',',',|---|',|'|???????|'|,'|---|,',',',',|\r\n|.'.'.'.'.|:::|'.|'|???????|'|.'|:::|.'.'.'.'.|\r\n|.'.'.'.'.|---|','   /%%%\\   ','|---|.'.'.'.'.|\r\n|.'.'.'.'.|===:'    /%%%%%\\    ':===|.'.'.'.'.|\r\n|.'.'.'.'.|%%%%%%%%%%%%%%%%%%%%%%%%%|.'.'.'.'.|\r\n|.'.'.'.','       /%%%%%%%%%\\       ','.'.'.'.|\r\n|.'.'.','        /%%%%%%%%%%%\\        ','.'.'.|\r\n|.'.','         /%%%%%%%%%%%%%\\         ','.'.|\r\n|.','          /%%%%%%%%%%%%%%%\\          ','.|\r\n|;____________/%%%%%Spicer%%%%%%\\____________;|",
            () => $"{returnArt()}",

        };

            int randomIndex = ranNum.Next(facts.Count);


            return facts[randomIndex]();
        }
    }








    public static class Utility                                                 //utility method to improve UX, by leaving a "title header" in certain sections to mimic structure
    {                                                                           //Decided to use a seperate method to optimize reusability and scalability
        public static void ClearScreenKeepLine(string lineToKeep)
        {
            Console.Clear();
            Console.WriteLine(lineToKeep);
        }
    }
}
