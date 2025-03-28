import math

class BankAccount:
    def __init__(self, name, pin, balance=0, loan_balance=0):
        self.name = name
        self.pin = pin
        self.balance = balance
        self.loan_balance = loan_balance  # Tracks outstanding loan amount
        self.loan_type = None  # Will store 'personal' or 'business'
        self.loan_rate = 0  # Interest rate for the loan
        self.loan_term = 0  # Term of the loan (in years)

    def apply_loan(self, amount, loan_type, loan_term):
        """Apply for a loan: 'personal' or 'business', with specific term and rate."""
        self.loan_balance += amount
        self.loan_type = loan_type
        self.loan_term = loan_term
        
        if loan_type == "personal":
            self.loan_rate = 5 / 100  # 5% interest for personal loans
        elif loan_type == "business":
            self.loan_rate = 8 / 100  # 8% interest for business loans

        print(f"Loan approved! You have borrowed {amount}. Loan Type: {loan_type.capitalize()}.\nInterest Rate: {self.loan_rate * 100}% over {loan_term} years.")

    def calculate_loan_repayment(self):
        """Calculate total repayment including interest for the outstanding loan."""
        total_repayment = self.loan_balance * math.exp(self.loan_rate * self.loan_term)
        return total_repayment

    def repay_loan(self, payment):
        """Repay part of the outstanding loan."""
        if payment > self.loan_balance:
            self.loan_balance = 0
            print(f"Loan fully repaid. Overpayment of {payment - self.loan_balance} credited to your account.")
        else:
            self.loan_balance -= payment
            print(f"Payment of {payment} accepted. Outstanding loan balance: {self.loan_balance}")

    def check_loan_status(self):
        """Check the current loan balance and interest."""
        if self.loan_balance > 0:
            total_repayment = self.calculate_loan_repayment()
            print(f"Outstanding Loan: {self.loan_balance}. Total repayment after interest: {total_repayment:.2f}")
        else:
            print("You have no outstanding loans.")

    def deposit(self, amount):
        self.balance += amount
        print(f"Deposited {amount}. Current balance: {self.balance}")

    def withdraw(self, amount):
        if amount > self.balance:
            print("Insufficient balance.")
        else:
            self.balance -= amount
            print(f"Withdrew {amount}. Current balance: {self.balance}")

    def check_balance(self):
        print(f"Current balance: {self.balance}")

def signin():
    name = input("Please create your username: ")
    while True:
        pin = input("Please create your 6 digits pin: ")
        if len(pin) == 6:
            break
        else:
            print("The pin has to be 6 digits. Try again.")
    
    return BankAccount(name, pin)

def login(accounts):
    """Login and return the user's account object."""
    while True:
        name = input("Please enter your username: ")
        pin = input("Please enter your pin: ")

        for account in accounts:
            if account.name == name and account.pin == pin:
                print(f"Welcome, {name}!")
                return account
        print("Incorrect username or pin. Try again or create a new account.")

def mainmenu():
    accounts = []  # List of bank accounts
    
    while True:
        option = input("Choose 1 to sign up, 2 to log in: ")
        if option == "1":
            account = signin()
            accounts.append(account)
        elif option == "2":
            account = login(accounts)
            account_menu(account)
        else:
            print("Invalid option. Please try again.")

def account_menu(account):
    """User-specific menu for banking and loan options."""
    while True:
        print("\nPlease choose an option:")
        menu = [
            "1-Deposit", "2-Withdraw", "3-Check Balance", 
            "4-Apply for Loan", "5-Check Loan Status", 
            "6-Repay Loan", "7-Exit"
        ]
        for item in menu:
            print(item)
        choice = input("Enter your choice: ")

        if choice == "1":
            amount = float(input("Enter the amount to deposit: "))
            account.deposit(amount)

        elif choice == "2":
            amount = float(input("Enter the amount to withdraw: "))
            account.withdraw(amount)

        elif choice == "3":
            account.check_balance()

        elif choice == "4":
            loan_type = input("Enter loan type ('personal' or 'business'): ").lower()
            amount = float(input(f"Enter the amount to borrow for {loan_type} loan: "))
            term = int(input(f"Enter the loan term (in years): "))
            account.apply_loan(amount, loan_type, term)

        elif choice == "5":
            account.check_loan_status()

        elif choice == "6":
            repayment = float(input("Enter the amount to repay: "))
            account.repay_loan(repayment)

        elif choice == "7":
            print("Thank you for using the app!")
            break

        else:
            print("Invalid option. Please try again.")

mainmenu()
