using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("--- PANELS ---")]
    public GameObject completedPanel;
    public GameObject failedPanel;

    [SerializeField] TextMeshProUGUI _moveText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TMP_Text moveCountText;

    [Header("--- SCORES PROCESS")]
    public Slider scoreSlider;
    public GameObject[] levelStars;



}
