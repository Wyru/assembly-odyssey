using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public float PieceMoveSpeed;
    public float PieceRotateSpeed;

    public Animator HandleModeHUD;


    public enum HandleMode
    {
        Move,
        Rotate
    }
    public HandleMode mode;

    public Transform CameraTransform;

    void Start()
    {
        Instance = this;
    }


    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q)){
            mode = HandleMode.Move;
        }

        if(Input.GetKeyDown(KeyCode.E)){
            mode = HandleMode.Rotate;
        }

        if(mode == HandleMode.Move)
            HandleModeHUD.SetBool("Move", true);
        else
            HandleModeHUD.SetBool("Move", false);

    
        // if(Input.GetMouseButton(1)){
        //     // CameraTransform.RotateAround(Vector3.zero, Vector3.up, 100 * Time.deltaTime * Input.GetAxis("Mouse X"));
        //     // CameraTransform.RotateAround(Vector3.zero, Vector3.,20 * Time.deltaTime * Input.GetAxis("Mouse Y"));
        // }
        
    }




}
