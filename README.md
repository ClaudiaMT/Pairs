Să se realizeze in C# si WPF (Windows Presentation Foundation) jocul Pairs  
 
Jocul va avea o pagina de “Sign in...”  
Aceasta fereastra permite de asemenea crearea unui nou utilizator si asocierea lui cu o imagine, a carei cale se preia prin fereastra de dialog. Jucatorul poate de asemenea sa aleaga din conturile existente. Butoanele „Delete User” si „Play” vor fi initial inactive, devenind active doar in momentul cand este selectat un utilizator. 
 
2. Jocul 
 
La click pe Play va aparea o fereastra cu urmatorul meniu:  File     -     New Game – incepe un nou joc;  - Open Game – se deschide un joc pentru jucatorul curent, pe care l-a salvat anterior; a se vedea sectiunea 3 - Save Game – se salveaza jocul curent; a se vedea sectiunea 3 
- Exit – se iese din fereastra curenta, se ajunge in cea de login, din care se poate alege alt utilizator, sterge cont de utilizator, crea nou cont sau iesire din joc. 
 
Options: - Beginner (tabla de joc va fi de dimensiune 4x4) - Intermediate (tabla de joc va fi de dimensiune 6x6)  - Custom (tabla de joc va fi de dimensiune specificata de utilizator - MxN).  Help – About, in care sa apara o fereastra cu numele studentului, numarul grupei si specializarea.  
 
La apasarea butonului „New Game” se deschide o fereastra pe care se vor incarca toate jetoanele care vor trebui intoarse pe fata. Daca s-a dat click consecutiv pe 2 butoane, acestea se vor intoarce cu fata. Daca imaginile de pe fata acestora sunt identice, jetoanele vor disparea. In caz contrar, la apasarea celui de-al treilea jeton, cele 2 anterioare se intorc la loc cu spatele si cel de-al treilea ramane cu fata, si tot asa pana se scurge timpul sau pana dispar toate jetoanele de pe tabla de joc. 
 
Utilizatorul va avea posibilitatea de a specifica intervalul de timp in care va trebui sa termine jocul. Daca in acel timp nu se intorc toate jetoanele de pe tabla, jocul e pierdut. Atunci cand se salveaza jocul, va trebui sa se aiba in vedere timpul setat si cel scurs pana in momentul salvarii. 
 
3. Salvarea si deschiderea jocului 
 
Jucatorul va avea posibilitatea de a-si salva jocul (configuratia jetoanelor si timpul ramas pana la terminarea jocului) in orice moment al desfasurarii acestuia si apoi de a-l redeschide si de a continua de unde a ramas. 
 
4. Stergerea unui utilizator 
 
Stergerea unui utilizator implica stergerea acestuia din fisier, stergerea asocierii cu imaginea si stergerea oricarui joc salvat de catre acesta. 
