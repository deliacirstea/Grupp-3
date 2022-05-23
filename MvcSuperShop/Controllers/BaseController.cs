using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MvcSuperShop.Data;
using MvcSuperShop.Infrastructure.Context;

namespace MvcSuperShop.Controllers;

public class BaseController : Controller
{
    protected readonly ApplicationDbContext _context;

    public BaseController(ApplicationDbContext context)
    {
        _context = context;
    }
    protected CurrentCustomerContext GetCurrentCustomerContext()
    {
        if (User.Identity.IsAuthenticated) return null;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        return new CurrentCustomerContext { Email = email, UserId = Guid.Parse(userId) };
    }
}