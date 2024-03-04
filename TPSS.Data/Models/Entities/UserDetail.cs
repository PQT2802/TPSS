using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class UserDetail
{
    public string? Phone { get; set; }

    public string? PersonalId { get; set; }

    public string? Avatar { get; set; }

    public string UserId { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public string? TaxIdentificationNumber { get; set; }

    public string UserDetailId { get; set; } = null!;

    public bool? IsDelete { get; set; }

    public virtual User User { get; set; } = null!;
}
