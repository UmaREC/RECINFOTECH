using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;

    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
        Debug.Log("Exit Transition Coroutine Stops");
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
            Debug.Log("Exit Transition Coroutine Starts");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Revere direction everytime   the ghost hits the wall to create the effect of the ghost bouncing around the home

        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log($"$$GHOSTHOME---> OncollisionEnter2D{gameObject.name}");
            Debug.Log("Ghost Collision occurs with Obstacle Layer");
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
            Debug.Log("SetDirection");
        }
    }

    private IEnumerator ExitTransition()
    {
        Debug.Log($"$$GHOST -----> ExitTransition{gameObject.name}");
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.5f;

        //Animate to the starting point

        while (elapsed < duration)
        {
            Debug.Log("Elapsed Less Than Duration where elapsed Equals 0.5");
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Animate exiting the ghost home

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Debug.Log("Elapsed Less Than Duration where elapsed Equals 0.0");

            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            Debug.Log($"{elapsed += Time.deltaTime}{Time.deltaTime}");

            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        //Pick a random direction left or right and re-enable movement

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        Debug.Log("Taking random direction");
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
    }

}

