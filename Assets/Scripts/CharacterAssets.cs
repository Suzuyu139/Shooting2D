using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterAssets : ScriptableObject
{
    [System.Serializable]
    public class CharacterParameter
    {
        [SerializeField] private int _characterId = 0;
        [SerializeField] private string _name = "";
        [SerializeField] private GameObject _characterObject = null;
        [SerializeField] private BulletAsset[] _bulletAssets;

        public int CharacterId => _characterId;
        public string Name => _name;
        public GameObject CharacterObject => _characterObject;
        public BulletAsset[] BulletAssets => _bulletAssets;
    }

    [SerializeField] private CharacterParameter _playerParameter;

    public CharacterParameter PlayerParameter => _playerParameter;
}
