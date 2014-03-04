#pragma once

#include "Direct3DBase.h"

struct ModelViewProjectionConstantBuffer
{
	DirectX::XMFLOAT4X4 model;
	DirectX::XMFLOAT4X4 view;
	DirectX::XMFLOAT4X4 projection;
};

struct VertexPositionColor
{
	DirectX::XMFLOAT3 pos;
	DirectX::XMFLOAT3 color;
};

// Cette classe permet d'afficher un cube tournant simple.
ref class CubeRenderer sealed : public Direct3DBase
{
public:
	CubeRenderer();

	// Méthodes Direct3DBase.
	virtual void CreateDeviceResources() override;
	virtual void CreateWindowSizeDependentResources() override;
	virtual void Render() override;
	
	// Méthode de mise à jour des objets dépendant de l'heure.
	void Update(float timeTotal, float timeDelta);

	// Méthode de mise à jour de l'orientation d'affichage
	void UpdateOrientation(Windows::Graphics::Display::DisplayOrientations orientation);

private:
	bool m_loadingComplete;

	Microsoft::WRL::ComPtr<ID3D11InputLayout> m_inputLayout;
	Microsoft::WRL::ComPtr<ID3D11Buffer> m_vertexBuffer;
	Microsoft::WRL::ComPtr<ID3D11Buffer> m_indexBuffer;
	Microsoft::WRL::ComPtr<ID3D11VertexShader> m_vertexShader;
	Microsoft::WRL::ComPtr<ID3D11PixelShader> m_pixelShader;
	Microsoft::WRL::ComPtr<ID3D11Buffer> m_constantBuffer;

	uint32 m_indexCount;
	ModelViewProjectionConstantBuffer m_constantBufferData;
	DirectX::XMFLOAT4X4 m_orientationTransform3D;
};
