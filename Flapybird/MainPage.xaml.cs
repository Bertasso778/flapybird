using System.Net.Security;

namespace Flapybird;

public partial class MainPage : ContentPage
{
	const int Gravidade = 2;
	const int tempoEntreFrames = 25;
	bool estaMorto = true;
	double LarguraJanela = 0;
	double AlturaJanela = 0;
	int Velocidade = 20;
	const int ForçaPulo = 30;
	const int MaxTempoPulando = 3; //FRAMES
	bool EstaPulando = false;
	int tempoPulando = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	void AplicaGravidade()
	{
		imgPersonagem.TranslationY += Gravidade;
	}

	async Task Desenha()
	{
		while (!estaMorto)
		{
			GerenciaCanos();
			if (EstaPulando)
				AplicaPulo();
			else
				AplicaGravidade();

			if (VerificaColisao())
			{
				estaMorto = true;
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
		}
	}

	void OnGameOverClicked(object s, TappedEventArgs e)
	{
		frameGameOver.IsVisible = false;
		Inicializar();
		Desenha();
	}

	void Inicializar()
	{
		estaMorto = false;
		imgPersonagem.TranslationY = 0;
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);

		LarguraJanela = width;
		AlturaJanela = height;
	}

	void GerenciaCanos()
	{
		posteCima.TranslationX -= Velocidade;
		posteBaixo.TranslationX -= Velocidade;
		if (posteBaixo.TranslationX < -LarguraJanela)
		{
			posteBaixo.TranslationX = 200;
			posteBaixo.TranslationX = 200;
		}
	}

	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			if (VerificaColisaoTeto() || verificaColisaoChao())
			{
				return true;
			}
		}
		return false;
	}
	bool VerificaColisaoTeto()
	{
		var minY = -AlturaJanela / 2;
		if (imgPersonagem.TranslationY <= minY)
			return true;
		else
			return false;
	}

	bool verificaColisaoChao()
	{
		var maxY = AlturaJanela / 2 - imgChao.HeightRequest;
		if (imgPersonagem.TranslationY >= maxY)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	void AplicaPulo()
	{
		imgPersonagem.TranslationY -= ForçaPulo;
		tempoPulando++;
		if (tempoPulando >= MaxTempoPulando)
		{
			EstaPulando = false;
			tempoPulando = 0;

		}
	}
	void OnGridClicked(object sender, TappedEventArgs a)
	{
		EstaPulando = true;
	}
}


