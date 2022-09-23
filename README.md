# Sistemos paskirtis

Projektas skirtas valdyti įvairius mokymo kursus, leidžiant dėstytojams talpinti medžiagą bei valdyti studentus
Veikimo principas – naudotojams bus pateikta grafinė sąsaja, kuri mainysis duomenimis su aplikacijų programavimo sąsaja (angl. API).

Tėviniais ryšiais sujungti objektai – kursas, paskaita ir įvertinimas. 

Studentas, išsirinkęs kursą į jį užsiregistruoja, bei gauna prieiga prie visų tame kurse įkeltų paskaitų. Paskaitose kurioms sukurti atsiskaitymai, studentas gali įkelti padarytą darbą. 

Dėstytojai, savo priskirtuose kursuose gali modifikuoti paskaitų medžiagas, tam tikroms paskaitoms sukurti atsiskaitymus, matyti įkeltus atsiskaitymo darbus bei juos įvertinti pažymiu ir / ar komentaru. 

Administratorius valdo visus sistemoje esančius kursus, sukuria bei priskiria kursams dėstytojus, gali matyti visų studentų pažymius, bei gali pašalinti studentus iš tam tikro kurso

# Funkciniai reikalavimai

Studentas:
1.	Prisijungti prie sistemos.
2.	Registruotis sistemoje.
3.	Peržiūrėti siūlomų kursų sąrašą
4.	Registruotis į kursą
5.	Atsisiųsti paskaitų medžiagą
6.	Įkelti paskaitos darbus

Dėstytojas:
1.	Prisijungti prie sistemos.
2.	Įkelti kurso paskaitos medžiagą
3.	Naikinti paskaitos medžiagą
4.	Sukurti atsiskaitymą
5.	Įvertinti atsiskaitymą

Administratorius:
1.	Prisijungti prie sistemos.
2.	Sukurti dėstytojo vartotoją
3.	Pašalinti dėstytojo vartotoją
4.	Sukurti kursą
5.	Naikinti kursą
6.	Šalinti studentą iš kurso
7.	Peržiūrėti studento visų kursų pažymius


# Sistemos architektūra

Sistemos sudedamosios dalys:
•	Kliento pusė (angl. Frontend) – naudojantis React.js ir Typescript.
•	Serverio pusė (angl. Backend)  – naudojantis .NET ir ASP.NET karkasu.

2.1 pav. atvaizduojama kuriamos sistemos diegimo diagrama. Sistema bus talpinama naudojantis Azure server paslaugomis. Visos sistemos dalys talpinamos toje pačioje erdvėje. Internetinė aplikacija bendrauja su vartotoju naudojantis HTTP protokolu. Jos veikimas paremtas duomenų mainas su „Mokymo sistema“ API. Pats sistemos API gauna duomenis iš MSSQL duombazės naudojantis TCP protokolu.

 ![image](https://user-images.githubusercontent.com/15565869/191912182-b448759d-cff7-4b6d-81ed-8a957ab269b9.png)

