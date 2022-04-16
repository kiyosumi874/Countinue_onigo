using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    //　移動速度
    private Vector3 velocity;
    //　入力値
    private Vector3 input;
    //　速さ
    [SerializeField]
    private float walkSpeed = 4f;
    private void OnTriggerEnter(Collider other)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        //　接地確認
        //CheckGround();

        //　入力値
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //　移動速度計算
        var clampedInput = Vector3.ClampMagnitude(input, 1f);
        velocity = clampedInput * walkSpeed;
        transform.LookAt(rigidBody.position + input);
        //　今入力から計算した速度から現在のRigidbodyの速度を引く
        velocity = velocity - rigidBody.velocity;
        //　速度のXZを-walkSpeedとwalkSpeed内に収めて再設定
        velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));
        //　接地時の処理
        //if (isGrounded)
        //{
        //    if (clampedInput.magnitude > 0f)
        //    {
        //        animator.SetFloat("Speed", clampedInput.magnitude);
        //    }
        //    else
        //    {
        //        animator.SetFloat("Speed", 0f);
        //    }
        //}
        //　移動処理
        rigidBody.AddForce(rigidBody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);
    }
}
