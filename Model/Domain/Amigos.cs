﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Amigos
    {
        public int Id { get; set; }
        public virtual Emisor Emisor { get; set; }
        public virtual Receptor Receptor { get; set; }
        public DateTime Fecha { get; set; }
    }
}
