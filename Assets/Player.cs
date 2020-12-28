using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rb;
    public float speed , torqueSpeed;
    bool angleCheck, velocityCheck;
    [SerializeField] Transform winText, lostText;
        bool didntCrash = true;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Controllers();
        CheckLandingAngleIsOk();
    
    }


    void Controllers() {

        float Force = Input.GetAxis("Vertical");
        float torque = -Input.GetAxis("Horizontal");


        _rb.AddTorque((torque / 2) * torqueSpeed, ForceMode2D.Force);
        _rb.AddRelativeForce(new Vector2(0, Force * speed), ForceMode2D.Force);

        // Debug.Log(_rb.velocity.magnitude);
        velocityCheck = CheckVelocity();
        Debug.Log("velocityCheck : " + velocityCheck);  
        Debug.Log("_rb.velocity.magnitude : " + _rb.velocity.magnitude);  
    }

    bool CheckLandingAngleIsOk()
    {
        Vector3 direction = new Vector3(Mathf.Cos((transform.eulerAngles.z +90f)*Mathf.Deg2Rad) , Mathf.Sin((-transform.eulerAngles.z + 90f) * Mathf.Deg2Rad), 0);
  
        Debug.DrawLine(transform.position, transform.position - direction.normalized);

      
        float f= (Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg);
        float x = f - MapManager._instance.GetAnglesOfTwoPoints(transform.position);

        angleCheck = (x > 80f && x < 100f);
        Debug.Log(angleCheck);

        return angleCheck;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Landed");
        if (angleCheck && velocityCheck && didntCrash)
        {
            winText.gameObject.SetActive(true);
            _rb.simulated = false;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            didntCrash = false;
            lostText.gameObject.SetActive(true);
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    } 

    private bool CheckVelocity()
    {
        return _rb.velocity.magnitude< 2f;
     
    }
}
