using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    float TeleportSec = 15.0f;
    float time = 0f;
    float moveSec = 0f;
    float rotateSec = 0f;

    float moveSpd = 10f;
    //float rotateSpd = 3f;

    int walk = 0;
    int rotate;
    int missDistance = 200;

    List<GameObject>  SlenderPoses;
    public List<GameObject> target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SlenderPoses = new List<GameObject>();
        agent = GetComponent<NavMeshAgent>();

        for(int i = 1; i <= 8; i++)
        {
            GameObject slenderPos = GameObject.Find("SlenderPos" + i);

            if(slenderPos != null)
            {
                SlenderPoses.Add(slenderPos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target.Count > 0)
        {
            if (time != 0)
                time = 0;

            if(target[0] != null)
            {
                agent.SetDestination(target[0].transform.position);

                anim.SetBool("scream", true);
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("scream", false);
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
            }

            if(Vector3.Distance(gameObject.transform.position, target[0].transform.position) > missDistance) {
                anim.SetBool("scream", false);
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
                Debug.Log("Miss");
                target.Clear();
            }
        }
        else {
            EnemyMove();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Wall"))
        {
            time = 10f;
            moveSec = 0;
            walk = 0;
        }
        if (target.Count > 0) //현재 타겟이 지정된 상태에서 플레이어와 부딪혔을 때
        {
            if(target[0] != null)
            {
                if (collision.gameObject == target[0])
                {
                    anim.SetBool("scream", false);
                    anim.SetBool("run", false);
                    anim.SetBool("idle", true);
                    Debug.Log("Catch");
                    target.Clear();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (target.Count == 0) //현재 타겟이 지정되지 않은 상태에서 플레이어를 감지했을 때
            {
                Debug.Log("Got You");
                target.Add(other.gameObject);
            }
        }
    }

    void EnemyMove()
    {
        time += Time.deltaTime;

        if (time <= TeleportSec) //텔레포트 하기 전이면 걷기, 멈추기 혹은 두리번거리기
        {
            moveSec += Time.deltaTime;
            rotateSec += Time.deltaTime;

            if (moveSec >= 2.5f)
            {
                walk = Random.Range(0, 2);
                moveSec = 0f;
            }

            if (rotateSec >= 2.5f)
            {
                rotate = Random.Range(0, 4);
                rotateSec = 0f;
            }


            if (walk == 1)
            {
                transform.Translate(Vector3.forward * moveSpd * Time.deltaTime);
                anim.SetBool("idle", false);
                anim.SetBool("walk", true);
            }
            else
            {
                anim.SetBool("walk", false);
                anim.SetBool("idle", true);
            }

            if (rotate > 0)
            {
                if (rotate == 1)
                {
                    transform.Rotate(Vector3.up * 18f * Time.deltaTime);
                }
                else if (rotate == 2)
                {
                    transform.Rotate(Vector3.down * 18f * Time.deltaTime);
                }
                else if (rotate == 3)
                {
                    transform.Rotate(Vector3.up * 36f * Time.deltaTime);
                }

                if (walk == 0)
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("idle", true);
                }
            }
        }
        else if (time > TeleportSec)
        {
            int randomPos = Random.Range(0, SlenderPoses.Count);
            gameObject.transform.position = SlenderPoses[randomPos].transform.position;

            time = 0;
        }
    }
}
