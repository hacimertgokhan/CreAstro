using CreAstro.user;
using Octokit;


var Client = new GitHubClient(new ProductHeaderValue("CreAstro"));
var user = await Client.User.Get("hacimertgokhan");

int followers = user.Followers;
String userEmail = user.Email;

void Start() {
    var gitrecre = true; 
    LoggingTools.Send("For the repo creation, you mush log-in your github account.");
    LoggingTools.Send("Enter your github token.");
    string? inputToken = Console.ReadLine();
    Client.Credentials = new Credentials(inputToken);
    var login = Client.User.Current();
    GithubUser githubUser = new GithubUser(login);
    Console.WriteLine(" >> Get more information with 'help'");
    if (login.IsFaulted) LoggingTools.Send("An error occur.");
    if (login.IsCanceled) LoggingTools.Send("An error occur.");
    if (login.IsCompletedSuccessfully)
    {
        while (gitrecre)
        {
            LoggingTools.Empty(2);
            Console.Write(" >> ");
            var repname = Console.ReadLine();
            switch (repname)
            {
                case "help":
                {
                    Console.WriteLine("CreAstro Help");
                    Console.WriteLine(" -  exit: runtime will stop.");
                    Console.WriteLine(" -  me: personal information.");
                    Console.WriteLine(" -  new: make new github repo");
                    Console.WriteLine(" -  developer: creator information");
                    break;
                }
                case "exit":
                {
                    LoggingTools.Send("Done.");
                    gitrecre = false;
                    break;
                }
                case "me":
                {
                    Console.WriteLine($"{githubUser.getUserName()}`s github profile");
                    Console.WriteLine($" - Followers: {githubUser.getFollowers()}");
                    Console.WriteLine($" - Following: {githubUser.getFollowing()}");
                    Console.WriteLine($" - Email: {githubUser.getEmail()}");
                    Console.WriteLine($" - Bio: {githubUser.getBio()}");
                    Console.WriteLine($" - API-URL: {githubUser.getURL()}");
                    break;
                }
                case "new":
                {
                    Console.Write(" >>  Repository name: ");
                    var repository = Console.ReadLine();
                    Console.Write(" >>  License template: (mit/...) ");
                    var license = Console.ReadLine();
                    Console.Write(" >>  Description: ");
                    var description = Console.ReadLine();
                    Console.Write(" >>  Is private: (y/n) ");
                    var status = Console.ReadLine();
                    bool privateMode = true;
                    if (status.Equals("y"))
                    {
                        privateMode = true;
                    }
                    else if (status.Equals("n"))
                    {
                        privateMode = false;
                    }

                    try
                    {
                        var repos = new NewRepository(repository)
                        {
                            AutoInit = false,
                            Description = description,
                            LicenseTemplate = license,
                            Private = privateMode
                        };
                        var newRepository = Task.Run(async () => await Client.Repository.Create(repos)).GetAwaiter()
                            .GetResult();
                        Console.WriteLine("[CreAstro]: The repository {0} was created.", newRepository.FullName);
                    }
                    catch (AggregateException e)
                    {
                        LoggingTools.Send(e.Message);
                    }

                    break;
                }
                case "developer":
                {
                    Console.WriteLine("hacimertgokhan (H. Mert Gökhan)");
                    Console.WriteLine(" - Email: " + userEmail);
                    Console.WriteLine(" - Followers: " + followers);
                    break;
                }
            }
        }
    }
    else
    {
        LoggingTools.Send("Unknow github account.");
    }

}

Start();
