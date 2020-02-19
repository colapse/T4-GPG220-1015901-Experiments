using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BoidsAgent : MonoBehaviour
{
    public Rigidbody rb;

    public List<BoidsAgent> nearestBoids;

    public Quaternion desiredRotation;

    public float rotationSpeed = 10f; // degrees/sec
    
    public float maxSpeed = 2f;
    public float minSpeed = 0;
    private float currentSpeed;

    public float obstacleDetectionDistance = 5f;

    public float[] obstacleRayDegrees;
    public float CurrentSpeed
    {
        get => currentSpeed;
        set => currentSpeed = value > maxSpeed ? maxSpeed : value < minSpeed ? minSpeed : value;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        desiredRotation = transform.rotation;
        CurrentSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO calc desired rotation
        CheckForObstacles();
        CheckForNeighbourBoids();
        
        
        Rotate();
        Move();
    }

    public bool CheckForNeighbourBoids()
    {
        var neighboursInFront = nearestBoids.Where(boid => Vector3.Dot(transform.forward, (transform.position - boid.transform.position)) >= 0).OrderBy(boid => Vector3.Distance(boid.transform.position, transform.position)).ToList(); // 90deg view
        //var neighboursInFront = nearestBoids.Where(boid => Vector3.Dot(transform.forward, (transform.position - boid.transform.position)) >= 0).ToList().Sort((boid1, boid2) => return Vector3.Distance(boid1.transform.position, transform.position).CompareTo(Vector3.Distance(boid2.transform.position, transform.position))); // 90deg view

        if (neighboursInFront != null && neighboursInFront.Count > 0)
        {
            var nearestBoid = neighboursInFront[0];
            desiredRotation = Quaternion.LookRotation(nearestBoid.transform.position-transform.position);
        }
        
        return true;
    }


    private bool CheckForObstacles()
    {
        if(obstacleRayDegrees == null || obstacleRayDegrees.Length == 0)
            return false;

        Array.Sort(obstacleRayDegrees);

        var fwdDir = transform.forward;
        var curPos = transform.position;
        var curRot = transform.rotation;
        
        
        var minAngle = Mathf.Abs(obstacleRayDegrees.Min());
        var frontCollisions = Physics.BoxCastAll(curPos, new Vector3(minAngle, minAngle, minAngle), fwdDir, curRot,
            obstacleDetectionDistance);

        if (frontCollisions == null || frontCollisions.Length == 0) return false;
            
        var sideCollisions = new List<RaycastHit>();

        
        for (var i = 0; i < obstacleRayDegrees.Length; i++)
        {
            var angle = Mathf.Abs(obstacleRayDegrees[i]);
            var sphereRadius = obstacleDetectionDistance *
                                   Mathf.Sqrt(2 * (1 - Mathf.Cos(angle)));
            var boxHalfExtens = new Vector3(sphereRadius,sphereRadius,sphereRadius);
            
            
            var dirLeft = Quaternion.Euler(0, -angle, 0) * fwdDir;
            var dirRight = Quaternion.Euler(0, angle, 0) * fwdDir;
            var dirUp = Quaternion.Euler(angle, 0, 0) * fwdDir;
            var dirDown = Quaternion.Euler(-angle, 0, 0) * fwdDir;
            
            var hits = new RaycastHit[4];

            Vector3 newDesiredDir;
            var newDesiredDirHitDist = float.MaxValue;

            if (Physics.BoxCast(curPos, boxHalfExtens, dirLeft, out var hitLeft, curRot, obstacleDetectionDistance))
            {
                newDesiredDir = curPos - hitLeft.point;
                newDesiredDirHitDist = hitLeft.distance;
            }
            else
            {
                newDesiredDir = dirLeft;
                newDesiredDirHitDist = float.MaxValue;
            }

            if (Physics.BoxCast(curPos, boxHalfExtens, dirRight, out var hitRight, curRot, obstacleDetectionDistance) && hitRight.distance > newDesiredDirHitDist)
            {
                newDesiredDir = curPos - hitRight.point;
                newDesiredDirHitDist = hitRight.distance;
            }else if (!Mathf.Approximately(newDesiredDirHitDist, float.MaxValue))
            {
                newDesiredDir = dirRight;
                newDesiredDirHitDist = float.MaxValue;
            }

            if (Physics.BoxCast(curPos, boxHalfExtens, dirUp, out var hitUp, curRot, obstacleDetectionDistance) && hitUp.distance > newDesiredDirHitDist)
            {
                newDesiredDir = curPos - hitUp.point;
                newDesiredDirHitDist = hitUp.distance;
            }else if (!Mathf.Approximately(newDesiredDirHitDist, float.MaxValue))
            {
                newDesiredDir = dirUp;
                newDesiredDirHitDist = float.MaxValue;
            }

            if (Physics.BoxCast(curPos, boxHalfExtens, dirDown, out var hitDown, curRot, obstacleDetectionDistance) && hitDown.distance > newDesiredDirHitDist)
            {
                newDesiredDir = curPos - hitDown.point;
                newDesiredDirHitDist = hitDown.distance;
            }else if (!Mathf.Approximately(newDesiredDirHitDist, float.MaxValue))
            {
                newDesiredDir = dirDown;
                newDesiredDirHitDist = float.MaxValue;
            }

            

            /*var collisionLeft =
                Physics.BoxCastAll(curPos, boxHalfExtens, dirLeft, curRot, obstacleDetectionDistance);
            var collisionRight =
                Physics.BoxCastAll(curPos, boxHalfExtens, dirRight, curRot, obstacleDetectionDistance);
            var collisionUp =
                Physics.BoxCastAll(curPos, boxHalfExtens, dirUp, curRot, obstacleDetectionDistance);
            var collisionDown =
                Physics.BoxCastAll(curPos, boxHalfExtens, dirDown, curRot, obstacleDetectionDistance);

            sideCollisions = sideCollisions.Union(collisionLeft).Union(collisionRight).Union(collisionUp).Union(collisionDown).ToList();*/
        }
        
        /*
        sideCollisions?.ForEach(hit =>
        {
            
        });*/

        return true;
    }

    private void Rotate()
    {
        if (!transform.rotation.Equals(desiredRotation))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed);
        }
    }

    private void Move()
    {
        transform.position += transform.forward * currentSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        var agent = other.GetComponent<BoidsAgent>();
        if(agent != null)
            nearestBoids.Add(agent);
    }

    public void OnTriggerExit(Collider other)
    {
        var agent = other.GetComponent<BoidsAgent>();
        if(agent != null)
            nearestBoids.Remove(agent);
    }
}