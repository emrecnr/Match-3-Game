using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("--- PANELS ---")]
    public GameObject completedPanel;
    public GameObject failedPanel;

    [SerializeField] TextMeshProUGUI _moveText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;



}
