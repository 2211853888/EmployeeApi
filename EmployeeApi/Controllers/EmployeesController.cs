using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeApi.Data;
using EmployeeApi.Models;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;

        if (!_context.Employees.Any())
        {
            _context.Employees.AddRange(
                new Employee { JobNumber = "09", FullName = "محمد احمد علي محمد", EmployeeType = "مدني", Position = "مهندس", Department = "ادارة السيطره", EvaluationStatus = "لم يتم تقييمه" },
                new Employee { JobNumber = "08", FullName = "محمد علي احمد سالم", EmployeeType = "مدني", Position = "موظف", Department = "ادارة السيطره", EvaluationStatus = "لم يتم تقييمه" },
                new Employee { JobNumber = "555", FullName = "علي احمد محمد علي", EmployeeType = "مدني", Position = "موظف", Department = "ادارة السيطره", EvaluationStatus = "لم يتم تقييمه" },
                new Employee { JobNumber = "234", FullName = "حسام احمد علي احمد", EmployeeType = "مدني", Position = "مهندس it", Department = "إدارة التخطيط", EvaluationStatus = "لم يتم تقييمه" }
            );
            _context.SaveChanges();
        }
    }

    // 1. جلب الكل
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _context.Employees.ToListAsync();
        return Ok(list);
    }

    // 2. إضافة موظف جديد
    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return Ok(employee);
    }

    // 3. تحديث التقييم
    [HttpPut("evaluate/{id}")]
    public async Task<IActionResult> EvaluateEmployee(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null) return NotFound("الموظف غير موجود");

        emp.EvaluationStatus = "تم التقييم";
        await _context.SaveChangesAsync();

        return Ok(emp);
    }

    // 4. حذف موظف
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null) return NotFound("الموظف غير موجود");

        _context.Employees.Remove(emp);
        await _context.SaveChangesAsync();

        return Ok(new { message = "تم الحذف بنجاح" });
    }
}