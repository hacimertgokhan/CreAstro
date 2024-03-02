namespace CreAstro.user;

public class LoggingTools {
    public static void Send(string message) { Console.WriteLine("[CreAstro]: " + message); }

    public static void Empty(int len) {
        for (int i = 0; i < len; i++) {
            Console.WriteLine(" ");
        }
    }
    
}