namespace Managers.UIManager.Signals
{
    public struct OpenScreenSignal
    {
        public OpenScreenSignal(ScreenType type, bool hidePrev)
        {
            Type = type;
            HidePrevScreen = hidePrev;
            Context = null;
        }
    
        public OpenScreenSignal(ScreenType type, object context = null, bool hidePrev = false)
        {
            Type = type;
            Context = context;
            HidePrevScreen = hidePrev;
        }
    
        public ScreenType Type;
        public bool HidePrevScreen;
        public object Context;
    }
}