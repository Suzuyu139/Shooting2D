using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CreateBullet")]
public class Bullet : ScriptableObject
{
    [System.Serializable]
    public class BulletParameter
    {
        [SerializeField] int _id = 0;
        [SerializeField] string _address = string.Empty;

        public int Id => _id;
        public string Address => _address;
    }

    [SerializeField] List<BulletParameter> _bullets;
    public List<BulletParameter> Bullets => _bullets;
}
