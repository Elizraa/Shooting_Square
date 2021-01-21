using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public static LevelControl levelControl;

    public int shot;
    public Text shotLeft;

    // Start is called before the first frame update
    void Start()
    {
        if (levelControl == null) levelControl = this;
        UpdateShot();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateShot()
    {
        if (shotLeft != null)
            shotLeft.text = "x "+shot.ToString();
    }

    public void goHome()
    {
        SceneManager.LoadScene(0);
    }
}
