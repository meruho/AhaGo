using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("RoomCreate")]
    public GameObject MagicCircle;
    public GameObject GongUI;
    public GameObject Hole;

    [Header("TouchCtrl")]
    public GameObject SelectGong;
    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject CloneGong;
    public int selectGongIdx;
    public Material[] GongMats;
    public Material[] SkyMats;

    [Header("GongCtrl")]
    public GameObject Skybox;
    public GameObject CloneWaterPang;
}
