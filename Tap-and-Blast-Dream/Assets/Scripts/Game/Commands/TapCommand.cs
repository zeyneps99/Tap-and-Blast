

public class TapCommand : Command
{
    private Blastable _blastable;
    public TapCommand(Blastable blastable)
    {
        _blastable = blastable;
    }
    public override void Execute()
    {
        if (_blastable != null)
        {
            Events.GameEvents.OnPlayerInput?.Invoke(_blastable);
        }
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }
}
