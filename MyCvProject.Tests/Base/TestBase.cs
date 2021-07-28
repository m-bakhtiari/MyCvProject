using Microsoft.EntityFrameworkCore;
using MyCvProject.Infra.Data.Context;
using System;

namespace MyCvProject.Tests.Base
{
    public abstract class TestBase
    {
        protected MyCvProjectContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MyCvProjectContext>().Options;
            return new MyCvProjectContext(options);
        }
    }
}
