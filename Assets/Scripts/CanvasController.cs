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

    [SerializeField] private CanvasGroup _chooseLevelCanvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        buttonStars = new List<Image>();
    }

    private void OnEnable()
    {
        buttonOk.onClick.AddListener(ButtonOkPressed);
    }

    private void OnDisable()
    {
        buttonOk.onClick.RemoveListener(ButtonOkPressed);
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
        }
        _winScreen.SetActive(false);
        _chooseLevelScreen.SetActive(true);
        _winScreenActive = false;
        _chooseLevelCanvasGroup.gameObject.SetActive(true);
        //buttonStars.Clear();
        
    }

    public void ChooseLevelButtonPressed(int i)
    {
        StartCoroutine(FadeOut(0.04f));
        
        List<Transform> children = new List<Transform>();
        
        foreach (Transform child in _chooseLevelButtons[i].transform)
        {
            children.Add(child);
        }

        if (buttonStars != null)
        {
            buttonStars.Clear();
        }
        else
        {
            buttonStars = new List<Image>();
        }
        for (int j = 0; j < children.Count; j++)
        {
            buttonStars.Add(children[j].gameObject.GetComponent<Image>());
        }
        buttonStars.RemoveAt(0);

        //_chooseLevelScreen.SetActive(false);
        _testUIScreen.SetActive(true);

        
    }

    IEnumerator FadeOut(float tick)
    {
        while (_chooseLevelCanvasGroup.alpha > 0)
        {
            _chooseLevelCanvasGroup.alpha -= tick;
            yield return null;
        }
        _chooseLevelCanvasGroup.alpha = 1f;
        _chooseLevelCanvasGroup.gameObject.SetActive(false);
    }
}
