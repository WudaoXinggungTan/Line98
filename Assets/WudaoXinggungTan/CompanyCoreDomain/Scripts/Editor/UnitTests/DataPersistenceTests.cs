using NUnit.Framework;
using Zenject;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.DataPersistenceService;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.LoggerService;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.SerializersService.Serializer;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Editor.UnitTests
{
    public class PlayerPrefsDataPersistenceTests : ZenjectUnitTestFixture
    {
        private IDataPersistence _dataPersistence;
        private const string Key = "test-key";
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Container.BindInterfacesTo<CustomLogger>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SerializerService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerPrefsDataPersistence>().AsSingle().NonLazy();
            _dataPersistence = Container.Resolve<IDataPersistence>();
        }

        [Test]
        public void Load_ReturnsDeserializedData_IfKeyExists()
        {
            var testData = new TestData { Value = 42 };
            _dataPersistence.Save(Key, testData);
            var result = _dataPersistence.Load<TestData>(Key);
            Assert.AreEqual(testData.Value, result.Value);
        }

        [Test]
        public void Load_ReturnsDefault_IfKeyMissing()
        {
            var defaultVal = new TestData { Value = 100 };
            var result = _dataPersistence.Load("unknown-key", defaultVal);
            Assert.AreEqual(defaultVal.Value, result.Value);
        }

        public class TestData
        {
            public int Value;
        }
    }
}
