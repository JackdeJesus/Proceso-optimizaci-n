using System;
using System.Windows.Controls;
using System.Windows;

namespace PoderJudicial.Views
{
    /// <summary>
    /// Host de "Registro de Copias" en modo creación. Aloja una instancia
    /// de <see cref="CopiasFormControl"/> y, tras guardar, prepara el
    /// siguiente folio para seguir capturando sin salir de la página
    /// (mismo comportamiento que tenía antes de extraer el formulario).
    /// </summary>
    public partial class RegistroCopias : Page
    {
        private readonly CopiasFormControl _control;

        public RegistroCopias()
        {
            InitializeComponent();

            _control = new CopiasFormControl();
            _control.GuardarClick += Control_GuardarClick;
            PanelFormulario.Children.Add(_control);
        }

        private void Control_GuardarClick(object sender, EventArgs e)
        {
            if (!_control.Validar(out string mensajeError))
            {
                MessageBox.Show(mensajeError, "Validación",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var registro = _control.ConstruirModelo();
                _control.PersistirModelo(registro);

                MessageBox.Show("Registro de copia guardado correctamente.", "Éxito",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _control.PrepararSiguienteRegistro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar:\n{ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
