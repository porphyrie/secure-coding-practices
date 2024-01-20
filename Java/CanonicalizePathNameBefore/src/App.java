import java.io.BufferedReader;
import java.io.File;
import java.io.InputStreamReader;
import java.nio.file.Files;

public class App {
    public static void main(String[] args) throws Exception {

        BufferedReader reader = new BufferedReader(
                new InputStreamReader(System.in));
        System.out.print("Enter a file name: ");
        String userInput = reader.readLine();

        String filePath = System.getProperty("user.dir") +
                "/uploads/" + userInput;
        File file = new File(filePath);
        if (!file.exists()) {
            System.out.println("File not found.");
            return;
        }

        byte[] fileContent = Files.readAllBytes(file.toPath());
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

