namespace ProjectHelix.Core
{
    class Timer
    {
        private int _maxTime;      //How long the timer will run till
        private int _currentTime;  //The current recorded time
        private bool  _startTimer; //Can the timer start now?

        //Accessor property to display whether the timer is currently running or not
        public bool IsStopped
        {
            get { return !_startTimer; }
        }

        //Constructor sets target time and resets values
        public Timer(int time)
        {
            _maxTime = time;
            _startTimer = false;
            _currentTime = 0;
        }

        //Checks whether the current time is more than the target time
        public bool IsTimeUp()
        {
            return _currentTime >= _maxTime;
        }

        //Updates the timer with the elapsed time and returns whether the timer has reached its target or not
        public bool IsTimeUp(int elapsedTime)
        {
            if (!_startTimer) 
                return false;
            
            _currentTime += elapsedTime;

            return _currentTime >= _maxTime;
        }

        //Calls Restart. Helper function as Restarting the first time makes no logical sense
        public void Start()
        {
            Restart();
        }

        //Calls Restart. Helper function as Restarting the first time makes no logical sense
        public void Start(int time)
        {
            Restart(time);
        }

        //Stops the Timer and resets it to 0
        public void Stop()
        {
            _currentTime = 0;
            _startTimer = false;
        }

        //Pauses and Unpauses the Timer
        public void TogglePause()
        {
            _startTimer = !_startTimer;
        }

        //Resets the Timer to 0 and starts it again
        public void Restart()
        {
            _startTimer = true;
            _currentTime = 0;
        }

        //Resets the Timer to 0 and starts it again with a new target time
        public void Restart(int time)
        {
            _maxTime = time;

            Restart();
        }
    }
}
