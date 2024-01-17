using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private bool eventCalled;
    public UnityEvent OnStartAnimEvent;
    public UnityEvent OnActionAnimEvent;

    private FungusInfoReader infoReader;
    private void Awake()
    {
        infoReader = GetComponentInParent<FungusInfoReader>();
    }
    public void AttackEvent()
    {
        if (!eventCalled) return;

        OnActionAnimEvent?.Invoke();
        eventCalled = false;
    }
    public void StartEvent()
    {
        OnStartAnimEvent?.Invoke();
        eventCalled = true;

    }
}
