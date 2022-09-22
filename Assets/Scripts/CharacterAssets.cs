using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterAssets : ScriptableObject
{
    [System.Serializable]
    public class CharacterParameter
    {
        [SerializeField] private GameObject _characterObject;

        public GameObject CharacterObject => _characterObject;
    }

    [SerializeField] private CharacterParameter _playerParameter;

    public CharacterParameter PlayerParameter => _playerParameter;
}
