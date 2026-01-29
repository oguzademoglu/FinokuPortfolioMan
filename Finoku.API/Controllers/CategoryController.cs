using Finoku.Domain.Entities;
using Finoku.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finoku.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] AssetCategory category) 
        {
            _context.AssetCategories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(new {message = $"{category.Name} eklendi"});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id, [FromBody] AssetCategory category)
        {
            var existing = await _context.AssetCategories.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = category.Name;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Kategori güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, [FromBody] AssetCategory category)
        {
            var existing = await _context.AssetCategories.FindAsync(id);
            if (existing == null) return NotFound();

            _context.AssetCategories.Remove(existing);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Kategori silindi." });
        }

    }
}
