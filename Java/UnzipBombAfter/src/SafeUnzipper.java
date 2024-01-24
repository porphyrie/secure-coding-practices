import java.io.*;
import java.nio.file.*;
import java.util.zip.*;

public class SafeUnzipper {

    private static final int BUFFER_SIZE = 512;
    private static final long MAX_UNZIPPED_SIZE = 100 * 1024 * 1024; // 100 MB
    private static final int MAX_FILE_COUNT = 1024; // maximum number of files allowed

    public static void main(String args[]) throws IOException {
        Path zipFilePath = Paths.get(System.getProperty("user.dir"), "src" + File.separator + "bomb.zip");
        unzip(zipFilePath);
    }

    private static void unzip(Path zipFilePath) throws IOException {
        try (ZipInputStream zis = new ZipInputStream(new BufferedInputStream(
                new FileInputStream(zipFilePath.toFile())))) {
            ZipEntry entry;
            long totalUnzippedSize = 0;
            int fileCount = 0;

            while ((entry = zis.getNextEntry()) != null) {
                fileCount++;
                if (fileCount > MAX_FILE_COUNT) {
                    throw new IllegalStateException("Too many files to unzip.");
                }

                Path filePath = zipFilePath.resolveSibling(entry.getName());
                if (!filePath.startsWith(zipFilePath.getParent())) {
                    throw new IOException("Bad zip entry: " + entry.getName());
                }

                System.out.println("Extracting: " + entry);

                if (entry.isDirectory()) {
                    Files.createDirectories(filePath);
                    continue;
                }

                totalUnzippedSize += entry.getSize();
                if (totalUnzippedSize > MAX_UNZIPPED_SIZE) {
                    throw new IOException("Unzipped data exceeds allowable limit.");
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
