using System;
using System.Linq;
using Core.Gameplay.Models;
using Core.Gameplay.Views;
using Cysharp.Threading.Tasks;
using SceneSwitchLogic.Switchers;
using Services.SectionSwitchService;
using UnityEngine.Assertions;
using VContainer.Unity;

namespace Core.Gameplay.Controllers
{
    public class GameplayController : IInitializable
    {
        private readonly SectionSwitchParams _sectionSwitchParams;

        private readonly SectionSwitchService _sectionSwitchService;

        private readonly GameplayView _view;

        public GameplayController(SectionSwitchParams sectionSwitchParams, GameplayView view,
            SectionSwitchService sectionSwitchService)
        {
            _sectionSwitchParams = sectionSwitchParams;
            _view = view;
            _sectionSwitchService = sectionSwitchService;
        }

        void IInitializable.Initialize()
        {
            var gameplaySettings =
                (GameplaySettings) _sectionSwitchParams.SwitchParams.FirstOrDefault(p =>
                    p.GetType() == typeof(GameplaySettings));

            Assert.IsNotNull(gameplaySettings);

            _view.SetName(gameplaySettings.Name);

            StartGameplay().Forget();
        }

        private async UniTask StartGameplay()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5));

            var gameResult = new GameplayResult("Player 1");

            _sectionSwitchService.Switch("MainMenu", gameResult); //todo: Вынести ключи в константы или в енумы
        }
    }
}