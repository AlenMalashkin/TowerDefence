using System;
using UnityEngine;

public class TriggerObserver : MonoBehaviour
{
    public event Action<Collider> OnTriggerEnteredEvent;
    public event Action<Collider> OnTriggerExitedEvent;
    
    private void OnTriggerEnter(Collider other) =>
        OnTriggerEnteredEvent?.Invoke(other);
    

    private void OnTriggerExit(Collider other) =>
        OnTriggerExitedEvent?.Invoke(other);
}
