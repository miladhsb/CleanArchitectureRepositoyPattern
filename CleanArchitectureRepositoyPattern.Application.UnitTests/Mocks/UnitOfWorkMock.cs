using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureRepositoyPattern.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {

            Mock<IBlogPostRepository> mockIBlogPostRepository = BlogPostRepositoryMock.GetBlogPostRepository();

            Mock<IUnitOfWork> mockIUnitOfWork = new Mock<IUnitOfWork>();
            mockIUnitOfWork.Setup(p => p.BlogPostRepository).Returns(mockIBlogPostRepository.Object);
            mockIUnitOfWork.Setup(p => p.SaveChangesAsync()).ReturnsAsync(1);

            return mockIUnitOfWork;
        }
    }
}
