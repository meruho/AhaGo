using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RoomCreate : MonoBehaviour
{
    public ARRaycastManager RM; //레이캐스트 매니저
    public ARAnchorManager AM;  //앵커 매니저

    //레이캐스트에 충돌된 오브젝트 넣어둠
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    GameManager gm;

    private void Start()
    {
        gm = GameManager.Inst;
    }

    void Update()
    {
        //폰 화면 중앙
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        //감지된 플랜에 폰 중앙에서 부터 광선을 쏘앗는데 부딛혔다면
        if (RM.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose; //1번째로 부딛친 위치값을 가져온다.

            gm.MagicCircle.SetActive(true); // 마법진 켜기
            //마법진을 부딛친 곧으로 이동시킨다.
            gm.MagicCircle.transform.position = hitPose.position;

            if (Input.touchCount > 0)   //터치가 하나 이상 발생한다면
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began) //첫 터치라면
                {
                    var anchorPoint = AM.AddAnchor(hitPose); //깃발 박기

                    gm.Hole.SetActive(true);   // 방 켜기
                    gm.Hole.transform.position = hitPose.position; //위치 동기화
                    gm.Hole.transform.rotation = hitPose.rotation; //회전 동기화
                    gm.Hole.transform.parent = anchorPoint.transform; //깃발 자식으로

                    gm.GongUI.SetActive(true);

                    gm.MagicCircle.SetActive(false); // 마법진 끄기
                    ShowSenseVisual(false);       // 감지비주얼라이징 끄기

                    //Camera.main.GetComponent<Camera>().cameraType.s

                    this.enabled = false;         // 방 생성 스크립트 끄기
                }
            }
        }
    }

    //시각화 온오프
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


