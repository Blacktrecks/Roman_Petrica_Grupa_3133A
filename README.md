# Roman_Petrica_Grupa_3133A
# Tema Laborator 2  
```bash
# Raspunsuri intrebari
```


## 1. Viewport
Un viewport în OpenGL reprezintă o portiune a ferestrei în care se va realiza randarea unui continut. De obicei este definit de coordonatele sale în fereastră si prezinta zona in care se vor desena diferitele obiecte.

## 2. Frames Per Second (FPS)
FPS reprezintă numărul de cadre per secunda prevazute in bibleotecile OpenGL. Cu cât rata FPS-ului este mai ridicata, cu atat va creste fluiditatea continutului.

## 3. Metoda OnUpdateFrame()
Metoda `OnUpdateFrame()` este rulată in OpenGL in bucla sa principala și este utilizată pentru a reimprospata starea jocului sau a scenei respective în fiecare cadru.

## 4. Modul imediat de randare
Modul imediat (immediate mode) de randare în OpenGL este o tehnică mai veche de randare în care obiectele sunt desenate direct prin intermediul funcțiilor `glBegin()` și `glEnd()`. Această tehnică nu mai este sustinuta si recomandata in versiunile noi de OpenGL.

## 5. Ultima versiune care acceptă modul imediat
Open GL 2.1 este ultima versiune ce suporta modul imediat, versiunile ulterioare incepand cu 3.0 nu mai ofera suport specific.

## 6. Metoda OnRenderFrame()
Metoda `OnRenderFrame()` este rulată în bucla principală a aplicației OpenGL utilizata pentru randarea scenei si a obiectelor cadru cu cadru(in fiecare cadru).

## 7. Metoda OnResize()
Metoda `OnResize()` rulata cel putin odata pentru a stabili dimensiunile ferestrei de randare. Ea va actualiza matricea de proiecție și va asigura că randarea se face corect atunci când fereastra este redimensionată de catre utilizator.

## 8. CreatePerspectiveFieldOfView()
Metoda `CreatePerspectiveFieldOfView()` creaza o matrice de proiecție perspectivă în OpenGL. Această metodă primește trei parametri: unghiul de vedere vertical (FOV - field of view), raportul de aspect al ferestrei și planurile de proiecție apropiat și îndepărtat. Acești parametri controlează câmpul vizual al camerei.

- FOV: exprimat in grade si este unghiul de vedere.
- Raportul de aspect: Raportul stabilit intre latimea si inaltimea ferestrei.
- Planul apropiat și planul îndepărtat: Distanta de la camera intre planul apropiat si indepartat

### Exemplu de utilizare al acestora:
```c#
Matrix4 perspective = CreatePerspectiveFieldOfView(45.0f, aspectRatio, 0.1f, 100.0f);
```

## Tema Laboratoe 3

## 1. Ordinea de desenare a vertexurilor
```
  GL.Begin(PrimitiveType.Lines);

    // Axa X deseneasza (roșie)
    GL.Color3(Color.Red);
    GL.Vertex3(-1.0f, 0.0f, 0.0f);
    GL.Vertex3(1.0f, 0.0f, 0.0f);

    // Axa Y deseneaza (mov)
    GL.Color3(Color.Purple);
    GL.Vertex3(0.0f, -1.0f, 0.0f);
    GL.Vertex3(0.0f, 1.0f, 0.0f);

    // Axa Z deseneaza (albastră)
    GL.Color3(Color.Blue);
    GL.Vertex3(0.0f, 0.0f, -1.0f);
    GL.Vertex3(0.0f, 0.0f, 1.0f);

    GL.End();
```
Ordinea de desenare a vertex-urilor este anti-orar. Putem pune in aplicație un Begin la început când se dorește desenarea liniilor si end la final, deoarece se lucrează cu același tip de primitive.

## 2. Amti-aliasing
`Anti-aliasing` utilizează diverse tehnici pentru a scăpa de marginile zimțate de pe ecran, care apar ca urmare a desenării unei drepte oblice pe ecran atunci când  poziționarea naturală a pixelilor pe ecran este cea  perpendiculară.
Un exemplu de anti-aliasing este: `amplificam cu un factor de scalare imaginea` apoi o randăm la dimensiunea inițială  pentru a pasta o claritate cat mai bună.

## 3. GL.LineWidth(float) & GL.PointSize(float)
Prin folosirea comenzii `GL.LineWidth(float)` vom putea mari diametrul liniei(ingrosare), iar prin folosirea `GL.PointSize(float)` si nu functioneaaza inafara zonei GL.Begin.

## 4. 1. LineLoop
Efectul  acestei directive `LineLoop` creeaza legatura intre liniile desenate pe ecran in care ultimul vertex desenat este conectat la primul vertex.

## 4. 2. LineStrip
Prin folosirea `LineStrip`, putem crea legaturi intre segmentele desenaate pe ecran, iar la fiecare 2 segemente se va specifica un vertex de legatura.

## 4. 3. TriangleFan
Se deseneaza triunghiuri multiple precum `TriangleStrip`, cu mica exceptie ca sunt au o pozitie circulara.

## 4. 4. TriangleStrip
Deseneaza triunghiuri conectate pe ecran, fiecare vertex este specificat dupa ce s-au dat 3 vertex-uri pentru crearea unui triunghi.

## 6. Avantajul si importanta utilizarii culorii diferite in desenarea obiectelor 3D
Utilizarea culorilor diferite sau a gradientului scot in evidenta mai bine formele obiectelor si efectul 3D. Avantajul e dat de efectul de pronuntare 3D al obiectelor ce face diferenta in grafica.

## 7. Gradientul de Culoare
Reprezinta o selectie de culori care evidentiaza trecerea de la o culoare la alta. Folosind OpenGL putem realiza acest lucru prin specificarea culorii vertex-urilor ce creaza o anumita figura si astfel se va realiza gradient de la un vertex la altul.

## 8. Canalul de Transparenta
