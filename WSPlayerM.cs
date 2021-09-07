using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WSPlayerM : MonoBehaviour
{
    //enum PlayerState
    //{
    //    Idle,
    //    Walk,
    //    Attack,
    //    Die,
    //    Ghost
    //}

    //PlayerState state;

    ////애니메이터
    //Animator anim;

    float h;
    float v;

    //속력
    public float moveSpeed = 5.0f;
    public float rotSpeed = 3.0f;

    public Camera playerCam;

    ////Knife 공장
    //public GameObject knifeFactory;
    ////발사위치
    //public Transform firePos;
    ////던지는 힘
    //public float throwPower =100;

    //bool isAttack;

    //float currTime;
    //float attackTime = 1.5f;

    //void Start()
    //{

    //    //게임 시작 전에는 Idle 상태
    //    state = PlayerState.Idle;

    //    //Animator 셋팅
    //    anim = GetComponentInChildren<Animator>();
    //}

    void Update()
    {
        MoveCtrl();
        RotCtrl();

        //    //상태에 따라 정해진 기능 수행
        //    switch (state)
        //    {
        //        case PlayerState.Idle:
        //            Idle();
        //            break;
        //        case PlayerState.Walk:
        //            Walk();
        //            break;
        //        case PlayerState.Attack:
        //            Attack();
        //            break;
        //        case PlayerState.Die:
        //            Die();
        //            break;
        //        case PlayerState.Ghost:
        //            Ghost();
        //            break;
        //        default:
        //            break;
        //    }

    }

        //void Idle()
        //{

        //    //만약 게임플레이 시작하면
        //    if(h != 0 || v != 0)
        //    {
        //        //Walk상태로 전이
        //        state = PlayerState.Walk;
        //        print("상태 전환 : Idle -> Walk");
        //        //Walk애니로 변경
        //        anim.SetTrigger("Walk");
        //    }
        //    //만약 공격버튼(스페이스)을 누르면
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        //Attack 상태로 전이
        //        state = PlayerState.Attack;
        //        print("상태 전환 : Idle -> Attack");
        //        //Attack 애니로 변경
        //        anim.SetTrigger("Attack");
        //        isAttack = true;
        //    }
        //    //만약 칼에 닿으면

        //    {
        //        //Die 상태로 전이
        //        //Die 애니로 변경
        //    }
        //}

        //void Walk()
        //{
        //    if (h == 0 && v == 0)
        //    {
        //        //Walk상태로 전이
        //        state = PlayerState.Idle;
        //        print("상태 전환 : Walk -> Idle");
        //        //Walk애니로 변경
        //        anim.SetTrigger("Idle");
        //    }

        //    //만약 공격버튼(스페이스)을 누르면
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {

        //        //Attack 상태로 전이
        //        state = PlayerState.Attack;
        //        print("상태 전환 : Walk -> Attack");
        //        //Attack 애니로 변경
        //        anim.SetTrigger("Attack");
        //        isAttack = true;
        //    }
        //}

        //void Attack()
        //{
        //    Invoke("AttackKnife", 0.5f);

        //    currTime += Time.deltaTime;
        //    if (currTime > attackTime)
        //    {
        //        state = PlayerState.Idle;
        //        anim.SetTrigger("Idle");
        //        print("상태 전환 : Attack -> Idle");

        //        currTime = 0;
        //    }
        //}

        //void AttackKnife()
        //{
        //    if (isAttack == true)
        //    {
        //        //칼 공장에서 칼 생성해서 발사
        //        GameObject knife = Instantiate(knifeFactory);
        //        ////만들어진 칼을 발사위치에 위치시키고
        //        knife.transform.position = firePos.position;
        //        knife.transform.forward = new Vector3(firePos.forward.x, 0, firePos.forward.z);
        //        //(중력적으로 떨어지게 하려고)rigidbody 가져오자
        //        Rigidbody rb = knife.GetComponent<Rigidbody>();
        //        //카메라가 바라보는 방향으로 물리적인 힘을 가한다
        //        rb.AddForce(Camera.main.transform.forward * throwPower);
        //    }

        //    isAttack = false;
        //}

        //void Die()
        //{
        //    //Ghost 상태로 전이
        //    state = PlayerState.Ghost;
        //    print("상태 전환 : Die -> Ghost");
        //    //Ghost 애니로 변경
        //    anim.SetTrigger("Ghost");
        //}

        //void Ghost()
        //{
        //    MoveCtrl();
        //}

        public void MoveCtrl()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        //밑에랑 같은거 아래를 보거나 위를 봐도 땅을 걷는 느낌 transform.Translate(dir * moveSpeed * Time.deltaTime);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void RotCtrl()  //마우스 Up, Down 각도 제한 범위 설정 해줘야함 360도 돌아가니까   Mathf
    {
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;

        rotX = Mathf.Clamp(rotX, -90, 90);

        transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        playerCam.transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.name == "Knife")
    //    {
    //        Destroy(gameObject, 1);
    //        state = PlayerState.Die;
    //    }
    //}
}
