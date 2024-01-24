import java.io.*;
import java.nio.file.*;
import java.util.zip.*;

public class UnsafeUnzipper {

    private static final int BUFFER_SIZE = 512;

    public static void main(String args[]) throws IOException {
        Path zipFilePath = Paths.get(System.getProperty("user.dir"), "src" + File.separator + "bomb.zip");
        unzip(zipFilePath);
    }

    private static void unzip(Path zipFilePath) throws IOException {
        try (ZipInputStream zis = new ZipInputStream(new BufferedInputStream(
                new FileInputStream(zipFilePath.toFile())))) {
            ZipEntry entry;

            while ((entry = zis.getNextEntry()) != null) {

                Path filePath = zipFilePath.resolveSibling(entry.getName());
                if (!filePath.startsWith(zipFilePath.getParent())) {
                    throw new IOException("Bad zip entry: " + entry.getName());
                }

                System.out.println("Extracting: " + entry);

                if (entry.isDirectory()) {
                    Files.createDirectories(filePath);
                    continue;
                }

                try (BufferedOutputStream dest = new BufferedOutputStream(
                        new FileOutputStream(filePath.toFile()), BUFFER_SIZE)) {
                    byte[] data = new byte[BUFFER_SIZE];
                    int count;
                    while ((count = zis.read(data)) != -1) {
                        dest.write(data, 0, count);
                    }
                }
                zis.closeEntry();
            }
        }
    }
}


