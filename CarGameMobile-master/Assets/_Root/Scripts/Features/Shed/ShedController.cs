using Tool;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;
using Features.Inventory;
using Features.Inventory.Items;
using Features.Shed.Upgrade;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        private readonly ShedView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryController _inventoryController;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;


        public ShedController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _upgradeHandlersRepository = CreateRepository();
            _inventoryController = CreateInventoryController(placeForUi);
            _view = LoadView(placeForUi);

            _view.Init(Apply, Back);
        }


        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private InventoryController CreateInventoryController(Transform placeForUi)
        {
            InventoryView inventoryView = LoadInventoryView(placeForUi);
            InventoryModel inventoryModel = _profilePlayer.Inventory;
            ItemsRepository itemsRepository = CreateItemsRepository();

            var inventoryController = new InventoryController(inventoryView, inventoryModel, itemsRepository);
            AddController(inventoryController);

            return inventoryController;
        }

        private InventoryView LoadInventoryView(Transform placeForUi)
        {
            var path = new ResourcePath("Prefabs/Inventory/InventoryView");

            GameObject prefab = ResourcesLoader.LoadPrefab(path);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        private ItemsRepository CreateItemsRepository()
        {
            var path = new ResourcePath("Configs/Inventory/ItemConfigDataSource");

            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(path);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }


        private void Apply()
        {
            _profilePlayer.CurrentTransport.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.CurrentTransport,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Apply. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Back. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
        }


        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    handler.Upgrade(upgradable);
        }
    }
}
