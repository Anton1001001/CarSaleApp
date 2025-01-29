using System;
using System.Collections.Generic;

namespace Pizda.Models;

/// <summary>
/// Цвета салона автомобилей
/// </summary>
public partial class CarInteriorColor
{
    /// <summary>
    /// ID цвета салона
    /// </summary>
    public int IdCarInteriorColor { get; set; }

    /// <summary>
    /// Название цвета салона
    /// </summary>
    public string Name { get; set; } = null!;
}
