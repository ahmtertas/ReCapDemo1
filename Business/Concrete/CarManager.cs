using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
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
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Car car)
        {
            var result = BusinessRules.Run(CheckCountOfCar());
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            _carDal.Add(car);
            return new SuccessResult(Message.CarAdded);

        }

        public IResult Delete(Car car)
        {            
            _carDal.Delete(car);
            return new SuccessResult(Message.CarDelete);
        }


        [CacheAspect]
        [PerformanceAspect(3)]
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Message.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Message.CarListed);
        }

        public IDataResult<List<Car>> GetAllByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p=>p.ColorId == id));
        }


        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p => p.Id == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        //özellikle Iproductserviceteki get metoduna ait olan ne kadar cachleme varsa hepsini siler.
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Car car)
        {          
            _carDal.Update(car);
            return new SuccessResult(Message.CarUpdated);
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccessResult(Message.CarUpdated);
        }

        //Business Rules
        private IResult CheckCountOfCar()
        {
            var result = _carDal.GetAll().Count;
            if (result>=11)
            {
                return new ErrorResult(Message.OverCountOfCar);
            }
            return new SuccessResult();
        }
        

    }
}
