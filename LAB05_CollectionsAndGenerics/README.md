# Generikusok és kollekciók laboratórium

## Labor célkitűzése

Megismerkedés a C#-ban használt főbb kollekciókkal és generikus függvényekkel.

## Feladatok

(A labor elején, már most hozz létre egy új branchet és utána azon dolgozz, hogy a pull requestet könnyű legyen majd a labor végén létrehozni! És ha az egyetemen kezdted el a munkát, mielőtt elmész, ne felejtsd el felpusholni a változásokat a laborgépről a githubra!)

A labor során két nagyobb feladatot kell megoldani. Az első különböző típusú tárolók vizsgálatával foglalkozik, a második során TDD elven kell egyszerű műveleteket implementálni. (A unit tesztek már készen vannak.)

## Első feladat

A feladatot egy már megvalósított repository (tároló) osztály vizsgálatával kezdjük. Ebben a repository-ban User-eket tárolunk és keresésnél id alapján visszaadjuk őket. A fejlesztők megvalósítottak már egy ilyen repository-t ArrayUserRepository néven és számoltak a bővíthetőség lehetőségével ezért minden helyen, ahol szükség van erre a repository-ra nem ezt a konkrét típust használják, hanem egy interface-t, amit az ArrayUserRepository is használ: IUserRepository. A feladat során ezt az interface-t implementáló többi osztály kiegészítése, bővítése a feladat egy pár szempont figyelembevételével.

Vizsgáljuk meg, hogy mit csinál a program majd próbáljuk meg futtatni! Mi miatt nem működik az alkalmazás? Próbáljuk meg kijavítani.

Hint: tömb méret

Ahogy láttuk, az első megoldás egy tömbben tárolja a felhasználókat és ideiglenesen a tömb méretének megváltoztatásával tudtuk orvosolni a problémát. Ez azonban éles projektnél nem jó megoldás. A program nem fog működni, ha egyre több és több User adatait szeretnénk eltárolni és nem lesz lehetőségünk a már meglévőket folyton lementeni, szervert/programot újraindítani, bugfix-elni....

Implementáljuk a ListUserRepository-t! Használjunk tömb helyett egy egyszerű listát!

Miután listát használunk tömbök helyett, a kódrészlet működik. Elkezdik mások is használni ezt, majd azzal a panasszal fordulnak hozzánk, hogy nem elég gyors az alkalmazás. A User-ek lekérdezése egyre hosszabb időt vesz igénybe minél többet tárolunk. Ekkor a fejlesztő csapatnak az az ötlete támad, hogy a User-eket sorrendbe töltsük fel, mert akkor lehet rajtuk bináris keresést végezni.

Készítsük el a User-eket rendezetten tároló listánkat! Egészítsük ki az OrderedListUserRepository osztályt!

Hint: Maga a bináris keresés algoritmusa adott, csak ki kell kommentezni, a rendezett beszúrás viszont megvalósítandó!

Vizsgáljuk meg a futási időket! Mennyivel tart tovább a beszúrás, mint eddig? A választ ide írd be és majd commitold ennek a fájlnak a változásait is a megoldással együtt:

VÁLASZ: Régi futásidő: 00:00:000140799, új futásidő: 00:00:00.5682822

Készítsünk egy olyan megvalósítást, ami egy másik adatstruktúrát használ, ahol új elem bárhova beszúrása könnyedén lehetséges!

Hint: LinkedList (és megfelelő függvényei)

A láncolt listás megvalósításnál a bináris keresés lelassult, mivel az elemeket nem tudjuk közvetlenül címezni. Nem nyertünk sokat a listás megvalósításhoz képest. Az elemek sorrendben tárolása nem is annyira fontos, csak a gyors elérésük a cél. A User-eket id-juk alapján akarjuk elérni.

VÁLASZ: A MeasureGetByIdNumber által mért futásidő ebben az esetben: 7.984 s

Használjunk egy olyan adatstruktúrát, ahol az egyes elemeket valamilyen kulcs alapján gyorsan el lehet érni!

Hint: Dictionary (és megfelelő függvényei)

A példaalkalmazás méri, hogy az egyes megvalósítások mennyi idő alatt végzik el a beszúrást és nagyobb számban a lekérdezést. Miután elkészültünk a repository-k különböző adatstruktúrás megvalósításával jegyezzük le az alábbi táblázatba, hogy melyik megoldás mennyi ideig végezte a műveleteket!


Adattároló      | Feltöltési idő    | Lekérdezési idő
----------------|-------------------|----------------
Array           |00:00:00.0079707   |00:00:01.238405
List            |00:00:00.01400799  |00:00:01.4001019
List (sorted)   |00:00:00.5682822   |00:00:00.0266554
LinkedList      |00:00:00.6761937   |00:00:07.8943655
Dictionary      |00:00:00.0029989   |00:00:00.0016540

## Második feladat

A feladat során egy wrapper (csomagoló) osztályt kell készíteni Dictionary adatstruktúrához. Tudnunk kell tárolni egy bolt számára könyveket és játékokat (Book, Game), de lehet, hogy később bővül a kínálat és más dolgokat is tudnunk kell tárolni, aminek van id-ja és raktáron tárolt darabszáma (IStorable).

A tárolótól az alap CRUD (Create-Read-Update-Delete) műveletek elvártak. A csomagolóhoz készített unit tesztek a műveletek elvárt működését írják le. Próbáljuk meg ezek alapján implementálni az osztályt (Store)!

Hints:
* Tesztelgessük, próbálgassuk magunk is a tároló működését! Ehhez legyen a kiinduló project a solution-ön belüli a Storage project (jobb klikk, Set as StartUp Project).
* Új termék raktározását a Store.Insert() végzi. Raktározásnál fel tudunk egy elemet vagy Store.InsertMany()-vel egy egész lista tartalmát vinni a saját tárolónkba.
* Építsünk már meglévő függvényekre! Store.InsertMany() ugyanazt a beillesztést, ugyanazokkal a validációkkal hajtja végre, mint a Store.Insert(), csak éppen a kapott lista minden elemére.
* Egy elem beillesztésénél érdemes az alábbi dolgokra figyelni:
  * Dictionary-t használunk. Hogyan viselkedik a Dictionary, ha null kulcs értéket akarunk beszúrni? Hogyan kellene viselkednie a tesztek alapján?
  * Mi van, ha a beszúrandó elem null?
  * Hogyan viselkedik a Dictionary, ha már létező kulccsal szeretnénk másik elemet beszúrni?
  * Nincs sok értelme, ha olyan új elemet raktárazunk, amiből 0 vagy esetleg negatív mennyiség áll rendelkezésre.
  * Használjuk a C# környezetben használt kivételeket, ha azok leírása, jellege megegyezik a saját problémánkkal!
* Id alapján történő lekérdezést már láthattunk az előző feladatban.
* A bolt vásárlását és eladását a Store.Buy() és Store.Sell() metódusok valósítják meg.
* Vásárlásnál (Buy) gondoljunk olyan esetekre is, amikor még nem raktározott elemből veszünk, illetve ha negatív mennyiséget "veszünk".
* Eladásnál (Sell) gondoljunk olyan esetekre is, amikor nincs elég eladandó termék raktáron vagy nem létező termékből akarunk eladni valamennyit.
* Ha valamilyen terméket már nem szeretne tovább forgalmazni a bolt, akkor azt tudni kell törölni is a nyilvántartásból. Gondoljuk olyan esetekre is, amikor nem létező terméket szeretnénk törölni.

Miután megvagyunk a csomagolóval, készítsünk egy új terméket! (Ez lehet bármi. :) ) Ennek a terméknek is tárolhatónak kell lennie a boltban.

Utolsó feladatként vizsgáljuk meg a Program.cs fájlban kikommentezett Iterate, Modify és Reassign függvényeket! Ezek a kollekciókon való iterálást, módosítást, új értékadást szemléltetik. Milyen következtetésre jutunk az új értékadás esetében?

Hint: foreach
