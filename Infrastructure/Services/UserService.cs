using AutoMapper;
using Infrastructure.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Infrastructure;

public interface IUserService
{
    IEnumerable<UserDto> GetAll();
    UserDto GetById(Guid id);
    Task<UserDto> CreateAsync(CreateUserDto model);
    Task<UserDto> GetByEmailAsync(LoginUserDto loginDto);
    void Delete(Guid id);

    Task<IList<string>> GetUserRoles(UserDto loginDto);

}

public class UserService : IUserService
{
    private DemoContext _context;
    public readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UserService(DemoContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public IEnumerable<UserDto> GetAll()
    {
        var list = _mapper.Map<IList<User>, IList<UserDto>>(_context.Users.ToList());
        return list;
    }




    public UserDto GetById(Guid id)
    {
        return getUserDto(id);
    }

    public async Task<UserDto> GetByEmailAsync(LoginUserDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        UserDto userDto = null;
        if (user != null && passwordValid)
            userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }


    public async Task<IList<string>> GetUserRoles(UserDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        var roles = await _userManager.GetRolesAsync(user);

        return roles;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto model)
    {

        var search = _context.Users.Where(s => s.Email == model.Email);
        if(search.Any())
        {
            throw new Exception("Doar un singur cont per email este acceptat.");
        }

        var user = _mapper.Map<User>(model);
        user.Active = true;

        var resultAdd = await _userManager.CreateAsync(user, model.Password);
        if (resultAdd.Succeeded)
        {
            var createdEntry = _context.Users.Where(s => s.Email == model.Email).First();
            var roleAttribution = await _userManager.AddToRoleAsync(user, "User");
            return _mapper.Map<UserDto>(user);
        }
        else
        {
            throw new Exception($"Unable to create the specified user,{resultAdd.Errors.First().Description}");
        }
        
       
    }

    public void Delete(Guid id)
    {
        var user = getUser(id);
        user.Active = false;
        _context.Update(user);
        _context.SaveChanges();
    }

    public async Task<UserDto> Activate(Guid id)
    {
        try
        {
            var user = getUser(id);
            user.Active = true;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }


    private UserDto getUserDto(Guid id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");

        var _mappedUser = _mapper.Map<UserDto>(user);
        return _mappedUser;
    }

    private User getUser(Guid id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}