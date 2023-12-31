#include <wchar.h>
#include <stdlib.h>
#include <strsafe.h>
#include <windows.h>
#include <dwmapi.h>

#define BUF_SIZE 20
/*#define WCA_ACCENT_POLICY 19

enum AccentState
{
	ACCENT_DISABLED = 0,
	ACCENT_ENABLE_GRADIENT = 1,
	ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
	ACCENT_ENABLE_BLURBEHIND = 3,
	ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
	ACCENT_INVALID_STATE = 5
};

struct ACCENTPOLICY
{
	AccentState AccentState;
	INT AccentFlags;
	INT GradientColor;
	INT AnimationId;
};

struct WINCOMPATTRDATA
{
	DWORD attribute;
	ACCENTPOLICY *pData;
	ULONG dataSize;
};*/

BOOL CALLBACK TemaClasico(HWND hWnd, LPARAM lParam) {
	LPCWSTR id = (lParam == TRUE ? L" " : NULL);
	SetWindowTheme(hWnd, id, id);
	EnumChildWindows(hWnd, TemaClasico, lParam);
	return TRUE;
}

BOOL CALLBACK RefrescarVentana(HWND hWnd, LPARAM lParam) {
	InvalidateRect(hWnd, NULL, true);
	InvalidateRgn(hWnd, NULL, true);
	PostMessage(hWnd, WM_THEMECHANGED, NULL, NULL);
	PostMessage(hWnd, WM_ERASEBKGND, NULL, NULL);
	PostMessage(hWnd, WM_NCPAINT, NULL, NULL);
	PostMessage(hWnd, WM_PAINT, NULL, NULL);
	EnumChildWindows(hWnd, RefrescarVentana, 1);
	return TRUE;
}

HWND hWnd;
BOOL activar;
HINSTANCE hInstance = nullptr;

bool __stdcall FuncionACE3BEA5EAD7AA86494FAC6CB6740BED6F91() {
	DWORD resultFlags = GetThemeAppProperties();
	if (((resultFlags & STAP_ALLOW_NONCLIENT) != 0) | ((resultFlags & STAP_ALLOW_CONTROLS) != 0))
	{
		FreeLibraryAndExitThread(hInstance, true);
		return true;
	}
	else
	{
		FreeLibraryAndExitThread(hInstance, false);
		return false;
	}
}

bool __stdcall FuncionCLOAK3BEA5EAD7AA86494FAC6CB6740BED6F91() {
	HRESULT hr = DwmSetWindowAttribute(hWnd, DWMWA_CLOAK, &activar, sizeof(activar));
	FreeLibraryAndExitThread(hInstance, true);
}

bool __stdcall DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpReserved)
{
	if (fdwReason == DLL_PROCESS_ATTACH)
	{
		hInstance = hinstDLL;
		HANDLE hMapFile;
		LPWSTR pBuf;

		hMapFile = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, L"ComandosACEDLL_{BEA5EAD7-AA86-494F-AC6C-B6740BED6F91}");

		if (hMapFile == NULL)
		{
			return false;
		}

		pBuf = (LPWSTR)MapViewOfFile(hMapFile, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(LPWSTR));

		if (pBuf == NULL)
		{
			return false;
		}

		LPWSTR buff1;
		LPWSTR pedazo = wcstok_s(pBuf, L" ", &buff1);
		int i = 0;
		int Fn = -1;
		while (pedazo)
		{
			switch (i) {
			case 0:
				Fn = _wtoi(pedazo);
				break;
			case 1:
				activar = _wtoi(pedazo) == 1;
				break;
			case 2:
#ifdef _WIN64
				hWnd = (HWND)_wtoll(pedazo);
#else
				hWnd = (HWND)_wtoi(pedazo);
#endif //_WIN64
				break;
			}
			pedazo = wcstok_s(NULL, L" ", &buff1);
			i++;
		}
		UnmapViewOfFile(pBuf);
		pBuf = NULL;

		CloseHandle(hMapFile);
		hMapFile = NULL;

		WCHAR Ret[BUF_SIZE];
		bool retb = false;
		int Rets = 0;
		BOOL retv;
		/*typedef BOOL(WINAPI*pfnGWA)(HWND, WINCOMPATTRDATABEA5EAD7AA86494FAC6CB6740BED6F91*);
		HMODULE hMod = LoadLibrary(L"user32.dll");
		pfnGWA GetWindowCompositionAttribute = (pfnGWA)GetProcAddress(hMod, "GetWindowCompositionAttribute");
		ACCENTPOLICYBEA5EAD7AA86494FAC6CB6740BED6F91 pData;
		WINCOMPATTRDATABEA5EAD7AA86494FAC6CB6740BED6F91 attrData = { WCA_ACCENT_POLICYBEA5EAD7AA86494FAC6CB6740BED6F91, &pData, sizeof(pData) };*/
		DWORD data;
		FARPROC test = GetProcAddress(hinstDLL, "FuncionACE3BEA5EAD7AA86494FAC6CB6740BED6F91");
		switch (Fn) {
		case 0:
			SetThemeAppProperties(activar ? NULL : (STAP_ALLOW_NONCLIENT | STAP_ALLOW_CONTROLS | STAP_ALLOW_WEBCONTENT));
			TemaClasico(hWnd, activar);
			RefrescarVentana(hWnd, 0);
			break;
		case 1:
			retv = SetWindowDisplayAffinity(hWnd, activar);
			if (retv == 0)
			{
				StringCbPrintfW(Ret, BUF_SIZE * sizeof(WCHAR), L"%d %d", retv, GetLastError());
			}
			else
			{
				StringCbPrintfW(Ret, BUF_SIZE * sizeof(WCHAR), L"%d", retv);
			}
			break;
		case 2:
			retv = 0; //GetWindowCompositionAttribute(hWnd, &attrData)
			if (retv == 0)
			{
				StringCbPrintfW(Ret, BUF_SIZE * sizeof(WCHAR), L"%d %d", retv, GetLastError());
			}
			else
			{
				//StringCbPrintfW(Ret, BUF_SIZE * sizeof(WCHAR), L"%b", attrData.AccentState);
			}
			break;
		case 3:
			StringCbPrintfW(Ret, BUF_SIZE * sizeof(WCHAR), L"%p", test);
			retb = true;
			break;
		case 4:
			retv = GetWindowDisplayAffinity(hWnd, &data);
			if (retv == 0)
			{
				StringCbPrintfW(Ret, BUF_SIZE * sizeof(WCHAR), L"%d %d", retv, GetLastError());
			}
			else
			{
				StringCbPrintfW(Ret, BUF_SIZE * sizeof(WCHAR), L"%d %d", retv, data);
			}
			break;
		case 5:
			StringCbPrintfW(Ret, BUF_SIZE * sizeof(WCHAR), L"%p", GetProcAddress(hinstDLL, "FuncionCLOAK3BEA5EAD7AA86494FAC6CB6740BED6F91"));
			retb = true;
			break;
		}

		hMapFile = CreateFileMapping(INVALID_HANDLE_VALUE, NULL, PAGE_READWRITE, 0, BUF_SIZE * sizeof(WCHAR), L"RetornoACEDLL_{BEA5EAD7-AA86-494F-AC6C-B6740BED6F91}");

		if (hMapFile == NULL)
		{
			return false;
		}

		pBuf = (LPWSTR)MapViewOfFile(hMapFile, FILE_MAP_ALL_ACCESS, 0, 0, BUF_SIZE * sizeof(WCHAR));

		if (pBuf == NULL)
		{
			CloseHandle(hMapFile);
			return false;
		}

		wmemcpy_s(pBuf, BUF_SIZE * sizeof(WCHAR), Ret, BUF_SIZE * sizeof(WCHAR));

		UnmapViewOfFile(pBuf);

		return retb;
	}
	
}