using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using LocalERP.WinForm;
using System.IO;

namespace LocalERP.WinForm
{
    public class WaitFormService
    {
        /// <summary>
        /// 创建等待窗口并写入提示文字
        /// </summary>
        /// <param name="str">提示文字</param>
        public static void CreateWaitForm(string text)
        {
            WaitFormService.Instance.CreateForm(text);
        }

        public static void CloseWaitForm()
        {
            WaitFormService.Instance.CloseForm();
        }
        /// <summary>
        /// 提示文字
        /// </summary>
        /// <param name="text">提示文字</param>
        public static void SetWaitFormCaption(string text)
        {
            WaitFormService.Instance.SetFormCaption(text);
        }

        private static WaitFormService _instance;
        private static readonly Object syncLock = new Object();

        public static WaitFormService Instance
        {
            get
            {
                if (WaitFormService._instance == null)
                {
                    lock (syncLock)
                    {
                        if (WaitFormService._instance == null)
                        {
                            WaitFormService._instance = new WaitFormService();
                        }
                    }
                }
                return WaitFormService._instance;
            }
        }

        private WaitFormService()
        {
        }

        private Thread waitThread;
        private LoadingForm waitFM;
        private int needOpenNum = 0;

        public void CreateForm(string text)
        {
            File.AppendAllText("e:\\debug.txt", string.Format("open: call. needOpenNum={0}\r\n", needOpenNum));
            /*
            if (waitThread != null)
            {
                try
                {
                    //waitThread.Abort();
                    waitThread = null;
                    waitFM = null;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }*/
            if (needOpenNum == 0)
            {
                waitThread = new Thread(new ThreadStart(delegate()
                {
                    waitFM = new LoadingForm();
                    //System.Windows.Forms.Application.Run(waitFM);
                    waitFM.ShowDialog();
                    File.AppendAllText("e:\\debug.txt", string.Format("open: dialog show\r\n"));
                    needOpenNum++;
                }));
                waitThread.Start();
            }
            else
                needOpenNum++;
        }

        public void CloseForm()
        {
            File.AppendAllText("e:\\debug.txt", string.Format("close: call. needOpenNum={0}\r\n", needOpenNum));
            
            if (needOpenNum== 1)
            {
                try
                {
                    waitFM.SetText("close");
                    waitThread.Abort();
                    waitThread = null;
                    waitFM = null;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
            needOpenNum--;
            File.AppendAllText("e:\\debug.txt", string.Format("close: end\r\n"));
        }

        public void SetFormCaption(string text)
        {
            if (waitFM != null)
            {
                try
                {
                    waitFM.SetText(text);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
    }
}
