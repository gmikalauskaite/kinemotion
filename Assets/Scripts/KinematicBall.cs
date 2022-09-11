using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KinematicBall : MonoBehaviour
{
    [SerializeField]
    private Vector3 gravity;

    [SerializeField]
    private float shootVelocity;

    [SerializeField]
    private float shootAngle;

    [SerializeField]
    private float xyAngle;

    [SerializeField]
    private float zAngle;

    [SerializeField]
    private TextMeshProUGUI velText;

    [SerializeField]
    private TextMeshProUGUI posText;


    private Vector3 velocity;

    private bool shot;

    private TrailRenderer trailRender;

    private float landTime;
    private float highestTime;
    private float tim;
    private bool slowed;

    private void Awake() 
    {
            trailRender = GetComponent<TrailRenderer>();
    }


    public void Shoot(Vector3 initialVelocity) 
    {
        tim = 0;
        slowed =false;

        velocity = initialVelocity;
        landTime = (2*velocity.y) / Mathf.Abs(gravity.y);
        highestTime = velocity.y / Mathf.Abs(gravity.y);
        trailRender.Clear();
        trailRender.enabled = true;

        shot = true;
    }

    private IEnumerator SlowdownMiddle() 
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5);
        slowed = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!shot) {
            return;
        }

        velocity += gravity * Time.fixedDeltaTime;
    }

    void Update()
    {
        if (!shot) {
            return;
        }

        tim += Time.deltaTime;

        transform.Translate(velocity * Time.deltaTime, Space.World);
        velText.text = string.Format("(x={0:0.00}\ny={1:0.00}\nz={2:0.00}", velocity.x, velocity.z,  velocity.y);
        posText.text = string.Format("(x={0:0.00}\ny={1:0.00}\nz={2:0.00}", transform.position.x, transform.position.z,  transform.position.y);
        if (!slowed && tim >= highestTime) 
        {
            StartCoroutine(SlowdownMiddle());
        }

        if (tim >= landTime) {
            shot = false;
        }
    }
}
