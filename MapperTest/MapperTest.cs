using System;
using System.Collections.Generic;
using Mapper;
using Moq;
using Xunit;

namespace MapperTest
{
    public class MapperTest
    {
        private static readonly Source SourceToTest = new Source()
        {
            FirstProperty = 10,
            SecondProperty = "abc",
            ThirdProperty = 1.5,
            FourthProperty = 5
        };

        private static readonly Destination ExpectedDestination = new Destination()
        {
            FirstProperty = SourceToTest.FirstProperty,
            SecondProperty = SourceToTest.SecondProperty,
        };

        private static readonly IFunctionBuilder FunctionBuilder = new FunctionBuilder();
        private static readonly IFunctionsCache FunctionsCache = new FunctionsCache();

        [Fact]
        public void Constructor_NullBuilder_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new DtoMapper(null, FunctionsCache));
        }

        [Fact]
        public void Constructor_NullCache_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new DtoMapper(FunctionBuilder, null));
        }

        [Fact]
        public void Map_NullParameter_ArgumentNullExceptionThrown()
        {
            IMapper mapper = new DtoMapper();
            Assert.Throws<ArgumentNullException>(() => mapper.Map<Source, Destination>(null));
        }

        [Fact]
        public void Map_SimpleMappring_ReturnMappedObject()
        {
            IMapper mapper = new DtoMapper();
            Destination actualDestination = mapper.Map<Source, Destination>(SourceToTest);
            Assert.Equal(ExpectedDestination, actualDestination);
        }

        [Fact]
        public void Map_CacheMapping_ReturnMappedObject()
        {
            IMapper mapper = new DtoMapper();
            mapper.Map<Source, Destination>(SourceToTest);

            Destination actualDestination = mapper.Map<Source, Destination>(SourceToTest);
            Assert.Equal(ExpectedDestination, actualDestination);
        }

        [Fact]
        public void Map_CacheTest_ReturnMappedObject()
        {
            IMapper mapper = new DtoMapper();
            mapper.Map<Source, Destination>(SourceToTest);

            Destination actualDestination = mapper.Map<Source, Destination>(SourceToTest);
            Assert.Equal(ExpectedDestination, actualDestination);
        }

        [Fact]
        public void Map_NoCache_CheckCacheNotCalled()
        {
            Mock<IFunctionsCache> cacheMock = new Mock<IFunctionsCache>();
            cacheMock.Setup(cache => cache.Contains(It.IsAny<MappingTypeAssociation>())).Returns(false);

            Mock<IFunctionBuilder> builderMock = new Mock<IFunctionBuilder>();
            builderMock.Setup(builder => builder.Build<object, object>(
                It.IsAny<List<MappingProperty>>())).Returns(o => o);

            IMapper mapper = new DtoMapper(builderMock.Object, cacheMock.Object);
            mapper.Map<object, object>(new object());

            cacheMock.Verify(cache => cache.Get<object, object>(It.IsAny<MappingTypeAssociation>()), Times.Never);
            builderMock.Verify(builder => builder.Build<object, object>(It.IsAny<List<MappingProperty>>()), Times.Once);
        }

        [Fact]
        public void Map_HasCache_CheckCacheCalled()
        {
            Mock<IFunctionsCache> cacheMock = new Mock<IFunctionsCache>();
            cacheMock.Setup(cache => cache.Contains(It.IsAny<MappingTypeAssociation>())).Returns(true);

            Mock<IFunctionBuilder> builderMock = new Mock<IFunctionBuilder>();
            builderMock.Setup(builder => builder.Build<object, object>(
                It.IsAny<List<MappingProperty>>())).Returns(o => o);

            IMapper mapper = new DtoMapper(builderMock.Object, cacheMock.Object);
            mapper.Map<object, object>(new object());

            cacheMock.Verify(cache => cache.Get<object, object>(It.IsAny<MappingTypeAssociation>()), Times.Once);
            builderMock.Verify(builder => builder.Build<object, object>(It.IsAny<List<MappingProperty>>()), Times.Never);
        }



    }
}
