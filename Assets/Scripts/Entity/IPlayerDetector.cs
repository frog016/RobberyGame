namespace Entity
{
    public interface IPlayerDetector
    {
        void DetectPlayer(Character character);
        void UnDetectPlayer(Character character);
    }
}