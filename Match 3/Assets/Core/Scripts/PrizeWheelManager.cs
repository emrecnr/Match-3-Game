
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrizeWheelManager : MonoBehaviour
{
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private float _rotatePower;
    [SerializeField] private float _stopPower;
    [SerializeField] private AudioSource _buttonClick;
    private float t;
    private Rigidbody2D _rigidBody;
    int inRotate;


    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        if (_rigidBody.angularVelocity > 0)
        {
            _rigidBody.angularVelocity -= _stopPower * Time.deltaTime;

            _rigidBody.angularVelocity = Mathf.Clamp(_rigidBody.angularVelocity, 0, 1440);
        }

        if (_rigidBody.angularVelocity == 0 && inRotate == 1)
        {
            t += 1 * Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward();

                inRotate = 0;
                t = 0;
            }
        }
    }
    public void GetReward()
    {
        _rewardManager.ButtonInteractable(false);
        float rot = transform.eulerAngles.z;

        if (rot > 0 && rot <= 45)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 45 - 22.5f);

            _rewardManager.ClaimReward("Coin", 50);
        }
        else if (rot > 45 && rot <= 90)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90 - 22.5f);

            _rewardManager.ClaimReward("Exchange");

        }
        else if (rot > 90 && rot <= 135)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 135 - 22.5f);
            _rewardManager.ClaimReward("Heal");
        }
        else if (rot > 135 && rot <= 180)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 180 - 22.5f);

            _rewardManager.ClaimReward("Mixer");

        }
        else if (rot > 180 && rot <= 225)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 225 - 22.5f);

            _rewardManager.ClaimReward("Coin", 50);

        }
        else if (rot > 225 && rot <= 270)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 270 - 22.5f);

            _rewardManager.ClaimReward("Milk");

        }
        else if (rot > 270 && rot <= 315)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 315);

            _rewardManager.ClaimReward("Heal");

        }
        else if (rot > 315 + 22 && rot <= 360)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);

            _rewardManager.ClaimReward("Bomb");
        }
    }
    public void GoToMenu()
    {
        _buttonClick.Play();
        SceneManager.LoadScene("Main Menu");
    }
    public void Rotate()
    {
        _buttonClick.Play();
        _rotatePower = Random.Range(1000, 5000);
        _stopPower = Random.Range(100, 600);
        if (inRotate == 0)
        {
            _rigidBody.AddTorque(_rotatePower);
            inRotate = 1;
        }
    }




















}

