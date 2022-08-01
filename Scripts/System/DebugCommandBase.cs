using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DebugCommandBase
{
    private string _commandID;
    private string _commandDescription;
    private string _commandFormat;

    public string commandID { get { return _commandID; } } //call the command
    public string commandDescription { get { return _commandDescription; } } //explains what the command does
    public string commandFormat { get { return _commandFormat; } } //tell the user how to format the command

    public DebugCommandBase(string id, string description, string format)
    {
        _commandID = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action command;

    public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}
