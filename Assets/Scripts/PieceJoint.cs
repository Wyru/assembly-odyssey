using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PieceJoint : MonoBehaviour
{
    [BoxGroup("References")]public ParticleSystem successParticles;
    [BoxGroup("Settings")]public bool isSocket;
    [BoxGroup("Settings"), ShowIf("isSocket")]public bool isCenterJoint;
    [BoxGroup("Settings"), ShowIf("isSocket")]public PieceJoint RightJoint;
    [BoxGroup("Settings"), ShowIf("isCenterJoint")]public List<PieceJoint> corners;
    public bool joinSuccess;
    [HideInInspector]public PieceJoint currentJoint;

    private void Update()
    {
        if (isSocket)
        {
            if (isCenterJoint)
            {
                if (!joinSuccess && corners.TrueForAll((PieceJoint a) =>
                {
                    return a.joinSuccess;
                }))
                {
                    successParticles?.Play();
                    LevelController.Instance.PlayJoinPieceSuccess();
                    joinSuccess = true;
                    currentJoint.transform.parent.SetParent(transform.parent);
                    currentJoint.GetComponentInParent<PieceBehavior>().parent = GetComponentInParent<PieceBehavior>();
                    Debug.Log("success");
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isSocket)
        {
            currentJoint = other.GetComponent<PieceJoint>();

            if (!isCenterJoint)
            {
                if (RightJoint != null)
                {
                    if (currentJoint == RightJoint)
                    {
                        if (!isCenterJoint)
                        {
                            joinSuccess = true;
                        }
                    }
                }
                else
                {

                    joinSuccess = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isSocket)
        {
            if (!isCenterJoint)
            {
                if (RightJoint != null)
                {
                    currentJoint = other.GetComponent<PieceJoint>();
                    if (currentJoint == RightJoint)
                    {
                        joinSuccess = false;
                    }
                }
                else
                {
                    joinSuccess = false;
                }
            }
        }
    }
}
