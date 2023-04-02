using Doska.AppServices.Services.Category;
using Doska.Contracts.CategoryDto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    public class CategoryController : ControllerBase
    {
        ICategoryService _CategoryService;
        public CategoryController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }



        [HttpGet("/allCategorys")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _CategoryService.GetAll(take, skip);

            return Ok(result);
        }

        [HttpPost("/createCategory")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCategory(CreateOrEditCategoryRequest request)
        {
            var result = await _CategoryService.CreateCategoryAsync(request);

            return Created("", result);
        }

        [HttpPut("/updateCategory/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory(Guid id, CreateOrEditCategoryRequest request)
        {
            var result = await _CategoryService.EditCategoryAsync(id, request);

            return Ok(result);
        }

        [HttpDelete("/deleteCategory/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCategory(Guid id, CreateOrEditCategoryRequest request)
        {
            await _CategoryService.DeleteAsync(id);
            return Ok();
        }
    }
}
