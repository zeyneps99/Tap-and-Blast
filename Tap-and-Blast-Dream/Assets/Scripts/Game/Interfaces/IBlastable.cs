

public interface IBlastable
{
    public abstract void TryBlast();
    public abstract bool CanBlastNeighbor(Blastable neighbor);
}
