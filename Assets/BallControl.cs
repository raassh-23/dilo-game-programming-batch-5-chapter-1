using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    //xInitialForce tidak diperlukan lagi, karena nilai x dihitung dari gaya dan y
    //public float xInitialForce;
    public float yInitialForce;
    public float gaya;

    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;

        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetBall()
    {
        transform.position = Vector2.zero;
        rigidbody2d.velocity = Vector2.zero;
    }

    void PushBall()
    {
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0, 2);

        // agar besar gaya bola sama, berarti magnitude dari gaya sama.
        // rumus dari magnitude adalah sqrt(x^2 + y^2)
        // maka bila gaya dan y yang diketahui, rumus untuk x-nya adalah:
        // sqrt(gaya^2 - y^2)

        float x = Mathf.Sqrt(gaya * gaya - yInitialForce * yInitialForce);

        if (randomDirection < 1.0f)
        {
            rigidbody2d.AddForce(new Vector2(-x, yRandomInitialForce));
        }
        else
        {
            rigidbody2d.AddForce(new Vector2(x, yRandomInitialForce));
        }
    }

    void RestartGame()
    {
        ResetBall();

        Invoke("PushBall", 2);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin;  }
    }
}
