using System;
using System.Collections.Generic;

namespace Pizda.Models;

/// <summary>
/// Варианты обмена автомобилей
/// </summary>
public partial class CarExchangeOption
{
    /// <summary>
    /// ID варианта обмена
    /// </summary>
    public int IdCarExchangeOption { get; set; }

    /// <summary>
    /// Название варианта обмена
    /// </summary>
    public string Name { get; set; } = null!;
}
