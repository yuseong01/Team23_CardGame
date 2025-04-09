using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Pool;

public class SparkleObjectPoolManager : MonoBehaviour
{
    public IObjectPool<GameObject> objectPool { get; private set; }
    public static SparkleObjectPoolManager instance;

    //Ǯ���� ������Ʈ ������
    [SerializeField] private GameObject prefab;

    private const int defaultCapacity = 2;  //�ʱ� Ǯ ũ��
    private const int maxSize = 10;         //Ǯ �ִ� ũ��


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        objectPool = new ObjectPool<GameObject>(
            createFunc,         //���ο� ������Ʈ ����
            actionOnGet,        //objectPool.Get(obj) �ϸ� ����
            actionOnRelease,    //objectPool.Release(obj) �ϸ� ����
            actionOnDestroy,    //Ǯ���� ������ �� ������ �Լ�
            true, defaultCapacity, maxSize);

        //�̸� ������Ʈ �����صα�
        for (int i = 0; i < defaultCapacity; i++)
        {
            objectPool.Release(createFunc());
        }
    }

    //���ο� ������Ʈ ����
    private GameObject createFunc()
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false); // ó������ ��Ȱ��ȭ ���·� ����
        return obj;
    }

    //objectPool.Get(obj) �ϸ� ����
    private void actionOnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    //objectPool.Release(obj) �ϸ� ����
    private void actionOnRelease(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }

    //Ǯ���� ������ �� ������ �Լ�
    private void actionOnDestroy(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetObject(Vector2 pos)
    {
        GameObject obj = objectPool.Get();
        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.identity;
        return obj;
    }

    public void ReleaseObject(GameObject obj)
    {
        if (objectPool != null)
        {
            objectPool.Release(obj);
        }
    }
}