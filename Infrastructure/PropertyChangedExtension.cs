using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;

namespace WPF_DB_Person
{
    public static class PropertyChangedExtension
    {
        public static IObservable<T> OnChanged<T>(this INotifyPropertyChanged propertyChangedBase, Expression<Func<T>> propertyExtension)
        {
            var memberExpression = (MemberExpression)propertyExtension.Body;
            var expression = memberExpression.Expression;
            var property = (PropertyInfo)memberExpression.Member;
            var constantExpression = (ConstantExpression)expression;
            var propertyName = property.Name;
            return Observable
                .FromEventPattern<PropertyChangedEventArgs>(propertyChangedBase, "PropertyChanged")
                .Where(prop => prop.EventArgs.PropertyName == propertyName)
                .Select(_ => property.GetValue(constantExpression.Value, null))
                .DistinctUntilChanged()
                .Cast<T>();
        }

        public static IObservable<bool> HasChanged<T>(this INotifyPropertyChanged propertyChangedBase, Expression<Func<T>> propertyExtension)
        {
            var memberExpression = (MemberExpression)propertyExtension.Body;
            var property = (PropertyInfo)memberExpression.Member;
            return Observable
                .FromEventPattern<PropertyChangedEventArgs>(propertyChangedBase, "PropertyChanged")
                .Where(prop => prop.EventArgs.PropertyName == property.Name)
                .Select(_ => true);
        }
    }
}
