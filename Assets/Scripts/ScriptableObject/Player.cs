using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CreatePlayer")]
public class Player : ScriptableObject
{
    [System.Serializable]
    public class PlayerParameter
    {
        [SerializeField] int _id = 0;
        [SerializeField] string _address = string.Empty;

        public int Id => _id;
        public string Address => _address;
    }

    [SerializeField] List<PlayerParameter> _players;
    public List<PlayerParameter> Players => _players;
}
