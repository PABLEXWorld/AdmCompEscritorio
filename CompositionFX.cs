using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace AdmCompEscritorio
{
    static class CompositionFX
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct SIZE
        {
            public int cx;
            public int cy;

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }
        private struct Parametros
        {
            public string titulo;
            public IntPtr hIcon;
            public IntPtr hIconSm;
            public SIZE tamano;
            public int cCant;
            public IntPtr[] ventanas;
            public bool redimensionable;
            public bool aditivo;
            public bool modocompuesto;
            public bool barradetitulooscura;
            public bool mezclarcontenido;

            public Parametros(string titulo, IntPtr hIcon, IntPtr hIconSm, SIZE tamano, int cCant, IntPtr[] ventanas, bool redimensionable, bool aditivo, bool modocompuesto, bool barradetitulooscura, bool mezclarcontenido)
            {
                this.titulo = titulo;
                this.hIcon = hIcon;
                this.hIconSm = hIconSm;
                this.tamano = tamano;
                this.cCant = cCant;
                this.ventanas = ventanas;
                this.redimensionable = redimensionable;
                this.aditivo = aditivo;
                this.modocompuesto = modocompuesto;
                this.barradetitulooscura = barradetitulooscura;
                this.mezclarcontenido = mezclarcontenido;
            }
        }
        [DllImport("CompositionFX.dll", EntryPoint = "CrearVentanadeCompositor", CharSet = CharSet.Unicode)]
        private static extern void CrearVentanadeCompositor32(string titulo, IntPtr hIcon, IntPtr hIconSm, SIZE tamano, int cCant, IntPtr[] ventanas, bool redimensionable, bool aditivo, bool modocompuesto, bool barradetitulooscura, bool mezclarcontenido);
        [DllImport("CompositionFX64.dll", EntryPoint = "CrearVentanadeCompositor", CharSet = CharSet.Unicode)]
        private static extern int CrearVentanadeCompositor64(string titulo, IntPtr hIcon, IntPtr hIconSm, SIZE tamano, int cCant, IntPtr[] ventanas, bool redimensionable, bool aditivo, bool modocompuesto, bool barradetitulooscura, bool mezclarcontenido);
        internal static void CrearVentanadeCompositor(string titulo, IntPtr hIcon, IntPtr hIconSm, SIZE tamano, int cCant, IntPtr[] ventanas, bool redimensionable, bool aditivo, bool modocompuesto, bool barradetitulooscura, bool mezclarcontenido)
        {
            Thread th = new Thread(CrearVentanadeCompositor_proc);
            th.Start(new Parametros(titulo, hIcon, hIconSm, tamano, cCant, ventanas, redimensionable, aditivo, modocompuesto, barradetitulooscura, mezclarcontenido));
        }
        private static void CrearVentanadeCompositor_proc(object data)
        {
            Parametros parametros = (Parametros)data;
            if (IntPtr.Size > 4)
                CrearVentanadeCompositor64(parametros.titulo, parametros.hIcon, parametros.hIconSm, parametros.tamano, parametros.cCant, parametros.ventanas, parametros.redimensionable, parametros.aditivo, parametros.modocompuesto, parametros.barradetitulooscura, parametros.mezclarcontenido);
            else
                CrearVentanadeCompositor32(parametros.titulo, parametros.hIcon, parametros.hIconSm, parametros.tamano, parametros.cCant, parametros.ventanas, parametros.redimensionable, parametros.aditivo, parametros.modocompuesto, parametros.barradetitulooscura, parametros.mezclarcontenido);
        }
    }
}
