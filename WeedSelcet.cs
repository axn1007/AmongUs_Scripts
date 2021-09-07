using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedSelcet : MonoBehaviour
{
    Transform weed;
    LineRenderer lr;
    public Transform[] wd;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawGuideLine();
        WeedCatch();
        //WeedDrop();
    }

    void DrawGuideLine()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
        }
    }

    void WeedCatch()
    {
        if(Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if(Physics.SphereCast(ray, 0.5f , out hit, 10f, LayerMask.NameToLayer("Weed")))
            {
                weed = hit.transform;
                hit.transform.SetParent(transform);
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Ming);
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.isKinematic = true;
            }
        }
    }

    void WeedDrop()
    {
        if (weed = null) return;

        if(Input.GetMouseButtonUp(0) || OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Ming);
            weed.SetParent(null);
            weed.GetComponent<Rigidbody>().isKinematic = false;
            weed = null;

        }
    }
}
