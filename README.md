# Sistemos paskirtis

Projekto tikslas – Palengvinti dėstytojų darbą su studentais, sukurti vietą internete talpinti paskaitų medžiagą bei uždavinius, vesti pažymius.

Veikimo principas – Sistemą sudaro dvi dalys – internetinė aplikacija, kurią gali naudoti galutiniai sistemos vartotojai, bei API, kuriuo gali prisijungti trečių šalių klientinės aplikacijos

# Funkciniai reikalavimai

Neregistruotas sistemos vartotojas gali:
+	Peržiūrėti egzistuojančių kursų sąrašą
+	Registruotis sistemoje kaip dėstytojas
+	Prisijungti

Registruotas sistemos vartotojas (Dėstytojas) gali:
+	Paskaitų valdymas
-	Kursuose kurti paskaitas
-	Modifikuoti savo sukurtas paskaitas
-	Ištrinti savo sukurtas paskaitas
+	Pažymių valdymas
-	Pridėti naujus pažymius
-	Keisti savo sukurtus pažymius
-	Trinti savo sukurtus pažymius

Administratorius gali:
+	Kurti naujus kursus
+	Keisti ir trinti kitų naudotojų sukurtas paskaitas bei pažymius

# Sistemos architektūra

Sistemos sudedamosios dalys:
•	Kliento pusė (angl. Frontend) – naudojantis React.js ir Typescript.
•	Serverio pusė (angl. Backend)  – naudojantis .NET ir ASP.NET karkasu.

2.1 pav. atvaizduojama kuriamos sistemos diegimo diagrama. Sistema bus talpinama naudojantis Azure server paslaugomis. Visos sistemos dalys talpinamos toje pačioje erdvėje. Internetinė aplikacija bendrauja su vartotoju naudojantis HTTP protokolu. Jos veikimas paremtas duomenų mainas su „Mokymo sistema“ API. Pats sistemos API gauna duomenis iš MSSQL duombazės naudojantis TCP protokolu.

 ![image](https://user-images.githubusercontent.com/15565869/191912182-b448759d-cff7-4b6d-81ed-8a957ab269b9.png)

# Vartotojo sąsajos eskizai ir realizacija
Registracija / Prisijungimas
 ![image](https://user-images.githubusercontent.com/15565869/208710506-80c07f83-ce24-425e-ad0e-c5d215674def.png)
 ![image](https://user-images.githubusercontent.com/15565869/208710545-f30cc603-0818-40ec-8236-e44c612a2a4c.png)

Kursai
 ![image](https://user-images.githubusercontent.com/15565869/208710601-a573932c-40d9-444d-80dd-7cbfe6f6c222.png)
![image](https://user-images.githubusercontent.com/15565869/208710634-e8e7a7bb-5466-487d-8289-44416d6838d5.png)

Paskaitos:
 ![image](https://user-images.githubusercontent.com/15565869/208710674-1925338f-c8e6-4bc2-a221-2650ccbe51c4.png)
![image](https://user-images.githubusercontent.com/15565869/208710689-f1b53209-b4e4-4fa6-a30b-d705b1c6ccf3.png)

Pažymiai
 ![image](https://user-images.githubusercontent.com/15565869/208710725-7e800fd6-4b11-4640-ae8f-7006a5e2080e.png)
![image](https://user-images.githubusercontent.com/15565869/208710745-9e81295d-cdea-4999-b3e3-911c2dbad0dd.png)

# API Specifikacija
Sukurti kursą
![image](https://user-images.githubusercontent.com/15565869/208711008-07911e05-4e0b-4e2c-8428-ab7849485946.png)

Gauti visus kursus
![image](https://user-images.githubusercontent.com/15565869/208711026-d0a0460a-9924-43dc-be5b-028f42705398.png)

Gauti kursą
 ![image](https://user-images.githubusercontent.com/15565869/208711055-aba1743b-b02b-453a-962b-245ce7ea73ed.png)

Keisti kursą
 ![image](https://user-images.githubusercontent.com/15565869/208711092-df7a13a3-1e5b-4e8d-b7db-81d588773d8a.png)

Ištrinti kursą
 ![image](https://user-images.githubusercontent.com/15565869/208711127-2687e15c-d833-4914-bb67-f54b1ece0800.png)

Gauti paskaitas
 ![image](https://user-images.githubusercontent.com/15565869/208711148-ae2e46bf-3ced-46bd-9d5b-45aec26b7295.png)

Sukurti paskaitą
 ![image](https://user-images.githubusercontent.com/15565869/208711174-24947e5f-83e3-4616-a4b4-200ab23f51c5.png)

Gauti paskaitą
 ![image](https://user-images.githubusercontent.com/15565869/208711201-ce2b407f-7793-4e87-9e30-8fe097b29983.png)
 
Pakeisti paskaitą
 ![image](https://user-images.githubusercontent.com/15565869/208711235-8dfdc96f-91d0-4214-bddd-bef1a41cf3ed.png)

Ištrinti paskaitą
 ![image](https://user-images.githubusercontent.com/15565869/208711251-67c9dc82-cb24-458b-a92d-891f6616b66b.png)

Gauti pažymius
 ![image](https://user-images.githubusercontent.com/15565869/208711279-89a5a0a9-57a1-43c4-ba0d-47fadb1ae2c3.png)

Sukurti pažymį
 ![image](https://user-images.githubusercontent.com/15565869/208711301-5277363a-1cf0-4e67-a233-51cdd3563f96.png)

Gauti pažymį
 ![image](https://user-images.githubusercontent.com/15565869/208711335-63c611cc-ba7e-4abb-a614-c909e4242a08.png)

Pakeisti pažymį
 ![image](https://user-images.githubusercontent.com/15565869/208711344-d131865d-95ac-432f-97c0-0144f8845e85.png)

Ištrinti pažymį
 ![image](https://user-images.githubusercontent.com/15565869/208711360-0f18a8c4-bdc7-4810-a2ad-a2cdeef0f3ba.png)

# Išvados
Semestro metu pavyko sukurti veikiantį projektą, naudojant „React“ „.Net“ technologijas, pasinaudojant MSSQL duomenų baze. Visas projektas sėkmingai patalpintas „Azure“ debesyje, todėl yra patikimai pasiekiamas internetu visame pasaulyje.
 
