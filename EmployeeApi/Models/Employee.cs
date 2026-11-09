namespace EmployeeApi.Models;

public class Employee
{
    public int Id { get; set; }
    public string JobNumber { get; set; } = string.Empty;       // الرقم الوظيفي أو عسكري
    public string EmployeeType { get; set; } = "مدني";          // نوع الموظف (مدني/عسكري)
    public string FullName { get; set; } = string.Empty;        // اسم الموظف
    public string Position { get; set; } = string.Empty;        // الوظيفة
    public string Department { get; set; } = string.Empty;      // القسم
    public string EvaluationStatus { get; set; } = "لم يتم تقييمه"; // حالة التقييم
}