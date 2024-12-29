using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    private float health = 3;

    public void Kill()
    {
        SceneManager.LoadScene("Game");
    }

    public void TakeDamage()
    {
        health--;
        GameObject.Find("UIManager").GetComponent<UIManager>().UpdateHealth(health);
        if (health <= 0)
        {
            Kill();
        }
    }

}
