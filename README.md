# Roman_Petrica_Grupa_3133A
# Tema Laborator 2  

##1. Viewport
Un viewport în OpenGL reprezintă o portiune a ferestrei în care se va realiza randarea unui continut. De obicei este definit de coordonatele sale în fereastră si prezinta zona in care se vor desena diferitele obiecte.

##2. Frames Per Second (FPS)
FPS reprezintă numărul de cadre per secunda prevazute in bibleotecile OpenGL. Cu cât rata FPS-ului este mai ridicata, cu atat va creste fluiditatea continutului.

##3. Metoda OnUpdateFrame()
Metoda `OnUpdateFrame()` este rulată in OpenGL in bucla sa principala și este utilizată pentru a reimprospata starea jocului sau a scenei respective în fiecare cadru.

##4. Modul imediat de randare
Modul imediat (immediate mode) de randare în OpenGL este o tehnică mai veche de randare în care obiectele sunt desenate direct prin intermediul funcțiilor `glBegin()` și `glEnd()`. Această tehnică nu mai este sustinuta si recomandata in versiunile noi de OpenGL.

##5. Ultima versiune care acceptă modul imediat
Open GL 2.1 este ultima versiune ce suporta modul imediat, versiunile ulterioare incepand cu 3.0 nu mai ofera suport specific.

##6. Metoda OnRenderFrame()
Metoda `OnRenderFrame()` este rulată în bucla principală a aplicației OpenGL utilizata pentru randarea scenei si a obiectelor cadru cu cadru(in fiecare cadru).

##7. Metoda OnResize()
Metoda `OnResize()` rulata cel putin odata pentru a stabili dimensiunile ferestrei de randare. Ea va actualiza matricea de proiecție și va asigura că randarea se face corect atunci când fereastra este redimensionată de catre utilizator.

##8. CreatePerspectiveFieldOfView()
Metoda `CreatePerspectiveFieldOfView()` creaza o matrice de proiecție perspectivă în OpenGL. Această metodă primește trei parametri: unghiul de vedere vertical (FOV - field of view), raportul de aspect al ferestrei și planurile de proiecție apropiat și îndepărtat. Acești parametri controlează câmpul vizual al camerei.

- FOV: exprimat in grade si este unghiul de vedere.
- Raportul de aspect: Raportul stabilit intre latimea si inaltimea ferestrei.
- Planul apropiat și planul îndepărtat: Distanta de la camera intre planul apropiat si indepartat

### Exemplu de utilizare al acestora:
```c#
Matrix4 perspective = CreatePerspectiveFieldOfView(45.0f, aspectRatio, 0.1f, 100.0f);
