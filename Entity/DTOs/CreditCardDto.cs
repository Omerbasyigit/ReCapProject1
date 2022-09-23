using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTOs
{
    public class CreditCardDto:IDto
    {
        public int CustomerId { get; set; }
        public string CardNumber { get; set; }
        public string FullName { get; set; }
    }
}
