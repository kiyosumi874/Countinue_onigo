using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    //�@�ړ����x
    private Vector3 velocity;
    //�@���͒l
    private Vector3 input;
    //�@����
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
        //�@�ڒn�m�F
        //CheckGround();

        //�@���͒l
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //�@�ړ����x�v�Z
        var clampedInput = Vector3.ClampMagnitude(input, 1f);
        velocity = clampedInput * walkSpeed;
        transform.LookAt(rigidBody.position + input);
        //�@�����͂���v�Z�������x���猻�݂�Rigidbody�̑��x������
        velocity = velocity - rigidBody.velocity;
        //�@���x��XZ��-walkSpeed��walkSpeed���Ɏ��߂čĐݒ�
        velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));
        //�@�ڒn���̏���
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
        //�@�ړ�����
        rigidBody.AddForce(rigidBody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);
    }
}
