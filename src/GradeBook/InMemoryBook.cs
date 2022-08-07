namespace GradeBook
{
    public delegate void GradeAddedDelegate(Object sender, EventArgs args);

    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public override void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Inavlid {nameof(grade)}");
            } 
            
        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            
            for(int index = 0; index < grades.Count; index ++)     
            {
                result.Add(grades[index]);
            }

            return result;
        }

        private List<double> grades;

        public const string CATEGORY = "Scinece";        
    }
}