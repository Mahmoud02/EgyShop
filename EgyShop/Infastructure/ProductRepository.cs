using Azure.Storage;
using Azure.Storage.Blobs;
using EgyShop.Data;
using EgyShop.Models;
using EgyShop.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace EgyShop.Infastructure
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductRepository(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public List<Product> GetStoreProductsWithCategories(List<Category> categories)
        {

            var products =  _context.CategoryProducts.
                 Include(p => p.Category)
                .Where(p => categories.Contains(p.Category))
                .ToList();
            return products;

        }
        public List<Product> GetCategoryProducts(int categoryId )
        {

            var products = _context.CategoryProducts
                .Where(p =>p.CategoryId == categoryId)
                .ToList();
            return products;

        }
        public async Task<Product> GetProductWithCategory(int id)
        {
            var product = await _context.CategoryProducts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            return product;
        }
        public async Task SaveProductInDatabase(Product product)
        {

            _context.Add(product);
            await _context.SaveChangesAsync();
        }
       

        public async Task<Product> FindPrdouctById(int id )
        {
            var product = await _context.CategoryProducts.FindAsync(id);
            return product;
        }
        public async Task UpdateProductInDatabase(Product product)
        {

            _context.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductFromDatabase(int id , string imageFolder)
        {
            var product = await _context.CategoryProducts.FindAsync(id);
            var imageName = product.PhotoLink;
            _context.CategoryProducts.Remove(product);
            await _context.SaveChangesAsync();
            DeleteImageFileFromStoarge(imageName);
        }
        //Save Image in Soatrge
        public async Task<string> SaveFileInClouidAndReturnFileUrl(IFormFile  image, string userId )
        {
            String _accountname = "shopstoarge";
            string _accountkey = "zIH9IxbaIwNRdvGvsJMkS3yBIRZhVESGBEeIy/Rt9sx5QK0e6Cc/b3FyXe3fsAhZ1a759dDptkV3ChtE5XYxqQ==";
            // Create a URI to the blob
            var imageuri = userId + "/" + Guid.NewGuid().ToString() + "_" + image.FileName ;
            Uri blobUri = new Uri("https://" +
                                   _accountname +
                                  ".blob.core.windows.net/" +
                                  "products-images" +
                                  "/" + imageuri );


           // Create StorageSharedKeyCredentials object by reading
           // the values from the configuration (appsettings.json)
            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(_accountname, _accountkey);

            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            // Upload the file
            using (Stream s = image.OpenReadStream())
            {
                await blobClient.UploadAsync(s);
            }
            return imageuri;
        }
        //then return ImageName;

        //Save Image in FileSystem
        //then return ImageName;
        public string SaveFileAndReturnFileName(ProductViewModel product , string storeId)
        {
            string uniqueFileName = null;

            if (product.PhotoLink != null)
            {
                string Folder = Path.Combine(webHostEnvironment.WebRootPath, "store-images");
                string storeFolder = Path.Combine(Folder, storeId);
                System.IO.Directory.CreateDirectory(storeFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.PhotoLink.FileName;
                string filePath = Path.Combine(storeFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                product.PhotoLink.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public async Task<string> changeImageINstoarge(IFormFile image ,String oldImagePath, string userId)
        {
            DeleteImageFileFromStoarge(oldImagePath);
           return await SaveFileInClouidAndReturnFileUrl( image,userId);
        }
        public  void ChangeImageOfProduct(ProductViewModel product, String ImagePath,string storeId)
        {
            string Folder = Path.Combine(webHostEnvironment.WebRootPath, "store-images");
            string storeFolder = Path.Combine(Folder, storeId);
            string filePath = Path.Combine(storeFolder, ImagePath);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            product.PhotoLink.CopyTo(fileStream);

        }
       
        public void DeleteImageFileFromStoarge(String ImageNameUrl)
        {
            String _accountname = "shopstoarge";
            string _accountkey = "zIH9IxbaIwNRdvGvsJMkS3yBIRZhVESGBEeIy/Rt9sx5QK0e6Cc/b3FyXe3fsAhZ1a759dDptkV3ChtE5XYxqQ==";
            StorageCredentials storageCredentials =
               new StorageCredentials(_accountname, _accountkey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials,true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();  
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("products-images");
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(ImageNameUrl);
            blob.DeleteAsync();

        }
        public void DeleteImageFile(String ImageName ,String ImageFolder)
        {
            string Folder = Path.Combine(webHostEnvironment.WebRootPath, "store-images");
            string storeFolder = Path.Combine(Folder, ImageFolder);
            string filePath = Path.Combine(storeFolder, ImageName);
            File.Delete(filePath);
        }
        public Product  CreateProductFromProductViewModel(ProductViewModel model , String imageName) 
        {
            Product product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                PhotoLink = imageName,
                FeatureProduct = model.FeatureProduct,
                RecommendProduct = model.RecommendProduct
            };
            return product;
        }
        public Product CreateProductFromProductViewModel(ProductViewModel productViewModel)
        {
            Product product = new Product
            {
                ProductId = productViewModel.ProductId,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                CategoryId = productViewModel.CategoryId,
                PhotoLink = productViewModel.PreviousPhotoLink,
                FeatureProduct = productViewModel.FeatureProduct,
                RecommendProduct = productViewModel.RecommendProduct
            };
            return product;
        }
        public ProductViewModel CreateProductViewModelFromProduct(Product product)
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                PhotoLink = null,
                PreviousPhotoLink = product.PhotoLink,
                FeatureProduct = product.FeatureProduct,
                RecommendProduct = product.RecommendProduct
            };
            return productViewModel;
        }

    }
}
