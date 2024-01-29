using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoAPI.Services;

public class AuthService : IAuthService
{
    private readonly TodoDBContext _context;
    private readonly IConfiguration _config;
    
    public AuthService(TodoDBContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    
    public async Task Register(RegisterRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<string> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user =>
            user.Username == request.Username && user.Password == request.Password);

        if (user == null)
        {
            throw new Exception("Username or password is incorrect");
        }
        
        // If we have made it to this stage
        // the username are correct so we want to 
        // generate a JWT token and return it.
        var token = GenerateToken(user);
        
        return token;
    }
    
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);
            
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}