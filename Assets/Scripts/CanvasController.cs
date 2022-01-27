using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    private int _starsFillAmount;

    [SerializeField] private GameObject[] _emptyStars;
    [SerializeField] private GameObject[] _filledStars;

    [SerializeField] private Sprite _emptyStar;
    [SerializeField] private Sprite _filledStar;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _chooseLevelScreen;
    [SerializeField] private GameObject _testUIScreen;

    [SerializeField] private Button buttonOk;

    private Button[] _buttons;
    [SerializeField] private Button[] _chooseLevelButtons;
    
    private bool _winScreenActive;
    
    private List<Image> buttonStars;
    // Start is called before the first frame update
    void Start()
    {
        buttonStars = new List<Image>();
    }

    private void OnEnable()
    {
        buttonOk.onClick.AddListener(ButtonOkPressed);
        /*for (int i = 0; i < _chooseLevelButtons.Length; i++)
        {
            _chooseLevelButtons[i].onClick.AddListener(delegate { ChooseLevelButtonPressed(i); });
        }*/
    }

    private void OnDisable()
    {
        buttonOk.onClick.RemoveListener(ButtonOkPressed);
        /*for (int i = 0; i < _chooseLevelButtons.Length; i++)
        {
            _chooseLevelButtons[i].onClick.RemoveAllListeners();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!_winScreenActive)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                ActivateWinPanel(1);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                ActivateWinPanel(2);
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                ActivateWinPanel(3);
            }
        }

    }

    private void ActivateWinPanel(int amount)
    {
        _winScreenActive = true;
        _winScreen.SetActive(true);
        for (int i = 0; i < _emptyStars.Length; i++)
        {
            _filledStars[i].SetActive(i < amount);
            _emptyStars[i].SetActive(i >= amount);
        }
    }

    private void ButtonOkPressed()
    {
        for (int i = 0; i < buttonStars.Count; i++)
        {
            if (_filledStars[i].activeSelf)
            {
                buttonStars[i].sprite = _filledStar;
            }
            /*else
            {
                buttonStars[i].sprite = _emptyStar;
            }*/
            //buttonStars[i].sprite = 
        }
        _winScreen.SetActive(false);
        _chooseLevelScreen.SetActive(true);
        _winScreenActive = false;
        buttonStars.Clear();
        
    }

    public void ChooseLevelButtonPressed(int i)
    {
        List<Transform> children = new List<Transform>();
        

        foreach (Transform child in _chooseLevelButtons[i].transform)
        {
            //Debug.Log(child.name);
            children.Add(child);
        }

        for (int j = 0; j < children.Count; j++)
        {
            buttonStars.Add(children[j].gameObject.GetComponent<Image>());
            //buttonStars.RemoveAt();
        }
        buttonStars.RemoveAt(0);
        Debug.Log(buttonStars.Count);
        //buttonStars = _chooseLevelButtons[i].transform.GetChild(1);
        //Debug.Log(buttonStars.Length);

        //Debug.Log(buttonStars.Length);
        _chooseLevelScreen.SetActive(false);
        _testUIScreen.SetActive(true);
    }
}
