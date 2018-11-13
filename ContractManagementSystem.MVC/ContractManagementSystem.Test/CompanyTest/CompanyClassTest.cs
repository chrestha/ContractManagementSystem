﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContractManagementSystem.BusinessLogic.DataManager;
using System.Web.Mvc;
using ContractManagementSystem.BusinessLogic.DataManager.Interface;
using ContractManagementSystem.BusinessLogic.ViewModel;
using ContractManagementSystem.MVC.Controllers;
using Moq;
using Xunit;

namespace ContractManagementSystem.Test.CompanyTest
{
    public class CompanyClassTest
    {
        [Fact]
        public void GetCompaniesFromDatabase()
        {
            //arrange
            var dataSource = new Mock<ICompanyDM>();
            CompanyVM company = new CompanyVM() { ID=1,Name="Anish", CompanyABN_CAN="2134123", Description="",URL="abc.com"};
            dataSource.Setup(m => m.GetById(It.IsAny<object>())).Returns(company);
            CompanyController controller = new CompanyController(dataSource.Object);
             
            ViewResult result = controller.Details(1) as ViewResult;
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CompanyVM>(
                viewResult.ViewData.Model);
            Assert.Equal("Anish", model.Name);
            Assert.NotNull(result);
             
            //
        }
    }
}