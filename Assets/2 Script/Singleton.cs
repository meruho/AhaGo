using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �⺻ �̱���
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _inst;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                //T�� ���ִ� ������Ʈ�� ã�� T�� ����ȯ �ؼ� T�� �ִ´�
                _inst = FindObjectOfType(typeof(T)) as T; // as ��ü ĳ�����ؼ� ��ȯ , is ĳ���� ������ ��ȯ
            }
            return _inst;
        }
    }
}

// �⺻ �̱��� + ������Ʈ ����
public class SingletonKeep<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _inst;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                //T�� ���ִ� ������Ʈ�� ã�� T�� ����ȯ �ؼ� T�� �ִ´�
                _inst = FindObjectOfType(typeof(T)) as T; // as ��ü ĳ�����ؼ� ��ȯ , is ĳ���� ������ ��ȯ
                //������ ������ ���� , ������Ʈ ���� �ϱ� ���� �Լ�
                DontDestroyOnLoad(_inst.gameObject);
            }
            return _inst;
        }
    }
}

//�� ��ũ��Ʈ�� �� ���ΰ�
//��ũ��Ʈ �ϳ� ����
//public class Test : Singleton<Test>
//{
//    public int a;
//}
//�̷��� �����
//������ �����ͼ� ������
//Test.inst.a   - �̷��� �����
