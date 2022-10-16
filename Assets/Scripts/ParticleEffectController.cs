using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectController : MonoBehaviour
{
    [SerializeField] bool _isParentRotationInvalid = false;

    private Transform _transform;
    private CharacterControllerBase _characterControllerParent;

    // Start is called before the first frame update
    void Start()
    {
        _transform = this.transform;
        _characterControllerParent = this.transform.GetComponentInParent<CharacterControllerBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isParentRotationInvalid)
        {
            _transform.rotation = Quaternion.identity;
        }

        if(_characterControllerParent)
        {
            if(_characterControllerParent.IsDeath)
            {
                Destroy(gameObject);
            }
        }
    }
}
