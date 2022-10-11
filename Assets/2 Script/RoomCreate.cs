using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RoomCreate : MonoBehaviour
{
    public ARRaycastManager RM; //����ĳ��Ʈ �Ŵ���
    public ARAnchorManager AM;  //��Ŀ �Ŵ���

    //����ĳ��Ʈ�� �浹�� ������Ʈ �־��
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    GameManager gm;

    private void Start()
    {
        gm = GameManager.Inst;
    }

    void Update()
    {
        //�� ȭ�� �߾�
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        //������ �÷��� �� �߾ӿ��� ���� ������ ��Ѵµ� �ε����ٸ�
        if (RM.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose; //1��°�� �ε�ģ ��ġ���� �����´�.

            gm.MagicCircle.SetActive(true); // ������ �ѱ�
            //�������� �ε�ģ ������ �̵���Ų��.
            gm.MagicCircle.transform.position = hitPose.position;

            if (Input.touchCount > 0)   //��ġ�� �ϳ� �̻� �߻��Ѵٸ�
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began) //ù ��ġ���
                {
                    var anchorPoint = AM.AddAnchor(hitPose); //��� �ڱ�

                    gm.Hole.SetActive(true);   // �� �ѱ�
                    gm.Hole.transform.position = hitPose.position; //��ġ ����ȭ
                    gm.Hole.transform.rotation = hitPose.rotation; //ȸ�� ����ȭ
                    gm.Hole.transform.parent = anchorPoint.transform; //��� �ڽ�����

                    gm.GongUI.SetActive(true);

                    gm.MagicCircle.SetActive(false); // ������ ����
                    ShowSenseVisual(false);       // �������־����¡ ����

                    //Camera.main.GetComponent<Camera>().cameraType.s

                    this.enabled = false;         // �� ���� ��ũ��Ʈ ����
                }
            }
        }
    }

    //�ð�ȭ �¿���
    public ARPlaneManager PM;
    public ARPointCloudManager PCM;
    public void ShowSenseVisual(bool isShow)
    {
        foreach (var plane in PM.trackables)
        {
            plane.gameObject.SetActive(isShow);

        }
        foreach (var point in PCM.trackables)
        {
            point.gameObject.SetActive(isShow);
        }
        PM.enabled = false;
        PCM.enabled = false;
    }
}


