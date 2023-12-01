using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JobFixa.Models;
using JobFixa.Entities;
using JobFixa.Services.Interfaces;
using JobFixa.Data;
using Microsoft.EntityFrameworkCore;

namespace JobFixa.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly IJobFixaUserService _jobFixaUserService;
    private readonly JobFixaContext _context;
    public HomeController(ILogger<HomeController> logger, IJobFixaUserService jobFixaUserService, JobFixaContext context)
    {
        _logger = logger;
        _jobFixaUserService = jobFixaUserService;
        _context = context;
    }

    public IActionResult Index()
    {
        return View("Index");
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
            return View("EmployerRegister");
        }
        var userEntity = new JobFixaUser();
        userEntity.Email = userInput.Email;
        userEntity.Password = userInput.Password;
        userEntity.UserType = "Employer";


        var newUser = new JobFixaUser();
        try
        {

            newUser =  _jobFixaUserService.AddUser(userEntity);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message.ToString());
        }

        ViewBag.EmployerSuccessfulRegistration = "Successfully Created Employer Account";
        return View("EmployerLogin", newUser);
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

        var newUser = new JobFixaUser();
    
        try
        {

            newUser = _jobFixaUserService.AddUser(userEntity);

          
           

        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message.ToString());
        }
        var userDto = new LoginModel();
        userDto.Email = userInput.Email;
        ViewBag.JobSeekerSuccessfulRegistration = "Successfully Created JobSeeker Account";
        return View("JobseekerLogin", userDto);
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

                var existingEmployer = await _context.JobSeekers.Where(s => s.JobFixaUserId == userInRepo.JobFixaUserId).FirstOrDefaultAsync();
                return View("Views/Employers/Edit.cshtml", existingEmployer);
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
                return View("JobSeekerLogin");
            }
            else
            {
                var userDto = new JobFixaUserDTO();
                userDto.JobFixaUserId = userInRepo.JobFixaUserId.ToString();

                var existingJobSeeker = await _context.JobSeekers.Where(s => s.JobFixaUserId == userInRepo.JobFixaUserId).FirstOrDefaultAsync();
                return View("Views/JobSeekers/Edit.cshtml", existingJobSeeker);

            }
        }


    }
}
