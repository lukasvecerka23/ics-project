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

## **Fáze 2**

- In progress

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
