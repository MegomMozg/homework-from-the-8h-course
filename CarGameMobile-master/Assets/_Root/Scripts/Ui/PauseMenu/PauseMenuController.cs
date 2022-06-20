using Tool;
using Profile;
using UnityEngine;

namespace Ui
{
    internal class PauseMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Game/PauseMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly PauseMenuView _view;
        private readonly Pause _pause;


        public PauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;

            _view = LoadView(placeForUi);
            _view.Init(Game, Menu);

            _pause = new Pause();
        }

        protected override void OnDispose()
        {
            _pause.Dispose();
        }


        public void Show()
        {
            _pause.Enable();
            _view.Show();
        }

        public void Hide()
        {
            _pause.Disable();
            _view.Hide();
        }


        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }

        private void Game() => Hide();

        private void Menu() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
