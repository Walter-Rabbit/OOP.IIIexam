﻿using System;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class ClientEntity : IIdentifiable
    {
        public Guid Id { get; set; }
        
        public string FullName { get; set; }
    }
}