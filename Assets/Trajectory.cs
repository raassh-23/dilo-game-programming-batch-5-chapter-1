using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trajectory : MonoBehaviour
{
    public BallControl ball;
    private Rigidbody2D ballRigidBody;
    private CircleCollider2D ballCollider;

    public GameObject ballAtCollision;

    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool drawBallAtCollision = false;

        Vector2 offsetHitpoint = new Vector2();

        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigidBody.position,
            ballCollider.radius, ballRigidBody.velocity.normalized);

        foreach (RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            if (circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {
                Vector2 hitPoint = circleCastHit2D.point;
                Vector2 hitNormal = circleCastHit2D.normal;

                offsetHitpoint = hitPoint + hitNormal * ballCollider.radius;

                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitpoint);

                if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    Vector2 inVector = (offsetHitpoint - ball.TrajectoryOrigin).normalized;

                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    float outDot = Vector2.Dot(outVector, hitNormal);

                    if (outDot > -1.0f && outDot < 1.0f)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitpoint, offsetHitpoint + outVector * 10);

                        drawBallAtCollision = true;
                    }
                }

                break;
            }

        }

        if (drawBallAtCollision)
        {
            ballAtCollision.transform.position = offsetHitpoint;
            ballAtCollision.SetActive(true);
        }
        else
        {
            ballAtCollision.SetActive(false);
        }
    }
}
