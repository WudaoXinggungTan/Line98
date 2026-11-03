using System;
using System.Collections.Generic;

namespace GameplayScripts.MVC.Gameplay.Ball
{
    public interface IBallController
    {
        public bool CanUndo { get; }

        // Colours of the previews (next turn's balls)
        public event Action<IReadOnlyList<BallColorType>> NextBallsChanged;
        public event Action<bool> UndoAvailabilityChanged;

        public void InitEntryPoint();
        public void StartEntryPoint();
        public void InitExitPoint();

        public void NewGame();
        public void Restart();
        public void Undo();
    }
}
