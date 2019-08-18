using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public Transform piecesContainer;
    public List<Transform> spawnPoints;
    public List<GameObject> puzzles;
    [HideInInspector] public List<PieceJoint> sockets;
    int currentPuzzle;
    bool settingPuzzle;
    bool gameIsEnd;

    public AudioSource audioSource;
    [BoxGroup("Sounds config")] public AudioClip pieceJoinSuccess;
    [BoxGroup("Sounds config")] public AudioClip puzzleClear;
    [BoxGroup("Sounds config")] public AudioClip pieceSpawn;

    private void Start()
    {
        Instance = this;
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
                PlaySpawnPiece();
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

    public void PlayPuzzleSuccess(){
        if(puzzleClear!=null)
        audioSource.PlayOneShot(puzzleClear);
    }

    public void PlayJoinPieceSuccess(){
        if(pieceJoinSuccess!=null)
        audioSource.PlayOneShot(pieceJoinSuccess);
    }

    public void PlaySpawnPiece(){
        if(pieceSpawn!=null)
        audioSource.PlayOneShot(pieceSpawn);
    }
}
