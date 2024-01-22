import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

public class App {
    public static void main(String[] args) throws Exception {
        String filename = "example.txt";
        String directoryPath = "/var/www/uploads";
        Path filePath = Paths.get(directoryPath, filename);
        String content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut urna enim, egestas eget accumsan vel, fermentum at dolor. Vestibulum in nisl lectus.";
        
        try {
            Files.createDirectories(filePath.getParent());
            Files.write(filePath, content.getBytes());
            System.out.println("File created successfully with content at " + filePath);
        } catch (IOException e) {
            System.err.println("Error occurred while writing to the file: " + e.getMessage());
        }
    }
}
