using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour
{
    public Transform ball_;

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
}

