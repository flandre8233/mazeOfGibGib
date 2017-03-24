using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class connect_level : MonoBehaviour
{
    public void level_connect(int level_connect)
    {
        SceneManager.LoadScene(level_connect);
    }
}