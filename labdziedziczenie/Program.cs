namespace labdziedziczenie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Inheritance(); //błąd bo ujemna wartośc deklarowana
            int a = 10;
            int b = 0;
            while(true) {
                try
                {
                    Console.WriteLine("Wpisz liczbę:");
                    b = int.Parse(Console.ReadLine());
                    Console.WriteLine(a / b);
                    break;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine("Niepoprawny format. Spróbuj ponownie.");
                }
                catch(DivideByZeroException ex)
                {
                    Console.WriteLine("Dzielenie przez 0. Spróbuj ponownie.");
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Inny błąd");
                }
                finally
                {
                    Console.WriteLine("Spróbuj ponownie lub koniec.");
                }
            }
        }

        private static void Inheritance()
        {
            Student student = new Student()
            {
                Name = "Adam",
                Birth = new DateTime(2000, 10, 10),
                FieldStudy = "Informatyka stosowana"
            };

            Random random = new Random();

            //Person person = student;
            Person person = random.NextDouble() > 0.5 ? student : new Person() { Name = "Ewa", Birth = new DateTime(2005, 12, 12) };
            Console.WriteLine(person.Name);
            //Console.WriteLine(person.FieldStudy); <- nie zadziała
            Console.WriteLine(person is Student); //true ale nie posiada FieldStudy
            Console.WriteLine(person is Person);
            Console.WriteLine(person is object);
            Console.WriteLine(person);



            //Shape shape = new Shape(); <- BŁĄD
            IDrawable[] drawables = {
                new Rectangle() { Color = 5, Width = -19, Height = 20 } ,
                student,
                new Circle(){ Radius = 10 }
            };

            foreach (var drawable in drawables)
            {
                drawable.Draw();
            }
        }

        public class Circle : Shape
        {
            public double Radius { get; set; }

            public override double Area => (Radius * Radius) * Math.PI;

            public override void Draw() => Console.WriteLine(Radius);
            
        }


        public abstract class Shape: IDrawable
        {
            public int Color { get; set; }
            public abstract double Area { get; }
            public abstract void Draw();
        }

        public class Rectangle : Shape
        {
            private double _w;

            public double Width { get => _w; 
                set {
                    if(value < 0)
                    {
                        throw new ArgumentException("Szerokość nie może być ujemna");
                    }
                } 
            }
            public double Height { get; set; }

            public override double Area => Width * Height;

            public override void Draw()
            {
                Console.WriteLine($"Rectangle {Width} x {Height}");
            }
        }

        public interface IDrawable
        {
            void Draw();
        }

        public class Person
        {
            public string Name { get; set; }
            public DateTime Birth { get; set; }
            public override string ToString()
            {
                return $"Person{{Name: {Name}, Birth: {Birth}}}";
            }
        }
        //Student dziedziczy od Person
        public class Student: Person, IDrawable
        {
            public string FieldStudy { get; set; }

            public void Draw()
            {
                Console.WriteLine(ToString());
            }

            //alt + enter -> generate overrides
            public override string? ToString()
            {
                return $"Student{{Name: {Name}, Birth: {Birth}, FieldStudy: {FieldStudy}}}";
            }
        }

    }
}