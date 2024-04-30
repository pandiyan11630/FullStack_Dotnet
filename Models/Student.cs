using System.ComponentModel.DataAnnotations;

namespace MyFullStackBackendDemo1;

public class Student
{
    [Key]
    public int Student_Id{get;set;}
    public string? Student_Name{get;set;}
    public int Department_Id{get;set;}
    public string? Gender{get;set;}
    public string? Email_Id{get;set;}
}
