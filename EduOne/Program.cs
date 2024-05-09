// See https://aka.ms/new-console-template for more information
using EduOne.DataAccess;
using EduOne.Models.Instructors;
using EduOne.Services.Courses;
using EduOne.Services.Instructors;
using EduOne.Services.Students;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

// 

var context = new AppDbContext();


var course = await context.Courses
    .FirstOrDefaultAsync(t => t.Id == 3);

context.Entry(course).Reference(t => t.Instructor).Load();

Console.WriteLine();