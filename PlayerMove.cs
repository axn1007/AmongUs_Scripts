using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    struct SyncData
    {
        public Vector3 pos;
        public Quaternion rotation;
    }

    Vector3 setPos;
    Quaternion setRot;

    SyncData[] syncData;

    public GameObject myModel;
    public GameObject otherModel;
    public GameObject UIHelper;
    public GameObject playerCam;
    public GameObject MyStatus;
    public GameObject Speaker;

    public GameObject pos;
    public GameObject doc;
    public GameObject middlePos;
    public GameObject startPos;

    public Transform[] myBody;
    public Transform[] otherBody;

    public Animator myAni;
    public Animator otherAni;

    public Player myPlayer;

    //등장한 플레이어들 담을 배열
    public int[] Players;

    void Start()
    {
        pos = GameObject.Find("Mission/MoveMission/Pos");
        doc = GameObject.Find("Mission/MoveMission/Document");
        middlePos = GameObject.Find("Mission/MoveMission/MiddlePos");
        startPos = GameObject.Find("Mission/MoveMission/FirstPos");

        if (photonView.IsMine == false)
        {
            syncData = new SyncData[myBody.Length];
            anim = otherAni;
        }
        else
        {
            anim = myAni;
        }

        myModel.SetActive(photonView.IsMine);
        UIHelper.SetActive(photonView.IsMine);
        playerCam.SetActive(photonView.IsMine);
        otherModel.SetActive(!photonView.IsMine);
        MyStatus.SetActive(photonView.IsMine);
        Speaker.SetActive(photonView.IsMine);

        //등장 소리
        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_1);

        //게임 시작 전에는 Idle 상태
        state = PlayerState.Idle;
    }

    void Update()
    {
        if (photonView.IsMine == false)
        {
            for (int i = 0; i < otherBody.Length; i++)
            {
                otherBody[i].position = Vector3.Lerp(otherBody[i].position, syncData[i].pos, 0.2f);
                otherBody[i].rotation = Quaternion.Lerp(otherBody[i].rotation, syncData[i].rotation, 0.2f);
            }
        }

        MoveCtrl();
        RotCtrl();

        //상태에 따라 정해진 기능 수행
        switch (state)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Walk:
                Walk();
                break;
            default:
                break;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            for (int i = 0; i < myBody.Length; i++)
            {
                stream.SendNext(myBody[i].position);
                stream.SendNext(myBody[i].rotation);
            }
        }

        if (stream.IsReading)
        {
            setPos = (Vector3)stream.ReceiveNext();
            setRot = (Quaternion)stream.ReceiveNext();

            if (syncData != null)
            {
                for (int i = 0; i < otherBody.Length; i++)
                {
                    syncData[i].pos = (Vector3)stream.ReceiveNext();
                    syncData[i].rotation = (Quaternion)stream.ReceiveNext();
                }
            }
        }
    }


    enum PlayerState
    {
        Idle,
        Walk
    }

    PlayerState state;

    //애니메이터
    public Animator anim;
    float v;

    //속력
    public float moveSpeed = 5.0f;
    public float rotSpeed = 3.0f;

    public int rotSpe = 60;

    //Knife 공장
    public GameObject knifeFactory;
    public float pressTime = 5.0f;

    float currTime;
    //float attackTime = 1.5f;

    public Transform catchedObj;

    void Idle()
    {
        //만약 게임플레이 시작하면
        if (v != 0)
        {
            //Walk상태로 전이
            state = PlayerState.Walk;
            //Walk애니로 변경
            //anim.SetTrigger("Walk");
            photonView.RPC("AniTrigger", RpcTarget.All, "Walk");
        }
    }

    void Walk()
    {
        if (v == 0)
        {
            //Walk상태로 전이
            state = PlayerState.Idle;
            //Walk애니로 변경
            //anim.SetTrigger("Idle");
            photonView.RPC("AniTrigger", RpcTarget.All, "Idle");
        }
    }

/*
        //만약 공격버튼(스페이스)을 누르면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (photonView.IsMine)
            {
                //Attack 상태로 전이
                state = PlayerState.Attack;
                //Attack 애니로 변경
                //anim.SetTrigger("Attack");
                photonView.RPC("AniTrigger", RpcTarget.All, "Attack");
                isAttack = true;
            }
        }
        
    }

    
    void Attack()
    {
        Invoke("AttackKnife", 0.5f);

        currTime += Time.deltaTime;
        if (currTime > attackTime)
        {
            state = PlayerState.Idle;
            //anim.SetTrigger("Idle");
            photonView.RPC("AniTrigger", RpcTarget.All, "Idle");
            currTime = 0;
        }
    }

    void Die()
    {
        //Ghost 상태로 전이
        state = PlayerState.Ghost;
        //Ghost 애니로 변경
        //anim.SetTrigger("Ghost");
        photonView.RPC("AniTrigger", RpcTarget.All, "Ghost");
    }

    void Ghost()
    {
        MoveCtrl();
    }
*/

    [PunRPC]
    void AniTrigger(string state)
    {
        anim.SetTrigger(state);
    }

    void MoveCtrl()
    {
        if (photonView.IsMine)
        {
            v = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(0, 0, v);
            dir.Normalize();

            //밑에랑 같은거 아래를 보거나 위를 봐도 땅을 걷는 느낌 transform.Translate(dir * moveSpeed * Time.deltaTime);
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            transform.position += dir * moveSpeed * Time.deltaTime;
            //GetComponent<CharacterController>().Move(dir * moveSpeed * Time.deltaTime);

            Vector2 joyStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

            transform.Rotate(0, joyStick.x * rotSpe * Time.deltaTime, 0);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, setPos, Time.deltaTime * 20.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, setRot, Time.deltaTime * 20.0f);
        }
    }

    void RotCtrl()  //마우스 Up, Down 각도 제한 범위 설정 해줘야함 360도 돌아가니까   Mathf
    {
        if (photonView.IsMine)
        {
            float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
            float rotY = Input.GetAxis("Mouse X") * rotSpeed;

            rotX = Mathf.Clamp(rotX, -90, 90);

            transform.localRotation *= Quaternion.Euler(0, rotY, 0);
            Camera.main.transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);
        }
    }

    [PunRPC]
    void AttackKnife()
    {
        GameObject knife = Instantiate(knifeFactory);

        if (photonView.IsMine)
        {
            knife.transform.SetParent(myBody[1]);
        }
        else
        {
            knife.transform.SetParent(otherBody[1]);
        }

        knife.transform.localPosition = Vector3.zero;
        knife.transform.rotation = myBody[1].rotation * Quaternion.Euler(-35,0,35);
    }

    [PunRPC]
    void DestroyKnife()
    {
        if (photonView.IsMine)
        {
            Destroy(myBody[1].GetChild(4).gameObject);
        }
        else
        {
            Destroy(otherBody[1].GetChild(0).gameObject);
        }
    }

    [PunRPC]
    void RpcGrabObj()
    {
        Transform rHand = otherBody[0];
        if (photonView.IsMine) rHand = myBody[0];

        Ray ray = new Ray(rHand.transform.position, rHand.transform.forward);
        RaycastHit hit;
         
        if (Physics.SphereCast(ray, 0.5f, out hit, 100))
        {
            if (hit.transform.tag != "Bottle") return;

            catchedObj = hit.transform;

            hit.transform.SetParent(rHand.transform);
            SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Grab);
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            hit.transform.localPosition = Vector3.zero;
            //hit.transform.position = rHand.transform.position;
        }
    }


    [PunRPC]
    void RpcDropObj()
    {
        catchedObj.SetParent(null);
        catchedObj.GetComponent<Rigidbody>().isKinematic = false;

        if (photonView.IsMine)
        {
            photonView.RPC("RpcThrowObj", RpcTarget.All, transform.TransformDirection(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch)) * 3,
                transform.TransformDirection(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch)));
        }

    }


    [PunRPC]
    void RpcThrowObj(Vector3 velocity, Vector3 angleVelocity)
    {
        Rigidbody rb = catchedObj.GetComponent<Rigidbody>();
        rb.velocity = velocity;
        rb.angularVelocity = angleVelocity;

        catchedObj = null;
    }

    [PunRPC]
    void RpcCatchObj()
    {
        Transform rHand = otherBody[0];
        if (photonView.IsMine) rHand = myBody[0];

        Ray ray = new Ray(rHand.transform.position, rHand.transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.5f, out hit, 100))
        {
            if (hit.transform.tag == "Weed")
            {
                if (Player.instance.mission[9] != true) return;

                catchedObj = hit.transform;
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Grab);
                hit.transform.SetParent(rHand.transform);
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = true;
            }
        }
    }

    [PunRPC]
    void RpcButtonOne()
    {
        Transform rHand = otherBody[0];
        if (photonView.IsMine) rHand = myBody[0];

        Ray ray = new Ray(rHand.transform.position, rHand.transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.1f, out hit, 100))
        {
            if (hit.transform.tag == "energy")
            {
                //Material mt = hit.transform.GetComponent<MeshRenderer>().material;
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Ming);
                Transform mt = hit.transform.GetChild(0);
                mt.GetChild(0).gameObject.SetActive(false);
                mt.GetChild(1).gameObject.SetActive(true);

                if (myPlayer.mission[2] != true) return;

                myPlayer.EnergyMission(1);
            }

            if (hit.transform.tag == "button")
            {
                currTime += Time.deltaTime;
                string objectName = hit.collider.gameObject.name;

                if (currTime > pressTime)
                {
                    if (Player.instance.mission[4] != true) return;


                    print(objectName + " Mission Complete!");
                    myPlayer.ButtonMission(1);
                    currTime = 0;
                    //Destroy(hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
                }
            }
            

            if (Physics.Raycast(ray, out hit, LayerMask.NameToLayer("Btn")))
            {
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Ming);

                if (Player.instance.mission[8] != true) return;

                currTime += Time.deltaTime;
                doc.transform.position = GetPoint(startPos.transform.position, middlePos.transform.position, pos.transform.position, currTime / 3);
            }

            Vector3 GetPoint(Vector3 s, Vector3 m, Vector3 e, float ratio)
            {
                Vector3 p1 = Vector3.Lerp(s, m, ratio);
                Vector3 p2 = Vector3.Lerp(m, e, ratio);
                Vector3 p3 = Vector3.Lerp(p1, p2, ratio);
                return p3;
            }
        }
    }
    
}
