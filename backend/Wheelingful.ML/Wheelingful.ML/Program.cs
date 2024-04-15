using Wheelingful.ML.Services.Db;
using Wheelingful.ML.Services.ML;

bool running = true;

var populateService = new PopulateDbService();
var teachModelService = new TeachModelService();

while (running)
{
    Console.WriteLine();
    Console.WriteLine("The list of available commands:");
    Console.WriteLine("1. Populate database with dataset");
    Console.WriteLine("2. Export full parsed data");
    Console.WriteLine("3. Truncate affected tables");
    Console.WriteLine("4. Populate database with parsed data");
    Console.WriteLine("5. Generate recommendations model");
    Console.WriteLine("6. Exit");
    Console.WriteLine();

    Console.Write("Please enter your choice: ");

    try
    {
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter the name of the dataset: ");
                var datasetName = Console.ReadLine();
                Console.WriteLine("Populating...");
                populateService.RunPopulate(datasetName);
                break;
            case "2":
                Console.WriteLine("Exporting...");
                populateService.ExportFullData();
                break;
            case "3":
                Console.WriteLine("Truncate...");
                populateService.TruncateAffectedTables();
                break;
            case "4":
                Console.WriteLine("Populating...");
                populateService.PopulateWitParsedReviews();
                break;
            case "5":
                Console.WriteLine("Learning...");
                teachModelService.StartLearning();
                break;
            case "6":
                Console.WriteLine("Exiting...");
                running = false;
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

Console.WriteLine("Good bye!");
Environment.Exit(0);
