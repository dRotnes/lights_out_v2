using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine.Playables;

public class SavingManager : MonoBehaviour
{
    [SerializeField] private List<Chest> chestArray = new List<Chest>();
    [SerializeField] private List<FireLighter> flArray = new List<FireLighter>();
    [SerializeField] private List<WoodBlock> wbArray= new List<WoodBlock>();
    [SerializeField] private List<Enemy> enArray = new List<Enemy>();
    [SerializeField] private List<GeneralTrigger> trArray = new List<GeneralTrigger>();

    private int currentScene;

    private bool[] chestBools;
    private bool[] flBools;
    private bool[] woodBools;
    private bool[] enBools;
    private bool[] trBools;

    public GameObject playerMO;
    public Player player;
    public SignalSend timeline;
    public BoolValue timelineBool;
    public BoolValue load;
    private void Start()
    {
        if (load.value)
        {
            LoadGame();
            playerMO.transform.position = new Vector3(player.positions[0], player.positions[1], player.positions[2]);
            Debug.Log(playerMO.transform.position);
        }
    }
    private void Awake() 
    { 
        if (timelineBool.value)
        {
            playerMO.transform.position = new Vector3(player.DEFAULT_positions[0], player.DEFAULT_positions[1], player.DEFAULT_positions[2]);
            timeline.RaiseSignal();
            timelineBool.value = false;

        }

    }

    public void SaveGame(int index)
    {
        chestBools = new bool[chestArray.Count];
        for (int i = 0; i < chestArray.Count; i++)
        {
            chestBools[i] = chestArray[i].GetStatus();
        }

        flBools = new bool[flArray.Count];
        for (int i = 0; i < flArray.Count; i++)
        {
            flBools[i] = flArray[i].GetStatus();
        }

        woodBools = new bool[wbArray.Count];
        for (int i = 0; i < wbArray.Count; i++)
        {
            woodBools[i] = wbArray[i].GetStatus();
        }

        enBools = new bool[enArray.Count];
        for (int i = 0; i < enArray.Count; i++)
        {
            enBools[i] = enArray[i].GetStatus();
        }

        trBools = new bool[trArray.Count];
        for (int i = 0; i < trArray.Count; i++)
        {
            trBools[i] = trArray[i].GetStatus();
        }

        SavingSystem.SaveGame(player, chestBools, flBools, woodBools, enBools, trBools, index);

        Debug.Log("Game was saved");
    }
    public void LoadGame()
    {
        Debug.Log("ok");
        SavingData data = SavingSystem.LoadGame();
        player.numberOfHearts = data.playerHearts;
        player.souls = data.playerSouls;
        player.currentHealth = data.playerHealth;
        player.maxHealth = data.maxHealth;
        player.numberOfKeys = data.numberOfKeys;
        player.numberOfSouls = data.numberOfSouls;
        player.positions[0] = data.position[0];
        player.positions[1] = data.position[1];
        player.positions[2] = data.position[2];

        if (chestArray.Count > 0)
        {

            for (int i = 0; i < data.chests.Length; i++)
            {
                chestArray[i].SetOpen(data.chests[i]);
            }

        }

        if (flArray.Count > 0)
        {
            for (int i = 0; i < data.firelighters.Length; i++)
            {
                flArray[i].SetStatus(data.firelighters[i]);
            }
        }

        if (wbArray.Count > 0)
        {
            for (int i = 0; i < data.woodblocks.Length; i++)
            {
                wbArray[i].SetStatus(data.woodblocks[i]);
            }
        }

        if (enArray.Count > 0)
        {
            for (int i = 0; i < data.enemies.Length; i++)
            {
                enArray[i].SetStatus(data.enemies[i]);
            }
        }

        if (trArray.Count > 0)
        {
            for (int i = 0; i < data.triggers.Length; i++)
            {
                trArray[i].SetStatus(data.triggers[i]);
            }
        }

    }

    public void ResetGame()
    {
        player.currentHealth = player.DEFAULT_currentHealth;
        player.maxHealth = player.DEFAULT_maxHealth;
        player.numberOfHearts = player.DEFAULT_numberOfHearts;
        player.numberOfKeys = player.DEFAULT_numberOfKeys;
        player.numberOfSouls = player.DEFAULT_numberOfSouls;
        player.souls = player.DEFAULT_souls;
        player.state = player.DEFAULT_state;
        player.positions[0] = player.DEFAULT_positions[0];
        player.positions[1] = player.DEFAULT_positions[1];
        player.positions[2] = player.DEFAULT_positions[2];
        SaveGame(2);
        Debug.Log(player.positions[0]);
        Debug.Log(player.positions[1]);
        Debug.Log(player.positions[2]);
        Debug.Log("GameSaved");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AddToArray(Chest chest = null, FireLighter fl = null, WoodBlock wb = null, Enemy en=null, GeneralTrigger tr = null)
    {
        if (chest!=null)
            chestArray.Add(chest);
        else if (fl != null)
            flArray.Add(fl);
        else if (wb != null)
            wbArray.Add(wb);
        else if (en != null)
            enArray.Add(en);
        else if (tr != null)
            trArray.Add(tr);
    }
}
