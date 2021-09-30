using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoleBase : MonoBehaviour
{
    private MeshRenderer mr;
    private NavMeshAgent nav;
    private LineRenderer lineRd;

    [SerializeField]
    float moveSpeed;

    public bool isSelected;     //�Ƿ�ѡ��

    [Header("������Դ")]
    public Material originalMat;    //ԭʼ����
    public Material selectedMat;    //��ѡ��ʱ����

    public bool isStopped;

    private void Awake()
    {
        mr = this.transform.GetComponent<MeshRenderer>();
        nav = this.transform.GetComponent<NavMeshAgent>();
        lineRd = this.transform.GetComponent<LineRenderer>();
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
