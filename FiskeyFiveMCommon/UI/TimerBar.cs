using CitizenFX.Core;
using NativeUI;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CommonClient.UI
{
    public class TimerBar : BaseScript
    {
        public bool IsRunning { get; private set; }
        public string Text { get; set; }
        public float Time { get; }
        public float Completion
        {
            get
            {
                return Convert.ToSingle(DateTime.Now.TimeOfDay.TotalMilliseconds / _endTime.TimeOfDay.TotalMilliseconds);
            }
            set
            {
                if (_timerBar != null) _timerBar.Percentage = value;
            }
        }

        public TimerBar() { }

        public TimerBar(string text, float time = 0f, float completion = 0f)
        {
            Text = text;
            Time = time;

            CreateStuff();
            Completion = completion;
        }
        public TimerBar(string text, Color backgroundColor, Color forgroundColor, float completion = 0f)
        {
            Text = text;
            _background = backgroundColor;
            _foreground = forgroundColor;

            CreateStuff();
            Completion = completion;
        }

        /// <summary>
        /// Prepares a timer bar with custom colors
        /// </summary>
        /// <param name="text">The text for the timer bar</param>
        /// <param name="time">The time for the timer bar IN MILLISECONDS</param>
        /// <param name="backgroundColor"></param>
        /// <param name="forgroundColor"></param>
        public TimerBar(string text, float time, Color backgroundColor, Color forgroundColor)
        {
            Text = text;
            Time = time;
            _background = backgroundColor;
            _foreground = forgroundColor;

            CreateStuff();
        }

        /// <summary>
        /// Starts the bar
        /// </summary>
        public void Start()
        {
            if (_timerBar != null) TimerBarHandler.AddTimerBar(_timerBar);
            if (Time > 0f)
            {
                if (_fiber == null) return;
                IsRunning = true;
                _fiber.Start();
            }
        }

        /// <summary>
        /// Stops and disposes the bar
        /// </summary>
        public void Stop() => Dispose();


        // PRIVATE

        private BarTimerBar _timerBar;
        private Task _fiber;
        private Color _background;
        private Color _foreground;
        private DateTime _endTime = DateTime.MaxValue;

        /// <summary>
        /// Create all the timer stuff
        /// </summary>
        private void CreateStuff()
        {
            _fiber = new Task(Process);

            _timerBar = new BarTimerBar(Text)
            {
                BackgroundColor = _background,
                ForegroundColor = _foreground
            };
        }

        public void SetForegroundColor(Color color)
        {
            if (_timerBar != null) _timerBar.ForegroundColor = color;
        }

        private async void Process()
        {
            _endTime = DateTime.Now.AddMilliseconds(Time);

            while (DateTime.Now.CompareTo(_endTime) < 0)
            {
                await Delay(0);

                try
                {
                    _timerBar.Percentage = Completion;
                }
                catch (Exception ex)
                {
                    break;
                }

                if (Completion > 1) break;
            }

            OnCompleted?.Invoke(this);
            IsRunning = false;
            Dispose();
        }

        public void Dispose()
        {
            if (_fiber != null && !_fiber.IsCompleted) _fiber.Dispose();
            if (_timerBar != null) TimerBarHandler.RemoveTimerBar(_timerBar);
            IsRunning = false;
        }

        public event Completed OnCompleted;
        public delegate void Completed(TimerBar bar);
    }

    public class TimerBarHandler : BaseScript
    {
        private static TimerBarPool _timerPool = new TimerBarPool();

        public TimerBarHandler() => Tick += OnTick;

        public static bool DoesTimerBarExist(string name) => _timerPool.ToList().Any(t => t.Label == name);

        internal static void AddTimerBar(TimerBarBase timerBar)
        {
            if (_timerPool == null) _timerPool = new TimerBarPool();
            _timerPool.Add(timerBar);
        }

        internal static void RemoveTimerBar(TimerBarBase timerBar)
        {
            if (_timerPool.ToList().Any(t => t == timerBar)) _timerPool.Remove(timerBar);
        }

        internal async Task OnTick()
        {
            if (_timerPool != null)
            {
                _timerPool.Draw();
            }
        }
    }
}
