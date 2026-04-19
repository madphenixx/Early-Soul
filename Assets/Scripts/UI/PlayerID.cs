using UnityEngine;

public class PlayerID : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject playerIdPanel;

    private string enteredID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("playerID") == false)
        {
            playerIdPanel.SetActive(true);
        }
    }

    public void ConfirmID()
    {
        PlayerPrefs.SetString("playerID", enteredID);
        playerIdPanel.SetActive(false);
    }

    public void EnterID(string inputID)
    {
        enteredID = inputID;
    }
}
