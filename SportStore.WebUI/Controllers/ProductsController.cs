using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportStore.Application.Common.Repositories;
using SportStore.Application.Services;
using SportStore.Domain.Entities;
using SportStore.WebUI.ViewModels;

namespace SportStore.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileUploadService _fileUploadService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductsController(
        IUnitOfWork unitOfWork,
        IFileUploadService fileUploadService,
        IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _fileUploadService = fileUploadService;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var products = _unitOfWork.Products.GetAll()
            .Select(product => new ProductCatalogItemViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            })
            .ToList();

        return View(products);
    }

    public IActionResult Details(int id)
    {
        var product = _unitOfWork.Products.GetById(id);
        if (product is null)
        {
            return NotFound();
        }

        var viewModel = new ProductDetailsViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            CategoryName = product.Category?.Name ?? string.Empty
        };

        return View(viewModel);
    }

    [Authorize(Policy = "CanManageCatalog")]
    [HttpGet]
    public IActionResult Create()
    {
        return View(new ProductFormViewModel
        {
            Categories = GetCategorySelectList()
        });
    }

    [Authorize(Policy = "CanManageCatalog")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductFormViewModel viewModel)
    {
        if (viewModel.Image is null)
        {
            ModelState.AddModelError(nameof(ProductFormViewModel.Image), "Please upload a product image");
        }

        if (!ModelState.IsValid)
        {
            viewModel.Categories = GetCategorySelectList();
            return View(viewModel);
        }

        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "products");
        string imageFileName = GenerateUniqueFileName(viewModel.Image!.FileName);
        string filePath = Path.Combine(uploadsFolder, imageFileName);

        using (var inputStream = viewModel.Image.OpenReadStream())
        {
            _fileUploadService.UploadFile(inputStream, filePath);
        }

        var product = new Product
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Price = viewModel.Price,
            CategoryId = viewModel.CategoryId,
            ImageUrl = imageFileName
        };

        _unitOfWork.Products.Add(product);
        _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "CanManageCatalog")]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _unitOfWork.Products.GetById(id);
        if (product is null)
        {
            return NotFound();
        }

        var viewModel = new ProductFormViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            ExistingImageUrl = product.ImageUrl,
            Categories = GetCategorySelectList()
        };

        return View(viewModel);
    }

    [Authorize(Policy = "CanManageCatalog")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProductFormViewModel viewModel)
    {
        var product = _unitOfWork.Products.GetById(viewModel.Id);
        if (product is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            viewModel.Categories = GetCategorySelectList();
            viewModel.ExistingImageUrl = product.ImageUrl;
            return View(viewModel);
        }

        product.Name = viewModel.Name;
        product.Description = viewModel.Description;
        product.Price = viewModel.Price;
        product.CategoryId = viewModel.CategoryId;

        if (viewModel.Image is not null)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "products");
            string imageFileName = GenerateUniqueFileName(viewModel.Image.FileName);
            string filePath = Path.Combine(uploadsFolder, imageFileName);

            using (var inputStream = viewModel.Image.OpenReadStream())
            {
                _fileUploadService.UploadFile(inputStream, filePath);
            }

            product.ImageUrl = imageFileName;
        }

        _unitOfWork.Products.Update(product);
        _unitOfWork.Save();

        return RedirectToAction(nameof(Details), new { id = product.Id });
    }

    [Authorize(Policy = "CanManageCatalog")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var product = _unitOfWork.Products.GetById(id);
        if (product is null)
        {
            return NotFound();
        }

        _unitOfWork.Products.Delete(product);
        _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    private IEnumerable<SelectListItem> GetCategorySelectList()
    {
        return _unitOfWork.Categories.GetAll()
            .Select(category => new SelectListItem
            {
                Value = category.Id.ToString(),
                Text = category.Name
            })
            .ToList();
    }

    private static string GenerateUniqueFileName(string fileName)
    {
        string safeFileName = Path.GetFileName(fileName);
        return $"{Path.GetFileNameWithoutExtension(safeFileName)}_{Guid.NewGuid():N}{Path.GetExtension(safeFileName)}";
    }
}
