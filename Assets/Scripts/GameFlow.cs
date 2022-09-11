using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameFlow : MonoBehaviour
{
    [SerializeField]
    private KinematicBall ball;

    [SerializeField]
    private TMP_InputField xyText;

    [SerializeField]
    private TMP_InputField zText;

    [SerializeField]
    private TMP_InputField velocityText;


    public void Shoot() 
    {
        string xyRaw = xyText.text;
        string velRaw = velocityText.text;
        string zRaw = zText.text;
       
        Debug.Log(xyRaw);
        float xy = float.Parse(xyRaw) * Mathf.Deg2Rad;
        float zu = float.Parse(zRaw) * Mathf.Deg2Rad;
        float vel = float.Parse(velRaw);

        float x = Mathf.Sin(xy) * Mathf.Cos(zu);
        float y = Mathf.Cos(xy) * Mathf.Cos(zu);
        float z = Mathf.Sin(zu);

        var v = new Vector3(x, z, y) * vel;
        Debug.Log(v);
        ball.Shoot(v);
    }
}
