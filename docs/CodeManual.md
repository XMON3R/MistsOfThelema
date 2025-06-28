Programátorská dokumentace k C# zápočtovému programu

2024

Mists of Thelema

Šimon Jůza

Obsah

[1 Úvod 2](#_Toc173976053)

[2 Program a mechaniky 3](#_Toc173976054)

[2.1 Postup tvoření programu a popis funkcí. 4](#_Toc173976055)

[2.2 Možná alternativní řešení a možné postupy 4](#_Toc173976056)

[2.2.1 Použité knihovny 4](#_Toc173976057)

[2.3 Reprezentace vstupních a výstupních dat 4](#_Toc173976058)

[2.3.1 Reprezentace vstupních a výstupních dat 4](#_Toc173976059)

[2.4 Průběh prací 5](#_Toc173976060)

[2.5 Možná rozšíření (Co se nestihlo) 5](#_Toc173976061)

[3 Závěr 5](#_Toc173976062)

# Úvod

Projekt **Mists of Thelema** je videoherní adventura s prvky interakce a volby. Hra je implementována v C# a využívá Windows Forms pro grafické rozhraní. Hráč prozkoumává různé scénáře a reaguje na situace pomocí inventáře a rozhodování. Hra je spustitelná na počítačích s Windows se základním .Net Frameworkem.

# Program a mechaniky

Hra probíhá v scénách reprezentovanými Formy vycházejících z WinForms. Vstupní soubor Program.cs prvně otevře TitleScreen scénu, která obsahuje logo, textový úvod a funkční tlačítka, přes které se člověk dostane do hlavní scény, tedy „scene_1“.

Scéna 1 obsahuje detekci kolizí mezi hráčem a herní objekty (NPCs, dům). Kontroluje i registraci klávesových úhozů, při kterých provádí další akce (např. interakce).

Tato scéna zároveň využívá několik speciálně vytvořených tříd, jako je DialogLoader, cPlayer, npc, Houses, InvItem či core. Tyto třídy slouží ke zjednodušení samotné orientace programu a ulehčí práci na případných rozšířených hry. Využití interface IInteractable z třídy core.cs mi zase usnadnilo práci co se týče interakcí s objekty. Využití interface lze taktéž nalézt ve třídě InvItem.cs, kde definuje základní vlastnosti všech předmětů, které se mohu vyskytovat v inventáři.

Pro detailní pochopení jednotlivých tříd doporučuji nahlédnout na samotného kódu s komentáři, většina konstrukcí a názvů by měla být poměrně vypovídající. Dovolil bych si však zmínit třídu DialogLoader, která dokáže přijímat buď jednoduché .txt soubory, či strukturované JSON soubory. Ty se mi na reprezentaci dialogů a scénářů následků velmi osvědčily.

Nyní ještě krátce zpět ke scéně 1. Z té se buď přes vypršení globálního časovače, či interakcí s herním objektem typu Houses s názvem „Your House“ hráč přesouvá do scény 2. Tam na něho čeká náhodná událost využívající Random z knihovny System. Samotné události jsou také popsány v přiloženém JSON souboru a několik funkcí přes tyto soubory vyhodnocuje, co se má právě stát. Pro lepší pochopení si opět dovolím se odkázat na samotný kód. V této scéně hra končí.

\--- intersect + bounds

## Postup tvoření programu a popis funkcí

Práce na hře jsem započal tvorbou jednotlivých scén a pixelových textur, které jsem, herní objekty i pozadí, vytvořil v online aplikaci **_Piskel_**. Dále jsem se soustředil na pohyb hráče a pohyb v omezeném poli („boundaries“), aby nemohl vyjet mimo hrací pole. Následně jsem zapracoval na rozložení jednotlivých prvků, překrývání, zobrazování, zahalování atd.

Z pohledu funkcí bych si dovolil vyzdvihnout načítání a zpracování JSON souborů přes třídu DialogLoader jako i samotné zobrazování dialogů a interakci s NPCs ve scéně 1.

## Možná alternativní řešení a možné postupy

Hra by šla efektivně vytvořit i v herním enginu Unity. Tento přístup jsem však nezvolil, protože jsem ve Winforms již vytvořil několik menších aplikací a her. Tudíž jsem usoudil, že se zde budu více věnovat tvoření programu než učení se nové platformy.

### Použité knihovny

1. **System**: Základní třídy pro datové typy a operace.
2. **System.Drawing**: Grafické objekty, barvy, písma.
3. **System.Windows.Forms**: Tvorba a správa uživatelského rozhraní Windows Forms.
4. **System.Collections.Generic**: Generické kolekce jako List a Dictionary.
5. **System.Text**: Manipulace s textem a kódováním.
6. **System.Text.Json**: Práce s JSON daty.

## Reprezentace vstupních a výstupních dat

### Reprezentace vstupních a výstupních dat

Vstupní data hry Mists of Thelema jsou reprezentovaná klávesovými úhozy, a to konkrétně klávesami WASD (pohyb), E (interakce), I (inventář) a kliknutí levým tlačítkem myši.

Výstupem aplikace je pak samotný vizuální aspekt hry, v případě rozšíření s ukládáním hry a rozhodnutími by výstupem programu mohl být i nějaký „savovací“ soubor.  

## Průběh prací

Samotná hra prošla mnoha přerody, kdy jsem narazil ať už na nějaké limitace, tak naopak velmi pomocné nástroje Winforms jako je široké spektrum použitelných labelů, „textboxů“ atd. Největším vývojem se stal přechod z textových souborů na soubory typu JSON pro reprezentaci dialogů či scénářů při ukončení dne.

## Možná rozšíření (Co se nestihlo)

V další verzi hry bych se hodlal zaměřit na vícedenní průběh hry s rozšířenými následky a novými dialogy. Také používání předmětů v dialozích s NPCs, kupování předmětů od NPC s ID „Shopkeeper“ či výměna předmětů. Také se nabízí možnost rozšíření herní plochy o les, kde by byla možnost zabíjet zvěř a monstra, ze kterých by padaly předměty.

# Závěr

Samotná tvorba aplikace mne mnohé naučila a mnoho konceptů z přednášek a cvičení jsem dle mého názoru lépe pochopil. V praxi jsem si na větším programu mohl vyzkoušet užitečnost mnoha konstrukcí, za což jsem opravdu vděčný. Zároveň jsem se rozhodl, že na hře bude pracovat i dále mimo samotný předmět.