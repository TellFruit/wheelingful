using Wheelingful.ML.Services.Db;

bool running = true;

var populateService = new PopulateDbService();

while (running)
{
    Console.WriteLine();
    Console.WriteLine("The list of available commands:");
    Console.WriteLine("1. Populate database with dataset");
    Console.WriteLine("2. Export full parsed data");
    Console.WriteLine("3. Export learning parsed data");
    Console.WriteLine("4. Truncate affected tables");
    Console.WriteLine("5. Populate database with parsed data");
    Console.WriteLine("6. Generate recommendations model");
    Console.WriteLine("7. Exit");
    Console.WriteLine();

    Console.Write("Please enter your choice: ");

    try
    {
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("Populating...");
                populateService.PopulateWithAmazonReviews();
                break;
            case "2":
                Console.WriteLine("Exporting...");
                populateService.ExportFullData();
                break;
            case "3":
                Console.WriteLine("Exporting...");
                populateService.ExportLearningData();
                break;
            case "4":
                Console.WriteLine("Truncate...");
                populateService.TruncateAffectedTables();
                break;
            case "5":
                Console.WriteLine("Populating...");
                populateService.PopulateWitParsedReviews();
                break;
            case "6":
                Console.WriteLine("Learning...");
                Console.WriteLine("Not implemented!");
                break;
            case "7":
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
