using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            var result = _rentalDal.Get(r => r.Id == rental.Id);
            if (result is not null)
            {
                return new ErrorResult(Message.RentalErrorAdded);
            }

            else
            {

                var response = _rentalDal.Get
                    (r => r.CarId == rental.CarId && (r.ReturnDate == null 
                    || r.ReturnDate > rental.RentDate));

                if (response is null)
                {
                    _rentalDal.Add(rental);
                    return new SuccessResult(Message.RentalAdded);
                }

                return new ErrorResult(Message.RentalErrorAdded);


            }

        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());

        }

        public IDataResult<List<Rental>> GetAllByCarId(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(p=>p.CarId == id ));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }
    }
}
