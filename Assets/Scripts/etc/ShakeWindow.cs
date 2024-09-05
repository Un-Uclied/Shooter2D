using System;
using System.Collections;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class WindowShaker : MonoBehaviour //Singleton<WindowShaker>
{
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string className, string windowName);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOZORDER = 0x0004;
    private const uint SWP_SHOWWINDOW = 0x0040;

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private IntPtr windowHandle;
    private Vector3 originalPosition;

    private void Start()
    {
        windowHandle = FindWindow(null, Application.productName);
        if (windowHandle == IntPtr.Zero)
        {
            Debug.LogError("윈도우 핸들 못 찾음");
            return;
        }

        else
        {
            Debug.LogError("윈도우 위치 못 가져옴");
        }
    }

    private void Update()
    {
        UpdateWindowPosition();
    }

    private void ShakeWindow(float pwr)
    {
        if (windowHandle == IntPtr.Zero) return;

        Vector3 newPosition = originalPosition + new Vector3(UnityEngine.Random.Range(-pwr, pwr), UnityEngine.Random.Range(-pwr, pwr), 0);
        SetWindowPos(windowHandle, IntPtr.Zero, (int)newPosition.x, (int)newPosition.y, 0, 0, SWP_NOSIZE | SWP_NOZORDER | SWP_SHOWWINDOW);
    }

    private void UpdateWindowPosition(){
        RECT rect;
        if (GetWindowRect(windowHandle, out rect))
        {
            originalPosition = new Vector3(rect.Left, rect.Top, 0);
        }
    }

    public void ShakeWindowForSeconds(float power, float time){
        float timer = time;
        StartCoroutine(ShakeWindowForTime());
        
        IEnumerator ShakeWindowForTime(){
            while(true){
                timer -= Time.deltaTime;
                if (timer <= 0) break;
                ShakeWindow(power);
                yield return null;
            }
        }
    }
}