import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;

public class File {
    public static void main(String args[]) throws IOException {
        String filePath = System.getProperty("user.dir") + "\\src\\secret.txt";
        // try-with-resources block automatically closes the BufferedReader when done
        try (BufferedReader br = new BufferedReader(new InputStreamReader(new FileInputStream(filePath)))) {
            String data;
            while ((data = br.readLine()) != null)
                System.out.println(data);
        } catch (Throwable e) {
            e.printStackTrace();
        }
    }
}
