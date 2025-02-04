using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;

    private BaseAction baseAction;
    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        textMeshPro.text = baseAction.GetActionName().ToUpper(); //fetches the name of every button and converts to upper case

        //button.onClick.AddListener(MoveActionBttn_Click);// can be done like this but using anonymous function for the sake of learning
        button.onClick.AddListener(() => //anonymous function
        {
            //code
            UnitSelector.Instance.SetSelectedAction(baseAction);
        });
    }

    //private void MoveActionBttn_Click()
    //{

    //}

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitSelector.Instance.GetSelectedAction();
        selectedGameObject.SetActive(selectedBaseAction == baseAction); //will set the border active when base action is same as selected
    }
}
