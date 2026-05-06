using CompanyCoreScripts.Services.SceneInitiatorsService;

namespace GameCoreScripts._MainMenuEnterData
{
    public class MainMenuInitiatorEnterData : IInitiatorEnterData
    {
        public int TestNumber { get; }
        public MainMenuInitiatorEnterData()
        {
            TestNumber = 69;
        }
    }
}