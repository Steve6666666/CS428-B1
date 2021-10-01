using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingObstacleController : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer mr;
    public float speed;
    [Header("材质资源")]
    public Material originalMat;    //原始材质
    public Material selectedMat;
    public bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void Awake()
    {
        mr = this.transform.GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isSelected)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0F, moveVertical);

            rb.AddForce(movement * speed);
        }
    }


    public void Selected()
    {
        if (!isSelected)
        {
            isSelected = true;
            mr.material = selectedMat;
           
        }
        else
        {
            isSelected = false;
            rb.velocity = Vector3.zero;
            mr.material = originalMat;
           
        }
    }
}
