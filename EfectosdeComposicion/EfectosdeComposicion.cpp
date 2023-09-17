#include "EfectosdeComposicion.h"

Compositor::Compositor(LPCWSTR titulo, HICON hIcon, HICON hIconSm, SIZE tamano, int cCant, HWND* ventanas, bool redimensionable, bool aditivo, bool modocompuesto, bool barradetitulooscura, bool mezclarcontenido) :
	m_hVentanaPrincipal(NULL),
	nombreVentana(titulo),
	m_hIcon(hIcon),
	m_hIconSm(hIconSm),
	m_hVentanasSeleccionadas(ventanas),
	ventanaRedimensionable(redimensionable),
	tamVentana1({ 0, 0, tamano.cx, tamano.cy }),
	tamVentana2({ 0, 0, tamano.cx, tamano.cy }),
	aditivo(aditivo),
	modo2(modocompuesto),
	oscuro(barradetitulooscura),
	mezclar(mezclarcontenido)
{
	CrearArrayDinamicadeVentanasdeContenido(cCant);
}

int CrearVentanadeCompositor(LPCWSTR titulo, HICON hIcon, HICON hIconSm, SIZE tamano, int cCant, HWND* ventanas, bool redimensionable, bool aditivo, bool modocompuesto, bool barradetitulooscura, bool mezclarcontenido) {
	Compositor app(titulo, hIcon, hIconSm, tamano, cCant, ventanas, redimensionable, aditivo, modocompuesto, barradetitulooscura, mezclarcontenido);
	return app.Iniciar();
}

void Compositor::CrearArrayDinamicadeVentanasdeContenido(int size) {
	m_hVentanasContenido_tamanoarray = size;
	m_hVentanasContenido = new HWND[size];
	m_pVisualesVentanasContenido = new CComPtr<IDCompositionVisual2>[size];
	m_pTexturasContenido = new CComPtr<IUnknown>[size];
	for (int k = 0; k < size; k++)
		m_hVentanasContenido[k] = NULL;
}

int Compositor::Iniciar()
{
	HRESULT hr = S_OK;

	hr = Inicializar();

    if (SUCCEEDED(hr))
    {
       RecibirMensajesdeVentana();
    }

    Destruir();

    return hr;
}

HRESULT Compositor::Inicializar()
{
    HRESULT hr = InicializarVentanaPrincipal();

	if (m_hVentanasContenido_tamanoarray > 0) {
		if (SUCCEEDED(hr))
		{
			hr = InicializarVentanasSecundarias();
		}

		if (SUCCEEDED(hr))
		{
			hr = CrearDispD3D11();
		}

		if (SUCCEEDED(hr))
		{
			hr = CrearDispDComp();
		}

		if (SUCCEEDED(hr))
		{
			hr = CrearRenderDComp();
		}

		if (SUCCEEDED(hr))
		{
			hr = CrearArbolDComp();
		}

		if (SUCCEEDED(hr))
		{
			// Commit the batch.
			hr = m_pDisp->Commit();
		}
	}
    return hr;
}

HRESULT Compositor::InicializarVentanaPrincipal()
{
    HRESULT hr = S_OK;
    WNDCLASSEX wc     = {0};
    wc.cbSize         = sizeof(wc);
    wc.style          = CS_HREDRAW | CS_VREDRAW;
    wc.lpfnWndProc    = WindowProc;
    wc.hInstance      = GetModuleHandle(NULL);
	wc.hIcon          = NULL;
	wc.hIconSm        = NULL;
    wc.hCursor        = LoadCursor(NULL, IDC_ARROW);
    wc.hbrBackground  = (HBRUSH)(TRANSPARENT + 1);
    wc.lpszClassName  = L"CompositionOutput";

    RegisterClassEx(&wc);
	DWORD exstyle1 = WS_EX_LAYERED | WS_EX_TRANSPARENT;
	DWORD exstyle2 = WS_EX_NOREDIRECTIONBITMAP;
	DWORD style1 = WS_POPUP;
	DWORD style2 = WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX;
	DWORD style3 = WS_THICKFRAME | WS_MAXIMIZEBOX;
	AdjustWindowRectEx(&tamVentana2, modo2 ? style1 : (ventanaRedimensionable ? style2 | style3 : style2), FALSE, modo2 ? exstyle1 : exstyle2);
    m_hVentanaPrincipal = CreateWindowEx(modo2 ? exstyle1 : exstyle2,
                                   wc.lpszClassName,
                                   nombreVentana,
                                   modo2 ? style1 : (ventanaRedimensionable ? style2 | style3 : style2),
                                   modo2 ? 0 : CW_USEDEFAULT,
                                   modo2 ? 0 : CW_USEDEFAULT,
                                   tamVentana2.right-tamVentana2.left,
                                   tamVentana2.bottom-tamVentana2.top,
                                   NULL,
                                   NULL,
                                   GetModuleHandle(NULL),
                                   NULL
                                   );
    if (!m_hVentanaPrincipal)
    {
        hr = HRESULT_FROM_WIN32(GetLastError());
    }

	if (SUCCEEDED(hr))
	{
		SetWindowLongPtr(m_hVentanaPrincipal, GWLP_USERDATA, (LONG_PTR)this);
		SendMessage(m_hVentanaPrincipal, WM_SETICON, ICON_BIG, (LPARAM)m_hIcon);
		SendMessage(m_hVentanaPrincipal, WM_SETICON, ICON_SMALL, (LPARAM)m_hIconSm);
		if (modo2) {
			HBITMAP hbmp;
			{
				HDC hdcScreen = GetDC(NULL);
				HDC hDC = CreateCompatibleDC(hdcScreen);
				ReleaseDC(0, hdcScreen);
				hbmp = CreateCompatibleBitmap(hDC, tamVentana1.right, tamVentana1.bottom);
				HBRUSH hbrushFill = (HBRUSH)(TRANSPARENT + 1);
				HBITMAP hbmpOld = (HBITMAP)SelectObject(hDC, hbmp);
				HBRUSH  hbrushOld = (HBRUSH)SelectObject(hDC, hbrushFill);
				Rectangle(hDC, 0, 0, tamVentana1.right, tamVentana1.bottom);
				SelectObject(hDC, hbmpOld);
				SelectObject(hDC, hbrushOld);
				DeleteObject(hbrushFill);
				DeleteObject(hDC);
				SetBitmapDimensionEx(hbmp, tamVentana1.right, tamVentana1.bottom, NULL);
			}

			HDC hdcScreen = GetDC(0);
			HDC hdc = CreateCompatibleDC(hdcScreen);
			ReleaseDC(0, hdcScreen);
			HBITMAP hbmpold = (HBITMAP)SelectObject(hdc, hbmp);
			BLENDFUNCTION blend = { 0 };
			blend.BlendOp = AC_SRC_OVER;
			blend.SourceConstantAlpha = 255;
			blend.AlphaFormat = AC_SRC_ALPHA;
			POINT ptPos = { 0, 0 };
			SIZE sizeWnd = { tamVentana1.right, tamVentana1.bottom };
			POINT ptSrc = { 0, 0 };
			UpdateLayeredWindow(m_hVentanaPrincipal, NULL, &ptPos, &sizeWnd, hdc, &ptSrc, 0, &blend, ULW_ALPHA);
			SelectObject(hdc, hbmpold);
			DeleteDC(hdc);
			DeleteObject(hbmp);
		}
		if (oscuro) {
			HMODULE uxTheme = LoadLibrary(L"uxtheme.dll");
			AllowDarkModeForApp = (pfnAllowDarkModeForApp)GetProcAddress(uxTheme, MAKEINTRESOURCEA(135));
			AllowDarkModeForWindow = (pfnAllowDarkModeForWindow)GetProcAddress(uxTheme, MAKEINTRESOURCEA(133));
			AllowDarkModeForApp(TRUE);
			AllowDarkModeForWindow(m_hVentanaPrincipal, TRUE);
			int valor = 0x01000000;
			DwmSetWindowAttribute(m_hVentanaPrincipal, 19, &valor, sizeof(int));
		}
        ShowWindow(m_hVentanaPrincipal, modo2 ? SW_SHOWNOACTIVATE : SW_SHOWDEFAULT);
    }

    return hr;
}

HRESULT Compositor::InicializarVentanasSecundarias()
{
	HRESULT hr = S_OK;

	WNDCLASSEX wcex = { 0 };
	wcex.cbSize = sizeof(wcex);
	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WindowProc;
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(TRANSPARENT + 1);
	wcex.lpszClassName = L"CompositionWindow";

	RegisterClassEx(&wcex);
	for (int i = 0; i < m_hVentanasContenido_tamanoarray; i++) {
		if (SUCCEEDED(hr)) {
			WCHAR winTitle[1025];
			wsprintf(winTitle, L"Compositor %i (%s)", i+1, nombreVentana);
			m_hVentanasContenido[i] = CreateWindowEx(WS_EX_TOOLWINDOW,
				wcex.lpszClassName,
				winTitle,
				WS_POPUP,
				0, 0, tamVentana1.right, tamVentana1.bottom,
				NULL,
				NULL,
				GetModuleHandle(NULL),
				NULL);

			if (!m_hVentanasContenido[i])
			{
				hr = HRESULT_FROM_WIN32(GetLastError());
			}

			if (SUCCEEDED(hr))
			{
				SetWindowLongPtr(m_hVentanasContenido[i], GWLP_USERDATA, (LONG_PTR)this);
				ShowWindow(m_hVentanasContenido[i], SW_SHOWNOACTIVATE);
				MARGINS margins = { -1 };
				DwmExtendFrameIntoClientArea(m_hVentanasContenido[i], &margins);
				BOOL valor = TRUE;
				DwmSetWindowAttribute(m_hVentanasContenido[i], DWMWA_CLOAK, &valor, sizeof(valor));
			}
		}
	}

	if (aditivo) {
		WCHAR winTitle[1025];
		wsprintf(winTitle, L"Compositor (ADD) (%s)", nombreVentana);
		AdjustWindowRectEx(&tamVentana1, WS_CAPTION | WS_SYSMENU, FALSE, WS_EX_TOOLWINDOW | WS_EX_NOREDIRECTIONBITMAP);
		m_hVentanaContenido_ADITIVO = CreateWindowEx(WS_EX_TOOLWINDOW | WS_EX_NOREDIRECTIONBITMAP,
			wcex.lpszClassName,
			winTitle,
			WS_CAPTION | WS_SYSMENU,
			0, 0, tamVentana1.right - tamVentana1.left, tamVentana1.bottom - tamVentana1.top,
			NULL,
			NULL,
			GetModuleHandle(NULL),
			NULL);

		if (!m_hVentanaContenido_ADITIVO)
		{
			hr = HRESULT_FROM_WIN32(GetLastError());
		}

		if (SUCCEEDED(hr))
		{
			ShowWindow(m_hVentanaContenido_ADITIVO, SW_SHOWNOACTIVATE);
			MARGINS margins = { -1 };
			DwmExtendFrameIntoClientArea(m_hVentanaContenido_ADITIVO, &margins);
			BOOL valor = FALSE;
			DwmSetWindowAttribute(m_hVentanaContenido_ADITIVO, DWMWA_NCRENDERING_ENABLED, &valor, sizeof(valor));
			BOOL valor2 = TRUE;
			DwmSetWindowAttribute(m_hVentanaContenido_ADITIVO, DWMWA_CLOAK, &valor2, sizeof(valor2));
		}
	}

	return hr;
}

HRESULT Compositor::CrearDispD3D11()
{
    HRESULT hr = S_OK;

    D3D_DRIVER_TYPE driverTypes[] =
    {
        D3D_DRIVER_TYPE_HARDWARE,
        D3D_DRIVER_TYPE_WARP,
    };

    D3D_FEATURE_LEVEL featureLevelSupported;

    for (int i = 0; i < sizeof(driverTypes) / sizeof(driverTypes[0]); ++i)
    {
        CComPtr<ID3D11Device> d3d11Device;
        CComPtr<ID3D11DeviceContext> d3d11DeviceContext;

        hr = D3D11CreateDevice(
            nullptr,
            driverTypes[i],
            NULL,
            D3D11_CREATE_DEVICE_BGRA_SUPPORT,
            NULL,
            0,
            D3D11_SDK_VERSION,
            &d3d11Device,
            &featureLevelSupported,
            &d3d11DeviceContext);

        if (SUCCEEDED(hr))
        {
            _dispD3D11 = d3d11Device.Detach();
            _contextoDispD3D11 = d3d11DeviceContext.Detach();

            break;
        }
    }

    return hr;
}

HRESULT Compositor::CrearDispDComp()
{
    HRESULT hr = (_dispD3D11 == nullptr) ? E_UNEXPECTED : S_OK;

    CComPtr<IDXGIDevice> dxgiDevice;

    if (SUCCEEDED(hr))
    {
        hr = _dispD3D11->QueryInterface(&dxgiDevice);
    }

    if (SUCCEEDED(hr))
    {
        hr = DCompositionCreateDevice2(dxgiDevice, __uuidof(IDCompositionDesktopDevice), reinterpret_cast<void **>(&m_pDisp));
    }

    return hr;
}

HRESULT Compositor::CrearRenderDComp()
{
    HRESULT hr = ((m_pDisp == nullptr) || ((aditivo ? m_hVentanaContenido_ADITIVO : m_hVentanaPrincipal) == NULL)) ? E_UNEXPECTED : S_OK;

    if (SUCCEEDED(hr))
    {
        hr = m_pDisp->CreateTargetForHwnd(aditivo ? m_hVentanaContenido_ADITIVO : m_hVentanaPrincipal, TRUE, &m_pHvenRender);
    }

    return hr;
}

HRESULT Compositor::CrearArbolDComp()
{
    HRESULT hr = ((m_pDisp == nullptr) || ((aditivo ? m_hVentanaContenido_ADITIVO : m_hVentanaPrincipal) == NULL)) ? E_UNEXPECTED : S_OK;

    if (SUCCEEDED(hr))
    {
        hr = m_pDisp->CreateVisual(&m_pVisualBase);
    }

    if (SUCCEEDED(hr))
    {
        hr = m_pHvenRender->SetRoot(m_pVisualBase);
    }

	for (int i = 0; i < m_hVentanasContenido_tamanoarray; i++) {

		if (SUCCEEDED(hr))
		{
			hr = m_pDisp->CreateVisual(&m_pVisualesVentanasContenido[i]);
		}

		if (SUCCEEDED(hr))
		{
			hr = m_pDisp->CreateSurfaceFromHwnd(m_hVentanasContenido[i], &m_pTexturasContenido[i]);
		}
		if (SUCCEEDED(hr))
		{
			hr = m_pVisualesVentanasContenido[i]->SetContent(m_pTexturasContenido[i]);
		}

		if (SUCCEEDED(hr))
		{
			hr = m_pVisualesVentanasContenido[i]->SetCompositeMode(mezclar ? DCOMPOSITION_COMPOSITE_MODE_DESTINATION_INVERT : DCOMPOSITION_COMPOSITE_MODE_SOURCE_OVER);
		}

		if (SUCCEEDED(hr))
		{
			hr = m_pVisualBase->AddVisual(m_pVisualesVentanasContenido[i], TRUE, NULL);
		}

	}

	for (int i = 0; i < m_hVentanasContenido_tamanoarray; i++) {
		SetWindowLongPtr(m_hVentanasSeleccionadas[i], GWL_STYLE, GetWindowLongPtr(m_hVentanasSeleccionadas[i], GWL_STYLE) & ~WS_DLGFRAME & ~WS_BORDER & ~WS_THICKFRAME);
		MoveWindow(m_hVentanasSeleccionadas[i], 0, 0, tamVentana1.right, tamVentana1.bottom, TRUE);
		SetParent(m_hVentanasSeleccionadas[i], m_hVentanasContenido[i]);
	}

	if (aditivo) {
		DwmRegisterThumbnail(m_hVentanaPrincipal, m_hVentanaContenido_ADITIVO, &_hAditivo);

		DWM_THUMBNAIL_PROPERTIES dskThumbProps;
		dskThumbProps.fSourceClientAreaOnly = TRUE;
		dskThumbProps.fVisible = TRUE;
		dskThumbProps.dwFlags = DWM_TNP_SOURCECLIENTAREAONLY | DWM_TNP_VISIBLE | DWM_TNP_RECTDESTINATION | DWM_TNP_OPACITY;
		dskThumbProps.opacity = 255;
		SIZE size;
		DwmQueryThumbnailSourceSize(_hAditivo, &size);
		RECT thumbnailRect = { 0, 0, size.cx, size.cy };
		dskThumbProps.rcDestination = thumbnailRect;

		DwmUpdateThumbnailProperties(_hAditivo, &dskThumbProps);
	}

    return hr;
}

int Compositor::RecibirMensajesdeVentana()
{
    int result = 0;

    MSG msg = { 0 };

    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    result = static_cast<int>(msg.wParam);

    return result;
}

LRESULT CALLBACK Compositor::WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	Compositor* me = reinterpret_cast<Compositor*>(GetWindowLongPtr(hwnd, GWLP_USERDATA));
	switch (uMsg)
	{
	case WM_SIZE:
		if (me) {
			if (me->ventanaRedimensionable) {
				me->tamVentana1 = { 0, 0, LOWORD(lParam), HIWORD(lParam) };
				AdjustWindowRectEx(&me->tamVentana1, WS_POPUP, FALSE, NULL);
				me->tamVentana1 = { 0, 0, me->tamVentana1.right - me->tamVentana1.left, me->tamVentana1.bottom - me->tamVentana1.top, };
				for (int i = 0; i < me->m_hVentanasContenido_tamanoarray; i++) {
					MoveWindow(me->m_hVentanasSeleccionadas[i], 0, 0, me->tamVentana1.right, me->tamVentana1.bottom, TRUE);
					MoveWindow(me->m_hVentanasContenido[i], 0, 0, me->tamVentana1.right, me->tamVentana1.bottom, TRUE);
					if (me->aditivo) {
						AdjustWindowRectEx(&me->tamVentana1, WS_CAPTION | WS_SYSMENU, FALSE, WS_EX_TOOLWINDOW | WS_EX_NOREDIRECTIONBITMAP);
						MoveWindow(me->m_hVentanaContenido_ADITIVO, 0, 0, me->tamVentana1.right - me->tamVentana1.left, me->tamVentana1.bottom - me->tamVentana1.top, TRUE);
					}
				}
			}
		}
		return S_OK;
		break;
	case WM_ACTIVATE:
		if (me) {
			if (me->aditivo) {
				DWM_THUMBNAIL_PROPERTIES dskThumbProps;
				dskThumbProps.fSourceClientAreaOnly = TRUE;
				dskThumbProps.fVisible = TRUE;
				dskThumbProps.dwFlags = DWM_TNP_SOURCECLIENTAREAONLY | DWM_TNP_VISIBLE | DWM_TNP_RECTDESTINATION | DWM_TNP_OPACITY;
				dskThumbProps.opacity = 255;
				SIZE size;
				DwmQueryThumbnailSourceSize(me->_hAditivo, &size);
				RECT thumbnailRect = { 0, 0, size.cx, size.cy };
				dskThumbProps.rcDestination = thumbnailRect;

				DwmUpdateThumbnailProperties(me->_hAditivo, &dskThumbProps);
			}
		}
		return S_OK;
		break;
	case WM_PAINT:
		HDC hdc;
		PAINTSTRUCT ps;

		hdc = BeginPaint(hwnd, &ps);

		EndPaint(hwnd, &ps);

		return S_OK;
		break;

	case WM_CLOSE:
		if (me) {
			if (me->_hAditivo != NULL) {
				DwmUnregisterThumbnail(me->_hAditivo);
			}
			me->Destruir();
		}
		return S_OK;
		break;

	case WM_DESTROY:
		PostQuitMessage(0);
		return S_OK;
		break;

	default:
		return DefWindowProc(hwnd, uMsg, wParam, lParam);
	}
}

VOID Compositor::Destruir()
{
    DestruirVentanaPrincipal();
	if (m_hVentanasContenido_tamanoarray > 0) {
		DestruirVentanasSecundarias();
		DestruirArbolDComp();
		DestruirRenderDComp();
		DestruirDispDComp();
		DestruirDispD3D11();
		CoUninitialize();
	}
}

VOID Compositor::DestruirVentanaPrincipal()
{
    if (m_hVentanaPrincipal != NULL)
    {
       DestroyWindow(m_hVentanaPrincipal);
	   m_hVentanaPrincipal = NULL;
    }
}

VOID Compositor::DestruirVentanasSecundarias()
{
	for (int i = 0; i < m_hVentanasContenido_tamanoarray; i++) {
		if (m_hVentanasContenido[i] != NULL)
		{
			DestroyWindow(m_hVentanasContenido[i]);
			m_hVentanasContenido[i] = NULL;
		}
	}
	delete[] m_hVentanasContenido;
	m_hVentanasContenido_tamanoarray = 0;
}

VOID Compositor::DestruirArbolDComp()
{
	for (int i = 0; i < m_hVentanasContenido_tamanoarray; i++) {
		if (m_pTexturasContenido[i] != NULL)
		{
			m_pTexturasContenido = nullptr;
		}
	}
}

VOID Compositor::DestruirRenderDComp()
{
    m_pHvenRender = nullptr;
}

VOID Compositor::DestruirDispDComp()
{
    m_pDisp = nullptr;
}

VOID Compositor::DestruirDispD3D11()
{
    _contextoDispD3D11 = nullptr;
    _dispD3D11 = nullptr;
}
