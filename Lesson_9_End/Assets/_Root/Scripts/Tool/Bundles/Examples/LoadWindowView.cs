using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;
        [SerializeField] private Button _addBackgroundButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private AssetReference _spawningBackgroundPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Transform _spawnedBackgroundTransform;
        
        [Header("Addressables Button")]
        [SerializeField] private Button _spawnAssetButton;
        [SerializeField] private Button _SpawnBackgroundButton;
        [SerializeField] private Button _DespawnBackgroundButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();


        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _spawnAssetButton.onClick.AddListener(SpawnPrefab);
            _addBackgroundButton.onClick.AddListener(AddBackgroundButton);
            _SpawnBackgroundButton.onClick.AddListener(SpawnBackgroundPrefab);
            _DespawnBackgroundButton.onClick.AddListener(DeSpawnBackgroundPrefab);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _spawnAssetButton.onClick.RemoveAllListeners();
            _addBackgroundButton.onClick.RemoveAllListeners();
            _SpawnBackgroundButton.onClick.RemoveAllListeners();
            _DespawnBackgroundButton.onClick.RemoveAllListeners();

            DespawnPrefabs();
        }


        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetAssetBundles());
        }

        private void SpawnPrefab()
        {
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(_spawningButtonPrefab, _spawnedButtonsContainer);

            _addressablePrefabs.Add(addressablePrefab);
        }
        
        private void SpawnBackgroundPrefab()
        {
            _SpawnBackgroundButton.interactable = false;
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(_spawningBackgroundPrefab, _spawnedBackgroundTransform);

            _addressablePrefabs.Add(addressablePrefab);
        }
        
        private void DeSpawnBackgroundPrefab()
        {
            _DespawnBackgroundButton.interactable = false;
            Destroy(GameObject.FindWithTag("DestroyObject"));
            _spawningBackgroundPrefab = null;
            Addressables.Release(_spawningBackgroundPrefab);
        }

        private void DespawnPrefabs()
        {
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }

        private void AddBackgroundButton()
        {
            _addBackgroundButton.interactable = false;
            StartCoroutine(DownloadAndSetBackgroundButton());
            
            
        }
    }
}
