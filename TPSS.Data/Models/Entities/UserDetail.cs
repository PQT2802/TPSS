using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class UserDetail
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? PersonalId { get; set; }

    public string? Avatar { get; set; }

    public string UserId { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public string? TaxIdentificationNumber { get; set; }

    public virtual User? User { get; set; }
}
