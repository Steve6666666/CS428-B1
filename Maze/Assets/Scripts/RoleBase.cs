using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoleBase : MonoBehaviour
{
    private MeshRenderer mr;
    private NavMeshAgent nav;
    private LineRenderer lineRd;
    private Rigidbody rb;
    [SerializeField]
    float moveSpeed;

    public bool isSelected;     //是否被选中

    [Header("材质资源")]
    public Material originalMat;    //原始材质
    public Material selectedMat;    //被选中时材质

    public bool isStopped;

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
    }

    public void MoveToTarget(Vector3 targetPos)
    {
        //TODO
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
