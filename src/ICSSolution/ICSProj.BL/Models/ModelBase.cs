﻿using System;


namespace ICSProj.BL.Models;

public abstract record ModelBase : IModel
{
    public Guid Id { get; set; }
}
