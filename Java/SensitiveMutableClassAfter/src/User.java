import java.util.Arrays;

public class User {
    private int userId = 1;
    // other important fields like firstname = Maricica, lastname = Morrone
    // data set a long time ago when the user registered
    private char[] bankAccountNumber = "ING1234567812345678".toCharArray();

    User() {

    }

    User(User user) {
        userId = user.userId;
        bankAccountNumber = Arrays.copyOf(user.bankAccountNumber, bankAccountNumber.length);
    }

    // getters and setters for the other fields

    public char[] getBankAccountNumber() {
        return bankAccountNumber;
    }

    public void setBankAccountNumber(char[] bankAccountNumber) {
        this.bankAccountNumber = bankAccountNumber;
    }

    public int getUserId() {
        return userId;
    }
}