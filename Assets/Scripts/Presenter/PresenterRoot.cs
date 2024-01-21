namespace Presenter
{
    public class PresenterRoot : LocalPlayerUIPresenter
    {
        public void Initialize()
        {
            var presenters = GetComponentsInChildren<IUIPresenter>();
            foreach (var presenter in presenters)
            {
                presenter.Initialize();
            }
        }
    }
}
