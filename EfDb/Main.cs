using EfDb;
using EfDb.Enums;
using EfDb.Models;
using EfDb.Repositories;

var db = new AppEfContext();
while (true)
{
    Console.WriteLine("1.User   2.UserProfile   3.Team  4.Task");
    try
    {
        switch (Console.ReadLine())
        {
            case "1":
                switch (Console.ReadLine())
                {
                    case "1":
                        var newUser = new CreateUserModel
                        {
                            FirstName = Console.ReadLine(),
                            SecondName = Console.ReadLine(),
                            DateOfBirth = DateTime.Parse(Console.ReadLine())
                        };

                        UserRepo.CreateUser(db, newUser);
                        Console.WriteLine("Success");
                        break;
                    case "2":
                        var users = UserRepo.ReadUsers(db);
                        foreach (var user in users)
                        {
                            Console.WriteLine($"Id: {user.Id}, fName: {user.FirstName}, sName: {user.SecondName}");
                        }
                        break;
                }
                break;
            case "2":
                switch (Console.ReadLine())
                {
                    case "1":
                        var newProfile = new CreateProfileModel
                        {
                            Country = Console.ReadLine(),
                            City = Console.ReadLine(),
                            Citizenship = Console.ReadLine()
                        };
                        var sName1 = Console.ReadLine();

                        UserProfRepo.CreateProfile(db, sName1, newProfile);
                        Console.WriteLine("Success");
                        break;
                    case "2":
                        var sName2 = Console.ReadLine();
                        var userProf = UserProfRepo.ReadProfile(db, sName2);
                        Console.WriteLine($"fName: {userProf.User.FirstName}, sName: {userProf.User.SecondName},\n " +
                            $"{userProf.Country} {userProf.City} ");
                        break;
                }
                break;
            case "3":
                switch (Console.ReadLine())
                {
                    case "1":
                        var teamName = Console.ReadLine();
                        var managers = int.Parse(Console.ReadLine());
                        TeamRepo.CreateTeam(db, teamName, managers);
                        Console.WriteLine("Success");
                        break;
                    case "2":
                        var teams = TeamRepo.ReadTeams(db);
                        foreach (var team in teams)
                        {
                            Console.WriteLine(team.Name);
                            foreach (var u in team.Users)
                            {
                                Console.WriteLine(u.FirstName, u.SecondName);
                            }
                            Console.WriteLine();
                        }
                        break;
                    case "3":
                        var uName3 = Console.ReadLine();
                        var teamId = int.Parse(Console.ReadLine());
                        TeamRepo.AddToTeam(db, uName3, teamId);
                        Console.WriteLine("Success");
                        break;
                }
                break;
            case "4":
                switch (Console.ReadLine())
                {
                    case "1":
                        var taskModel = new CreateTaskModel
                        {
                            Complexity = (TaskComplexity)int.Parse(Console.ReadLine()),
                            Hours = int.Parse(Console.ReadLine()),
                            Status = (Status)int.Parse(Console.ReadLine()),
                            Description = Console.ReadLine()
                        };
                        TaskRepo.CreateTask(db, taskModel);
                        Console.WriteLine("Success");
                        break;
                    case "2":
                        var uName4 = Console.ReadLine();
                        var taskId = int.Parse(Console.ReadLine());
                        TaskRepo.AddUserToTask(db, uName4, taskId);
                        Console.WriteLine("Success");
                        break;
                }
                break;
            default:
                return;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
Console.ReadLine();