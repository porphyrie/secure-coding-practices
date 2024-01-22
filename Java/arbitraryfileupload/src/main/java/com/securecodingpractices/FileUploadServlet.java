package com.securecodingpractices;

import java.io.*;
import java.nio.file.*;
import java.util.*;
import jakarta.servlet.*;
import jakarta.servlet.annotation.*;
import jakarta.servlet.http.*;
import org.apache.tika.parser.*;
import org.apache.tika.metadata.Metadata;
import org.apache.tika.sax.BodyContentHandler;
import org.xml.sax.SAXException;
import org.apache.tika.exception.TikaException;
import org.apache.tika.io.TikaInputStream;

@WebServlet(urlPatterns = { "/uploadbefore", "/uploadafter", "/uploads" })
@MultipartConfig
public class FileUploadServlet extends HttpServlet {
    private static final long serialVersionUID = 1L;

    // define the path where uploads will be stored through an environment variable
    // easy to maintain, portable, secure (if you make sure it's not accessible
    // directly through the web server)
    private Path uploadPath;
    private final List<String> acceptedContentTypes = Arrays.asList(
            "image/jpeg",
            "image/png");

    @Override
    public void init() throws ServletException {
        super.init();
        uploadPath = Paths.get("/var/www/uploads"); //get it from an env var
        try {
            if (!Files.exists(uploadPath)) {
                Files.createDirectories(uploadPath);
            }
        } catch (IOException e) {
            throw new ServletException("Could not create upload directory", e);
        }
    }

    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        String servletPath = request.getServletPath();
        switch (servletPath) {
            case "/uploadbefore":
                uploadBefore(request, response);
                break;
            case "/uploadafter":
                uploadAfter(request, response);
                break;
            default:
                response.sendError(HttpServletResponse.SC_NOT_FOUND, "Unknown servlet path: " + servletPath);
                return;
        }
    }

    private void uploadBefore(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        Part filePart = request.getPart("file");
        Path filePath = uploadPath.resolve(filePart.getSubmittedFileName());
        String mimeTypeFromContentType = filePart.getContentType();
        String mimeFromExtension = Files.probeContentType(filePath);

        if (filePart != null && filePart.getSize() > 0) {
            if (acceptedContentTypes.contains(mimeTypeFromContentType) &&
                    acceptedContentTypes.contains(mimeFromExtension) &&
                    mimeTypeFromContentType.equals(mimeFromExtension)) {
                filePart.write(filePath.toString());
                response.getWriter().println("File uploaded successfully to " + filePath);
            } else {
                response.getWriter().println("The file type is not supported or does not match the expected content.");
            }
        } else {
            response.getWriter().println("No file uploaded.");
        }
    }

    private void uploadAfter(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        Part filePart = request.getPart("file");
        Path filePath = uploadPath.resolve(filePart.getSubmittedFileName());
        String mimeTypeFromContentType = filePart.getContentType();
        String mimeFromExtension = Files.probeContentType(filePath);

        if (filePart != null && filePart.getSize() > 0) {
            if (acceptedContentTypes.contains(mimeTypeFromContentType) &&
                    acceptedContentTypes.contains(mimeFromExtension) &&
                    mimeTypeFromContentType.equals(mimeFromExtension) &&
                    checkMagicAndMetadata(filePart)) {
                filePart.write(filePath.toString());
                response.getWriter().println("File uploaded successfully to " + filePath);
            } else {
                response.getWriter().println("The file type is not supported or does not match the expected content.");
            }
        } else {
            response.getWriter().println("No file uploaded.");
        }
    }

    private boolean checkMagicAndMetadata(Part filePart) {
        AutoDetectParser parser = new AutoDetectParser();
        BodyContentHandler handler = new BodyContentHandler();
        Metadata metadata = new Metadata();
        try (InputStream is = TikaInputStream.get(filePart.getInputStream())) {
            parser.parse(is, handler, metadata);
            boolean typeIsCorrect = metadata.get(Metadata.CONTENT_TYPE)
                    .equalsIgnoreCase(filePart.getContentType());
            boolean dimensionsExist = (metadata.get(Metadata.IMAGE_WIDTH) != null &&
                    metadata.get(Metadata.IMAGE_LENGTH) != null);
            return typeIsCorrect && dimensionsExist;
        } catch (IOException | SAXException | TikaException e) {
            return false;
        }
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        String filename = request.getParameter("filename");
        if (filename == null || filename.isEmpty()) {
            response.sendError(HttpServletResponse.SC_BAD_REQUEST, "Filename is missing");
            return;
        }

        Path file = uploadPath.resolve(filename).normalize();
        if (!file.startsWith(uploadPath) || !Files.isReadable(file)) {
            response.sendError(HttpServletResponse.SC_NOT_FOUND, "File not found");
            return;
        }

        response.setContentType(
                Files.probeContentType(file) != null ? Files.probeContentType(file) : "application/octet-stream");

        try (InputStream in = Files.newInputStream(file)) {
            Files.copy(file, response.getOutputStream());
        }
    }
}
