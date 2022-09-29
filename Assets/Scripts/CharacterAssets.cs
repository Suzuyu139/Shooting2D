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
        [SerializeField] private int _normalBulletId1 = 0;
        [SerializeField] private int _normalBulletId2 = 0;
        [SerializeField] private int _normalBulletId3 = 0;
        [SerializeField] private int _specialBulletId1 = 0;
        [SerializeField] private int _specialBulletId2 = 0;
        [SerializeField] private int _specialBulletId3 = 0;
        [SerializeField] private List<BulletControllerBase> _bulletControllers;

        public int CharacterId => _characterId;
        public string Name => _name;
        public GameObject CharacterObject => _characterObject;
        public int NormalBulletId1 => _normalBulletId1;
        public int NormalBulletId2 => _normalBulletId2;
        public int NormalBulletId3 => _normalBulletId3;
        public int SpecialBulletId1 => _specialBulletId1;
        public int SpecialBulletId2 => _specialBulletId2;
        public int SpecialBulletId3 => _specialBulletId3;
        public List<BulletControllerBase> BulletControllers => _bulletControllers;
    }

    [SerializeField] private List<CharacterParameter> _playerParameters;
    [SerializeField] private List<CharacterParameter> _enemyParameters;

    public List<CharacterParameter> PlayerParameters => _playerParameters;
    public List<CharacterParameter> EnemyParameters => _enemyParameters;
}
