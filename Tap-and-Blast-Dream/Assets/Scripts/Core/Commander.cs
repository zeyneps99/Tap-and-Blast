using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    private Command _currentCommand;

    private void OnEnable()
    {
        Events.CoreEvents.OnCommand += ExecuteCommand;
    }

    private void OnDisable()
    {
        Events.CoreEvents.OnCommand -= ExecuteCommand;
    }
    public void ExecuteCommand(Command command)
    {
        _currentCommand = command;
        _currentCommand.Execute();
    }

    public void UndoCommand()
    {
        if (_currentCommand == null)
        {
            return;
        }
        _currentCommand.Undo();
        _currentCommand = null;
    }
}
