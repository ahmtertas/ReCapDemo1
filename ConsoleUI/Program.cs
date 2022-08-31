// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

RentalManager rentalManager = new RentalManager(new EfRentalDal());
var result = rentalManager.Add( new Rental 
{
    CarId=3,
    CustomerId=2,
    RentDate=DateTime.Now   
});
if (result.Success)
{
    Console.WriteLine(result.Message);
}
else
{
    Console.WriteLine(result.Message);
}



Console.ReadLine();
Console.ReadKey();
