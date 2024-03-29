﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVien.Models.Entity;

[Table("Khoa")]
public partial class Khoa
{
    [Key]
    public string Id { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    [Required(ErrorMessage = "Không được phép để trống!")]
    public string? MaKhoa { get; set; }

    [StringLength(150)]
    [Required]
    public string? TenKhoa { get; set; }

    [Column("SDT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Sdt { get; set; }

    public string? Filter { get; set; }

    public string? UrlImage { get; set; }
}
