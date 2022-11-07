﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ISHRM.Models;
using Microsoft.EntityFrameworkCore;

namespace ISHRM.Controllers
{
    public class HomeController : Controller
    {
        private IStudentRepository repo;
        public HomeController(IStudentRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditEmployee(int id)
        {
            ViewBag.Supervisors = repo.Supervisors;
            ViewBag.SemesterYears = repo.SemesterYears;
            ViewBag.Positions = repo.Positions;
            ViewBag.Courses = repo.Course;
            ViewBag.Programs = repo.ProgramYears;
            Student_Employment s = repo.GetEmployees().Where(s => s.StudentEmploymentID == id).FirstOrDefault();
            return View(s);
        }
        [HttpPost]
        public IActionResult EditEmployee(Student_Employment stu)
        {
            repo.EditStudentEmployee(stu);

            return RedirectToAction("EditEmployee", new { id = stu.StudentEmploymentID });
        }
        public IActionResult CreateEmployee()
        {
            ViewBag.Supervisors = repo.Supervisors;
            ViewBag.SemesterYears = repo.SemesterYears;
            ViewBag.Positions = repo.Positions;
            ViewBag.Courses = repo.Course;
            ViewBag.Programs = repo.ProgramYears;
            return View();
        }
        [HttpPost]
        public IActionResult CreateEmployee(Student_Employment stu)
        {
            repo.CreateStudentEmployee(stu);

            return RedirectToAction("EmployeeData");
        }
        public IActionResult EmployeeData()
        {
            ViewBag.Employees = repo.GetEmployees();

            ViewBag.Supervisors = repo.Supervisors.ToList();

            ViewBag.Semesters = repo.SemesterYears.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult EmployeeData(int SupervisorID, int SemesterYearID)
        {

            ViewBag.Employees = repo.GetEmployees()
                .Where(s => s.SupervisorID == SupervisorID || SupervisorID == 0)
                .Where(s => s.SemesterYearID == SemesterYearID || SemesterYearID == 0);

            ViewBag.Supervisors = repo.Supervisors.ToList();

            ViewBag.Semesters = repo.SemesterYears.ToList();

            return View();
        }
        public IActionResult DeleteEmployee(int id)
        {
            Student_Employment s = repo.GetEmployees().Where(s => s.StudentEmploymentID == id).FirstOrDefault();
            repo.DeleteStudentEmployee(s);

            return RedirectToAction("EmployeeData");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
