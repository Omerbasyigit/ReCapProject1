using Core.Entities;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTOs
{
    public class CarDetailDto:IDto
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int ModelYear { get; set; }
        public string ColorName { get; set; }
        public string BrandName { get; set; }
        public List<CarImage> CarImages{ get; set; }
        public int DailyPrice { get; set; }
        public string Description { get; set; }
    }
}
