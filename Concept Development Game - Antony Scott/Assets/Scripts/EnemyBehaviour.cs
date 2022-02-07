using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghosts))]
public abstract class EnemyBehaviour : MonoBehaviour
{
    public Ghosts enemy { get; private set; }
    public float duration;

    private void Awake()
    {
        this.enemy = GetComponent<Ghosts>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
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
