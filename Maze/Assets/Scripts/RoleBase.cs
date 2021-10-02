using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class RoleBase : MonoBehaviour
{
    private MeshRenderer mr;
    private NavMeshAgent nav;
    private LineRenderer lineRd;
    private Rigidbody rb;
    [SerializeField]
    float moveSpeed;
    Vector3 adversePos;

    public bool isSelected;     //??????????

    [Header("????????")]
    public Material originalMat;    //????????
    public Material selectedMat;    //????????????

    public bool isStopped;
    public bool isCollide;

    private void Awake()
    {
        mr = this.transform.GetComponent<MeshRenderer>();
        nav = this.transform.GetComponent<NavMeshAgent>();
        lineRd = this.transform.GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        isSelected = false;
        isCollide = false;
        //if(gameObject.layer== LayerMask.NameToLayer("adverse"))
        //{
        //    adversePos = rb.position;
        //    gameObject
        //}
    }

    void Update()
    {
        lineRd.enabled = nav.velocity.sqrMagnitude > 0.0f ? true : false;

        if (nav.path.corners.Length > 1 && lineRd.enabled)
        {
            lineRd.enabled = true;
            lineRd.positionCount = nav.path.corners.Length;
            lineRd.SetPositions(nav.path.corners);
        }

        if ((nav.destination.x - nav.nextPosition.x <= 0.05f)
                        && (nav.destination.y - nav.nextPosition.y <= 0.05f)
                        && (nav.destination.z - nav.nextPosition.z <= 0.05f) && lineRd.enabled)
        {
            //nav.velocity = Vector3.zero;
            //rb.velocity = Vector3.zero;
        }
        else if(nav.isStopped || (nav.destination.x - nav.nextPosition.x > 0.5f)
                        && (nav.destination.y - nav.nextPosition.y > 0.5f)
                        && (nav.destination.z - nav.nextPosition.z > 0.5f))
        {
            nav.isStopped = false;
            lineRd.enabled = true;
            isCollide = false;
        }
        if (isCollide)
        {
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
            lineRd.enabled = false;
            nav.path = null;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle")|| collision.collider.gameObject.layer == LayerMask.NameToLayer("adverse"))
        {
            isCollide = true;
        }

    }

    public void MoveToTarget(Vector3 targetPos)
    {
        nav.SetDestination(targetPos);
    }

    public void Selected()
    {
        if(!isSelected)
        {
            isSelected = true;
            mr.material = selectedMat;
            RoleManager.Instance.roleList.Add(this);
        }
        else
        {
            isSelected = false;
            mr.material = originalMat;
            RoleManager.Instance.roleList.Remove(this);
        }
    }
}
