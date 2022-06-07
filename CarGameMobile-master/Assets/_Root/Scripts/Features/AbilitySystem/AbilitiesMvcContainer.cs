using Tool;
using UnityEngine;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal class AbilitiesMvcContainer : BaseMvcContainer
    {
        private static readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Ability/AbilityItemConfigDataSource");
        private static readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Ability/AbilitiesView");


        public AbilitiesMvcContainer(IAbilityActivator activator, Transform placeForUi) =>
            CreateController(activator, placeForUi);


        private AbilitiesController CreateController(IAbilityActivator activator, Transform placeForUi)
        {
            AbilityItemConfig[] itemConfigs = LoadConfigs();
            AbilitiesRepository repository = CreateRepository(itemConfigs);

            AbilitiesView view = CreateView(placeForUi);
            var controller = new AbilitiesController(view, repository, itemConfigs, activator);

            AddController(controller);

            return controller;
        }

        private AbilitiesView CreateView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }

        private AbilityItemConfig[] LoadConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_dataSourcePath);

        private AbilitiesRepository CreateRepository(AbilityItemConfig[] abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }
    }
}
