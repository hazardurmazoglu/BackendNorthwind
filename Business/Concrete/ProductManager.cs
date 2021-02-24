
using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;


        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

            //Ayni isimde ürün eklenemez.
            IResult result =BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfProductNameExists(product.ProductName) , CheckIfCategoryLimitExceeded());
            if (result!= null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);

          
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==15)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>( _productDal.GetAll(),Messages.ProductsListed);
            
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id),Messages.ProductsListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.UnitPrice>= min && p.UnitPrice<=max),Messages.ProductsListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),Messages.ProductDetailsListed);

        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                _productDal.Add(product);

                return new SuccessResult(Messages.ProductAdded);
            }
            return new ErrorResult();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //Select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();



        }

        //Eğer mevcut kategori sayısı 15 i geçtiyse sisteme yeni ürün eklenemez.
        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }
            return new SuccessResult();
        }

       
    }
}
