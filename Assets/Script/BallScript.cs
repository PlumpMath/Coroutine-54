using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{
    public static List<BallScript> allBalls = new List<BallScript>();
	
    private void Awake()
    {
        allBalls.Add(this);
    }
    
    private void OnDestroy()
    {
        allBalls.Remove(this);
    }

	// Update is called once per frame
	private void Update ()
    {
        if (transform.position.y < -5)
        Destroy(gameObject);
	}
}
