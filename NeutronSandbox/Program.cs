using Neutron.Scripts;
using System.Text.Json.Serialization;

namespace NeutronSandbox;

internal class Program
{

    enum Status
    {
        Success,
        Failed,
        Error
    }

    class Person
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("age")]
        public int? Age { get; set; }
    }

    class TheMeaningOfEverything
    {
        [JsonPropertyName("result")]
        public string? Result { get; set; }
    }

    [STAThread]
    static void Main(string[] args)
    {
        Application application;

#if DEBUG
        application = new Application(title: "NeutronSandbox", width: 960, height: 540, webContentPath: Path.Combine(AppContext.BaseDirectory, "dist"), debug: true);
#else
        application = new Application(title: "NeutronSandbox", width: 960, height: 540, webContentPath: Path.Combine(AppContext.BaseDirectory, "dist"));
#endif

        // first generic type is the first parameter type of the js function you are trying to bind
        // second generic type is the output type of the js function you are trying to bind
        application.Bind<string?, object?>("firstSubmit", (input) => {

            if (input is null)
            {
                Console.WriteLine("input is null or undefined");
                return null;
            }
            
            Console.WriteLine($"\"{input}\"");

            return null;
        });

        // first generic type is the first parameter type of the js function you are trying to bind
        // second generic type is the second parameter type of the js function you are trying to bind
        // third generic type is the output type of the js function you are trying to bind
        application.Bind<string?, int?, object?>("secondSubmit", (name, age) =>
        {
            if (name is null)
            {
                Console.WriteLine("name is null or undefined");
                return null;
            }

            if (age is null)
            {
                Console.WriteLine("age is null or undefined");
                return null;
            }

            Console.WriteLine($"Hi my name is {name}, i'm {age} years old!");

            return null;
        });

        // first generic type is the output type of the js function you are trying to bind
        application.Bind<object?>("thirdSubmit", () =>
        {
            Console.WriteLine("Called from javascript");

            return null;
        });

        // you get the idea
        application.Bind<Status?, object?>("fourthSubmit", (status) =>
        {
            Console.WriteLine($"The status are {status}");

            return null;
        });


        // you get the idea
        application.Bind<TheMeaningOfEverything?, object?>("fifthSubmit", (theMeaningOfEverything) =>
        {
            if (theMeaningOfEverything is null)
            {
                throw new Exception("theMeaningOfEverything is null");
            }

            Console.WriteLine($"The answer to life the universe and everything is {theMeaningOfEverything.Result}");

            return null;
        });


        // you get the idea
        application.Bind<string?, object?>("sixthSubmit", (text) =>
        {
            if (text is null)
            {
                Console.WriteLine("text is null or undefined");
                return null;
            }

            Console.WriteLine(text);

            return null;
        });


        // you get the idea
        application.Bind<Status?[]?, object?>("seventhSubmit", (statuses) =>
        {
            if (statuses is null)
            {
                Console.WriteLine("statuses is null or undefined");
                return null;
            }

            foreach (var status in statuses)
            {
                Console.WriteLine($"Status: {status}");
            }

            return null;
        });


        // you get the idea
        application.Bind<bool?[]?, object?>("eighthSubmit", (array) =>
        {
            if (array is null)
            {
                Console.WriteLine("array is null or undefined");
                return null;
            }

            foreach (var arr in array)
            {
                Console.WriteLine($"Array: {arr}");
            }

            return null;
        });

        // you get the idea
        application.Bind<Person?[]?, string?>("ninthSubmit", (personArray) =>
        {
            if (personArray is null)
            {
                Console.WriteLine("array is null or undefined");
                return null;
            }

            List<string> greetings = [];

            foreach (var person in personArray)
            {
                if (person is null)
                {
                    Console.WriteLine("array is element is null or undefined");
                    return null;
                }

                greetings.Add($"PersonArray: Hi my name is {person.Name}, i'm {person.Age} years old!");
            }

            return string.Join("\n", greetings);
        });

        // you get the idea
        application.Bind<Person[]>("tenthSubmit", () =>
        {
            return
            [
                new Person(){
                   Name = "John", 
                   Age = 53
                },

                new Person() {
                    Name = "Mia", 
                    Age = 34
                }
            ];
        });

        application.Run();
    }
}