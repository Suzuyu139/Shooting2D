using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BulletModel : MonoBehaviour
{
    [SerializeField] int _id = 0;
    public int Id => _id;
    public void SetId(int id) => _id = id;

    public bool IsSetupInitialized { get; private set; } = false;
    public void SetIsSetupInitialized(bool isSetupInitialized) => IsSetupInitialized = isSetupInitialized;

    BoolReactiveProperty _isShoot = new(false);
    public IReadOnlyReactiveProperty<bool> IsShoot => _isShoot;
    public void SetIsShoot(bool isShoot) => _isShoot.Value = isShoot;

    [SerializeField] float _forceSpeed = 0.0f;
    public float ForceSpeed => _forceSpeed;
    public void SetForceSpeed(float speed) => _forceSpeed = speed;

    public Vector2 ForceDirection { get; private set; } = Vector2.zero;
    public void SetForceDirection(Vector2 forceDirection) => ForceDirection = forceDirection;

    [SerializeField] float _disappearTime = 0.0f;
    public float DisappearTime => _disappearTime;
    public void SetDisappearTime(float time) => _disappearTime = time;
}
