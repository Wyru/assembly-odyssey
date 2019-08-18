using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Transform piecesContainer;
    public List<Transform> spawnPoints;
    public List<GameObject> puzzles;
    public List<PieceJoint> sockets;
    int currentPuzzle;
    bool settingPuzzle;
    bool gameIsEnd;

    private void Start()
    {
        settingPuzzle = true;
        currentPuzzle = 0;
        gameIsEnd = false;
        StartCoroutine("SpawnPuzzle");
    }

    private void Update()
    {

        if (gameIsEnd)
            return;

        if (!settingPuzzle){
        
            if (sockets.TrueForAll((PieceJoint a) =>
            {
                return a.joinSuccess;
            }))
            {
                settingPuzzle = true;
                StartCoroutine("SpawnPuzzle");
                currentPuzzle++;
            }
        }
    }

    IEnumerator SpawnPuzzle()
    {
        PieceBehavior[] pieces = piecesContainer.GetComponentsInChildren<PieceBehavior>();

        for (int i = 0; i < pieces.Length; i++)
        {
            Destroy(pieces[i].gameObject);
        }

        yield return new WaitForSeconds(.1f);

        if (currentPuzzle >= puzzles.Count)
        {
            Debug.Log("Obrigado por ter jogar ^^");
            gameIsEnd = true;
        }

        if (!gameIsEnd)
        {
            PieceBehavior[] pb = puzzles[currentPuzzle].GetComponentsInChildren<PieceBehavior>();

            for (int i = 0; i < pb.Length; i++)
            {
                Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                Quaternion rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                Instantiate(pb[i], position, Quaternion.identity, piecesContainer);
                yield return new WaitForSeconds(.1f);
            }

            sockets = new List<PieceJoint>(piecesContainer.GetComponentsInChildren<PieceJoint>());

            sockets = sockets.FindAll((PieceJoint p) =>
            {
                return p.isCenterJoint;
            });

            settingPuzzle = false;
        }

    }
}
