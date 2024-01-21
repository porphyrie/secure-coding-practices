import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

public class App {
    public static void main(String[] args) throws Exception {

        BufferedReader reader = new BufferedReader(
                new InputStreamReader(System.in));
        System.out.print("Enter a file name: ");
        String userInput = reader.readLine();

        Path baseDir = Paths.get(System.getProperty("user.dir")).resolve("uploads");
        Path filePath = baseDir.resolve(userInput).normalize().toAbsolutePath();

        byte[] fileContent = Files.readAllBytes(filePath);
        if (fileContent != null) {
            System.out.print("File content: ");
            for (int i = 0; i < fileContent.length; i++) {
                System.out.print((char) fileContent[i]);
                fileContent[i] = 0;
            }
        } else {
            System.out.println("Failed to read file.");
        }

    }
}

