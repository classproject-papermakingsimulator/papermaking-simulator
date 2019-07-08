using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    private Animator animator;
    private Transform transform;
    private Vector3 te;
    private Quaternion qu;
    private Vector3 qe;
    public float speed;
    private float rspeed;
    public float f;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        te = transform.position;
        qu = transform.rotation;
        animator = GetComponent<Animator>();
        if (animator.layerCount >= 2)
            animator.SetLayerWeight(1, 1);
        controller = GetComponent<CharacterController>();
        rspeed = 2 * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator == null)
            return;
        AnimatorStateInfo stataInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKey(KeyCode.LeftShift) && !animator.GetBool("isMoveS"))
        {
            animator.SetBool("isRun", true);
        }
        else
            animator.SetBool("isRun", false);
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("isMove", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("isMove", false);
        }
        if (animator.GetBool("isMove"))
        {
            qu = transform.rotation;
            qe = qu.eulerAngles;
            float k = qe.y;
            if (animator.GetBool("isRun"))
            {
                controller.Move(Vector3.forward * rspeed * Mathf.Cos(k * Mathf.Deg2Rad));
                controller.Move(Vector3.right * rspeed * Mathf.Sin(k * Mathf.Deg2Rad));
            }
            else
            {
                controller.Move(Vector3.forward * speed * Mathf.Cos(k * Mathf.Deg2Rad));
                controller.Move(Vector3.right * speed * Mathf.Sin(k * Mathf.Deg2Rad));
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("isMoveS", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isMoveS", false);
        }
        if (animator.GetBool("isMoveS"))
        {
            qu = transform.rotation;
            qe = qu.eulerAngles;
            float k = qe.y;
            controller.Move(Vector3.back * speed * Mathf.Cos(k * Mathf.Deg2Rad));
            controller.Move(Vector3.left * speed * Mathf.Sin(k * Mathf.Deg2Rad));
        }
        if (Input.GetKey(KeyCode.D))
        {
            qu = transform.rotation;
            qe = qu.eulerAngles;
            qe.y += (float)0.001 * f;
            qu = Quaternion.Euler(qe);
            GetComponent<Transform>().rotation = qu;
        }
        if (Input.GetKey(KeyCode.A))
        {
            qu = transform.rotation;
            qe = qu.eulerAngles;
            qe.y -= (float)0.001 * f;
            qu = Quaternion.Euler(qe);
            GetComponent<Transform>().rotation = qu;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartGame");
        }
        //if (Input.GetButtonDown("Vertical"))
        //{
        //    animator.SetBool("isMove", true);
        //}
        //if (Input.GetButtonUp("Vertical"))
        //{
        //    animator.SetBool("isMove", false);
        //}
        if (!controller.isGrounded)
            controller.Move(Vector3.down * speed);
    }

    void FixedUpdate()
    {
    }
}

