using System.Net.Security;

namespace Flapybird;

public partial class MainPage : ContentPage
{
	const int Gravidade = 2;
	const int tempoEntreFrames = 25;
	bool estaMorto = true;
	double LarguraJanela = 0;
	double AlturaJanela = 0;
	int Velocidade =20;

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
			AplicaGravidade();
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
		
		AlturaJanela =height;
    }

	void GerenciaCanos()
	{
 		posteCima.TranslationX -= Velocidade;
	posteBaixo.TranslationX -= Velocidade;
	if(posteBaixo.TranslationX < -LarguraJanela)
	{
		posteBaixo.TranslationX = 200;
		posteBaixo.TranslationX = 200;
	}
	}
}

