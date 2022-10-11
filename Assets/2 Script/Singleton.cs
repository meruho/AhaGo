using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기본 싱글톤
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _inst;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                //T가 들어가있는 오브젝트를 찾아 T로 형변환 해서 T에 넣는다
                _inst = FindObjectOfType(typeof(T)) as T; // as 객체 캐스팅해서 반환 , is 캐스팅 참거짓 반환
            }
            return _inst;
        }
    }
}

// 기본 싱글톤 + 오브젝트 유지
public class SingletonKeep<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _inst;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                //T가 들어가있는 오브젝트를 찾아 T로 형변환 해서 T에 넣는다
                _inst = FindObjectOfType(typeof(T)) as T; // as 객체 캐스팅해서 반환 , is 캐스팅 참거짓 반환
                //데이터 유지를 위해 , 오브젝트 유지 하기 위한 함수
                DontDestroyOnLoad(_inst.gameObject);
            }
            return _inst;
        }
    }
}

//위 스크립트는 걍 냅두고
//스크립트 하나 만들어서
//public class Test : Singleton<Test>
//{
//    public int a;
//}
//이렇게 만들고
//딴데서 가져와서 쓸때는
//Test.inst.a   - 이렇게 쓰면됨
