namespace StartBlazor.Helpers
{
    public class SharedStateService
    {
        public event Action OnChange;
        private int _totalShoppingCartCount;

        public int TotalShoppingCartCount
        {
            get => _totalShoppingCartCount;
            set
            {
                _totalShoppingCartCount = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
