using System;
using System.Collections.Generic;

namespace Pizda.Models;

/// <summary>
/// Состояние автомобилей
/// </summary>
public partial class CarCondition
{
    /// <summary>
    /// ID состояния авто
    /// </summary>
    public int IdCarCondition { get; set; }

    /// <summary>
    /// Название состояния
    /// </summary>
    public string Name { get; set; } = null!;
}
