using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCtrl : MonoBehaviour
{
    GameManager gm;
    bool isPick;

    Vector3 beganPos;
    Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Inst;
        isPick = false;
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);//광선을 만들어서
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (hit.collider.CompareTag("GONG"))
                {
                    isPick = true;
                    beganPos = hit.collider.transform.position;
                }
                if (hit.collider.CompareTag("LEFT"))
                {
                    int idx = gm.selectGongIdx;
                    idx--;
                    if (idx < 0) idx = 4;
                    gm.SelectGong.GetComponent<MeshRenderer>().material = gm.GongMats[idx];
                    //gm.SelectGong2.GetComponent<MeshRenderer>().material = gm.GongMats[idx];
                    gm.selectGongIdx = idx;
                }
                if (hit.collider.CompareTag("RIGHT"))
                {
                    int idx = gm.selectGongIdx;
                    idx++;
                    if (idx > 4) idx = 0;
                    gm.SelectGong.GetComponent<MeshRenderer>().material = gm.GongMats[idx];
                    //gm.SelectGong2.GetComponent<MeshRenderer>().material = gm.GongMats[idx];
                    gm.selectGongIdx = idx;
                }
            }

        }

        //
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (isPick)
            {
                endPos = Input.GetTouch(0).position;
                GameObject t = Instantiate(gm.CloneGong);
                t.GetComponent<MeshRenderer>().material = gm.GongMats[GameManager.Inst.selectGongIdx];
                t.transform.position = beganPos;
                Vector3 dir = gm.Hole.transform.position - beganPos;
                dir.Normalize();
                dir += new Vector3(0, 0.7f, 0);
                float dis = Vector3.Distance(endPos, beganPos) / 500f;
                t.GetComponent<Rigidbody>().AddForce(dir * dis, ForceMode.Impulse);

                isPick = false;
            }
        }
    }
}
