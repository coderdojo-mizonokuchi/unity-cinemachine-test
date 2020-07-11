using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject ballPrefab;
    public CinemachineTargetGroup targetGroup;
    public int ballMax = 5;
    public float interval = 1f;
    public float elapsedTime = 0f;

    private GameObject[] balls;
    private int addIndex;
    private int removeIndex;

    // Start is called before the first frame update
    void Start()
    {
        balls = new GameObject[ballMax];
        addIndex = 0;
        removeIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBalls();
    }

    void CheckBalls()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= interval)
        {
            elapsedTime = 0f;
            AddBall();
        }
    }

    void AddBall()
    {
        if (IsNeedRemove()) {
            RemoveBall();
        }

        GameObject ball = Instantiate(ballPrefab) as GameObject;
        ball.transform.position = new Vector3(Random.Range(-15, 15), Random.Range(15, 20), Random.Range(-15, 15));
        ball.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
        targetGroup.AddMember(ball.transform, 1, 5);
        balls[addIndex] = ball;
        addIndex = (addIndex + 1) % balls.Length;
    }

    void RemoveBall()
    {
        GameObject ball = balls[removeIndex];
        if (ball != null)
        {
            targetGroup.RemoveMember(ball.transform);
            Destroy(ball);
            removeIndex = (removeIndex + 1) % balls.Length;
        }
    }

    bool IsNeedRemove()
    {
        return (addIndex == removeIndex);
    }
}
