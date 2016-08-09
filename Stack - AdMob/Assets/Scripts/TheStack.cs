using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class TheStack : MonoBehaviour
{
    public Color32[] gameColors = new Color32[4];
    public Material stackMat;
    public Text scoreText;
    public GameObject endPanel;
    public AudioSource placeTileSound;
    public AudioSource growUpTileSound;
    public AudioSource backgroundLoopSound;
    //public Text comboText;

    private const float BOUNDZ_SIZE = 3.5f;
    private const float STACK_MOVING_SPEED = 5.0f;
    private const float ERROR_MARGIN = 0.2f;
    private const float STACK_BOUNDS_GAIN = 0.25f;
    private const int COMBO_START_GAIN = 2;

    private GameObject[] theStack;

    private int scoreCount = 0;
    private int stackIndex;
    private int combo = 0;

    private float tileTransition = 0.0f;
    private float tileSpeed = 2.5f;
    private float secondaryPosition;

    private bool isMovingOnX = true;
    private bool gameOver = false;

    private Vector2 stackBounds = new Vector2(BOUNDZ_SIZE, BOUNDZ_SIZE);

    private Vector3 desirePosition;
    private Vector3 lastTilePosition;

    void Start()
    {
        theStack = new GameObject[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            theStack[i] = transform.GetChild(i).gameObject;
            ColorMesh(theStack[i].GetComponent<MeshFilter>().mesh);
            
        }
        stackIndex = transform.childCount - 1;
        AdManager.Instance.ShowBanner();
    }


    void Update()
    {
        if (gameOver)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceTile())
            {
                SpawnTile();
                scoreCount++;
                scoreText.text = scoreCount.ToString();
                //comboText.text = "Combo: " + combo;
            }
            else
            {
                EndGame();
            }
        }
        MoveTile();

        //move theStack
        transform.position = Vector3.Lerp(transform.position, desirePosition, STACK_MOVING_SPEED * Time.deltaTime);
    }

    private void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.AddComponent<Rigidbody>();

        go.GetComponent<MeshRenderer>().material = stackMat;
        ColorMesh(go.GetComponent<MeshFilter>().mesh);
    }

    private void MoveTile()
    {
        if (gameOver)
            return;
        tileTransition += Time.deltaTime * tileSpeed;
        if (isMovingOnX)
            theStack[stackIndex].transform.localPosition = new Vector3(Mathf.Sin(tileTransition) * BOUNDZ_SIZE, scoreCount, secondaryPosition);
        else
            theStack[stackIndex].transform.localPosition = new Vector3(secondaryPosition, scoreCount, Mathf.Sin(tileTransition) * BOUNDZ_SIZE);
    }

    private bool PlaceTile()
    {
        Transform t = theStack[stackIndex].transform;

        if (isMovingOnX)
        {
            float deltaX = lastTilePosition.x - t.position.x;
            if (Mathf.Abs(deltaX) > ERROR_MARGIN)
            {
                //Cut the TILE
                combo = 0;
                stackBounds.x -= Mathf.Abs(deltaX);
                if (stackBounds.x <= 0)
                    return false;
                float middle = lastTilePosition.x + t.localPosition.x / 2;
                t.localScale = (new Vector3(stackBounds.x, 1, stackBounds.y));
                CreateRubble(new Vector3((t.position.x > 0) ? t.position.x + (t.localScale.x / 2) : t.position.x - (t.localScale.x / 2),
                            t.position.y, t.position.z),
                             new Vector3(Mathf.Abs(deltaX), 1, t.localScale.z));
                t.localPosition = new Vector3((middle - (lastTilePosition.x / 2)), scoreCount, lastTilePosition.z);
            }
            else
            {
                if (combo > COMBO_START_GAIN)
                {
                    stackBounds.x += STACK_BOUNDS_GAIN;
                    if (stackBounds.x > BOUNDZ_SIZE)
                        stackBounds.x = BOUNDZ_SIZE;
                    float middle = lastTilePosition.x + t.localPosition.x / 2;
                    t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                    t.localPosition = new Vector3(middle - (lastTilePosition.x / 2), scoreCount, lastTilePosition.z);
                    growUpTileSound.Play();
                }
                combo++;
                t.localPosition = new Vector3(lastTilePosition.x, scoreCount, lastTilePosition.z);
            }
        }
        else
        {
            float deltaZ = lastTilePosition.x - t.position.z;
            if (Mathf.Abs(deltaZ) > ERROR_MARGIN)
            {
                //Cut the TILE
                combo = 0;
                stackBounds.y -= Mathf.Abs(deltaZ);
                if (stackBounds.y <= 0)
                    return false;
                float middle = lastTilePosition.z + t.localPosition.z / 2;
                t.localScale = (new Vector3(stackBounds.x, 1, stackBounds.y));
                CreateRubble(new Vector3(t.position.x, t.position.y,
        (t.position.z > 0) ? t.position.z + (t.localScale.z / 2) : t.position.z - (t.localScale.z / 2)),
         new Vector3(t.localScale.z, 1, Mathf.Abs(deltaZ)));
                t.localPosition = new Vector3(lastTilePosition.x, scoreCount, middle - (lastTilePosition.z / 2));
            }
            else
            {
                if (combo > COMBO_START_GAIN)
                {
                    stackBounds.y += STACK_BOUNDS_GAIN;
                    if (stackBounds.y > BOUNDZ_SIZE)
                        stackBounds.y = BOUNDZ_SIZE;
                    float middle = lastTilePosition.y + t.localPosition.z / 2;
                    t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                    t.localPosition = new Vector3(lastTilePosition.x, scoreCount, middle - (lastTilePosition.z / 2));
                    growUpTileSound.Play();
                }
                combo++;
                t.localPosition = new Vector3(lastTilePosition.x, scoreCount, lastTilePosition.z);
            }
        }
        
        secondaryPosition = (isMovingOnX) ? t.transform.localPosition.x : t.transform.localPosition.z;
        isMovingOnX = !isMovingOnX;
        return true;
    }

    private void SpawnTile()
    {
        lastTilePosition = theStack[stackIndex].transform.localPosition;
        stackIndex--;
        if (stackIndex < 0)
            stackIndex = transform.childCount - 1;

        desirePosition = (Vector3.down) * scoreCount;
        theStack[stackIndex].transform.localPosition = new Vector3(0, scoreCount, 0);
        theStack[stackIndex].transform.localScale = (new Vector3(stackBounds.x, 1, stackBounds.y));

        ColorMesh(theStack[stackIndex].GetComponent<MeshFilter>().mesh);
        placeTileSound.Play();
    }

    private void EndGame()
    {
        if (PlayerPrefs.GetInt("score") < scoreCount)
            PlayerPrefs.SetInt("score", scoreCount);
        gameOver = true;
        endPanel.SetActive(true);
        backgroundLoopSound.Stop();
        theStack[stackIndex].AddComponent<Rigidbody>();

    }

    private Color32 Lerp4(Color32 a, Color32 b, Color32 c, Color32 d, float t)
    {
        if (t < 0.33f)
            return Color.Lerp(a, b, t / 0.33f);
        else if (t < 0.66f)
            return Color.Lerp(b, c, (t - 0.33f) / 0.33f);
        else
            return Color.Lerp(c, d, (t - 0.66f) / 0.33f);
    }

    private void ColorMesh(Mesh mesh)
    {
        Vector3[] vertice = mesh.vertices;
        Color32[] colors = new Color32[vertice.Length];
        float f = Mathf.Sin(scoreCount * 0.25f);

        for (int i = 0; i < vertice.Length; i++)
        {
            colors[i] = Lerp4(gameColors[0], gameColors[1], gameColors [2], gameColors[3], f);
        }
        mesh.colors32 = colors;
    }

    public void OnButtonClick(string scaneName)
    {
        SceneManager.LoadScene(scaneName);
    }
}
