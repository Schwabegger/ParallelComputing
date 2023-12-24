namespace Simulator;

public interface IWorld
{
    void Initialize();
    WorldUpdate Update();
}