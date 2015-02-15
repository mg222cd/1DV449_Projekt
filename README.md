# 1DV449 - Webbteknik II - Projekt

## Film
<a href="https://www.youtube.com/watch?v=dWJdCKwtpas&feature=youtu.be">https://www.youtube.com/watch?v=dWJdCKwtpas&feature=youtu.be</a> Tyvärr väldigt dålig kvalitet på filmen på Youtube. Samma film i bättre kvalitet finns här: <a href="https://github.com/mg222cd/1DV449_Projekt/blob/master/demo_film.swf">https://github.com/mg222cd/1DV449_Projekt/blob/master/demo_film.swf</a>

## Inledning
* Som slutprojekt i kursen 1DV449 Webbteknik II och IDV409 ASP.NET MVC har jag skapat en väderapplikation som hämtar data från Geonames.org och Yr.no. Applikationen fungerar så att man skriver in namnet på en ort, och i samband med detta presenteras en lista för orter som matchar sökningen (hämtas från Geonames.org). Den ort som väljs används för att hämta väderdata från Yr.no, och detta presenteras som en femdygnsprognos. 
* Det finns många liknande applikationer på marknaden, t.ex yr.no, smhi.se, klart.se med flera. Personligen valde jag just den här Mashupen eftersom båda väldokumenterade och fungerande API:er och som kändes lätta att jobba med när man vill ”träna på” att göra mashupapplikationer.

## Schematisk bild
<img src="https://github.com/mg222cd/1DV449_Projekt/blob/master/Weather_Schema.png" alt="schematisk bild" />

## Serversida
* Den största delen av applikationens funktionalitet hanteras av serversidan. Jag har använt mig av ramverket ASP.NET MVC 5, dvs. MVC som arkitektur med C# som programmeringsspråk. Databasen hanteras med Entity Framework och är skapad i MS SQL Server.  Det är även här merparten av all cachning hanteras. 
* Applikationen är uppbyggd så att alla sökningar som görs på städer med dess prognoser lagras i databasen. Databasen fungerar som datakälla i de fall flera vädersökningar görs på samma ort inom kort tid (så länge prognosen är giltig) samt även om något av API:ern går ner. Här kontrolleras även de vanligaste felen (t.ex felstavad stad, inga sökträffar, inga väderprognoser, något av API:erna går ner, ingen anslutning m.m.) och dessa hanteras tillsammans med informationsmeddelande på sidan. Mer generella fel (400, 404, 500) hanteras av ramverket med allmänna felsidor.

## Klientsida 
* På klientesidan har jag använt mig av HTML5, CSS och JavaScript. Felhantering i samband med de regler som satts upp för inputfältet sker på klientsidan på samma sätt som för servern, men med snyggare felmeddelanden. För detta har jag använt Javascriptramverket jQuery. 
* För responsiv design används CSS-ramverket Bootstrap. Både söksida och prognossida fungerar på alla skärmtyper.
* På klientsidan sköts även cachning av sidans nödvändigaste resurser med AppCache.

## Säkerhet och prestandaoptimering
* I och med användandet av ramverket ASP.NET MVC 5 finns skydd inbyggt mot taggar, script, SQL och XSS-attacker. Gällande databasen använder sig applikationen av ORM-biblioteket Entity Framework och frågespråket LINQ, och i dessa finns skydd mot SQL-injections. Kommunikation med databas sker alltid via användaren ”appUser”, som i MS SQL endast tilldelats begränsade rättigheter till tabeller och operationer i databasen. 
Skydd finns även token-skydd mot XSRF-attacker (i ramverket i princip bara 2 st rader kod att skriva), på fältet där användaren skickar in en input. 

* När det kommer till optimering var även här fallet att mycket följde med ramverket gratis. För Javascript- och CSS-bibliotek sker automatisk minifiering av filer. För att minska inläsningstiden för filerna har jag länkat in CSS-filer i head och JS-filer längst ned i bodyn. 
Cachning av både städer och prognoser sker i databasen. För varje prognos-sökning görs en kontroll i databasen om aktuell/uppdaterad prognos redan finns för vald stad. Isåfall hämtas prognosen därifrån istället för från Yr.no's webbservice. Detta för optimeringen men även för att inte belasta YR:s webbservice i onödan. Databasen utgör även en backup om API:erna skulle gå ner.
I utformningen av flödet för en förfrågan har jag byggt upp strukturen så att inte alla träffar hos geonames.org efter en sökning på stad lagras i databasen,. Det är alltså bara staden som väljs av dem som Geonames.org returnerar som sparas i databasen. Detta för att inte databasen ska växa sig onödigt stor och lagra data som kanske aldrig kommer att användas. Sökningar på stora städer kan generera hundratals och ibland tusentals träffar. 

## Offline-first
* Min huvudtanke när jag jobbade med Offline-first har varit att applikationen fortfarande ska vara synlig även om anslutningen går ner, och att användaren ska slippa se en vit sida eller i värsta fall ingen sida alls. Istället visas den välbekanta väderapplikationssidan, men med informationsmeddelande om att uppkoppling saknas och att sidan inte fungerar i offline-läge. Här tas även login-funktionen helt bort och sökfältet samt knappen gråmarkeras för att tydligt klargöra för användaren att funktionerna inte är tillgängliga. Tekniken som använts för detta är AppCache.

* Min tanke kring Offline-first har också utgått ifrån hur applikationen ska bete sig om något av använda API:er skulle gå ner. Kontrollfunktioner finns för att testa både Geonames- och Yrs webbservices, och denna kontrollfunktion anropas alltid i samband med sökningar, dels som affärslogik (om API:et inte svarar sker istället sökning mot databasen) och dels som presentationslogik för att informera användaren om varför sökningen endast gav ett begränsat antal eller till och med inga träffar av städer eller prognoser. 

## Egen reflektion
* Övergripande tycker jag att arbetet med projektet fungerat bra, jag har kunnat använda mycket av det jag lärt mig under kursens gång och fått tillfälle att praktisk tillämpa den teori som diskuterats på peer-instructions och laborationstillfällen.
Min tanke från början var att jobba mot SMHI:s API, men jag valde tillslut YR pga att formatet på data bättre sammanföll med projektkraven från den andra kursen. 
* De problem jag stött på har mycket handlat om att lära mig ramverket ASP.NET MVC. Att arbeta mot ramverk är både positivt och negativt, man mister till viss del kontrollen över vissa delar av funktionaliteten, samtidigt som det underlättar med annan funktionalitet man slipper skriva själv. Jag har på slutet haft problem med kompileringsfel i Visual Studio-Projektet, som blev "trasigt" någonstans. Detta var väldigt svårt att felsöka. Felet uppstod i samband med implementeringen av Offline First. Jag fick ihop alla delar tillslut, men gräms över att felet uppstod efter att ASP.NET MVC-kursen slutat, varför jag missade chansen att få hjäkp med detta. Jag inser också att jag att jag borde ha implementerat Offline-tänket mycket tidigare under arbetets gång, dels för att själv få bättre förståelse men också för att lättare kunna få hjälp och handledning med de delar jag hade svårt att implementera. Jag hade en tanke om att lagra prognoserna med Local Storage och visa i offline-läge, men pga de oväntade problemen med Visual Studio-projektet fanns ej tid till detta. Jag hade även en tanke om att använda Google-autentiseringen till att kunna lagra favoritstäder med prognoser, men detta blev för stort för att hinnas med inom projekttiden.
* Jag tror inte att jag kommer att arbeta vidare med just den här applikationen, men jag skulle gärna bygga en väderapplikation igen, och kanske lägga till ytterligare API:er som SMHI och GoogleMaps. Förmodligen skulle jag då välja andra tekniker, exempelvis PHP, MySQL. 

## Risker med applikationen
* Den allvarligaste risken med applikationen som jag kan se är att jag inte skrivit säkerhetsfunktionaliteten själv utan helt förlitat mig på ramverkets hantering för detta. Hade detta varit en ”riktig” applikation hade man velat uppdatera den då ramverket uppdateras. Detta gäller dock egentligen alla applikationer, då säkerhetshål alltid måste försöka täppas igen allteftersom attackerna förändras.
* Att arbeta mot externa API:er utgör också en risk, då min applikation är helt beroende av att dessa är välfungerande och inte ändras. 

## Övrigt
* Pga. kombinationen med kursen ASP.NET MVC samt ovan nämnda strul med Visual Studio-projektet så har projektet under arbetsts gång flyttat några gånger på Github. För följa historik över gjorda commits, se dessa repositorier:
* [ASP.NET MVC - Väderappplikation](https://github.com/1dv409/mg222cd-2-1-individuellt-arbete)
* [Webbteknik II - Väderapplikation före flytt](https://github.com/mg222cd/Projekt_1dv449_1dv409)
