using myStore.DTO;
using myStore.Models;
using myStore.Repository;

namespace myStore.ServiceLayer
{public interface IproductServices
    {
        List<ProductDTO> GetAllProducts();
        ProductDTO? GetProductById(int id);
        void Add(ProductDTO productDTO );
        void Update(ProductDTO productDTO,int id);
        void Delete(ProductDTO productDTO,int id);

    }
    public class ProductServices:IproductServices
    {
        private readonly IproductRepository _repository;
        public ProductServices(IproductRepository repository) { 
        _repository = repository;
        }
        public List<ProductDTO> GetAllProducts()
        {
            var result2 = _repository.GetAll();
            return result2.Select(x => new ProductDTO
            {
                productName = x.productName,
                productDescription = x.productDescription,
                productprice = x.productprice,
                productQuantity = x.productQuantity,
            }).ToList();
        }
        public ProductDTO? GetProductById(int id) {
            var result= _repository.GetByID(id);
            if (result == null)
                return null;

            return new ProductDTO
            {
                productName=result.productName,
                productDescription=result.productDescription,
                productprice=result.productprice,
                productQuantity=result.productQuantity,
                
            };
        }
        public void Add(ProductDTO productDTO  ) {
             
            
                productModel productModel = new productModel();
            productModel.productName = productDTO.productName;
            productModel.productDescription = productDTO.productDescription;
            productModel.productprice= productDTO.productprice;
            productModel.productQuantity = productDTO.productQuantity;
            _repository.Add(productModel);
            
        }
        public void Update(ProductDTO productDTO, int id) {
            var findProductId = _repository.GetByID(id);
            if (findProductId == null)
            {
                throw new Exception("product not Exist actually");
            }
            findProductId.productName= productDTO.productName;
            findProductId.productDescription= productDTO.productDescription;
            findProductId.productprice= productDTO.productprice;
            findProductId.productQuantity= productDTO.productQuantity;
            _repository.Update(findProductId);
        }
        public void Delete(ProductDTO productDTO,int id) {
            var findProductId = _repository.GetByID(id);
            if (findProductId == null)
            {
                throw new Exception("product not Exist actually");
            }
            _repository.Delete(findProductId);
        }

    }
}
