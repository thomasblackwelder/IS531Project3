using System;
using System.ComponentModel.DataAnnotations;

public class Semester_Year	
{
	[Key]
	public int SemesterYearID { get; set; }
	public string SemesterName { get; set; }
	public int Year { get; set; }
}
