
using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.Models;

namespace TaskFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> Tasks = new()
    {
        new TaskItem
        {
            Id = 1,
            Title = "Müşteri taleplerinin analizi",
            Description = "Yeni taleplerin gereksinimlerini belirle",
            IsCompleted = false
        },
        new TaskItem
        {
            Id = 2,
            Title = "Raporlama modülünün geliştirilmesi",
            Description = "Aylık performans raporlarını oluştur",
            IsCompleted = false
        },
        new TaskItem
        {
            Id = 3,
            Title = "Kullanıcı yetkilendirme kontrolü",
            Description = "Rol bazlı erişim kontrollerini tamamla",
            IsCompleted = true
        }
    };

    [HttpGet]
    public ActionResult<List<TaskItem>> GetAll()
    {
        return Ok(Tasks);
    }


    [HttpPost]
    public ActionResult<TaskItem> Create(TaskItem task)
    {
        task.Id = Tasks.Count == 0
            ? 1
            : Tasks.Max(t => t.Id) + 1;

        task.CreatedAt = DateTime.UtcNow;

        Tasks.Add(task);

        return CreatedAtAction(
            nameof(GetAll),
            new { },
            task);
    }


    [HttpPut("{id}")]
    public IActionResult Update(int id, TaskItem updatedTask)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound();
        }

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;

        return NoContent();
    }

}
