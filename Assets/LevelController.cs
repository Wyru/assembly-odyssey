using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<PieceJoint> scockets;
    private void Update() {
        if(scockets.TrueForAll((PieceJoint a)=>{
            return a.joinSuccess;
        })){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
