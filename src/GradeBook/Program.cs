namespace GradeBook
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var book = new InMemoryBook("August Grade Book");
            book.GradeAdded += OnGradeAdded;
            EnterGrade(book);

            var stats = book.GetStatistics();

            System.Console.WriteLine(InMemoryBook.CATEGORY);
            System.Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The highest grade is {stats.High:N1}");
            Console.WriteLine($"The lowest grade is {stats.Low:N1}");
            Console.WriteLine($"The letter grade is {stats.letter:N1}");
        }

        private static void EnterGrade(IBook book)
        {
            while (true)
            {
                System.Console.WriteLine("Enter a grade or q to quit");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);

                }
                catch (ArgumentException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                finally
                {
                    System.Console.WriteLine("**");
                }

            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            System.Console.WriteLine("A grade was added");
        }
    }
}