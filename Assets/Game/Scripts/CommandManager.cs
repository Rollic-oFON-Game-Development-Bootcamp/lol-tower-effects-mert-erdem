using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private static CommandManager instance;
    public static CommandManager Instance => instance ?? (instance = FindObjectOfType<CommandManager>());

    private Queue<Command> queuedCommands = new Queue<Command>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (queuedCommands.Count > 0)
        {
            Command cmd = queuedCommands.Dequeue();
            ExecuteCommand(cmd);
        }
    }

    public void IssueCommand(Vector2 direction)
    {
        Command cmd = new Command();
        cmd.Direction = direction;
        queuedCommands.Enqueue(cmd);
    }

    private void ExecuteCommand(Command cmd)
    {
        ObjectFactory.Instance.SpawnObject(cmd.Direction);
    }
}

public class Command
{
    public Vector2 Direction;
}
