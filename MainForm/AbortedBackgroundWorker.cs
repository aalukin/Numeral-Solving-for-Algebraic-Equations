using System.ComponentModel;
using System.Threading;

namespace MainForm
{
    /// <summary>
    /// Фоновый поток с возможностью независимого прерывания
    /// </summary>
    class AbortedBackgroundWorker : BackgroundWorker
    {
        /// <summary>
        /// Поток 
        /// </summary>
        private Thread workThread;

        /// <summary>
        /// Создание события OnWork
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            workThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch
            {
                e.Cancel = true;
                Thread.ResetAbort();
            }
        }

        /// <summary>
        /// Прерывание потока
        /// </summary>
        public void Abort()
        {
            if (workThread != null)
            {
                workThread.Abort();
                workThread = null;
            }
        }
    }
}
