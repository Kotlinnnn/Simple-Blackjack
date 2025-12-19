import sys

def zadania():
    while True:
        print("\n--- MENU PYTHON ---")
        print("1. Kalkulator | 2. Temp | 3. Srednia | 0. Wyjscie")
        choice = input("Wybor: ")
        
        if choice == '1':
            try:
                a = float(input("Liczba 1: "))
                b = float(input("Liczba 2: "))
                op = input("Znak (+,-,*,/): ")
                if op == '+': print(f"Wynik: {a+b}")
                elif op == '-': print(f"Wynik: {a-b}")
                elif op == '*': print(f"Wynik: {a*b}")
                elif op == '/': print(f"Wynik: {a/b}" if b!=0 else "Nie dziel przez 0")
            except: print("Blad danych")
        elif choice == '2':
            k = input("Kierunek (C/F): ").upper()
            try:
                t = float(input("Temp: "))
                if k == 'C': print(f"{t}C = {t*1.8+32:.2f}F")
                elif k == 'F': print(f"{t}F = {(t-32)/1.8:.2f}C")
            except: print("Blad danych")
        elif choice == '3':
            try:
                n = int(input("Ile ocen? "))
                s = 0
                for _ in range(n): s += float(input("Ocena: "))
                sr = s/n
                print(f"Srednia: {sr:.2f} -> {'Zdal' if sr>=3 else 'Nie zdal'}")
            except: print("Blad danych")
        elif choice == '0': break

if __name__ == "__main__":
    zadania()
