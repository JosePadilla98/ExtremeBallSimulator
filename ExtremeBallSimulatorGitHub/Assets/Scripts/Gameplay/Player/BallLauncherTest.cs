using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncherTest : MonoBehaviour {

    //ESTE SCRIPT ES SOLO UNA PRUEBA PARA VER QUE TAL REACCIONA EL ESCENARIO
     
    public GameObject ballReference;
    public float impulse = 100f;
    private Rigidbody _ballRigidbody;
	// Use this for initialization
	void Start ()
    {
        if (ballReference != null)
        {
            _ballRigidbody = ballReference.GetComponent<Rigidbody>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResteBall();
        }
	}

    public void ResteBall()
    {
        if (ballReference)
        {
            ballReference.transform.parent = this.transform;
            _ballRigidbody.isKinematic = true;
            ballReference.transform.localPosition = Vector3.zero;
            ballReference.transform.localRotation = Quaternion.identity;
        }
    }

    public void LaunchBall()
    {
        if (ballReference != null)
        {
            ballReference.transform.parent = null;
            _ballRigidbody.isKinematic = false;
            _ballRigidbody.AddRelativeForce(0, -impulse, 0, ForceMode.Impulse);
        }
    }
}
