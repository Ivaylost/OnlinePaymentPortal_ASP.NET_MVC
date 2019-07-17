using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Tests.Utilis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Tests.RoleServiceTest
{
    [TestClass]
    public class GetRoleByIdAsync_Should
    {
        [TestMethod]
        public async Task Return_Correct_Role()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Correct_Role));


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Roles.Add(TestUtils.roleFull);
                await arrangeContext.SaveChangesAsync();

                var roleDTOMapper = new Mock<IDtoMapper<Role, RoleDTO>>();
                var sut = new RoleService(arrangeContext, roleDTOMapper.Object);
                var result = await sut.GetRoleByIdAsync(TestUtils.roleFull.Id);
                roleDTOMapper.Verify(x => x.MapFrom(It.Is<Role>(xx => xx.Id == TestUtils.roleFull.Id)));
            }
        }
    }
}
