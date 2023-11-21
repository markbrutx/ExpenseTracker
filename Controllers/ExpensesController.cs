using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers;



[ApiController]
[Route("api/[controller]")]
public class ExpensesController: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ExpensesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    [HttpGet]
    public IActionResult GetExpenses()
    {
        var expenses = _context.Expenses.Where(e=> !e.IsDeleted).ToList();
        return Ok(expenses);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Expense> GetExpense(int id)
    {
        var expense = _context.Expenses.Find(id);

        if (expense == null)
        {
            return NotFound();
        }

        return expense;
    }
    
    [HttpPost]
    public IActionResult CreateExpense([FromBody] Expense expense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Expenses.Add(expense);
        _context.SaveChanges();

        return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateExpense(int id, [FromBody] Expense expense)
    {
        if (id != expense.Id)
        {
            return BadRequest();
        }

        _context.Entry(expense).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Expenses.Any(e=> e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteExpense(int id)
    {
        var expense = _context.Expenses.Find(id);
        if (expense == null)
        {
            return NotFound();
        }

        expense.IsDeleted = true;
        _context.SaveChanges();

        return NoContent();
    }

}