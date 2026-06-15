Write-Host "========================================="
Write-Host "Testing .NET (C#) Version"
Write-Host "========================================="
dotnet run

if ($LASTEXITCODE -eq 0) {
    Write-Host ".NET version executed successfully!" -ForegroundColor Green
} else {
    Write-Host ".NET version failed!" -ForegroundColor Red
}

Write-Host ""
Write-Host "========================================="
Write-Host "Testing Java Version"
Write-Host "========================================="
cd JavaDemo
javac -cp "aspose-tasks-20.2-jdk18.jar" Main.java
if ($LASTEXITCODE -ne 0) {
    Write-Host "Java compilation failed!" -ForegroundColor Red
} else {
    java -cp ".;aspose-tasks-20.2-jdk18.jar" Main
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Java version executed successfully!" -ForegroundColor Green
    } else {
        Write-Host "Java version failed during execution!" -ForegroundColor Red
    }
}
cd ..
Write-Host "========================================="
Write-Host "Tests Completed."
