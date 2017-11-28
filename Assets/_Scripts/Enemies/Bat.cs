using UnityEngine;
using Glitch.Enemy;
using Glitch.Player;
// By @JavierBullrich

public class Bat : Enemy
{
    public Vector2[] localWaypoints;
    Vector2[] globalWaypoints;

    public float speed;
    public bool cyclic;
    [Range(0, 2)]
    public float waitTime;
    [Range(0, 2)]
    public float easeAmount;

    int fromWaypointIndex;
    float percentBetweenWaypoins;
    float nextMoveTime;

    Animator anim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        globalWaypoints = new Vector2[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + new Vector2(transform.position.x, transform.position.y);
        }
    }

    private void Update()
    {
        UpdateRaycastOrigins();
        Vector3 velocity = CalculatePlatformMovement();
        if (velocity.y > 0)
            anim.Play("CSS_Bat");
        else
            anim.Play("CSS_Bat_Down");
        transform.Translate(velocity);
        DamageAnim();
    }

    public override void Destroy()
    {
        Glitch.Manager.GameManagerBase.instance.PlaySfx(Glitch.Manager.SoundManager.Sfx.explosion);
        gameObject.SetActive(false);
    }

    public override void ReceiveDamage(int damage)
    {
        TakeHit(damage);
        damagedAnim = 1;
    }

    Vector3 CalculatePlatformMovement()
    {
        if (Time.time < nextMoveTime)
            return Vector3.zero;

        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoins += Time.deltaTime * speed / distanceBetweenWaypoints;
        percentBetweenWaypoins = Mathf.Clamp01(percentBetweenWaypoins);
        float easedPercentBetweenWaypoints = Ease(percentBetweenWaypoins);

        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easedPercentBetweenWaypoints);

        if (percentBetweenWaypoins >= 1)
        {
            percentBetweenWaypoins = 0;
            fromWaypointIndex++;

            if (!cyclic)
                if (fromWaypointIndex >= globalWaypoints.Length - 1)
                {
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }
            nextMoveTime = Time.time + waitTime;
        }

        return newPos - transform.position;
    }

    float Ease(float x)
    {
        float a = easeAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    void OnDrawGizmos()
    {
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = .3f;

            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + new Vector2(transform.position.x, transform.position.y);
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }

    public override void InteractWith()
    {
        //throw new NotImplementedException();
    }

    public override void TriggerEntered(GlitcherBehaviorBase glitcher)
    {
        glitcher.ReceiveDamage(damagePerHit);
    }

    public override void TriggerExit(GlitcherBehaviorBase glitcher)
    {
        //throw new NotImplementedException();
    }
}
