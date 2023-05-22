﻿using System;
using System.Collections.Generic;

namespace Zadanie7.DAL;

public partial class Trip
{
    public int IdTrip { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }

    public virtual ICollection<ClientTrip> Clients { get; set; } = null!;
        
    public virtual ICollection<Country> Countries { get; set; } = null!;
}
