using Tool;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;
using Features.Inventory;
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
        
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        private readonly ShedView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryMvcContainer _inventoryMvcContainer;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;
        private readonly ShedUpgradeWithEquippedItems _upgrade;
        private readonly ShedLoadView _loadView;


        public ShedController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _inventoryMvcContainer = CreateInventoryContainer(_profilePlayer.Inventory, placeForUi);
            _upgradeHandlersRepository = CreateRepository();
            _loadView = new ShedLoadView();
            _view = _loadView.LoadView(placeForUi);

            _view.Init(Apply, Back);
            _upgrade = new ShedUpgradeWithEquippedItems();
        }


        private InventoryMvcContainer CreateInventoryContainer(IInventoryModel model, Transform placeForUi)
        {
            var container = new InventoryMvcContainer(model, placeForUi);
            AddContainer(container);

            return container;
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        


        private void Apply()
        {
            _profilePlayer.CurrentTransport.Restore();

            _upgrade.UpgradeWithEquippedItems(
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


        
    }
}
