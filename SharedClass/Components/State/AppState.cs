namespace SharedClass.Components.State
{
    public class AppState
    {
        public event Func<Task>? OnChange;

        private int _cartItemCount;
        private int _storageQuantity;
        private int _memoryQuantity;
        private bool _isAuthorized;
        private bool _isCustomerAuthorized;
        private string _username;
        private string _role;
        private string _motherboard;
        private string _processor;
        private string _memory;
        private string _storage;
        private string _gpu;
        private string _psu;
        private string _case;
        private string _cooler;

        public int CartItemCount
        {
            get => _cartItemCount;
            set
            {
                _cartItemCount = value;
                NotifyStateChanged();
            }
        }

        public int StorageQuantity
        {
            get => _storageQuantity;
            set
            {
                _storageQuantity = value;
                NotifyStateChanged();
            }
        }

        public int MemoryQuantity
        {
            get => _memoryQuantity;
            set
            {
                _memoryQuantity = value;
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
            get => _username;
            set
            {
                _username = value;
                NotifyStateChanged();
            }
        }

        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                NotifyStateChanged();
            }
        }

        public string Motherboard
        {
            get => _motherboard;
            set
            {
                _motherboard = value;
                NotifyStateChanged();
            }
        }

        public string Processor
        {
            get => _processor;
            set
            {
                _processor = value;
                NotifyStateChanged();
            }
        }

        public string Memory
        {
            get => _memory;
            set
            {
                _memory = value;
                NotifyStateChanged();
            }
        }

        public string Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                NotifyStateChanged();
            }
        }

        public string GPU
        {
            get => _gpu;
            set
            {
                _gpu = value;
                NotifyStateChanged();
            }
        }

        public string PSU
        {
            get => _psu;
            set
            {
                _psu = value;
                NotifyStateChanged();
            }
        }

        public string Case
        {
            get => _case;
            set
            {
                _case = value;
                NotifyStateChanged();
            }
        }

        public string Cooler
        {
            get => _cooler;
            set
            {
                _cooler = value;
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