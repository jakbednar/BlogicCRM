# Uživatelská příručka · BlogicCRM

---

## 1  Co aplikace umí
BlogicCRM sjednocuje správu **smluv**, **klientů** a **poradců** do jediné webové aplikace.  
Umožňuje vytvářet, editovat a prohlížet záznamy s minimem kliknutí.

## 2  Požadavky
| Prostředí | Podmínka |
|-----------|----------|
| Prohlížeč | Edge 120+, Chrome 120+, Firefox 121+ |
| Rozlišení | min. 1366 × 768 px |
| Přístup | HTTPS na portu 5001 (lokálně) nebo 443 (produkce) |

## 3  Navigace v rozhraní
| Záložka | Funkce |
|---------|--------|
| **Domů** | úvodní obrazovka s logem a tlačítkem *Začít* |
| **Smlouvy** | tabulka smluv, tlačítka CRUD |
| **Klienti** | tabulka klientů, tlačítka CRUD |
| **Poradci** | tabulka poradců, tlačítka CRUD |

## 4  Typické činnosti se **smlouvou**

### 4.1  Přidání nové smlouvy
1. **Smlouvy → Přidat smlouvu**  
2. Vyplňte instituci, data platnosti, vyberte klienta a správce.  
3. Přidejte účastníky (minimálně jednoho – tím je správce smlouvy).  
4. Klikněte **Uložit** – smlouva se objeví v seznamu.

### 4.2  Zobrazení detailu smlouvy
1. V seznamu **Smlouvy** klikněte na řádek požadované smlouvy.  
2. Otevře se detail; jména **klienta** a **správce** jsou prokliknutelná na jejich karty.  
3. V detailu najdete také tlačítka **Upravit**, **Smazat** a **Zpět**.

### 4.3  Úprava smlouvy
1. V detailu klikněte **Upravit**.  
2. Proveďte změny (např. datum ukončení, účastníci).  
3. Klikněte **Uložit**.

### 4.4  Smazání smlouvy
1. V detailu nebo v seznamu smluv stiskněte **Smazat** (ikona 🗑️).  
2. Potvrďte dialog **„Opravdu chcete smlouvu trvale odstranit?“**  
3. Po potvrzení je smlouva odstraněna ze systému.  
   *Pokud mazání blokují závislé položky, zobrazí se hláška  
   „Smlouvu nelze smazat, dokud existují závislé záznamy.” – nejprve odstraňte vazby.*

---

## 5  Typické činnosti s **klientem**

### 5.1  Přidání nového klienta
1. **Klienti → Přidat klienta**  
2. Vyplňte osobní údaje (jméno, e-mail, telefon, rodné číslo apod.).  
3. Klikněte **Uložit** – klient se objeví v seznamu.

### 5.2  Zobrazení detailu klienta
1. V seznamu **Klienti** klikněte na řádek požadovaného klienta.  
2. V detailu vidíte osobní údaje a seznam jeho smluv.  
3. K dispozici jsou tlačítka **Upravit**, **Smazat** a **Zpět**.

### 5.3  Úprava klienta
1. V detailu klikněte **Upravit**.  
2. Upravte požadovaná pole.  
3. Klikněte **Uložit**.

### 5.4  Smazání klienta
1. V detailu nebo v seznamu klientů stiskněte **Smazat** (ikona 🗑️).  
2. Potvrďte dialog **„Opravdu chcete klienta trvale odstranit?“**  
3. Pokud má klient přiřazené smlouvy, systém zobrazí hlášku  
   *„Klienta nelze smazat, dokud existují navázané smlouvy.”*

---

## 6  Typické činnosti s **poradcem**

### 6.1  Přidání nového poradce
1. **Poradci → Přidat poradce**  
2. Vyplňte osobní údaje a kontakty.  
3. Klikněte **Uložit** – poradce se objeví v seznamu.

### 6.2  Zobrazení detailu poradce
1. V seznamu **Poradci** klikněte na řádek požadovaného poradce.  
2. V detailu vidíte osobní údaje a seznam smluv, kde vystupuje jako správce nebo účastník.  
3. K dispozici jsou tlačítka **Upravit**, **Smazat** a **Zpět**.

### 6.3  Úprava poradce
1. V detailu klikněte **Upravit**.  
2. Upravte požadovaná pole.  
3. Klikněte **Uložit**.

### 6.4  Smazání poradce
1. V detailu nebo v seznamu poradců stiskněte **Smazat** (ikona 🗑️).  
2. Potvrďte dialog **„Opravdu chcete poradce trvale odstranit?“**  
3. Pokud je poradce uveden u některé smlouvy, systém zobrazí hlášku  
   *„Poradce nelze smazat, dokud existují navázané smlouvy.”*


---

*Barevné schéma aplikace : oranžová `#ff5924`, fialová `#513189`.* 
