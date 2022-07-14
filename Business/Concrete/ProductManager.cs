using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Caching;
using Core.Aspects.AutoFac.Logging;
using Core.Aspects.AutoFac.Performance;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Entities.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(), product);

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName));
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            if (_productDal.Get(p => p.ProductName == productName) != null)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult Delete(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IResult Update(Product product)
        {
            IResult result = CheckIfProductNameExists(product.ProductName);
            if (result != null)
            {
                return result;
            }
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        [PerformanceAspect(1)]
        public IDataResult<List<Product>> GetList()
        {
            Thread.Sleep(1000);
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        //[SecuredOperation("Product.List,Admin")]
        [CacheAspect(10)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }
    }
}