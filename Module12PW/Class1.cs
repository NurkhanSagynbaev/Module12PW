using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module12PW
{
    // Делегат для события изменения свойства
    public delegate void PropertyEventHandler(object sender, PropertyChangedEventArgs e);

    // Аргументы события изменения свойства
    public class PropertyChangedEventArgs : EventArgs
    {
        public string PropertyName { get; }

        public PropertyChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    // Интерфейс для уведомления об изменении свойства
    public interface IPropertyChanged
    {
        event PropertyEventHandler PropertyChanged;
    }

    // Класс, реализующий интерфейс IPropertyChanged
    public class MyClass : IPropertyChanged
    {
        private string myProperty;

        public string MyProperty
        {
            get { return myProperty; }
            set
            {
                if (myProperty != value)
                {
                    myProperty = value;
                    OnMyPropertyChanged(nameof(MyProperty));
                }
            }
        }

        // Событие изменения свойства
        public event PropertyEventHandler PropertyChanged;

        // Метод для вызова события изменения свойства
        protected virtual void OnMyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class Program
    {
        static void Main()
        {
            MyClass myObject = new MyClass();

            // Подписываемся на событие изменения свойства
            myObject.PropertyChanged += HandlePropertyChange;

            // Изменяем свойство, чтобы вызвать событие
            myObject.MyProperty = "Новое значение";
        }

        // Обработчик события изменения свойства
        static void HandlePropertyChange(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"Свойство {e.PropertyName} изменилось.");
        }
    }

}
