using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterData
{
    public class Bullet
    {
        public int Id { get; private set; } = 0;
        public string Address { get; private set; } = string.Empty;

        public Bullet(int id, string address)
        {
            Id = id;
            Address = address;
        }
    }
}