using UnityEngine;
using System.Collections;
// By @JavierBullrich

public class CharacterController2D {
    RaycastController raycastController;

    float maxJumpHeight = 4;
    float minJumpHeight = 1;
    float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;
    public CollisionInfo collisions;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    //public Vector2 velocity;
    float velocityXSmothing;
    Transform _tran;

    public CharacterController2D(RaycastController rayController, Transform tran, float maxJump, float minJump, float timetoJump, float MoveSpeed)
    {
        raycastController = rayController;
        _tran = tran;
        setGravity();
        collisions.faceDir = 1;
        maxJumpHeight = maxJump; minJumpHeight = minJump; timeToJumpApex = timetoJump; moveSpeed = MoveSpeed;
    }

    public void CalculateVelocity(ref Vector2 velocity, int direction)
    {
        float targetVelocityX = direction * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmothing, collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Glitch.Manager.GameManagerBase.DeltaTime;
    }

    public void Move(Vector2 moveAmount, bool standingOnPlatform = false)
    {
        raycastController.UpdateRaycastOrigins();
        collisions.Reset();

        if (moveAmount.x != 0)
            collisions.faceDir = (int)Mathf.Sign(moveAmount.x);

        HorizontalCollisions(ref moveAmount);
        if (moveAmount.y != 0)
        {
            VerticalCollisions(ref moveAmount);
        }

        _tran.Translate(moveAmount);

        if (standingOnPlatform)
        {
            collisions.below = true;
        }
    }

    void VerticalCollisions(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLength = Mathf.Abs(moveAmount.y) + RaycastController.skinWidth;

        for (int i = 0; i < raycastController.verticalRayCount; i++)
        {

            Vector2 rayOrigin = (directionY == -1) ? raycastController.raycastOrigins.bottomLeft : raycastController.raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (raycastController.verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, raycastController.collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                Debug.DrawLine(rayOrigin, hit.point, Color.blue);

                if (hit.collider.tag == "Through")
                {
                    if (directionY == 1 || hit.distance == 0)
                    {
                        continue;
                    }
                }
                    moveAmount.y = (hit.distance - RaycastController.skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }

    void HorizontalCollisions(ref Vector2 moveAmount)
    {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(moveAmount.x) + RaycastController.skinWidth;

        if (Mathf.Abs(moveAmount.x) < RaycastController.skinWidth)
        {
            rayLength = 2 * RaycastController.skinWidth;
        }

        for (int i = 0; i < raycastController.horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastController.raycastOrigins.bottomLeft : raycastController.raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (raycastController.horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, raycastController.collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                if (hit.distance == 0)
                {
                    continue;
                }

                moveAmount.x = (hit.distance - RaycastController.skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;


        public int faceDir;
        public bool fallingThroughPlatform;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }

    void setGravity()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        Debug.Log("gravity: " + gravity + " jump velocity " + maxJumpVelocity);
    }

}
