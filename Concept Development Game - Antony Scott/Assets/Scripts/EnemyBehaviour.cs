using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghosts))]
public abstract class EnemyBehaviour : MonoBehaviour
{
    public Ghosts enemy;
    public float duration;

    private void Awake()
    {
        enemy = GetComponent<Ghosts>();
        enabled = false;
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke();
    }
}
