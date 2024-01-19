namespace Scripts.UI
{
    public class RestartGameButton : ButtonComponent
    {
        protected override void HandleClick()
        {
            base.HandleClick();
            
            PressButton();
        }
    }
}