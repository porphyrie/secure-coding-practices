import java.io.Console;
import java.io.IOException;
import java.util.Arrays;

class Password {
    public static void main(String args[]) throws IOException {
        Console c = System.console();
        if (c == null) {
            System.err.println("No console.");
            System.exit(1);
        }
        String username = c.readLine("Enter your user name: ");
        char[] password = c.readPassword("Enter your password: ");
        if (!verify(username, password)) {
            throw new SecurityException("Invalid Credentials");
        }
        // clear the password
        Arrays.fill(password, ' ');
    }

    // dummy verify method, always returns true
    private static final boolean verify(String username, char[] password) {
        return true;
    }
}
