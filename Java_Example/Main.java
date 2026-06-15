import java.io.File;
import java.net.URL;
import java.net.URLClassLoader;

public class Main {
    public static void main(String[] args) {
        System.out.println("Testing Aspose.Total for Java Licenses");
        System.out.println("--------------------------------------");

        String licensePath = "lib" + File.separator + "Aspose.Total.lic";
        int successCount = 0;
        int failCount = 0;

        String[] classesToTest = {
            "com.aspose.threed.License",
            "com.aspose.barcode.License",
            "com.aspose.cad.License",
            "com.aspose.email.License",
            "com.aspose.html.License",
            "com.aspose.imaging.License",
            "com.aspose.note.License",
            "com.aspose.ocr.License",
            "com.aspose.omr.License",
            "com.aspose.page.License",
            "com.aspose.pdf.License",
            "com.aspose.psd.License",
            "com.aspose.slides.License",
            "com.aspose.tasks.License",
            "com.aspose.words.License"
        };

        for (String className : classesToTest) {
            String productName = className.split("\\.")[2];
            try {
                Class<?> licenseClass = Class.forName(className);
                Object licenseObj = licenseClass.getDeclaredConstructor().newInstance();
                
                try {
                    java.lang.reflect.Method setLicenseMethod = licenseClass.getMethod("setLicense", String.class);
                    setLicenseMethod.invoke(licenseObj, licensePath);
                    System.out.println(String.format("[SUCCESS] %-20s | License applied successfully.", productName));
                    successCount++;
                } catch (Exception e) {
                    java.lang.reflect.Method setLicenseMethod = licenseClass.getMethod("setLicense", java.io.InputStream.class);
                    java.io.FileInputStream fis = new java.io.FileInputStream(licensePath);
                    setLicenseMethod.invoke(licenseObj, fis);
                    fis.close();
                    System.out.println(String.format("[SUCCESS] %-20s | License applied successfully.", productName));
                    successCount++;
                }
            } catch (Exception ex) {
                System.out.println(String.format("[FAILED]  %-20s | %s", productName, ex.getMessage()));
                failCount++;
            }
        }

        System.out.println("--------------------------------------");
        System.out.println("Successful: " + successCount);
        System.out.println("Failed: " + failCount);

        if (failCount > 0) {
            System.exit(1);
        }
    }
}
