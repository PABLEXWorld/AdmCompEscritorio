#include <d3d11.h>
#include <atlbase.h>
#include <windows.h>
#include <dcomp.h>
#include <dwmapi.h>
#pragma comment(lib, "d3d11.lib")
#pragma comment(lib, "Dwmapi.lib")
#pragma comment(lib, "Dcomp.lib")

class Compositor
{
public:
	static LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

	static VOID MostrarError(HRESULT hr);

public:
	explicit Compositor(LPCWSTR title, HICON hIcon, HICON hIconSm, SIZE size, int cCount, HWND* windows, bool sizeable, bool add, bool layer, bool darkw, bool blendcontent);

	int Iniciar();

private:
	void CrearArrayDinamicadeVentanasdeContenido(int size);
	HRESULT Inicializar();

	HRESULT InicializarVentanaPrincipal();
	HRESULT InicializarVentanasSecundarias();

	HRESULT CrearDispD3D11();
	HRESULT CrearDispDComp();
	HRESULT CrearRenderDComp();
	HRESULT CrearArbolDComp();

	int RecibirMensajesdeVentana();

	VOID Destruir();
	VOID DestruirVentanaPrincipal();
	VOID DestruirVentanasSecundarias();
	VOID DestruirArbolDComp();
	VOID DestruirRenderDComp();
	VOID DestruirDispDComp();
	VOID DestruirDispD3D11();

private:

	HICON m_hIcon;
	HICON m_hIconSm;
	HWND* m_hVentanasSeleccionadas;
	HWND  m_hVentanaPrincipal;
	LPCWSTR nombreVentana;
	bool ventanaRedimensionable;
	int m_hVentanasContenido_tamanoarray;
	HWND* m_hVentanasContenido;
	HWND m_hVentanaContenido_ADITIVO;
	int m_hVentanasComposicion_tamanoarray;
	HWND* m_hVentanasComposicion;

	CComPtr<ID3D11Device> _dispD3D11;
	CComPtr<ID3D11DeviceContext> _contextoDispD3D11;

	HTHUMBNAIL _hAditivo;
	bool aditivo;
	bool modo2;
	bool oscuro;
	bool mezclar;
	RECT tamVentana1;
	RECT tamVentana2;

	typedef BOOL(WINAPI *pfnAllowDarkModeForApp)(BOOL);
	typedef BOOL(WINAPI *pfnAllowDarkModeForWindow)(HWND__*, BOOL);
	pfnAllowDarkModeForApp AllowDarkModeForApp;
	pfnAllowDarkModeForWindow AllowDarkModeForWindow;

	CComPtr<IDCompositionDesktopDevice> m_pDisp;
	CComPtr<IDCompositionTarget> m_pHvenRender;
	CComPtr<IDCompositionVisual2> m_pVisualBase;
	CComPtr<IDCompositionVisual2>* m_pVisualesVentanasContenido;
	CComPtr<IUnknown>* m_pTexturasContenido;
};