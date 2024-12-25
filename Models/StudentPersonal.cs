using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StudentSystemMvcCore.Models;

public partial class StudentPersonal
{
    public Guid Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Student Code")]
    [MaxLength(10)]
    [DisplayName("Student Code")]
    public string StudentCode { get; set; } = null!;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter First Name")]
    [MaxLength(50)]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Last Name")]
    [MaxLength(50)]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;

    public string? Address { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Mobile Number")]
    [MaxLength(20)]
    public string Mobile { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Email Address")]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]
    [MaxLength(50)]
    public string Email { get; set; } = null!;
    [DisplayName("Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime? Dob { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter NIC")]
    [MaxLength(20)]
    [DisplayName("N.I.C")]
    public string Nic { get; set; } = null!;
}
