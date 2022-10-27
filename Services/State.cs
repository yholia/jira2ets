namespace jira2ets.Services
{
    public class State
    {
        private bool _isBusy;
        private bool _isDialogOpened;
        private string _dialogText;
        private string _dialogTitle;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                NotifyStateChanged();
            }
        }

        public string DialogText
        {
            get => _dialogText;
            set
            {
                _dialogText = value;
                NotifyStateChanged();
            }
        }

        public string DialogTitle
        {
            get => _dialogTitle;
            set
            {
                _dialogTitle = value;
                NotifyStateChanged();
            }
        }

        public bool IsDialogOpened
        {
            get => _isDialogOpened;
            set
            {
                _isDialogOpened = value;
                NotifyStateChanged();
            }
        }
        
        public bool IsAuthorized { get; set; }
        
        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}