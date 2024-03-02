using Octokit;

namespace CreAstro.user;

public class GithubUser {
    static GitHubClient client = new GitHubClient(new ProductHeaderValue("CreAstro")); Task<User> _githubUser = client.User.Current();
    public GithubUser(Task<User> client) { _githubUser = client; }
    public String getUserName() { return _githubUser.Result.Name; }
    public int getFollowers() { return _githubUser.Result.Followers; }
    public int getFollowing() { return _githubUser.Result.Following; }
    public String getBio() { return _githubUser.Result.Bio; }
    public String getEmail() { return _githubUser.Result.Email; }
    public String getURL() { return _githubUser.Result.Url; }
}