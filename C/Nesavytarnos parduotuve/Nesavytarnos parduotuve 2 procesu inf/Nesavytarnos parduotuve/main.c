//Dominik Gabriel Lisovski, VU MIF PS6
/**Nesavitarnos parduotuvė (ADT: eilė, prioritetinė eilė).
Procesas 1: pardavėja pilnai aptarnauja klientą (paduoda prekes, išmuša čekį, paima pinigus).
Procesas 2: pardavėja surenka pirkėjo pageidaujamas prekes ir jas surašo ant popieriaus lapelio,
su šiuo lapeliu klientas eina prie kasininkės, kuri išmuša čekį ir paima pinigus,
su šiuo čekiu pirkėjas grįžta prie jį aptarnavusios pardavėjos ir, kai tik ši baigia aptarnauti eilinį pirkėją, be eilės gauna prekes.
Patyrinėti abu šiuos procesus tiek klientų pasitenkinimo požiūriu (minimalus, vidutinis ir maksimalus pilno aptarnavimo laikas),
tiek ekonominiu požiūriu (kiekvienas kasos aparatas kainuoja).
Laikykime, kad bendras darbuotojų skaičius yra fiksuotas.
Visi kiti rodikliai, nuo kurių priklauso procesas, įvedami kaip programos parametrai.
*/
#include "main.h"
int main()
{
	//testams
	int t = 0, x = 0;
	eile *ptr;
	eile *el;
	Aktivus *aktivus2 = NULL;				//Aktiviu klientu struktura

	//Bendri duomenys
	int punkte[MAX] = { 0 };						//Dabar aptarnaujami klientai antrame etape
	int eina[MAX_EINA] = { 0 };				//Klientai einantis prie arba is kasos
	int einask = -1;						//Klientu kiekis einanciu i arba is kasos
	int bendrak = 0;                        //Visu nauduojamu kasos aparatu kaina
	int min1, min2;							//minimalus pilno aptarnavimo laikas
	double vid1 = 0, vid2 = 0;				//vidutinis pilno aptarnavimo laikas
	int max1 = 0, max2 = 0;					//maximalus pilno aptarnavimo laikas
	int i = 0, j = 0;
	int dLaikas = 0;						//dabartinis Laikas
	int klientoid = 0;						//Kliento id
	Aktivus *aktivus = NULL;				//Aktiviu klientu struktura
	Aktivus *aptarnaujamas = NULL;			//Aptarnaujamas klientas
	int kaina1 = 0;							//1 proceso kaina
	int kaina2 = 0;							//2 proceso kaina
	int error = 0;
	int temp1 = 0;							//Laikinas skaicius
	int temp2 = 0;							//Laikinas skaicius
	int temp3 = 0;							//Laikinas skaicius
	int baige1 = 0;							//Kiek klientu pilnai apsipirko 1 procese
	int baige2 = 0;							//Kiek klientu pilnai apsipirko 2 prosece
	int naikintuvas = 0;					//Padeda susikloscius specifinei situacijoj 2 procese, kai mazesnio prioriteto elementas negali issitrinti

	//Bendri nuskaitomi duomenys
	int laikas;
	int	tikimybe = 0;						//lankytojo tikimybe
	int	darbuotojai = 0;					//darbuotoju skaicius
	int	kaina = 0;							//kasos aparato kaina
	int	cekis = 0;							//cekio ismusimo laikas
	int	pPaimimas = 0;						//pinigu paimimo laikas

	//Pirmojo proceso duomenys
	q kasa1[MAX];							//Pirmojo proceso pardavejei
	int prPadavimas = 0;					//prekiu padavimo laikas

	//Antrojo proceso duomenys
	eile *kasa2[MAX];						//Antrojo proceso kasa
	q kasa3;
	int	prSurinkimas = 0;					//prekiu surinkimo laikas			
	int	prSurasimas = 0;					//prekiu surasymo laikas
	int	kelias = 0;							//priejimo prie kasininkes laikas
	int	prGavimas = 0;						//prekiu gavimo laikas

	//Nuskaitome duomenys ir sukuriame eiles
	if ((error = Nuskaityti(&laikas, &tikimybe, &darbuotojai, &kaina, &cekis, &pPaimimas, &prPadavimas, &prSurinkimas, &prSurasimas, &kelias, &prGavimas)) != 0)
	{
		if (error == 1)
			printf("Nevisus duomenys pavyko nuskaityti!\n");
		getchar();
		return 0;
	}
	for (i = 0; i < darbuotojai; i++)
	{
		Create_Empty(&kasa1[i]);
		sukurtiSarasa(&kasa2[i]);
	}
	Create_Empty(&kasa3);
	kaina1 = kaina * darbuotojai;
	kaina2 = kaina;
	min1 = prPadavimas + cekis + pPaimimas + laikas;
	min2 = prSurinkimas + prSurasimas + 2 * kelias + cekis + pPaimimas + prGavimas + laikas;
	srand(time(NULL));
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//Pagrindinis ciklas
	while (dLaikas != laikas)
	{
		printf("\nLaikas: %d\n", dLaikas);
		//Ar ateis klientas
		if (rand() % 100 <= tikimybe)
		{
			klientoid++;
			//Sukuriamas klientas su abieju procesu informacija
			printf("Atiejo klientas: %d\n", klientoid);
			aktivus = (Aktivus*)malloc(sizeof(Aktivus));
			if (aktivus == NULL)
			{
				printf("Nepakako atminties naujam klientui sukurti\n");
				return 0;
			}
			aktivus->id = klientoid;
			aktivus->atiejo = dLaikas;
			aktivus->lauke = 0;
			temp1 = rand() % (cekis + 1);
			//printf("%d klientui cekio paimimas truks %d\n", aktivus->id, temp1);
			aktivus->pr1 = temp1;
			temp1 = rand() % (pPaimimas + 1);
			//printf("%d klientui pinigu paimimas paimimas truks %d\n", aktivus->id, temp1);
			aktivus->pr1 += temp1;
			aktivus->pr22 = aktivus->pr1;
		}

		//1 procesas
		printf("Vykdomas pirmas procesas\n");
		if (aktivus != NULL)
		{
			temp1 = rand() % (prPadavimas + 1);
			aktivus->pr1 += temp1;
			//printf("%d klientui prekiu padavimas truks %d\n", aktivus->id, temp1);
			printf("%d klientui 1 procesas truks %d\n", aktivus->id, aktivus->pr1);
			temp1 = rand() % (darbuotojai);
			aktivus->punktas = temp1;
			Enqueue(aktivus, &kasa1[temp1]);
			printf("%d klientas nuejo i %d kasa\n", aktivus->id, temp1);
		}

		for (i = 0; i < darbuotojai; i++)
		{
			if (Get_First(&temp1, &kasa1[i]) == 0)
			{
				aptarnaujamas = temp1;
				printf("Aptarnaujamas %d klientas liko %d prie %d aptarnavimo punkto\n", aptarnaujamas->id, aptarnaujamas->pr1, aptarnaujamas->punktas);
				if (aptarnaujamas->pr1 != 0) aptarnaujamas->pr1--;
				else
				{
					temp1 = dLaikas - aptarnaujamas->atiejo;
					if (temp1 > max1) max1 = temp1;
					if (temp1 < min1) min1 = temp1;
					vid1 += temp1;
					baige1++;
					printf("%d klientas pilnai apsipirko po %d minuciu is kuriu %d min praleido laukdamas\n", aptarnaujamas->id, temp1, aptarnaujamas->lauke);
					Dequeue(&temp1, &kasa1[i]);
					i--;
				}
			}
		}
		
		//2 procesas
		printf("Vykdomas antras procesas\n");
		if (aktivus != NULL)
		{
			temp1 = rand() % (prSurinkimas + 1);
			aktivus->pr21 = temp1;
			//printf("%d klientui prekiu surinkimas truks %d\n", aktivus->id, temp1);
			temp1 = rand() % (prSurasimas + 1);
			aktivus->pr21 += temp1;
			//printf("%d klientui prekiu surasimas truks %d\n", aktivus->id, temp1);
			printf("%d klientui aptarnavimas prie pardavejos truks %d\n", aktivus->id, aktivus->pr21);
			temp1 = rand() % (kelias) + 1;
			aktivus->k1 = temp1;
			printf("%d klientui kelias i kasa truks %d\n", aktivus->id, temp1);
			printf("%d klientui aptarnavimas prie kasos truks %d\n", aktivus->id, aktivus->pr22);
			temp1 = rand() % (kelias) + 1;
			aktivus->k2 = temp1;
			printf("%d klientui kelias is kasos truks %d\n", aktivus->id, temp1);
			temp1 = rand() % (prGavimas + 1);
			aktivus->pr23 = temp1;
			printf("%d klientui prekiu gavimas truks %d\n", aktivus->id, temp1);
			temp1 = rand() % (darbuotojai - 1);
			aktivus->kasa = temp1;
			aktivus->cekis = 0;
			idetiElementa(aktivus, 2, &kasa2[temp1]);
			printf("%d klientas nuejo i %d aptarnavimo punkta\n", aktivus->id, temp1);
			aktivus = NULL;
		}

		printf("Einantys:\n");
		for (i = 0; i <= einask; i++)										//Einantys
		{
			aptarnaujamas = eina[i];
			if (aptarnaujamas != 0)
			{
				if (aptarnaujamas->cekis == 0)								//Aptarnaujamas eina i kasa
				{
					printf("%d klientui liko nueiti %d min\n", aptarnaujamas->id, aptarnaujamas->k1);
					if (aptarnaujamas->k1 != 0) aptarnaujamas->k1--;
					else
					{
						Enqueue(aptarnaujamas, &kasa3);
						eina[i] = 0;
						printf("%d klientas atejo prie kasos\n", aptarnaujamas->id);
					}
				}
				else                                                         //Aptarnaujamas eina is kasos
				{
					printf("%d klientui liko nueiti %d min\n", aptarnaujamas->id, aptarnaujamas->k2);
					if (aptarnaujamas->k2 != 0) aptarnaujamas->k2--;
					else
					{
						temp1 = aptarnaujamas->kasa;
						idetiElementa(aptarnaujamas, 1, &kasa2[temp1]); 
						eina[i] = 0;
						printf("%d klientas grizo prie %d aptarnavimo punkto\n", aptarnaujamas->id, aptarnaujamas->kasa);
					}
				}
			}
		}



		printf("Aptarvanimo punktuose:\n");
		for (i = 0; i < darbuotojai - 1; i++)
		{
			if (punkte[i] == 0)
			{
				if (gautiMax(&temp1, &temp2, kasa2[i]) == 0)
				{
					punkte[i] = temp1;
				}
			}
			if (punkte[i] != 0)
			//if (gautiMax(&temp1, &temp2, kasa2[i]) == 0)
			{
				aptarnaujamas = punkte[i];
				//aptarnaujamas = temp1;
				if (aptarnaujamas->cekis == 1)												//Aptarnaujamas klientas grizes is kasos
				{
					printf("Aptarnaujamas %d klientas antroje dalyje liko %d prie %d aptarnavimo punkto\n", aptarnaujamas->id, aptarnaujamas->pr23, aptarnaujamas->kasa);
					if (aptarnaujamas->pr23 != 0) aptarnaujamas->pr23--;
					else
					{
						printf("%d klientas pilnai apsipirko ir isejo is %d aptarnavimo punkto\n", aptarnaujamas->id, aptarnaujamas->kasa);
						baige2++;
						temp1 = dLaikas - aptarnaujamas->atiejo;
						if (temp1 > max2) max2 = temp1;
						if (temp1 < min2) min2 = temp1;
						vid2 += temp1;
						baige1++;
						naikintiElementa(&kasa2[i]);
						if ((gautiMax(&temp1, &temp3, kasa2[i]) == 0) && naikintuvas == 1)
						{
							if (temp3 == 2)
							{
								aktivus2 = temp1;
								//printf("Antrame etape issitrine %d klientas\n", aktivus2->id);
								naikintiElementa(&kasa2[i]);
								naikintuvas = 0;
							}
						}
						punkte[i] = 0;
						i--;
					}
				}
				else                                                        //Aptarnaujamas klientas nebuves prie kasos
				{
					printf("Aptarnaujamas %d klientas pirmoje dalyje liko %d prie %d aptarnavimo punkto\n", aptarnaujamas->id, aptarnaujamas->pr21, aptarnaujamas->kasa);
					if (aptarnaujamas->pr21 != 0) aptarnaujamas->pr21--;	
					else                                                    //Aptarnaujamas eina prie kasos
					{
						punkte[i] = 0;
						if (gautiMax(&temp1, &temp3, kasa2[i]) == 0)
						{
							if (temp3 == 2)
							{
								aktivus2 = temp1;
								//printf("Pirmame etape issitrine %d klientas\n", aktivus2->id);
								naikintiElementa(&kasa2[i]);
							}
							else naikintuvas = 1;
						}
						printf("%d klientas isejo is %d aptarnavimo punkto\n", aptarnaujamas->id, aptarnaujamas->kasa);
						aptarnaujamas->k1--;
						i--;
						for (j = 0; j <= einask; j++)
						{
							if (eina[j] == 0)
							{
								eina[j] = aptarnaujamas;
								//printf("%d klientas uzeme einanciuju masyvo %d vieta\n", aptarnaujamas->id, j+1);
								aptarnaujamas = 0;
								break;
							}
							//printf("Einanciuju masyve %d yra uzimtas\n", j);
						}
						if (aptarnaujamas != 0)
						{
							einask++;
							if (einask > (MAX_EINA - 1))
							{
								printf("Einanciu klientu skaicius virsijo %d, sumazinkite klientu skaiciu\n", MAX_EINA);
								getchar();
								return 0;
							}
							eina[einask] = aptarnaujamas;
							//printf("Einanciuju skaicius padidejo iki %d\n", aptarnaujamas->id, einask + 1);
						}
					}
				}
			}
		}

		printf("Prie kasos aparato:\n");
		for (i = 0; i < 1; i++)
		{
			if (Get_First(&temp1, &kasa3) == 0)										//Aptarnaujamas klientas prie kasos aparato;
			{
				aptarnaujamas = temp1;
				if (aptarnaujamas->pr22 != 0)
				{
					printf("%d klientui liko %d stoveti prie kasos\n", aptarnaujamas->id, aptarnaujamas->pr22);
					aptarnaujamas->pr22--;
				}
				else
				{
					i--;
					printf("%d klientas gavo ceki ir eina atgal i %d aptarnavimo punkta\n", aptarnaujamas->id, aptarnaujamas->kasa);
					Dequeue(&temp1, &kasa3);
					aptarnaujamas->cekis = 1;
					aptarnaujamas->k2--;
					for (j = 0; j <= einask; j++)
					{
						if (eina[j] == 0)
						{
							eina[j] = aptarnaujamas;
							//printf("%d klientas uzeme einanciuju masyvo %d vieta\n", aptarnaujamas->id, j + 1);
							aptarnaujamas = 0;
							break;
						}
						//printf("Einanciuju masyve %d yra uzimtas\n", j);
					}
					if (aptarnaujamas != 0)
					{
						einask++;
						if (einask > (MAX_EINA - 1))
						{
							printf("Einanciu klientu skaicius virsijo %d, sumazinkite klientu skaiciu\n", MAX_EINA);
							getchar();
							return 0;
						}
						eina[einask] = aptarnaujamas;
						//printf("Einanciuju skaicius padidejo iki %d\n", aptarnaujamas->id, einask + 1);
					}
				}
			}
		}
		dLaikas++;
	}
	
	//Po simulacijos pabaigos
	printf("\n---------------------------------------------------------------------------\n");
	if (baige1 == 0) printf("Pirmojo proceso niekas nebaige\n");
	else
	{
		vid1 = vid1 / baige1;
		printf("Pirmame procesasas\napsipirko: %d\nmax: %d\nvid: %1.0f\nmin: %d\nkaina: %d\n", baige1, max1, vid1, min1, kaina1);
	}
	printf("\n---------------------------------------------------------------------------\n");
	if (baige2 == 0) printf("Antrojo proceso niekas nebaige\n");
	else
	{
		vid2 = vid2 / baige2;
		printf("Antrame procesasas\napsipirko: %d\nmax: %d\nvid: %1.0f\nmin: %d\nkaina: %d\n", baige2, max2, vid2, min2, kaina2);
	}
	printf("\n---------------------------------------------------------------------------\n");
	if (baige1 == 0 || baige2 == 0)	printf("Negalima paliginti procesu, nes nebuvo abieju procesu rezultatu\n");
	else
	{
		if(kaina1 < kaina2) printf("Pirmas procesas buvo ekonomiskai geresnis\n");
		else
		{
			if(kaina1 > kaina2) printf("Antras procesas buvo ekonomiskai geresnis\n");
			else printf("Abu procesai ekonomiskai buvo ekvivalentus\n");
		}
		if (max1 < max2) printf("Maximaliu aptarnavimo laiku klientai buvo daugiau patenkinti pirmame procese\n");
		else
		{
			if (max1 > max2) printf("Maximaliu aptarnavimo laiku klientai buvo daugiau patenkinti antrame procese\n");
			else printf("Klientai taip pat vertino abieju procesu maximaliu pilno aptarnavimo laika");
		}
		if (vid1 < vid2) printf("Vidutiniu aptarnavimo laiku klientai buvo daugiau patenkinti pirmame procese\n");
		else
		{
			if (vid1 > vid2) printf("Vidutiniu aptarnavimo laiku klientai buvo daugiau patenkinti antrame procese\n");
			else printf("Klientai taip pat vertino abieju procesu vidutini pilno aptarnavimo laika");
		}
		if (min1 < min2) printf("Manimaliu aptarnavimo laiku klientai buvo daugiau patenkinti pirmame procese\n");
		else
		{
			if (min1 > min2) printf("Manimaliu aptarnavimo laiku klientai buvo daugiau patenkinti antrame procese\n");
			else printf("Klientai taip pat vertino abieju procesu manimalu pilno aptarnavimo laika");
		}
	}
	getchar();
	return 0;
}

//Nuskaito duomenys
int Nuskaityti(int *laikas, int *tikimybe, int *darbuotojai, int *kaina, int *cekis, int *pPaimimas, int *prPadavimas, int *prSurinkimas, int *prSurasimas, int *kelias, int *prGavimas)
{
	const char * duomenuFailas = "Duomenys.txt";
	FILE * duomenys;
	char * line[MAX] = { 0 };

	duomenys = fopen(input, "r");
	if (duomenys == NULL)
	{
		printf("Negalejo atidaryti %s\n", input);
		return 2;
	}
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*laikas = strtol(line, NULL, 10);
	if (*laikas < 0)
	{
		printf("Simulacijos laikas turi buti teigiamas\n");
		return 2;
	}
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*tikimybe = strtol(line, NULL, 10);
	if ((*tikimybe > 100) || (*tikimybe < 0))
	{
		printf("Tikimybe, kad ateis klientas turi buti nuo 0 iki 100\n");
		return 2;
	}
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*darbuotojai = strtol(line, NULL, 10);
	if (*darbuotojai < 2)
	{
		printf("Darbuotoju skaicius turi buti didesnis uz du, kad butu galima ivertinti abu procesus\n");
		return 2;
	}
	if (*darbuotojai > MAX)
	{
		printf("Darbuotoju skaicius neturi virsyti %d\n", MAX);
	}
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*kaina = strtol(line, NULL, 10);
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*cekis = strtol(line, NULL, 10);
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*pPaimimas = strtol(line, NULL, 10);
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*prPadavimas = strtol(line, NULL, 10);
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*prSurinkimas = strtol(line, NULL, 10);
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*prSurasimas = strtol(line, NULL, 10);
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*kelias = strtol(line, NULL, 10);
	if (fgets(line, MAX, duomenys) == NULL) return 1;
	*prGavimas = strtol(line, NULL, 10);
	if (*cekis == 0 || *pPaimimas == 0 || *prPadavimas == 0 || *prSurinkimas == 0 || *prSurasimas == 0 || *kelias == 0 || *prGavimas == 0)
	{
		printf("Visi procesu laikai turi buti teigiami!");
		return 2;
	}
	printf("laikas: %d\ntikimybe: %d\ndarbuotojai: %d\nkaina: %d\ncekis: %d\npPaimimass: %d\nprPadavimas: %d\nprSurinkimas %d\nprSurasimas: %d\nkelias: %d\nprGavimas: %d\n\n", 
														*laikas, *tikimybe, *darbuotojai, *kaina, *cekis, *pPaimimas, *prPadavimas, *prSurinkimas, *prSurasimas, *kelias, *prGavimas);
	fclose(duomenys);
	return 0;
}