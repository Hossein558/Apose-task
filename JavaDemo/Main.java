import com.aspose.tasks.*;

import java.util.Calendar;
import java.util.Date;

public class Main {
    public static void main(String[] args) throws Exception {
        System.out.println("Starting Aspose.Tasks for Java Demo...");

        // Try to apply a license if available (Optional)
        try {
            License license = new License();
            // license.setLicense("path/to/license.lic");
            // System.out.println("License set successfully.");
        } catch (Exception ex) {
            System.out.println("Could not set license: " + ex.getMessage());
        }

        // Create a new project
        Project project = new Project();

        // Add a root task
        Task rootTask = project.getRootTask();
        rootTask.set(Tsk.NAME, "My Main Project (Java)");

        // Add 5 tasks to the root task
        Task task1 = rootTask.getChildren().add("Task 1: Requirements Gathering");
        Task task2 = rootTask.getChildren().add("Task 2: Design & Prototyping");
        Task task3 = rootTask.getChildren().add("Task 3: Implementation");
        Task task4 = rootTask.getChildren().add("Task 4: Testing & QA");
        Task task5 = rootTask.getChildren().add("Task 5: Deployment");

        // Set start date
        Calendar cal = Calendar.getInstance();
        cal.set(2026, Calendar.JUNE, 15, 8, 0, 0);
        Date projectStartDate = cal.getTime();

        // Task 1
        task1.set(Tsk.START, projectStartDate);
        task1.set(Tsk.DURATION, project.getDuration(3, TimeUnitType.Day));
        task1.set(Tsk.ACTUAL_WORK, project.getDuration(24, TimeUnitType.Hour));

        // Task 2
        cal.add(Calendar.DAY_OF_MONTH, 3);
        task2.set(Tsk.START, cal.getTime());
        task2.set(Tsk.DURATION, project.getDuration(5, TimeUnitType.Day));
        task2.set(Tsk.ACTUAL_WORK, project.getDuration(20, TimeUnitType.Hour));

        // Task 3
        cal.add(Calendar.DAY_OF_MONTH, 5);
        task3.set(Tsk.START, cal.getTime());
        task3.set(Tsk.DURATION, project.getDuration(10, TimeUnitType.Day));
        task3.set(Tsk.ACTUAL_WORK, project.getDuration(0, TimeUnitType.Hour));

        // Task 4
        cal.add(Calendar.DAY_OF_MONTH, 10);
        task4.set(Tsk.START, cal.getTime());
        task4.set(Tsk.DURATION, project.getDuration(2, TimeUnitType.Day));
        task4.set(Tsk.ACTUAL_WORK, project.getDuration(0, TimeUnitType.Hour));

        // Task 5
        cal.add(Calendar.DAY_OF_MONTH, 2);
        task5.set(Tsk.START, cal.getTime());
        task5.set(Tsk.DURATION, project.getDuration(1, TimeUnitType.Day));
        task5.set(Tsk.ACTUAL_WORK, project.getDuration(0, TimeUnitType.Hour));

        // Link tasks
        project.getTaskLinks().add(task1, task2, TaskLinkType.FinishToStart);
        project.getTaskLinks().add(task2, task3, TaskLinkType.FinishToStart);
        project.getTaskLinks().add(task3, task4, TaskLinkType.FinishToStart);
        project.getTaskLinks().add(task4, task5, TaskLinkType.FinishToStart);

        // Recalculate
        project.recalculate();

        System.out.println("5 Tasks created and scheduled successfully in Java.");

        // Save
        String outputPath = "OutputProject_Java.xml";
        project.save(outputPath, SaveFileFormat.XML);

        System.out.println("Project saved successfully to " + outputPath);
    }
}
