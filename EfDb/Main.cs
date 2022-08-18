using EfDb;
using EfDb.Enums;
using EfDb.Models;
using EfDb.Repositories;

//AppEfContext db = new AppEfContext();
while (true)
{
    Console.WriteLine("1.User   2.UserProfile   3.Team  4.TaskDb");
    try
    {
        switch (Console.ReadLine())
        {
            case "1":
                var uRepo = new UserRepo();
                switch (Console.ReadLine())
                {
                    case "1":
                        var newUser = new CreateUserModel
                        {
                            FirstName = Console.ReadLine(),
                            SecondName = Console.ReadLine(),
                            DateOfBirth = DateTime.Parse(Console.ReadLine())
                        };

                        uRepo.CreateUserAsync(newUser);
                        Console.WriteLine("Success");
                        break;
                    case "2":
                        var users = uRepo.ReadUsersAsync();
                        foreach (var user in users.Result)
                        {
                            Console.WriteLine($"Id: {user.Id}, fName: {user.FirstName}, sName: {user.SecondName}, Cityzenship: {user.UserProfile?.Citizenship}.");
                        }
                        break;
                }
                break;
            case "2":
                var uProf = new UserProfRepo();
                switch (Console.ReadLine())
                {
                    case "1":
                        var newProfile = new CreateProfileModel
                        {
                            Country = Console.ReadLine(),
                            City = Console.ReadLine(),
                            Citizenship = Console.ReadLine(),
                            UserId = int.Parse(Console.ReadLine())
                        };

                        uProf.CreateProfileAsync(newProfile);
                        Console.WriteLine("Success");
                        break;
                    case "2":
                        var userId2 = int.Parse(Console.ReadLine());
                        var userProf = await uProf.ReadProfileAsync(userId2);
                        Console.WriteLine($"fName: {userProf.User.FirstName}, sName: {userProf.User.SecondName},\n " +
                            $"{userProf.Country} {userProf.City} ");
                        break;
                }
                break;
            case "3":
                var tRepo = new TeamRepo();
                switch (Console.ReadLine())
                {
                    case "1":
                        var teamName = Console.ReadLine();
                        var managers = int.Parse(Console.ReadLine());
                        tRepo.CreateTeamAsync(teamName, managers);
                        Console.WriteLine("Success");
                        break;
                    case "2":
                        var teams = tRepo.ReadTeamsAsync();
                        foreach (var team in teams.Result)
                        {
                            Console.WriteLine($"{team.Id} {team.Name}");
                            Console.Write("В команде:");
                            foreach (var u in team.Users)
                            {
                                Console.Write($"{u.FirstName} {u.SecondName}, ");
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                        break;
                    case "3":
                        Console.WriteLine("Введите id команды:");
                        var teamId_3 = int.Parse(Console.ReadLine());

                        var usersId3 = new List<int>();
                        Console.WriteLine("Введите id пользователей:");
                        ConsoleKeyInfo btn3;
                        do
                        {
                            var idForList3 = int.Parse(Console.ReadLine());
                            
                            usersId3.Add(idForList3);
                            Console.WriteLine("Продолжить:Enter, Закончить:Esc");
                            btn3 = Console.ReadKey();
                        }
                        while (btn3.Key != ConsoleKey.Escape);

                        tRepo.AddToTeamAsync(usersId3, teamId_3);
                        Console.WriteLine("Success");
                        break;
                    case "4":
                        Console.WriteLine("Введите id команды:");
                        var teamId_4 = int.Parse(Console.ReadLine());

                        var usersId4 = new List<int>();
                        Console.WriteLine("Введите id пользователей:");
                        ConsoleKeyInfo btn4;
                        do
                        {
                            var idForList4 = int.Parse(Console.ReadLine());

                            usersId4.Add(idForList4);
                            Console.WriteLine("Продолжить:Enter, Закончить:Esc");
                            btn4 = Console.ReadKey();
                        }
                        while (btn4.Key != ConsoleKey.Escape);

                        tRepo.RemoveFromTeamAsync(usersId4, teamId_4);
                        Console.WriteLine("Success");
                        break;
                }
                break;
            case "4":
                var taskRepo = new TaskDbRepo();
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
                        taskRepo.CreateTaskAsync(taskModel);
                        Console.WriteLine("Success");
                        break;
                    case "2":
                        var userId4 = int.Parse(Console.ReadLine());
                        var taskId = int.Parse(Console.ReadLine());
                        taskRepo.AddUserToTaskAsync(userId4, taskId);
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