# یکپارچه‌سازی با Microsoft Project Web App (PWA)

این مستند نحوه اتصال به سرور PWA شرکت (Crouse) و استخراج اطلاعات پروژه‌ها و تسک‌ها از طریق `REST API` را توضیح می‌دهد.

## آدرس‌های API مهم

برای واکشی اطلاعات از سرور PWA، از `Endpoint`های زیر استفاده می‌شود:

- **دریافت لیست تمام پروژه‌ها:**
  ```text
  http://project.crouseco.com/project/PMO/_api/ProjectServer/Projects
  ```

- **دریافت لیست تسک‌های یک پروژه خاص:**
  ```text
  http://project.crouseco.com/project/PMO/_api/ProjectServer/Projects('GUID-پروژه')/Tasks
  ```
  *مثال شناسه پروژه:* `c2e806d1-c648-ef11-9bc4-00e0966819c1`

---

## ۱. نمونه اسکریپت PowerShell
برای تست سریع و گرفتن خروجی مستقیماً از طریق ویندوز، می‌توانید از این اسکریپت استفاده کنید:

```powershell
# اطلاعات ورود
$secpasswd = ConvertTo-SecureString 'YOUR_PASSWORD' -AsPlainText -Force
$mycreds = New-Object System.Management.Automation.PSCredential ("svc-sql", $secpasswd)

# آدرس دریافت پروژه‌ها
$url = "http://project.crouseco.com/project/PMO/_api/ProjectServer/Projects"

# ارسال درخواست
$response = Invoke-RestMethod -Uri $url -Credential $mycreds -Headers @{ "Accept" = "application/json;odata=verbose" }

# نمایش خروجی
$response.d.results | Select-Object Name, Id | Format-Table -AutoSize
```

---

## ۲. نمونه کد C# برای اتصال در پروژه NET.
اگر بخواهید لیست پروژه‌ها را داخل همین برنامه (C#) بگیرید و سپس با `Aspose.Tasks` آن‌ها را تبدیل کنید، باید از کلاس `HttpClient` همراه با `HttpClientHandler` برای احراز هویت `NTLM` استفاده کنید:

```csharp
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

public class PwaService
{
    public static async Task FetchProjectsAsync()
    {
        string url = "http://project.crouseco.com/project/PMO/_api/ProjectServer/Projects";
        
        // تنظیمات احراز هویت (Windows/NTLM Authentication)
        var credentials = new NetworkCredential("svc-sql", "YOUR_PASSWORD");
        var handler = new HttpClientHandler { Credentials = credentials };

        using (HttpClient client = new HttpClient(handler))
        {
            // درخواست فرمت JSON
            client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse);
                
                // در اینجا می‌توانید با استفاده از Newtonsoft.Json یا System.Text.Json خروجی را پردازش کنید
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to PWA: " + ex.Message);
            }
        }
    }
}
```

### نکات مهم:
- پسورد در کد بالا نباید به صورت `Hardcode` نوشته شود. بهتر است از `appsettings.json` یا `Environment Variables` خوانده شود.
- خروجی به صورت `JSON` برمی‌گردد و در شیء `d.results` آرایه‌ای از تسک‌ها یا پروژه‌ها قرار دارد.
- ویژگی‌هایی نظیر `PercentComplete`, `Start`, `Finish` مستقیماً از API برای تسک‌ها قابل دریافت است.
