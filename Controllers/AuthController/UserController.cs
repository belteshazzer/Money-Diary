using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyDiary.Models.Dtos.AuthDto;
using MoneyDiary.Models.Entities;
using MoneyDiary.Services.EmailService;
using System.Net;
using System.Text.Encodings.Web;

// using Serilog;
using System.Threading.Tasks;

namespace MoneyDiary.Controllers.AuthController
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<UserController> _logger;


        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // Send email with token to user
                var confirmationLink = Url.Action("ConfirmEmail", "User", new { email = user.Email, token }, protocol: HttpContext.Request.Scheme);

                _logger.LogInformation($"Confirmation Link: {confirmationLink}");
                
                await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.");
                
                return Ok(new { Message = "User registered successfully. Please check your email to confirm your account." });

            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                return BadRequest(new { Message = "Invalid email confirmation request" });
            
            _logger.LogInformation($"Received Token: {token}"); 

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new { Message = "Email not found" });
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Email confirmed successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User logged in successfully" });
            }

            if (result.IsLockedOut)
            {
                return BadRequest(new { Message = "User account locked out" });
            }

            return BadRequest(new { Message = "Invalid login attempt" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "User logged out successfully" });
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest(new { Message = "User not found" });
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Password changed successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest(new { Message = "User not found" });
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User deleted successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
    }
}