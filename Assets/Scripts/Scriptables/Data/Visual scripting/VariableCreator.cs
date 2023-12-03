using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VariableCreator : MonoBehaviour
{
    //BaseVariables
    [SerializeField] private GetBoolVariable _defaultBoolVariable;
    [SerializeField] private GetNumberVariable _defaultNumberVariable;

    //Prefabs
    [SerializeField] private GameObject _variablePrefab;

    //Creator components
    [SerializeField] private TMP_Dropdown _componentVariableType;
    [SerializeField] private TMP_InputField _componentVariableName;
    [SerializeField] private TMP_InputField _componentNumberValue;
    [SerializeField] private Toggle _componentConditionValue;

    //Current variable values
    private VariableType _variableType;
    private string _variableName;
    private float _numberValue;
    private bool _conditionValue;

    public List<BaseGet> DEBUGLIST = new List<BaseGet>();
    private Dictionary<string, BaseGet> _variables = new Dictionary<string, BaseGet>();

    private enum VariableType
    {
        Condition,
        Number
    }

    private void Start()
    {
        SetVariableType(0);
        ResetValues();
    }

    public void CreateNewVariable()
    {
        if (_variableName == null || _variableName == "")
            return;

        if (_variableType == VariableType.Condition)
        {
            //Create SO
            GetBoolVariable newConditionVariable = Instantiate(_defaultBoolVariable);
            newConditionVariable.BaseNodeName = _variableName; //Change baseName??
            newConditionVariable.name = _variableName;
            newConditionVariable.Value = _conditionValue;

            _variables[_variableName] = newConditionVariable;

            //Create object
            Instantiate(_variablePrefab, transform).GetComponent<DisplayGet>().InitializeAsVariable(_variableName, newConditionVariable);
        }
        else if (_variableType == VariableType.Number)
        {
            GetNumberVariable newNumberVariable = Instantiate(_defaultNumberVariable);
            newNumberVariable.BaseNodeName = _variableName; //Change baseName??
            newNumberVariable.name = _variableName;
            newNumberVariable.Value = _numberValue;

            _variables[_variableName] = newNumberVariable;

            //Create object
            Instantiate(_variablePrefab, transform).GetComponent<DisplayGet>().InitializeAsVariable(_variableName, newNumberVariable);
        }

        DEBUGLIST.Clear();
        foreach (var v in _variables.Values)
            DEBUGLIST.Add(v);

        ResetValues();
    }

    public void SetName(string variableName)
    {
        _variableName = variableName;
    }

    public void SetVariableType(int index)
    {
        switch(index)
        {
            case 0:
                _variableType = VariableType.Condition;
                _componentConditionValue.gameObject.SetActive(true);
                _componentNumberValue.gameObject.SetActive(false);
                break;
            case 1:
                _variableType = VariableType.Number;
                _componentConditionValue.gameObject.SetActive(false);
                _componentNumberValue.gameObject.SetActive(true);
                break;
        }
    }

    public void SetNumberValue(string value)
    {
        string inputString = value.Replace(',', '.');
        _componentNumberValue.text = inputString;

        _numberValue = float.Parse(inputString);
    }


    public void SetConditionValue(bool value)
    {
        _conditionValue = value;
    }

    public void ResetValues()
    {
        _componentVariableType.value = 0;
        _componentVariableName.text = "";
        _componentNumberValue.text = "0.0";
        _componentConditionValue.isOn = false;
    }
}