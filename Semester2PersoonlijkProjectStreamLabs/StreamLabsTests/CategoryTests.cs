using Data.Interfaces;
using Data.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Models;
using System;

namespace StreamLabsTests
{
    [TestClass]
    public class CategoryTests
    {
        private CategoryLogic _categoryLogic;
        private ICategoryContext _categoryContext;
        private List<Category> _categoryList = new List<Category>();

        private void InstanceLogic()
        {
            _categoryList.Add(new Category(1, "category1", "this is category 1"));
            _categoryList.Add(new Category(2, "category2", "this is category 2"));
            _categoryList.Add(new Category(3, "category3", "this is category 3"));
            _categoryList.Add(new Category(4, "category4", "this is category 4"));
            _categoryList.Add(new Category(5, "category5", "this is category 5"));
            _categoryContext = new CategoryMemory(_categoryList);
            _categoryLogic = new CategoryLogic(_categoryContext);
        }

        [TestMethod]
        public void AddNewCategory()
        {
            InstanceLogic();
            Category newCategory = new Category(6, "new category", "this is a new category");
            List<Category> testList = _categoryLogic.GetAllCategories();

            Assert.AreEqual(_categoryList, testList);

        }
    }
}