using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour
{
    public Transform ball_;
    private bool _runningSequence;
    // Use this for initialization
    private IEnumerator Start()
    {
        while (true)
        {
            Transform newBall = Instantiate(ball_, transform.position, Quaternion.identity) as Transform;
            newBall.rigidbody.AddForce(new Vector3(Random.value * 2000 - 1000, 2000, Random.value * 2000 - 1000));
            yield return new WaitForSeconds(1 + Random.value * 2);
        }
    }

    private void OnGUI()
    {
        if (_runningSequence)
            return;

        if (GUILayout.Button("Retrieve the balls"))
        {
            StartCoroutine(CutSequence());
        }
    }

    private IEnumerator MoveObject(Transform objectToMove, Vector3 position, float time)
    {
        float t = 0f;
        Vector3 originalPosition = objectToMove.position;
        float lastTime = Time.realtimeSinceStartup;

        while (t < 1)
        {
            objectToMove.position = Vector3.Lerp(originalPosition, position, t);
            t += (Time.realtimeSinceStartup - lastTime) / time;
            yield return null;
        }
    }

    private IEnumerator CutSequence()
    {
        _runningSequence = true;
        Time.timeScale = 0;

        Vector3 originalPoisiton = Camera.main.transform.position;

        foreach (BallScript ball in BallScript.allBalls.ToArray())
        {
            Vector3 targetPosition = ball.transform.position - (Vector3.forward * 3);
            yield return StartCoroutine(MoveObject(Camera.main.transform, targetPosition, 2));
            yield return WaitForRealSeconds(0.5f);
        }

        yield return StartCoroutine(MoveObject(Camera.main.transform, originalPoisiton, 2));
        yield return WaitForRealSeconds(2.0f);

        Time.timeScale = 1;
        _runningSequence = false;
    }

    private Coroutine WaitForRealSeconds(float time)
    {
        return StartCoroutine(Wait(time));
    }

    private IEnumerator Wait(float time)
    {
        float current = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup - current < time)
        {
            yield return null;
        }
    }
}


