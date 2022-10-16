using AutoMapper;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.AutoMapper
{
    public class AutoMapperConfigurationTests
    {
        private readonly MapperConfiguration _configuration;

        public AutoMapperConfigurationTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        [Fact]
        public void TestAutoMapperConfigurations()
        {
            _configuration.AssertConfigurationIsValid();
        }
    }
}
