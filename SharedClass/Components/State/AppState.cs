namespace SharedClass.Components.State
{
    public class AppState
    {
        public event Func<Task>? OnChange;

        private int _cartItemCount;
        public int CartItemCount
        {
            get => _cartItemCount;
            set
            {
                _cartItemCount = value;
                NotifyStateChanged();
            }
        }

        private async void NotifyStateChanged()
        {
            if (OnChange != null)
            {
                foreach (var handler in OnChange.GetInvocationList())
                {
                    await ((Func<Task>)handler)();
                }
            }
        }
    }
}
