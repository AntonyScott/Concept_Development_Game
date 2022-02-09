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
        if (gameObject.activeSelf)
        {
            StartCoroutine(EnemiesExit());
        }
    }

    private IEnumerator EnemiesExit()
    {
        enemy.movement.SetDirection(Vector2.up, true);
        enemy.movement.rb.isKinematic = true;
        enemy.movement.enabled = false;

        Vector3 position = transform.position;
        float duration = 0.5f;
        float timeElapsed = 0.0f;

        while(timeElapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, insideTransform.position, timeElapsed / duration);
            newPosition.z = position.z;
            enemy.transform.position = newPosition;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(insideTransform.position, outsideTransform.position, timeElapsed / duration);
            newPosition.z = position.z;
            this.enemy.transform.position = newPosition;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        enemy.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        enemy.movement.rb.isKinematic = false;
        enemy.movement.enabled = true;
    }
}
