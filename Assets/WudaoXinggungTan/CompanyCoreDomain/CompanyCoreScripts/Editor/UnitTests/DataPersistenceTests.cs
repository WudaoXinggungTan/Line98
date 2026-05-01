using NUnit.Framework;
using CompanyCoreScripts.Services.LoggerService;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.PlayerPrefDataPersistenceService;
using CompanyCoreScripts.Services.SerializersService.Serializer;
using Zenject;

namespace CompanyCoreScripts.Editor.UnitTests
{
    public class PlayerPrefDataPersistenceServiceTests : ZenjectUnitTestFixture
    {
        private IPlayerPrefDataPersistenceService playerPrefDataPersistenceService;
        private const string Key = "test-key";
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Container.BindInterfacesTo<CustomLogger1>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SerializerService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerPrefDataPersistenceService>().AsSingle().NonLazy();
            playerPrefDataPersistenceService = Container.Resolve<IPlayerPrefDataPersistenceService>();
        }

        [Test]
        public void Load_ReturnsDeserializedData_IfKeyExists()
        {
            var testData = new TestData { Value = 42 };
            playerPrefDataPersistenceService.Save(Key, testData);
            var result = playerPrefDataPersistenceService.Load<TestData>(Key);
            Assert.AreEqual(testData.Value, result.Value);
        }

        [Test]
        public void Load_ReturnsDefault_IfKeyMissing()
        {
            var defaultVal = new TestData { Value = 100 };
            var result = playerPrefDataPersistenceService.Load("unknown-key", defaultVal);
            Assert.AreEqual(defaultVal.Value, result.Value);
        }

        public class TestData
        {
            public int Value;
        }
    }
}
