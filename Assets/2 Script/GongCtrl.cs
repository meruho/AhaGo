using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GongCtrl : MonoBehaviour
{
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CIRCLE"))
        {
            GameManager.Inst.Skybox.SetActive(true);
            GameManager.Inst.Skybox.GetComponent<MeshRenderer>().material = GameManager.Inst.SkyMats[GameManager.Inst.selectGongIdx];
            this.GetComponent<Rigidbody>().velocity.Set(0, 0, 0);


            Vector3 effPos = new Vector3(this.transform.position.x, other.transform.position.y + 0.1f, this.transform.position.z);
            GameObject g = Instantiate(GameManager.Inst.CloneWaterPang, effPos, Quaternion.identity);
            DOVirtual.DelayedCall(0.1f, delegate ()
            {
                Destroy(this.gameObject);
                GameManager.Inst.SelectGong.SetActive(false);
                GameManager.Inst.LeftArrow.SetActive(false);
                GameManager.Inst.RightArrow.SetActive(false);
            });

            DOVirtual.DelayedCall(4.0f, delegate ()
            {
                GameManager.Inst.Skybox.SetActive(false);

                GameManager.Inst.SelectGong.SetActive(true);
                GameManager.Inst.LeftArrow.SetActive(true);
                GameManager.Inst.RightArrow.SetActive(true);
            });
        }
    }
}
