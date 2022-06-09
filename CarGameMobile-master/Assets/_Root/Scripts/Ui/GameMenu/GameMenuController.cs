using Tool;
using Profile;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class GameMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/GameMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly GameMenuView _view;


        public GameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(ExitGame);
        }

        private GameMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameMenuView>();
        }

        private void ExitGame() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
