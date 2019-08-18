using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PieceJoint : MonoBehaviour
{
    public bool isCenterJoint;
    public bool isSocket;
    public PieceJoint RightJoint;
    public List<PieceJoint> corners;
    public bool joinSuccess;

    public PieceJoint currentJoint;
    public ParticleSystem successParticles;

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
                    successParticles.Play();
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
