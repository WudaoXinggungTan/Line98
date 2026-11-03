# Line98

Unity 6 (6000.5.9f1, URP 2D) version of the Line 98 color-ball puzzle. Portrait UI, reference resolution 1080×1920.

## Layout
All first-party code lives under `Assets/WudaoXinggungTan/`, as nested domains. Each domain has its own scripts folder, assembly definition, scene and Zenject installer:

| Domain | Path | Assembly | Scene |
|---|---|---|---|
| CompanyCore (reusable framework) | `CompanyCoreDomain/` | `CompanyCoreAssembly` | `CompanyCoreScene` (boot) |
| GameCore (game states, loading screen) | `CompanyCoreDomain/GameCoreDomain/` | `GameCoreAssembly` | `GameCoreScene` |
| MainMenu | `.../GameCoreDomain/MainMenuDomain/` | `MainMenuAssembly` | `MainMenuScene` |
| Gameplay | `.../GameCoreDomain/GameplayDomain/` | `GameplayAssembly` | `GameplayScene` |

Scenes load in Build Settings order, starting from `CompanyCoreScene`, so start Play mode from that scene. Third-party code is in `ProjectPlugins/` (DOTween/DOTween Pro). Zenject, Addressables, Input System, Newtonsoft JSON and Unity Test Framework come from packages. The AI Game Developer (Unity MCP) plugin `com.ivanmurzak.unity.mcp` comes from OpenUPM (scoped registry in `Packages/manifest.json`) and pulls in `extensions.unity.playerprefsex`. Never commit `file:` package paths that point outside the repo (e.g. a local `.tgz` in Downloads); they break on every other machine.

Dependencies only go downward: Gameplay/MainMenu → GameCore → CompanyCore. Don't reference a sibling or child domain. If you add a dependency, update the `.asmdef` references.

## Architecture conventions
- **Controller + View**: each feature is `IXxxController` / `XxxController` (plain C#, constructor-injected) plus `XxxView` (a MonoBehaviour that only does visuals). Models are plain C# or ScriptableObjects (`...ModelScriptableObject`). Controllers expose `InitEntryPoint()`, `StartEntryPoint()` and `InitExitPoint()`. See `GameplayScripts/MVC/UI/HUD/` for the reference example.
- **Zenject**: register new controllers in the domain's `ZenjectInstallers/XxxInstaller.cs`, as `Bind<IXxx>().To<Xxx>().AsSingle().NonLazy()`. Bind views with `FromInstance` from a `[SerializeField]` on the installer. Group bindings in `#region` blocks.
- **Scene flow**: each scene has an initiator (`_XxxInitiator/`, implements `ISceneInitiator`). It runs `LoadXxxStateCommand` → `StartXxxStateCommand`, and on exit `ExitXxxStateCommand` (in `Commands/EntryPoint/`). Hook a new controller's entry and exit methods into those commands.
- **Commands** extend `BaseCommand`. They resolve dependencies in `ResolveDependencies()` via `diContainer` and are created through `ICommandFactory` (`CreateCommandAsync<T>()` / `CreateCommandVoid<T>()`).
- **Game states**: `GameStateService` switches between `MainMenuState` and `GameplayState` (Load → Start → Exit), with the loading screen shown in between.
- **UI groups**: `BaseUIGroupView.InitEntryPoint` disables *every* `Canvas` under the group, and `StartEntryPoint` re-enables only `startCanvasView`. Put always-visible UI inside the start canvas (the board lives in the HUD canvas), or make the extra canvas re-enable itself in its own `StartEntryPoint`. Screen-space canvases are set to Screen Space - Camera and need `IUICameraController.UICamera` assigned at runtime.
- **Safe area**: UI that must stay tappable goes under a `SafeArea` object with `SafeAreaFitter` (`CompanyCoreScripts/Helpers/UIHelpers/`); backgrounds stay full-screen outside it. Check notched devices in the Device Simulator.
- **Async**: use Unity `Awaitable` with a `CancellationTokenSource` (linked to `Application.exitCancellationToken`), not `Task` or coroutines. For a no-op, return `AwaitableUtils.CompletedTask`.
- **Reuse CompanyCore services** before writing new ones: logging goes through `MyLoggerService` (not `Debug.Log`). Others: `BroadcastService` (events), `UpdateSubscriptionService` (`IUpdatable` instead of `Update()`), object pools in `Helpers/ObjectPoolsHelpers/`, `PlayerPrefDataPersistenceService`, and the loaders for Addressables, AssetBundles and Resources.

## Code style
Match the surrounding files:
- `#region Dependencies / Constructor / Public Methods / Private Methods`
- `this.field = field` in constructors
- private fields in camelCase, without an underscore (the pool classes are the exception)
- namespaces follow the folder (`GameplayScripts.MVC.UI.HUD`)

## Working rules
- Don't hand-edit `.unity`, `.prefab` or `.asset` YAML for structural changes. Write the C# side, then either do the scene and prefab setup through the AI Game Developer (Unity MCP) plugin, when the Editor is connected, or give step-by-step Editor instructions.
- When driving Play mode through the MCP plugin, the Editor barely ticks while unfocused; set `Application.runInBackground = true` for that session.
- Every new asset or folder needs a `.meta` file. Let Unity create it; never write or delete `.meta` files by hand.
- Don't edit generated files: `*.csproj`, `*.sln`, `Library/`, `Temp/`, `obj/`, `Logs/`.
- Editor unit tests live in `CompanyCoreScripts/Editor/UnitTests/`. `PreBuildUnitTestsValidator` runs them before a build, so keep them passing.
- Commit messages follow the existing style: a date/time prefix, e.g. `31-08-2026 - Fix Input system namespace`.

## Status
The architecture, the menu/gameplay scene flow and the core Line 98 game work. Gameplay code is in `GameplayScripts/`:
- `MVC/Gameplay/Board/`: `BoardModel` (pure C#, `int[,]`, 0 = empty, +color = ball, -color = preview; x = column, y = row, (0,0) top-left; `Clone`/`CopyFrom` for save states), `BoardView` (on `HudUICanvas/SafeArea/BoardArea`, builds cells from `GameplayAssets/Prefabs/BoardCell.prefab` into `BoardGrid`, whole-unit square cells, click handling), `BoardController` (exposes `CellClicked` and cell transforms).
- `MVC/Gameplay/Ball/`: `BallController` runs the turn flow (deal balls + previews, select/move, clear lines, grow previews, game over, Restart, one-step Undo); `BallPathFinder` (BFS, 4-way) and `BallLineFinder` (5+ in a row, 4 axes) are static and pure; `BallView` + `BallPool` (`PrefabsPool`, `Ball.prefab`, sprites in `Textures/Balls/`).
- `Services/ScoreService/`: current and best score (best saved via `IPlayerPrefDataPersistenceService`). Scoring: 5 balls = 5 points, +2 per extra ball.
- `MVC/UI/HUD/` (score, next-ball previews, Restart / Undo / New Game / Settings buttons) and `MVC/UI/Settings/` (panel over the HUD). `Commands/GameOver/GameOverCommand` returns to the main menu for now.

Next steps: Editor unit tests for `BoardModel`, `BallPathFinder`, `BallLineFinder` and the scoring formula (none exist yet); a real game-over screen.
