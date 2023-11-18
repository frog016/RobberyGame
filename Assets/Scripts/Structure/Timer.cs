namespace Structure
{
    public class Timer
    {
        public float Time { get; private set; }
        public float CurrentTime { get; private set; }

        public Timer(float time)
        {
            Time = time;
        }

        public bool Tick(float delta)
        {
            CurrentTime += delta;

            if (CurrentTime < Time)
                return false;

            CurrentTime -= Time;
            return true;
        }

        public void Reset()
        {
            CurrentTime = 0;
        }
    }
}
