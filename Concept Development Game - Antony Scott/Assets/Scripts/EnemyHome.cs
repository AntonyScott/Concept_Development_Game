using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHome : EnemyBehaviour
{
    public Transform insideTransform;
    public Transform outsideTransform;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(EnemiesExit());
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.enemy.movement.SetDirection(-this.enemy.movement.direction);
        }
    }*/

    private IEnumerator EnemiesExit()
    {
        this.enemy.movement.SetDirection(Vector2.up, true);
        this.enemy.movement.rb.isKinematic = true;
        this.enemy.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float timeElapsed = 0.0f;

        while(timeElapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.insideTransform.position, timeElapsed / duration);
            newPosition.z = position.z;
            this.enemy.transform.position = newPosition;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.insideTransform.position, this.outsideTransform.position, timeElapsed / duration);
            newPosition.z = position.z;
            this.enemy.transform.position = newPosition;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        this.enemy.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.enemy.movement.rb.isKinematic = false;
        this.enemy.movement.enabled = true;
    }
}
