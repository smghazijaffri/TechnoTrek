namespace SharedClass.Components.State
{
    public class AppState
    {
        public event Func<Task>? OnChange;

        private int _cartItemCount;

        private bool _isAuthorized;

        private bool _isCustomerAuthorized;

        private string _Username { get; set; }

        private string _Role { get; set; }

        public int CartItemCount
        {
            get => _cartItemCount;
            set
            {
                _cartItemCount = value;
                NotifyStateChanged();
            }
        }

        public bool Authorized
        {
            get => _isAuthorized;
            set
            {
                _isAuthorized = value;
                NotifyStateChanged();
            }
        }
        
        public bool CustomerAuthorized
        {
            get => _isCustomerAuthorized;
            set
            {
                _isCustomerAuthorized = value;
                NotifyStateChanged();
            }
        }

        public string Username
        {
            get => _Username;
            set
            {
                _Username = value;
                NotifyStateChanged();
            }
        }

        public string Role
        {
            get => _Role;
            set
            {
                _Role = value;
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
