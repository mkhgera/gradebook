using Npgsql;

namespace GradeBook
{
    internal class DataBaseBook : Book
    {
        public DataBaseBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    using var cmd = new NpgsqlCommand("INSERT INTO GRADES(grade) values (@parameter)", connection)
                    {
                        Parameters =
                        {
                            new ("parameter", grade)
                        }
                    };
                    cmd.ExecuteNonQuery();
                }
            }
            catch (PostgresException ex)
            {
                System.Console.WriteLine(ex.MessageText);
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();


            List<string> numbers = new List<string>();
            using (var connection = GetConnection())
            {
                using (var cmd = new NpgsqlCommand("select * from grades", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            numbers.Add(reader[0].ToString());
                        }
                    }
                }
            }


            for (int i = 0; i < numbers.Count; i++)
            {
                var number = double.Parse(numbers[i]);
                result.Add(number);
            }

            return result;

        }

        NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        private const string _connectionString = "Host=localhost; Username=postgres; Password=123456; Database=gradebook";

    }
}