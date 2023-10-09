using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVien.Models.Entity;

[Table("HuongDan")]
public partial class HuongDan
{
    [Key]
    public string Id { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? MaDeTai { get; set; }

    [Column("MaGV")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MaGv { get; set; }

    [Column("MaSV")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MaSv { get; set; }

    public double? KetQua { get; set; }
    public string? Filter { get; set; }
}
