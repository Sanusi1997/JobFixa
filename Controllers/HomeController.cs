using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JobFixa.Models;
using JobFixa.Entities;
using JobFixa.Services.Interfaces;

namespace JobFixa.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly IJobFixaUserService _jobFixaUserService;
    public HomeController(ILogger<HomeController> logger, IJobFixaUserService jobFixaUserService)
    {
        _logger = logger;
        _jobFixaUserService = jobFixaUserService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult EmployerLogin()
    {

        return View();
    }
    public IActionResult EmployerRegister()
    {

        return View();
    }
    public IActionResult JobSeekerRegister()
    {

        return View();
    }
    public IActionResult JobSeekerLogin()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEmployer(AuthenticationModel userInput)
    {

        var userInRepo = await _jobFixaUserService.GetUserByEmail(userInput.Email);

        if (userInRepo != null)
        {
            ViewBag.EmployerRegistrationError = "Account with Email already Exists";
            return View("BuyerRegister");
        }
        var userEntity = new JobFixaUser();
        userEntity.Email = userInput.Email;
        userEntity.Password = userInput.Password;
        userEntity.UserType = "Employer";
        try
        {

            _jobFixaUserService.AddUser(userEntity);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message.ToString());
        }

        ViewBag.EmployerSuccessfulRegistration = "Successfully Created Employer Account";
        return View("EmployerLogin");
    }
    [HttpPost]
    public async Task<IActionResult> RegisterJobSeeker(AuthenticationModel userInput)
    {

        var userInRepo = await _jobFixaUserService.GetUserByEmail(userInput.Email);

        if (userInRepo != null)
        {
            ViewBag.JoBSeekerRegistrationError = "Account with Email already Exists";
            return View("JobSeekerRegister");
        }
        var userEntity = new JobFixaUser();
        userEntity.Email = userInput.Email;
        userEntity.Password = userInput.Password;
        userEntity.UserType = "JobSeeker";
        try
        {

            _jobFixaUserService.AddUser(userEntity);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message.ToString());
        }

        ViewBag.JobSeekerSuccessfulRegistration = "Successfully Created JobSeeker Account";
        return View("JobSeekerLogin");
    }


    [HttpPost]
    public async Task<IActionResult> LoginEmployer(LoginModel userInput)
    {

        var userInRepo = await _jobFixaUserService.AuthenticateUser(userInput.Email, userInput.Password);

        if (userInRepo == null)
        {
            ViewBag.EmployerLoginError = "Password or Email is incorrect";
            return View("EmployerLogin");
        }
        else
        {
            if (userInRepo.UserType != "Employer")
            {
                ViewBag.BuyerLoginError = "Not an Employer! Return to jobseeker login instead.";
                return View("EmployerLogin");
            }
            else
            {

                var userDto = new JobFixaUserDTO();
                userDto.JobFixaUserId = userInRepo.JobFixaUserId.ToString();
                return RedirectToAction("Index", userDto);
            }
        }
    }
    [HttpPost]
    public async Task<IActionResult> LoginJobSeeker(LoginModel userInput)
    {

        var userInRepo = await _jobFixaUserService.AuthenticateUser(userInput.Email, userInput.Password);

        if (userInRepo == null)
        {
            ViewBag.JobSeekerLoginError = "Password or Email is incorrect";
            return View("JobSeekerLogin");
        }
        else
        {
            if (userInRepo.UserType != "JobSeeker")
            {
                ViewBag.SellerLoginError = "Not a JobSeeker! Return to Employer login instead.";
                return View("JoBSeekerLogin");
            }
            else
            {
                var userDto = new JobFixaUserDTO();
                userDto.JobFixaUserId = userInRepo.JobFixaUserId.ToString();
                return RedirectToAction("Index", userDto); ;

            }
        }


    }
}
