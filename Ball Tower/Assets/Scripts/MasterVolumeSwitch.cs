using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject volumeText;
    private Text volumeInfo;

    public void ToogleVolume()
    {
        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0;
            volumeInfo.text = "Enable Audio";
        } else if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1.0f;
            volumeInfo.text = "Disable Audio";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        volumeInfo = volumeText.GetComponent<Text>();
        volumeInfo.text = "Disable Audio";
    }
}
