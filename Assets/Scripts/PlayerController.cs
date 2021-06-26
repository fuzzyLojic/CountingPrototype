using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xStartRotation = 0f;
    private float yStartRotation = 0f;
    private float xRotation;
    private float yRotation;
    private float maxX = 16f;
    private float maxY = 40f;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;

    private ShotPooler shotPool;
    [SerializeField] float shotForce;
    [SerializeField] float vertAngle;

    private void Awake() {
        shotPool = ShotPooler.ShotPool;
    }

    // Start is called before the first frame update
    void Start()
    {
        xRotation = xStartRotation;
        yRotation = yStartRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Fire();
        ResetLook();
    }

    private void Look(){
        verticalInput = Input.GetAxis("Mouse Y");
        horizontalInput = Input.GetAxis("Mouse X");

        xRotation += verticalInput * xSpeed;
        xRotation = Mathf.Clamp(xRotation, -maxX + xStartRotation, maxX + xStartRotation);

        yRotation += horizontalInput * ySpeed;
        yRotation = Mathf.Clamp(yRotation, -maxY + yStartRotation, maxY + yStartRotation);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void ResetLook(){
        if(Input.GetMouseButtonDown(1)){
            xRotation = xStartRotation;
            yRotation = yStartRotation;
        }
    }

    private void Fire(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 shotAngle = transform.forward + new Vector3(0, vertAngle, 0);
            GameObject shot = shotPool.GetPooledObject();
            shot.SetActive(true);
            var shotRb = shot.GetComponent<Rigidbody>();
            shotRb.velocity = shotAngle * 0;
            shot.transform.position = transform.position;
            shotRb.AddForce(shotAngle * shotForce, ForceMode.Impulse);
        }
    }
}
