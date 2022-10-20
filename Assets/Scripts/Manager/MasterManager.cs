using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance = null;

    [SerializeField] private CharacterAssets _character = null;
    [SerializeField] private TextAssets _text = null;

    public CharacterAssets Character => _character;
    public TextAssets Text => _text;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
