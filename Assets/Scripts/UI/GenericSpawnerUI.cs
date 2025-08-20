using Assets.Scripts.Spawners;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GenericSpawnerUI<T> : MonoBehaviour where T : FallingObject
    {
        [SerializeField] private GenericSpawner<T> _spawner;

        [SerializeField] private RawImage _icon;

        [SerializeField] private TMP_Text _spawnsCount;
        [SerializeField] private TMP_Text _activateCount;
        [SerializeField] private TMP_Text _instantiateCount;

        private void Update()
        {
            _spawnsCount.text = "СПАВНОВ:" + _spawner.SpawnCount.ToString();
            _activateCount.text = "АКТИВНО:" + _spawner.ActiveCount.ToString();
            _instantiateCount.text = "СОЗДАНО:" + _spawner.InstantiateCount.ToString();
        }
    }
}