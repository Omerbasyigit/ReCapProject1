using Business.Concrete;

using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
          
           
        }

        private static void CustomerDetails()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            foreach (var c in customerManager.GetCustomerDetails().Data)
            {
                Console.WriteLine(c.FirstName + "  " + c.LastName + "  " + c.CompanyName + "  " + c.Email);
            }
        }

        private static void UpdateColor()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            foreach (var c in colorManager.GetAll().Data) { }
            colorManager.Update(new Color { ColorId = 4, ColorName = "Pink" });
        }

     //
    }
}
