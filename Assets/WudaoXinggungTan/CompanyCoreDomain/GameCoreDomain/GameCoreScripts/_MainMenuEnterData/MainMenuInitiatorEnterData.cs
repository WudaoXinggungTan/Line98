using CompanyCoreScripts.Services.SceneInitiatorsService;

namespace GameCoreScripts._MainMenuEnterData
{
    public class MainMenuInitiatorEnterData : IInitiatorEnterData
    {
        public int Number { get; }
        public MainMenuInitiatorEnterData()
        {
            Number = 5;
        }
    }
}