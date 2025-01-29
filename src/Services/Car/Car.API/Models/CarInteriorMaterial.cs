using System;
using System.Collections.Generic;

namespace Pizda.Models;

/// <summary>
/// Материалы салона автомобилей
/// </summary>
public partial class CarInteriorMaterial
{
    /// <summary>
    /// ID материала салона
    /// </summary>
    public int IdCarInteriorMaterial { get; set; }

    /// <summary>
    /// Название материала салона
    /// </summary>
    public string Name { get; set; } = null!;
}
