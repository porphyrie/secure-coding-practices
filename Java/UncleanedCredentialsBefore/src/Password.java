import java.io.Console;
import java.io.IOException;

class Password {
    public static void main(String args[]) throws IOException {
        Console c = System.console();
        if (c == null) {
            System.err.println("No console.");
            System.exit(1);
        }
        String username = c.readLine("Enter your username: ");
        String password = c.readLine("Enter your password: ");
        if (!verify(username, password)) {
            throw new SecurityException("Invalid Credentials");
        }
        // ...
    }

    // dummy verify method, always returns true
    private static final boolean verify(String username, String password) {
        return true;
    }
}