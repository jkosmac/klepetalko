Klepetalko

Jaka Kosmač, 63230152

Anže Kos, 63230151

Github repozitorij:

https://github.com/jkosmac/klepetalko

https://github.com/jkosmac/KlepetalkoApp

Dokumentacija API

https://klepetalko.azurewebsites.net/swagger/index.html


Klepetalko je spletna aplikacija, ki pmogoča uporabnikom, da se registrirajo, dodajajo in komunicirajo prek besedilnih sporočil.

Uporabnik se ob vstopu aplikacije mora najprej registrirati ali prijaviti. Ko je prijavljen ima nad sabo toolbar z različnimi funkcionalnostmi. Ima opcijo za izbiranje med Klepeti, Prijatelji, Temni način, Nastavitve in Odjava.

Klepeti
Uporabnik ima pregled nad vsemi ustvarjenimi klepetalnicami. Vidni so mu tudi klepeti uporabnikov, ki so njega dodali med svoje stike, čeprav jih sam ni dodal. Na ta način ima opcijo, da ga doda nazaj, ali pa ga odstrani.
 
 ![image](https://github.com/user-attachments/assets/27816ba2-7d47-487a-91c8-115f14af1556)

Klepetalnica
Ko uporabnik odpre pogovor ima opcijo klepeta in pregled poslanih sporočil. Uporabnik ima tudi opcijo spremembe naslova klepeta z klikom na ikone za svinčnik.
 
 ![image](https://github.com/user-attachments/assets/cf946a0e-7080-4865-9693-832d49e8ff16)

Prijatelji
Uporabnik ima pregled nad vsemi dodanimi prijatelji.
 
 ![image](https://github.com/user-attachments/assets/cd56e665-0e8e-4f02-a32b-a02c188f5b80)

Temni način
Omogoča spremebe UI vmesnika iz svetlo na temno (primer na zavihku Klepeti).
 
 ![image](https://github.com/user-attachments/assets/20695091-c339-42f5-b7af-e8fcb46fb1ca)

Nastavitve
Nastavitve osebnih podatkov (default generirane).
 
 ![image](https://github.com/user-attachments/assets/54bd2db8-2749-4e09-ba1a-07ab60f93672)

Po končanem razvoju aplikacije na lokalnem strežniku (docker), sva aplikacijo naložila na spletni strežnik/oblak Azure. Na Azure sva najprej objavila podatkovno bazo, nato pa še aplikacijo preko GitHub repozitorija.

Nato sva potem še implementirala Web API kontroler in na koncu dodala samodejno zgenerirano dokumentacijo za uporabo vmesnika (Swagger UI).
 
 ![image](https://github.com/user-attachments/assets/45068883-d573-478f-bbbb-9aa790a0cf3d)

Android odjemalec:

<img width="119" alt="image" src="https://github.com/user-attachments/assets/ad08a187-2108-49bb-aa0e-1c65bca0549a" />

Podatkovni model:

<img width="335" alt="image" src="https://github.com/user-attachments/assets/1c0ad286-476f-4840-a2ad-0e7998da4c25" />
