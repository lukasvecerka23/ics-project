# ICS Projekt 2023 - Time Tracker
## **Autoři**
- Lukáš Večerka (xvecer30)
- Veronika Nevařilová (xnevar00)
- Alina Penkova (xpenko00)
- Jaroslav Streit (xstrei06)
- Jáchym Doležal (xdolez0c)


## **Fáze 1**

### Organizace

- komunikační kanál: Discord
- Azure Boards: rozdělení úkolů dle metodiky SCRUM
- týdenní schůzky: Zhodnocení,Plánování
### Repozitář

- pracovní verze ve větvi Dev
- automatický build a testování pomocí [![Build Status](https://dev.azure.com/ics-2023-xvecer30/project/_apis/build/status/project-.NET%20Desktop-CI?branchName=dev)](https://dev.azure.com/ics-2023-xvecer30/project/_build/latest?definitionId=1&branchName=dev)

### Datový model

#### Entity
- **User**: jméno, příjmení, URL obrázku,
    - má vlastní **seznam tagů**, které může libovolně vytvářet
    - může vytvořit libovolný počet projektů
    - může se příhlásit do existujícího **projektu**
    - může vytvářet libovolný počet **aktivit**
- **Tag**: jméno
    - obsahuje seznam **aktivit**, které jsou spojené daným **tagem**.
    - Obsahuje ID uživatele, který daný **tag** vytvořil
- **Project**: jméno
    - Obsahuje ID uživatele, který daný **projekt** vytvořil
    - Obsahuje **seznam** přihlášených uživatelů
    - Obsahuje **seznam** **aktivit** spojené s daným projektem
- **Activity**: popis, začátek, konec
    - Obsahuje ID uživatele, který daný **projekt** vytvořil
    - Obsahuje ID projektu, ke kterému může být **aktivita** přiřazena
    - Obsahuje ID tagu, ke kterému může být **aktivita** přiřazena
- **ProjectAssign**: vazební entita mezi entitami **User** a **Project**

### Testy

- Vytvořili jsme testy na DBContext pro jednotlivé entity

### Wireframe

- odkaz: [WireFrame s Flow sekcí](https://www.figma.com/file/rNclgcEzhsTbxAy5JjYLmt/prototype_2?node-id=0%3A1&t=8RDA6lxnLNI0J51D-0) , [WireFrame - pouze okna](https://www.figma.com/file/CR9VOebRYVmv749cf4yPlV/prototype-1?node-id=0%3A1&t=VXJItfW64tPG6yvp-0)
    - poznámka: Pro zakliknutí Kalendáře, Projektu, Tagů nebo Aktivit se musí použijte double-click na myši.

###

## **Fáze 2**

- Oprava výhrad z první fáze

### Modely, Model mappery

- Dle wireframů rozhodnutí, které atributy chceme uchovávat v modelech
- Namapování entit z datové vrstvy do modelů na byznysové vrstvě

### Fasády

- Vytvoření bázové fasády pro zajištění CRUD operací nad databází
- Zjištění byznysových požadavků
- Filtrování aktivit podle data, projektu, tagu atd.
- Kontrola konfliktních aktivit
- Získaní tagů podle ID uživatele
- Zajištění napojení přihlášení do projektu pro detail projektu a detail uživatele

### Testy fasád

- Otestování funkčnosti jednotlivých byznysových požadavků


## **Fáze 3**

- In progress

# Vývoj

- Vývoj na branchi Dev

- nové změny implemetujte z nejnovější verze Dev
    - Vytvořit novou branch dle doporučení níže

## Doporučení

### **Testy**

- testy implementovat na branchi: test/*

### **Nové funkcionality**

- branch: feature/*

### **Bugfix**

- branch: fix/*

### **Konec nové změny**

- Pull request z nové branch do Dev

### **Konec fáze projektu**

- Provést Pull Request do branch Main
- Codereview
