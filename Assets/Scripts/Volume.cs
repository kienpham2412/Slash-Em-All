using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    private GameManager gameManager;
    private Slider volume;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Slider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume()
    {
        gameManager.ChangeVolume(volume.value);
    }
}
