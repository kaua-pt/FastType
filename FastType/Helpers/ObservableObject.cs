/* [grial-metadata] id: Grial#ObservableObject.cs version: 1.0.1 */
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using UXDivers.Grial;

namespace FastType;

/// <summary>
/// Simple implementation of INotifyPropertyChanged.
/// </summary>
public class ObservableObject : CommunityToolkit.Mvvm.ComponentModel.ObservableObject, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private readonly CultureChangeNotifier _notifier;

    public ObservableObject(bool listenCultureChanges = false)
    {
        if (listenCultureChanges)
        {
            // Listen culture changes so they can be handled 
            // by derived viewmodels if needed
            _notifier = new CultureChangeNotifier();
            _notifier.CultureChanged += CultureChanged;
        }
    }

    protected void NotifyAllPropertiesChanged()
    {
        NotifyPropertyChanged(null);
    }

    protected bool SetProperty<T>(
        ref T backingStore,
        T value,
        [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
        {
            return false;
        }

        backingStore = value;
        NotifyPropertyChanged(propertyName);

        return true;
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnCultureChanged(CultureInfo culture)
    {
    }

    private void CultureChanged(object sender, CultureChangeEventArgs args)
    {
        OnCultureChanged(args.NewCulture);
    }
}