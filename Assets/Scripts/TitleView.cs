using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class TitleView : MonoBehaviour
{
    [SerializeField] private Button _startButton = null;

    public IObservable<Unit> OnObservableStartButton => _startButton.OnClickAsObservable();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
