using UnityEngine;
using System.Collections.Generic;
using ScriptableArchitecture.Data;

public class PartSettingsWindow : MonoBehaviour
{
    private PartData tempPartData; // temporarily testing
    private List<GameObject> _fieldPrefabs;
    private List<PartSetting> _settings;
    private PartData.PartType _partType;
    private Transform _windowContext;
    public static (PartData.PartType partType, List<PartSetting> settings) LastCopiedSettings;
    
    public void ReceivePartTypeAndSettings(PartData.PartType t, List<PartSetting> s)
    {
        _partType = t;
        _settings ??= s;
    }
    
    public void SetPartData(PartData partData)
    {
        tempPartData = partData;
        _settings = tempPartData.Settings; // temporarily testing
        //Clear();
        InitWindow();
    }

    public void Clear()
    {
        if (_windowContext != null)
            foreach(Transform child in _windowContext)
                Destroy(child.gameObject);
    }
    
    void Start()
    {
        _fieldPrefabs = GetComponent<Prefabs>().prefabs;
        _windowContext = transform.GetChild(0).transform;
        //InitWindow();
    }

    private void InitWindow()
    {
        Clear();

        foreach (var setting in _settings)
        {
            switch (setting.VariableType)
            {
                case PartSetting.SettingType.Bool:
                    Instantiate(_fieldPrefabs[0], _windowContext).AddComponent<BoolControl>().ReceiveSetting(setting);
                    break;
                case PartSetting.SettingType.Int:
                    Instantiate(_fieldPrefabs[1], _windowContext).AddComponent<IntControl>().ReceiveSetting(setting);
                    break;
                case PartSetting.SettingType.ClampedInt:
                    Instantiate(_fieldPrefabs[1], _windowContext).AddComponent<ClampedIntControl>().ReceiveSetting(setting);
                    break;
                case PartSetting.SettingType.Float:
                    Instantiate(_fieldPrefabs[1], _windowContext).AddComponent<FloatControl>().ReceiveSetting(setting);
                    break;
                case PartSetting.SettingType.ClampedFloat:
                    Instantiate(_fieldPrefabs[1], _windowContext).AddComponent<ClampedFloatControl>().ReceiveSetting(setting);
                    break;
                case PartSetting.SettingType.Vector3:
                    Instantiate(_fieldPrefabs[2], _windowContext).AddComponent<Vector3Control>().ReceiveSetting(setting);
                    break;
                case PartSetting.SettingType.Vector3Int:
                    Instantiate(_fieldPrefabs[2], _windowContext).AddComponent<Vector3IntControl>().ReceiveSetting(setting);
                    break;
                case PartSetting.SettingType.ClampedVector3Int:
                    Instantiate(_fieldPrefabs[2], _windowContext).AddComponent<ClampedVector3IntControl>().ReceiveSetting(setting);
                    break;
            }
        }
    }
    
}
