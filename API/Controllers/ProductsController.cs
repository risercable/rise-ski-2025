using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        var products = await repo.GetAllProductsAsync(brand, type, sort);

        if (products == null || !products.Any())
            return NotFound();

        return Ok(products);
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);

        if (await repo.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        return BadRequest("Failed to create product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product updatedProduct)
    {
        if (updatedProduct.Id != id || !ProductExists(id))
            return BadRequest("Cannot Update this Product");

        repo.UpdateProduct(updatedProduct);

        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);

        if (product == null) return NotFound();

        repo.DeleteProduct(product);

        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting product");
    }

    private bool ProductExists(int id)
    {
        return repo.ProductExists(id);
    }

    [HttpGet("brands")]
    public async Task<IReadOnlyList<string>> GetBrandsAsync(int id)
    {
        return await repo.GetBrandsAsync();
    }
    
    [HttpGet("types")]
    public async Task<IReadOnlyList<string>> GetTypesAsync(int id) 
    {
        return await repo.GetTypesAsync();
    }
}
