using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moveCount;
    [SerializeField] private Transform _goalContainer;
    [SerializeField] private GameObject _goalItemPrefab;

    [SerializeField] private FailPopUp _failPopUp;
    [SerializeField] private CelebrationPanel _celebrationPanel;

    private Dictionary<ObstacleTypes, GoalItem> _goalItems;

    private LevelInfo _levelInfo;

    public void Init(LevelInfo levelInfo)
    {
        _levelInfo = levelInfo;
        _goalItems = new Dictionary<ObstacleTypes, GoalItem>();
        SetMoves(levelInfo.MoveCount);

        if (_goalContainer != null && _goalContainer.GetComponentsInChildren<GoalItem>() != null)
        {
            foreach (GoalItem item in _goalContainer.GetComponentsInChildren<GoalItem>())
            {
                DestroyImmediate(item.gameObject);
            }
        }

        SetGoal(levelInfo.Goal);
        SetFailPopUp();
        SetCelebrationPanel();
    }

    public void SetMoves(int moveCount)
    {
        if (_moveCount != null)
        {
            _moveCount.text = moveCount.ToString();
        }
    }

    public void SetGoal(Dictionary<ObstacleTypes, int> goal)
    {
        if (goal == null || _goalItems == null)
        {
            return;
        }



        foreach (ObstacleTypes type in goal.Keys)
        {
            if (goal[type] <= 0 && _goalItems.ContainsKeySafe(type))
            {
                if (_goalItems[type] != null)
                {
                    DestroyImmediate(_goalItems[type].gameObject);
                }
                _goalItems.Remove(type);
            }

            if (goal[type] > 0 && !_goalItems.ContainsKeySafe(type))
            {
                GameObject go = Instantiate(_goalItemPrefab, _goalContainer);
                if (go.TryGetComponent(out GoalItem item))
                {
                    item.Set((int)type, goal[type]);
                    _goalItems.Add(type, item);
                }
            }
            else if (goal[type] > 0)
            {
                _goalItems[type].Set((int)type, goal[type]);
            }
        }
    }



    private void SetFailPopUp()
    {
        if (_failPopUp != null)
        {
            _failPopUp.Display(false);
            _failPopUp.Init();
            _failPopUp.gameObject.SetActive(false);
        }
    }

    private void SetCelebrationPanel()
    {
        if (_celebrationPanel != null)
        {
            _celebrationPanel.Init();
            _celebrationPanel.gameObject.SetActive(false);
        }
    }

    public void DisplayFailPopUp(bool isDisplay)
    {
        if (_failPopUp != null)
        {
            _failPopUp.Display(isDisplay);
            _failPopUp.gameObject.SetActive(isDisplay);
            _failPopUp.Animate();
        }
    }

    public void DisplayCelebrationPanel()
    {
        if (_celebrationPanel != null)
        {
            _celebrationPanel.Init();
            _celebrationPanel.gameObject.SetActive(true);
            _celebrationPanel.Display();
        }
    }




}
