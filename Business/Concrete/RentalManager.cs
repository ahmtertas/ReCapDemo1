using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(CheckRentalCarIsReturnDateNull(rental));
            if (result != null)
            {
                return new ErrorResult(Message.RentalNotAddedForReturnDate);
            }
            _rentalDal.Add(rental);
            return null;

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
        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(p => p.Id == id),Message.RentalGotById);
        }
        public IDataResult<List<Rental>> GetAllByCarId(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(p=>p.CarId == id));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        private IResult CheckRentalCarIsReturnDateNull(Rental rental)
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
                    return new SuccessResult(Message.RentalAdded);
                }
                return new ErrorResult(Message.RentalErrorAdded);

            }
        }

        public IDataResult<List<RentalDetailsDto>> GetDetailsDto()
        {
            return new SuccessDataResult<List<RentalDetailsDto>>(_rentalDal.GetRentalDetailsDtos());
        }
    }
}
