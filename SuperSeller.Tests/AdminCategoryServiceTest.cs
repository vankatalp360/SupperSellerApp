using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperSeller.Common.Admin.BindingModels;
using SuperSeller.Data;
using SuperSeller.Models;
using SuperSeller.Services.Admin;
using SuperSeller.Web.Mapping;

namespace SuperSeller.Tests
{
    [TestClass()]
    public class AdminCategoryServiceTest
    {
        public ApplicationDbContext Context { get; set; }

        [TestMethod]
        public void CreateCategory_WithExistCategory_ShouldReturnFalse()
        {
            Context.Categories.Add(new Category() {Name = "Cars"});
            Context.SaveChanges();

            AutoMapper.Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var service = new CategoryService(Context, AutoMapper.Mapper.Instance);
            
            //act
            var result = service.CreateCategory(new CreateCategoryBindingModel() {Name = "Cars"}).Result;

            //assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CreateCategory_WithCategory_ShouldReturnTrue()
        {
            Context.Categories.Add(new Category() { Name = "Cars" });
            Context.SaveChanges();

            AutoMapper.Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var service = new CategoryService(Context, AutoMapper.Mapper.Instance);

            //act
            var result = service.CreateCategory(new CreateCategoryBindingModel() { Name = "Cars" }).Result;

            //assert
            Assert.AreEqual(true, result);
        }

        [TestInitialize]
        public void InitializeTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Context = new ApplicationDbContext(options);
        }
    }
}