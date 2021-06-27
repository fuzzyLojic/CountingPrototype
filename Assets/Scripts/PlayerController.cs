using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    private float xStartRotation = 0f;
    private float yStartRotation = 0f;
    private float xRotation;
    private float yRotation;
    private float maxX = 40f;
    private float maxY = 20f;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;

    private ShotPooler shotPool;
    [SerializeField] float shotForce;
    [SerializeField] float maxVertAngle;
    private float vertAngle;

    private void Awake() {
        shotPool = ShotPooler.ShotPool;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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
        if(!gameManager.GameOver){
            Look();
            Fire();
            ResetLook();
        }
    }

    private void Look(){
        verticalInput = Input.GetAxis("Mouse Y");
        horizontalInput = Input.GetAxis("Mouse X");

        xRotation += horizontalInput * xSpeed;
        xRotation = Mathf.Clamp(xRotation, -maxX + xStartRotation, maxX + xStartRotation);

        yRotation += verticalInput * ySpeed;
        yRotation = Mathf.Clamp(yRotation, -maxY + yStartRotation, maxY + yStartRotation);

        transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        CalcVertAngle();
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
            GameObject shot = shotPool.GetPooledShot();
            shot.SetActive(true);
            var shotRb = shot.GetComponent<Rigidbody>();
            shotRb.velocity = shotAngle * 0;
            shot.transform.position = transform.position;
            shotRb.AddForce(shotAngle * shotForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Increase angle of shot as player looks up more
    /// </summary>
    private void CalcVertAngle(){
        float currentY = (yRotation - (yStartRotation - maxY));
        float yRange = (yStartRotation + maxY) - (yStartRotation - maxY);
        float percentAngle = 1 - (currentY / yRange);
        vertAngle = percentAngle * maxVertAngle; 
    }
}
