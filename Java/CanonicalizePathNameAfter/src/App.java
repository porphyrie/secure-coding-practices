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

        if (!isValidFilePath(filePath, baseDir)) {
            System.out.println("Invalid file path.");
            return;
        }

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

    public static boolean isValidFilePath(Path filePath, Path baseDir) {
        // Perform input validation to ensure that filePath doesn't contain directory traversal attempts
        // and that it is within the specified baseDir
        return filePath.startsWith(baseDir) && !filePath.toString().contains("..");
    }
}




