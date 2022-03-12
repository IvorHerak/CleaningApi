using CleaningManagement.BLL.CleaningPlans.Models;
using CleaningManagement.BLL.CleaningPlans.Services;
using CleaningManagement.BLL.Exceptions;
using CleaningManagement.BLL.Tests.Infrastructure;
using CleaningManagement.DAL.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningManagement.BLL.Tests
{
    public class Tests
    {
        [Test]
        public void GetCleaningPlanAsync_InvalidId_ShouldThrowException()
        {
            //Arrange
            var respository = new TestRepository<CleaningPlan>();
            var createValidator = new Mock<IValidator<CreateCleaningPlanRequest>>().Object;
            var updateValidator = new Mock<IValidator<UpdateCleaningPlanRequest>>().Object;
            var service = new CleaningPlanService(respository, createValidator, updateValidator);

            //Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetCleaningPlanAsync(Guid.Empty));
        }

        [Test]
        public async Task GetCleaningPlanAsync_ValidId_ShouldReturnCleaningPlan()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var date = DateTime.UtcNow;
            var respository = new TestRepository<CleaningPlan>();
            respository.Data.Add
            (
                new CleaningPlan
                {
                    Id = guid,
                    CreationDate = date,
                    CustomerId = 1,
                    Title = "Test",
                    Description = "Description"
                }
            );
            var createValidator = new Mock<IValidator<CreateCleaningPlanRequest>>().Object;
            var updateValidator = new Mock<IValidator<UpdateCleaningPlanRequest>>().Object;
            var service = new CleaningPlanService(respository, createValidator, updateValidator);

            //Act 
            var result = await service.GetCleaningPlanAsync(guid);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(guid, result.Id);
            Assert.AreEqual(date, result.CreationDate);
            Assert.AreEqual(1, result.CustomerId);
            Assert.AreEqual("Test", result.Title);
            Assert.AreEqual("Description", result.Description);
        }

        [Test]
        public async Task GetCleaningPlansForCustomerAsync_CustomerIdWithNoPlans_ShouldReturnEmptyList()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var date = DateTime.UtcNow;
            var respository = new TestRepository<CleaningPlan>();
            respository.Data.Add
            (
                new CleaningPlan
                {
                    Id = guid,
                    CreationDate = date,
                    CustomerId = 1,
                    Title = "Test",
                    Description = "Description"
                }
            );
            var createValidator = new Mock<IValidator<CreateCleaningPlanRequest>>().Object;
            var updateValidator = new Mock<IValidator<UpdateCleaningPlanRequest>>().Object;
            var service = new CleaningPlanService(respository, createValidator, updateValidator);

            //Act 
            var result = await service.GetCleaningPlansForCustomerAsync(2);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task GetCleaningPlansForCustomerAsync_CustomerIdWithPlans_ShouldReturnOnlyTheCustomerPlanList()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var date = DateTime.UtcNow;
            var respository = new TestRepository<CleaningPlan>();
            respository.Data.Add
            (
                new CleaningPlan
                {
                    Id = guid,
                    CreationDate = date,
                    CustomerId = 1,
                    Title = "Title",
                    Description = "Description"
                }
            );

            respository.Data.Add
            (
                new CleaningPlan
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.UtcNow,
                    CustomerId = 3,
                    Title = "Test 3",
                    Description = "Description 3"
                }
            );
            var createValidator = new Mock<IValidator<CreateCleaningPlanRequest>>().Object;
            var updateValidator = new Mock<IValidator<UpdateCleaningPlanRequest>>().Object;
            var service = new CleaningPlanService(respository, createValidator, updateValidator);

            //Act 
            var result = await service.GetCleaningPlansForCustomerAsync(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            var plan = result.Single();
            Assert.AreEqual(guid, plan.Id);
            Assert.AreEqual(date, plan.CreationDate);
            Assert.AreEqual(1, plan.CustomerId);
            Assert.AreEqual("Title", plan.Title);
            Assert.AreEqual("Description", plan.Description);
        }

        [Test]
        public void DeleteCleaningPlanAsync_InvalidId_ShouldThrowException()
        {
            //Arrange
            var respository = new TestRepository<CleaningPlan>();
            var createValidator = new Mock<IValidator<CreateCleaningPlanRequest>>().Object;
            var updateValidator = new Mock<IValidator<UpdateCleaningPlanRequest>>().Object;
            var service = new CleaningPlanService(respository, createValidator, updateValidator);

            //Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.DeleteCleaningPlanAsync(Guid.Empty));
        }

        [Test]
        public async Task DeleteCleaningPlanAsync_ValidId_ShouldDeleteCleaningPlan()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var date = DateTime.UtcNow;
            var respository = new TestRepository<CleaningPlan>();
            respository.Data.Add
            (
                new CleaningPlan
                {
                    Id = guid,
                    CreationDate = date,
                    CustomerId = 1,
                    Title = "Test",
                    Description = "Description"
                }
            );
            var createValidator = new Mock<IValidator<CreateCleaningPlanRequest>>().Object;
            var updateValidator = new Mock<IValidator<UpdateCleaningPlanRequest>>().Object;
            var service = new CleaningPlanService(respository, createValidator, updateValidator);

            //Act 
            await service.DeleteCleaningPlanAsync(guid);

            //Assert
            Assert.AreEqual(0, respository.Data.Count);
        }

        [Test]
        public async Task UpdateCleaningPlanAsync_InvalidRequest_ShouldThrowExceptionWithErrors()
        {
            //Arrange
            var respository = new TestRepository<CleaningPlan>();
            var createValidator = new Mock<IValidator<CreateCleaningPlanRequest>>().Object;

            var updateValidatorMock = new Mock<IValidator<UpdateCleaningPlanRequest>>();
            updateValidatorMock.Setup(v => v.Validate(It.IsAny<UpdateCleaningPlanRequest>()))
                .Returns
                (
                    new ValidationResult
                    (
                        new List<ValidationFailure> { new ValidationFailure("testProperty", "testMessage") }
                    )
                );

            var request = new UpdateCleaningPlanRequest
            {
                CustomerId = 1,
                Description = "Description",
                Id = Guid.NewGuid(),
                Title = "Title"
            };

            var service = new CleaningPlanService(respository, createValidator, updateValidatorMock.Object);

            //Act
            try
            {
                await service.UpdateCleaningPlanAsync(request);
            }
            catch (BusinessException ex)
            {
                //Assert
                var errors = ex.Errors;
                Assert.AreEqual(1, errors.Count());
                Assert.IsTrue(errors.ContainsKey("testProperty"));
                Assert.AreEqual(errors["testProperty"], "testMessage");
                return;
            }

            Assert.Fail();
        }

        [Test]
        public async Task UpdateCleaningPlanAsync_ValidRequest_ShouldUpdateCleaningPlan()
        {
            //Arrange
            var respository = new TestRepository<CleaningPlan>();
            var guid = Guid.NewGuid();
            var date = DateTime.UtcNow;
            respository.Data.Add
             (
                 new CleaningPlan
                 {
                     Id = guid,
                     CreationDate = date,
                     CustomerId = 1,
                     Title = "Title",
                     Description = "Description"
                 }
             );
            var createValidator = new Mock<IValidator<CreateCleaningPlanRequest>>().Object;

            var updateValidatorMock = new Mock<IValidator<UpdateCleaningPlanRequest>>();
            updateValidatorMock.Setup(v => v.Validate(It.IsAny<UpdateCleaningPlanRequest>()))
                .Returns
                (
                    new ValidationResult()
                );

            var request = new UpdateCleaningPlanRequest
            {
                CustomerId = 1,
                Description = "Description UPD",
                Id = guid,
                Title = "Title UPD"
            };

            var service = new CleaningPlanService(respository, createValidator, updateValidatorMock.Object);

            //Act
            var response = await service.UpdateCleaningPlanAsync(request);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.CustomerId);
            Assert.AreEqual(guid, response.Id);
            Assert.AreEqual("Description UPD", response.Description);
            Assert.AreEqual("Title UPD", response.Title);
            Assert.AreEqual(date, response.CreationDate);

            var plan = respository.Data.Single();
            Assert.IsNotNull(plan);
            Assert.AreEqual(1, plan.CustomerId);
            Assert.AreEqual(guid, plan.Id);
            Assert.AreEqual("Description UPD", plan.Description);
            Assert.AreEqual("Title UPD", plan.Title);
            Assert.AreEqual(date, plan.CreationDate);

        }

        [Test]
        public void UpdateCleaningPlanAsync_InvalidId_ShouldThrowException()
        {
            //Arrange
            var respository = new TestRepository<CleaningPlan>();
            var createValidator = new Mock<IValidator<CreateCleaningPlanRequest>>().Object;
            var updateValidatorMock = new Mock<IValidator<UpdateCleaningPlanRequest>>();
            updateValidatorMock.Setup(v => v.Validate(It.IsAny<UpdateCleaningPlanRequest>()))
                .Returns
                (
                    new ValidationResult()
                );

            var request = new UpdateCleaningPlanRequest
            {
                CustomerId = 1,
                Description = "Description UPD",
                Id = Guid.Empty,
                Title = "Title UPD"
            };

            var service = new CleaningPlanService(respository, createValidator, updateValidatorMock.Object);

            //Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.UpdateCleaningPlanAsync(request));
        }

        [Test]
        public async Task CreateCleaningPlanAsync_InvalidRequest_ShouldThrowExceptionWithErrors()
        {
            //Arrange
            var respository = new TestRepository<CleaningPlan>();
            var createValidatorMock = new Mock<IValidator<CreateCleaningPlanRequest>>();
            createValidatorMock.Setup(v => v.Validate(It.IsAny<CreateCleaningPlanRequest>()))
                .Returns
                (
                    new ValidationResult
                    (
                        new List<ValidationFailure> { new ValidationFailure("testProperty", "testMessage") }
                    )
                );

            var updateValidator = new Mock<IValidator<UpdateCleaningPlanRequest>>().Object;

            var request = new CreateCleaningPlanRequest
            {
                CustomerId = 1,
                Description = "Description",
                Title = "Title"
            };

            var service = new CleaningPlanService(respository, createValidatorMock.Object, updateValidator);

            //Act
            try
            {
                await service.CreateCleaningPlanAsync(request);
            }
            catch (BusinessException ex)
            {
                //Assert
                var errors = ex.Errors;
                Assert.AreEqual(1, errors.Count());
                Assert.IsTrue(errors.ContainsKey("testProperty"));
                Assert.AreEqual(errors["testProperty"], "testMessage");
                return;
            }

            Assert.Fail();
        }

        [Test]
        public async Task CreateCleaningPlanAsync_ValidRequest_ShouldCreateCleaningPlan()
        {
            //Arrange
            var respository = new TestRepository<CleaningPlan>();
            var createValidatorMock = new Mock<IValidator<CreateCleaningPlanRequest>>();
            createValidatorMock.Setup(v => v.Validate(It.IsAny<CreateCleaningPlanRequest>()))
                .Returns
                (
                    new ValidationResult()
                );
            var updateValidator = new Mock<IValidator<UpdateCleaningPlanRequest>>().Object;

            var request = new CreateCleaningPlanRequest
            {
                CustomerId = 1,
                Description = "Description",
                Title = "Title"
            };

            var service = new CleaningPlanService(respository, createValidatorMock.Object, updateValidator);

            //Act
            var response = await service.CreateCleaningPlanAsync(request);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.CustomerId);
            Assert.AreEqual("Description", response.Description);
            Assert.AreEqual("Title", response.Title);

            var plan = respository.Data.Single();
            Assert.IsNotNull(plan);
            Assert.AreEqual(1, plan.CustomerId);
            Assert.AreEqual("Description", plan.Description);
            Assert.AreEqual("Title", plan.Title);

        }
    }
}