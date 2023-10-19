# Roman_Petrica_Grupa_3133A
# OpenGL Concepts - README

## Viewport
Un viewport în OpenGL reprezintă o parte a ferestrei de desen în care se va realiza randarea. Acesta este definit de coordonatele sale în fereastră și specifică zona în care se vor desena obiectele.

## Frames Per Second (FPS)
FPS reprezintă numărul de cadre pe secundă procesate și afișate de biblioteca OpenGL. Cu cât FPS-ul este mai mare, cu atât jocul sau aplicația va părea mai fluidă.

## Metoda OnUpdateFrame()
Metoda `OnUpdateFrame()` este rulată în bucla principală a aplicației OpenGL și este utilizată pentru a actualiza starea jocului sau a scenei în fiecare cadru.

## Modul imediat de randare
Modul imediat (immediate mode) de randare în OpenGL este o tehnică veche de randare în care obiectele sunt desenate direct folosind funcții precum `glBegin()` și `glEnd()`. Această tehnică nu mai este recomandată și nu este susținută în versiunile OpenGL mai recente.

## Ultima versiune care acceptă modul imediat
Versiunile OpenGL 3.0 și ulterioare nu mai suportă modul imediat de randare. Prin urmare, ultima versiune care îl acceptă este OpenGL 2.1.

## Metoda OnRenderFrame()
Metoda `OnRenderFrame()` este rulată în bucla principală a aplicației OpenGL și este utilizată pentru randarea obiectelor și a scenei în fiecare cadru.

## Metoda OnResize()
Metoda `OnResize()` trebuie să fie executată cel puțin o dată pentru a seta corect dimensiunile ferestrei de randare. Ea este utilizată pentru a actualiza matricea de proiecție și a asigura că randarea se face corect atunci când fereastra este redimensionată.

## CreatePerspectiveFieldOfView()
Metoda `CreatePerspectiveFieldOfView()` este utilizată pentru a crea o matrice de proiecție perspectivă în OpenGL. Această metodă primește trei parametri: unghiul de vedere vertical (FOV - field of view), raportul de aspect al ferestrei și planurile de proiecție apropiat și îndepărtat. Acești parametri controlează câmpul vizual al camerei.

- FOV: Unghiul de vedere vertical exprimat în grade.
- Raportul de aspect: Raportul dintre lățimea și înălțimea ferestrei.
- Planul apropiat și planul îndepărtat: Distanta de la camera la planurile de proiecție apropiat și îndepărtat.

### Exemplu de utilizare:
```c++
Matrix4 perspective = CreatePerspectiveFieldOfView(45.0f, aspectRatio, 0.1f, 100.0f);
