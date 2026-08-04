using UnityEngine;

// 제네릭 싱글턴 클래스
// T에는 MonoBehaviour를 상속받은 클래스만 들어올 수 있음
// 예) GameManager : Singleton<GameManager>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // 싱글턴 객체를 저장하는 정적 변수
    // static이기 때문에 클래스당 하나만 존재
    private static T instance;


    // 외부에서 접근하는 싱글턴 객체
    // 사용 방법: GameManager.Instance
    public static T Instance
    {
        get
        {
            // 아직 인스턴스가 없다면
            if (instance == null)
            {
                // 현재 씬에서 T 타입의 객체를 찾음
                instance = FindObjectOfType<T>();

                // 씬에 객체가 없다면 새로 생성
                if (instance == null)
                {
                    // 새로운 GameObject 생성
                    GameObject obj = new GameObject(typeof(T).Name);

                    // 생성한 오브젝트에 T 컴포넌트 추가
                    instance = obj.AddComponent<T>();
                }
            }

            // 생성된 싱글턴 반환
            return instance;
        }
    }


    // 객체가 생성될 때 가장 먼저 실행되는 함수
    protected virtual void Awake()
    {
        // 아직 싱글턴이 없다면
        if (instance == null)
        {
            // 현재 객체를 싱글턴으로 등록
            instance = this as T;

            // 씬이 변경되어도 삭제되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 싱글턴이 존재하면 새로 생성된 객체 제거
            Destroy(gameObject);
        }
    }
}