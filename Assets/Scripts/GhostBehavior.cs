using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost))]

public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }

    public float duration;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Debug.Log("GhostBehavior enabled");
        Enable(this.duration);
    }


    public virtual void Enable(float duration)
    {
        Debug.Log("GhostBehavior virtual enabled");
        this.enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }
}