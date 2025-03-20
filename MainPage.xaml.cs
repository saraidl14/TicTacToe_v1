namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {
        // Indica si es turno de X (false) o de O (true)
        bool turnoO = false;

        // Variables para controlar la puntuación
        int addOneToScore = 0;

        // Variable para contar movimientos y detectar empates
        int movimientosRealizados = 0;

        /// <summary>
        /// Constructor de la página principal
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            ActualizarIndicadorTurno();
        }

        /// <summary>
        /// Actualiza el texto que indica de quién es el turno actual
        /// </summary>
        private void ActualizarIndicadorTurno()
        {
            if (!turnoO)
            {
                LabelX.Text = "Jugador X (Turno)";
                LabelX.TextColor = Colors.White;
                LabelO.Text = "Jugador O:";
                LabelO.TextColor = Colors.White;
            }
            else
            {
                LabelX.Text = "Jugador X:";
                LabelX.TextColor = Colors.White;
                LabelO.Text = "Jugador O (Turno)";
                LabelO.TextColor = Colors.White;
            }
        }

        /// <summary>
        /// Deshabilita todos los botones del tablero
        /// </summary>
        void DeshabilitarBotones()
        {
            Button1.IsEnabled = false;
            Button2.IsEnabled = false;
            Button3.IsEnabled = false;
            Button4.IsEnabled = false;
            Button5.IsEnabled = false;
            Button6.IsEnabled = false;
            Button7.IsEnabled = false;
            Button8.IsEnabled = false;
            Button9.IsEnabled = false;
        }

        /// <summary>
        /// Verifica si hay un ganador en el tablero
        /// </summary>
        void VerificarGanador()
        {
            // Color para resaltar la línea ganadora
            Color colorGanador = Colors.Black;
            bool hayGanador = false;

            // Verificar filas, columnas y diagonales para X
            if (VerificarLinea("X", Button1, Button2, Button3, colorGanador) ||
                VerificarLinea("X", Button4, Button5, Button6, colorGanador) ||
                VerificarLinea("X", Button7, Button8, Button9, colorGanador) ||
                VerificarLinea("X", Button1, Button4, Button7, colorGanador) ||
                VerificarLinea("X", Button2, Button5, Button8, colorGanador) ||
                VerificarLinea("X", Button3, Button6, Button9, colorGanador) ||
                VerificarLinea("X", Button1, Button5, Button9, colorGanador) ||
                VerificarLinea("X", Button3, Button5, Button7, colorGanador))
            {
                DisplayAlert("¡Tenemos un ganador!", "Jugador X ha ganado esta ronda", "OK");
                addOneToScore = int.Parse(LabelXScore.Text);
                LabelXScore.Text = (addOneToScore + 1).ToString();
                DeshabilitarBotones();
                hayGanador = true;

                // Automáticamente limpiar el tablero después de 2 segundos
                Dispatcher.Dispatch(async () => {
                    await Task.Delay(2000);
                    Reset();
                });
            }
            // Verificar filas, columnas y diagonales para O
            else if (VerificarLinea("O", Button1, Button2, Button3, colorGanador) ||
                    VerificarLinea("O", Button4, Button5, Button6, colorGanador) ||
                    VerificarLinea("O", Button7, Button8, Button9, colorGanador) ||
                    VerificarLinea("O", Button1, Button4, Button7, colorGanador) ||
                    VerificarLinea("O", Button2, Button5, Button8, colorGanador) ||
                    VerificarLinea("O", Button3, Button6, Button9, colorGanador) ||
                    VerificarLinea("O", Button1, Button5, Button9, colorGanador) ||
                    VerificarLinea("O", Button3, Button5, Button7, colorGanador))
            {
                DisplayAlert("¡Tenemos un ganador!", "Jugador O ha ganado esta ronda", "OK");
                addOneToScore = int.Parse(LabelOScore.Text);
                LabelOScore.Text = (addOneToScore + 1).ToString();
                DeshabilitarBotones();
                hayGanador = true;

                // Automáticamente limpiar el tablero después de 2 segundos
                Dispatcher.Dispatch(async () => {
                    await Task.Delay(2000);
                    Reset();
                });
            }

            // Verificar empate (si no hay ganador y se han realizado 9 movimientos)
            if (!hayGanador && movimientosRealizados >= 9)
            {
                DisplayAlert("¡Empate!", "No hay ganadores en esta ronda", "OK");
                // Automáticamente limpiamos el tablero después de un empate
                Dispatcher.Dispatch(async () => {
                    await Task.Delay(1500);
                    Reset();
                });
            }
        }

        /// <summary>
        /// Verifica si tres botones forman una línea ganadora
        /// </summary>
        /// <param name="simbolo">Símbolo a verificar (X u O)</param>
        /// <param name="b1">Primer botón</param>
        /// <param name="b2">Segundo botón</param>
        /// <param name="b3">Tercer botón</param>
        /// <param name="colorGanador">Color para resaltar la línea ganadora</param>
        /// <returns>True si hay una línea ganadora, False en caso contrario</returns>
        private bool VerificarLinea(string simbolo, Button b1, Button b2, Button b3, Color colorGanador)
        {
            if (b1.Text == simbolo && b2.Text == simbolo && b3.Text == simbolo)
            {
                b1.BackgroundColor = colorGanador;
                b2.BackgroundColor = colorGanador;
                b3.BackgroundColor = colorGanador;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Maneja el evento de clic en un botón del tablero
        /// </summary>
        public void ButtonClicked(object sender, EventArgs e)
        {
            Button b = sender as Button;

            // Si el botón ya ha sido pulsado, no hacer nada
            if (!string.IsNullOrEmpty(b.Text))
                return;

            if (!turnoO)
            {
              //  b.Text = "X"; // Mantener para la lógica del juego
                b.TextColor = Colors.Transparent; // Hacer el texto invisible
                b.ImageSource = ImageSource.FromFile("img_x.png");
            }
            else
            {
             //   b.Text = "O"; // Mantener para la lógica del juego
                b.TextColor = Colors.Transparent; // Hacer el texto invisible
                b.ImageSource = ImageSource.FromFile("img_o.png");
            }

            // Cambiar el turno
            turnoO = !turnoO;

            // Actualizar el indicador de turno
            ActualizarIndicadorTurno();

            // Incrementar contador de movimientos
            movimientosRealizados++;

            // Verificar si hay un ganador
            VerificarGanador();

            // Deshabilitar el botón para que no se pueda volver a pulsar
            b.IsEnabled = false;
        }

        /// <summary>
        /// Reinicia el tablero para una nueva ronda
        /// </summary>
        public void Reset()
        {
            // Habilitar todos los botones
            Button1.IsEnabled = true;
            Button2.IsEnabled = true;
            Button3.IsEnabled = true;
            Button4.IsEnabled = true;
            Button5.IsEnabled = true;
            Button6.IsEnabled = true;
            Button7.IsEnabled = true;
            Button8.IsEnabled = true;
            Button9.IsEnabled = true;

            // Limpiar el texto de todos los botones
            Button1.Text = "";
            Button2.Text = "";
            Button3.Text = "";
            Button4.Text = "";
            Button5.Text = "";
            Button6.Text = "";
            Button7.Text = "";
            Button8.Text = "";
            Button9.Text = "";

            // Restaurar las imágenes
            Button1.ImageSource = null;
            Button2.ImageSource = null;
            Button3.ImageSource = null;
            Button4.ImageSource = null;
            Button5.ImageSource = null;
            Button6.ImageSource = null;
            Button7.ImageSource = null;
            Button8.ImageSource = null;
            Button9.ImageSource = null;

            // Restaurar el color de fondo
            Button1.BackgroundColor = Color.FromArgb("#8A2BE2"); // BlueViolet
            Button2.BackgroundColor = Color.FromArgb("#8A2BE2");
            Button3.BackgroundColor = Color.FromArgb("#8A2BE2");
            Button4.BackgroundColor = Color.FromArgb("#8A2BE2");
            Button5.BackgroundColor = Color.FromArgb("#8A2BE2");
            Button6.BackgroundColor = Color.FromArgb("#8A2BE2");
            Button7.BackgroundColor = Color.FromArgb("#8A2BE2");
            Button8.BackgroundColor = Color.FromArgb("#8A2BE2");
            Button9.BackgroundColor = Color.FromArgb("#8A2BE2");

            // Reiniciar el contador de movimientos
            movimientosRealizados = 0;

            // Ponemos las imagenes en nulo
            Button1.ImageSource = null;
            Button2.ImageSource = null;

            // Restablecer el turno a X
            turnoO = false;
            ActualizarIndicadorTurno();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón de reiniciar partida
        /// </summary>
        void ButtonResetClick(object sender, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón de nueva partida
        /// </summary>
        void ButtonNewGameClick(object sender, EventArgs e)
        {
            Reset();
            LabelXScore.Text = "0";
            LabelOScore.Text = "0";
        }

        /// <summary>
        /// Muestra un diálogo de confirmación para salir del juego
        /// </summary>
        async Task exit()
        {
            bool answer = await DisplayAlert("¿Salir?", "¿Deseas salir del juego?", "Sí", "No");
            if (answer)
            {
                Application.Current?.CloseWindow(Application.Current.MainPage.Window);
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón de salir
        /// </summary>
        void ButtonExitClick(object sender, EventArgs e)
        {
            exit();
        }
    }
}