﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.SistemaComanda.Core.DTOs
{
    public class MensagemDTO
    {
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }
        public string Tipo { get; set; }
    }
}
