using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GUI.ViewModels.Windows
{
    public abstract class WindowViewModel : ViewModel
    {
        private Window window;
        private static readonly string WindowViewModelNamespace = typeof(WindowViewModel).Namespace;

        private static Type GetViewModelType( Type windowType )
        {
            string windowTypeName       = windowType.Name;
            string viewModelTypeName    = WindowViewModelNamespace + "." + windowTypeName + "ViewModel";

            return Type.GetType( viewModelTypeName );
        }

        public static Window CreateWindow( Type windowType )
        {
            Type viewModelType = GetViewModelType( windowType );
            if ( !typeof( WindowViewModel ).IsAssignableFrom( viewModelType ) )
                throw new InvalidCastException();

            WindowViewModel viewModel = (WindowViewModel)Activator.CreateInstance( viewModelType );
            viewModel.window = (Window)Activator.CreateInstance( windowType );

            viewModel.BindDataContext();

            return viewModel.window;
        }

        public static Window CreateWindow<TWindow>() where TWindow : Window
        {
            return CreateWindow( typeof( TWindow ) );
        }

        private void BindDataContext()
        {
            OnPreBindContext();
            window.DataContext = this;
            OnPostBindContext();
        }

        protected virtual void OnPreBindContext() { }
        protected virtual void OnPostBindContext() { }
    }
}
