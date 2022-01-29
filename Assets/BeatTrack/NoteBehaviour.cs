using UnityEngine;
using UnityEngine.Events;
using Utils;

public class NoteBehaviour : MonoBehaviour
{
    public bool CanBePressed { get; private set; }
    NoteBehaviourUnityEvent onOutOfSight;

    public void SetOnOutOfSight(NoteBehaviourUnityEvent onOutOfSight) => this.onOutOfSight = onOutOfSight;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Activator"))
            CanBePressed = true;

        if(other.CompareTag("OutOfSight"))
            onOutOfSight.Invoke(this);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator")
            CanBePressed = false;
    }
}
