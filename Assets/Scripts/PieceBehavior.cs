using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehavior : MonoBehaviour
{
    public static bool AnySelect;

    bool isSelected;
    float verticalMove;
    float horizontalMove;
    float deepMove;
    Vector3 movement;

    public PieceBehavior parent;
    public Transform meOrParent{
        get{
            if(parent!=null){
                return parent.meOrParent;
            }
            return transform;
        }
    }

    private void Start()
    {

    }

    private void FixedUpdate() {
        if(GameController.Instance.mode == GameController.HandleMode.Move){
            meOrParent.Translate(movement * GameController.Instance.PieceMoveSpeed, Space.World); 
        }
        else{
            if(Mathf.Abs(movement.x) > Mathf.Abs(movement.y) && Mathf.Abs(movement.x) > Mathf.Abs(movement.z) ){
                movement = new Vector3(0, movement.x, 0);
            }
            else if(Mathf.Abs(movement.y) > Mathf.Abs(movement.x) && Mathf.Abs(movement.y) > Mathf.Abs(movement.z) ){
                movement = new Vector3(0, 0, movement.y);
            }
            else if(Mathf.Abs(movement.z) > Mathf.Abs(movement.x) && Mathf.Abs(movement.z) > Mathf.Abs(movement.y) ){
                movement = new Vector3(movement.z, 0, 0);
            }
            meOrParent.Rotate( movement * GameController.Instance.PieceRotateSpeed, Space.World);
        }
    }

    private void Update()
    {
        if (isSelected)
        {
            verticalMove = Input.GetAxis("Mouse Y");
            horizontalMove = Input.GetAxis("Mouse X");
            deepMove = Input.GetAxis("Vertical");
            
            movement = new Vector3(horizontalMove, verticalMove, deepMove) * Time.deltaTime ;
            
        }
    }

    private void OnMouseDown()
    {
        AnySelect = isSelected = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnMouseUp()
    {
        AnySelect = isSelected = false;
        movement = Vector3.zero;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
