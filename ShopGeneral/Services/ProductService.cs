﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopGeneral.Data;
using ShopGeneral.Infrastructure.Context;

namespace ShopGeneral.Services;

public  class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly IPricingService _pricingService;
    private readonly IMapper _mapper;

    public ProductService(ApplicationDbContext context, IPricingService pricingService,  IMapper mapper)
    {
        _context = context;
        _pricingService = pricingService;
        _mapper = mapper;
    }
    public IEnumerable<ProductServiceModel> GetNewProducts(int cnt, CurrentCustomerContext context)
    {
        return _pricingService.CalculatePrices(_mapper.Map<IEnumerable<ProductServiceModel>>(_context.Products
            .Include(e=>e.Category)
            .Include(e=>e.Manufacturer)
            .OrderByDescending(e => e.AddedUtc)
            .Take(cnt)), context);
    }

    public IEnumerable<ProductServiceModel> GetAllProducts()
    {
        return _mapper.Map<IEnumerable<ProductServiceModel>>(_context.Products
            .Include(e => e.Category)
            .Include(e => e.Manufacturer)
            .OrderByDescending(e => e.AddedUtc));
    }
}