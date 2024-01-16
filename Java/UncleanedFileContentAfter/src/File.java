import java.io.FileInputStream;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;

class File {
    public static void main(String args[]) throws IOException {

        String filePath = System.getProperty("user.dir") + "\\src\\secret.txt";
        ByteBuffer buffer = ByteBuffer.allocateDirect(30);
        byte[] zeroes = new byte[buffer.capacity()];

        try (FileChannel channel = (new FileInputStream(filePath)).getChannel()) {
            while (channel.read(buffer) != -1) {
                buffer.flip();
                while (buffer.hasRemaining()) {
                    System.out.print((char) buffer.get());
                }
                buffer.clear();
                buffer.put(zeroes);
                buffer.clear();
            }

        } catch (Throwable e) {
            e.printStackTrace();
        }

    }
}
